﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spark.Engine;
using Spark.Engine.Extensions;
using Spark.Facade.Services;
using Spark.Facade.Store;

namespace Spark.Facade
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            SparkSettings settings = new SparkSettings();
            Configuration.Bind("SparkSettings", settings);

            FileStoreSettings storeSettings = new FileStoreSettings();
            Configuration.Bind("FileStoreSettings", storeSettings);

            services.AddSingleton(storeSettings);

            services.AddFhirFacade(settings);
            services.AddCustomStore<FileFhirStore>();
            
            services.AddTransient<PatientService>();
            services.AddTransient<SystemService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMiddleware<ResponseBufferingMiddleware>();

            app.UseFhir(r => r.MapRoute(name: "default", template: "{controller}/{action}/{id?}", defaults: new { controller = "Home", action = "Index" }));
        }
    }
}
