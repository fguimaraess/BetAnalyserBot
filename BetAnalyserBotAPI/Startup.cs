using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace AnalyserBetBotAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();
            services.AddFluentEmail("bet-analyser-bot@gmail.com")
                .AddSendGridSender(_configuration["Services:SendGrid"]);
            //Logic
            RegisterLogic(services);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bet Analyser Bot API", Version = "v1" });
            });
        }

        private void RegisterLogic(IServiceCollection services)
            => RegisterGeneric(services, typeof(BetLogic));

        private void RegisterGeneric(IServiceCollection services, Type baseType,
            InjectionType injectionType = InjectionType.Transient)
        {
            var assembly = baseType.Assembly;
            var registrations = from type in assembly.GetExportedTypes()
                                where type.Namespace != null
                                && !type.Namespace.StartsWith($"{baseType.Namespace}.Base")
                                from service in type.GetInterfaces()
                                select new { service, implementation = type };

            foreach (var reg in registrations)
            {
                switch (injectionType)
                {
                    case InjectionType.Scoped:
                        services.AddScoped(reg.service, reg.implementation);
                        break;
                    case InjectionType.Transient:
                        services.AddTransient(reg.service, reg.implementation);
                        break;
                    case InjectionType.Singleton:
                        services.AddSingleton(reg.service, reg.implementation);
                        break;
                    default:
                        break;
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bet Analyser Bot"));
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
        enum InjectionType
        {
            Scoped,
            Transient,
            Singleton
        }
    }
}
