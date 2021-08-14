using System;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.IoC;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.EventHandlers;
using MicroRabbit.Transfer.Domain.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
                .AddSwagger()
                .AddMediatR(typeof(Startup))
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