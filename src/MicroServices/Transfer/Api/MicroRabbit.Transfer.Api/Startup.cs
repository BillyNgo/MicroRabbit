using System;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.CrossCutting.Ioc;
using MicroRabbit.Transfer.Api.Infrastructure.Mapper;
using MicroRabbit.Transfer.Application.Behaviors;
using MicroRabbit.Transfer.Application.Models;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.Commands;
using MicroRabbit.Transfer.Domain.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace MicroRabbit.Transfer.Api
{
    public static class StartupExtension
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TransferDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TransferDbConnection"));
            });
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Transfer MicroService",
                    Description = "A simple transfer API ",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Billy Ngo",
                        Email = "datngo@nvg.vn",
                        Url = new Uri("https://nvg.com")
                    }

                });
            });
            return services;
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<TransferLogViewModel>();
                s.DisableDataAnnotationsValidation = true;
            });
            return services;
        }

        public static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApiAutoMappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddMediatRService(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly, typeof(Domain.Commands.TransferCreatedEventHandler).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            AssemblyScanner
                .FindValidatorsInAssembly(typeof(Domain.Commands.TransferCreatedEventHandler).Assembly)
                .ForEach(pair =>
                {
                    // RegisterValidatorsFromAssemblyContaining does this:
                    services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
                    // Also register it as its concrete type as well as the interface type
                    services.Add(ServiceDescriptor.Transient(pair.ValidatorType, pair.ValidatorType));
                });

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOptions();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomDbContext(Configuration)
                .AddCustomIntegrations(Configuration)
                .AddCustomConfiguration(Configuration)
                .AddFluentValidation()
                .AddSwagger()
                .AddAutoMapperService()
                .AddMediatRService()
                .AddControllers();

            RegisterServices(services, Configuration);
        }
        private void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            DependencyContainer.RegisterServices(services, configuration);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TransferDbContext transferDbContext)
        {
            transferDbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transfer Microservice V1");
            });

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<TransferCreatedEvent, TransferCreatedEventHandler>();
        }
    }
}