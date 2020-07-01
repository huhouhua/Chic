using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ChicAspNetCoreDemo.Extensions;
using System.IO;
using Microsoft.AspNetCore.HttpOverrides;
using ChicAspNetCoreDemo.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;
using ChicAspNetCoreDemo.Application;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Chic.Core.Modules.Extensions;
using Chic.Core.PlugIns;

namespace ChicAspNetCoreDemo
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
            services.AddControllers().AddControllersAsServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);

            });

            services.AddMapper();

            services.AddSqlServerDomainContext(Configuration.GetSection("ConnectionStrings").GetValue<string>("SqlServerConnection"));

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownNetworks.Clear();

                options.KnownProxies.Clear();

                options.ForwardedHeaders = ForwardedHeaders.All;
            });

            services.AddChic(options =>
            {
                options.PlugInSources.AddFolder(AppContext.BaseDirectory);

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseChic();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dc = scope.ServiceProvider.GetService<OrderingContext>();

                //dc.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
