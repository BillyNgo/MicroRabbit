using System;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MicroRabbit.Banking.Api.Infrastructure.Mapper;
using MicroRabbit.Banking.Application.Behaviors;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Infra.IoC;
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

namespace MicroRabbit.Banking.Api
{
    public static class StartupExtension
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BankingDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BankingDbConnection"));
            });
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Banking MicroService ",
                    Description = "A simple banking API",
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
                s.RegisterValidatorsFromAssemblyContaining<TransferViewModel>();
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
            services.AddMediatR(typeof(Startup).Assembly, typeof(Domain.Commands.CreateTransferCommandHandler).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            AssemblyScanner
                .FindValidatorsInAssembly(typeof(Domain.Commands.CreateTransferCommandHandler).Assembly)
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BankingDbContext bankingDbContext)
        {
            bankingDbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Banking MicroService V1");
            });

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
