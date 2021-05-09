using GroceryStoreBL.Infrastructure.Interfaces;
using GroceryStoreBL.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

namespace GroceryStoreAPI
{
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
            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Grocery Store API", Description = "Simple API with CRUD operations against *.json file.", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                var DataXmlPath = Path.Combine(AppContext.BaseDirectory, "GroceryStoreBL.xml");
                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments(DataXmlPath);
            });

            services.AddScoped<ICustomerService>(sp =>
            {
                var fileLocation = $@"{Directory.GetCurrentDirectory()}\{Configuration.GetSection("CustomerFile:Name").Value}";
                return new JsonCustomerService(fileLocation);
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Grocery Store API");
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
