using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using EuroTrim.api.Services;
using Microsoft.Extensions.Configuration;
using EuroTrim.api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
 

namespace EuroTrim.api
{
    public class Startup
    {
        
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                setupAction.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());


            });

            //.AddJsonOptions(o =>
            //{
            //    if (o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver
            //            as DefaultContractResolver;
            //            castedResolver.NamingStrategy = null;


            //    }
            //});
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
       
            var connectionString = Startup.Configuration["connectionStrings:euroTrimDBConnectionString"];
            services.AddDbContext<EuroTrimContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IEuroTrimRepository, EuroTrimRepository>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>()
                .ActionContext;
                return new UrlHelper(actionContext);
            });


            services.AddTransient<IPropertyMappingService, IPropertyMappingService>();

            // services.AddTransient<ITypeHelperService, TypeHelperService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            EuroTrimContext euroTrimContext)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug(LogLevel.Information);

            // loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider())
            //loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
                        if(exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");

                            logger.LogError(500,
                                exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message);
                        }
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");

                    });
                }
                );
            }

            euroTrimContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Customer, Models.CustomerDto>();
                cfg.CreateMap<Entities.Order, Models.OrderDto>();
                cfg.CreateMap<Models.CustomerForCreationDto, Entities.Customer>();
                cfg.CreateMap<Models.OrderForCreationDto, Entities.Order>();
                cfg.CreateMap<Entities.Product, Models.ProductDto>();
                cfg.CreateMap<Models.ProductForUpdateDto, Entities.Product>();
                cfg.CreateMap<Entities.Product, Models.ProductForUpdateDto>();

                cfg.CreateMap<Entities.Customer, Models.CustomersWithoutProductsDto>();
 
                cfg.CreateMap<Models.ProductForCreationDto, Entities.Product>();
                

            });

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
