using Contracts;
using Entities;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using NLog;
using Repository;
using System.Linq;

namespace CompanyEmployees
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                                                builder.AllowAnyOrigin()    //  This is ok for dev but      - WithOrigins("https://www.example.com")
                                                        .AllowAnyMethod()   //  will need to be changed     - WithMethods("Get","Post")
                                                        .AllowAnyHeader()   //  for production environment. - WithHeaders("accept", "content-type")
                            );
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
                {
                    //No need to configure, default options are good for now, some config avail as per below with their defaults:
                    //options.AutomaticAuthentication = true;
                    //options.AuthenticationDisplayName = null;
                    //options.ForwardClientCertificate = true;
                });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(options => 
                    options.UseSqlServer(configuration.GetConnectionString("sqlConnection"), 
                                            b => b.MigrationsAssembly("CompanyEmployees"))
            );

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
            builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));


        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var newtonsoftJsonOutputFormatter = config.OutputFormatters
                      .OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

                if (newtonsoftJsonOutputFormatter != null)
                {
                    newtonsoftJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.byron.hateoas+json");
                }

                var xmlOutputFormatter = config.OutputFormatters
                      .OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

                if (xmlOutputFormatter != null)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.byron.hateoas+xml");
                }
            });
        }
    }
}
