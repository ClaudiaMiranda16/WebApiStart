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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebAPIBase
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
            services.AddMemoryCache();
            services.AddSession();
            services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();;
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
       /*    app.Map("/level1", level1App => {
                level1App.Map("/level2a", level2App => {
                    level2App.Run(context => context.Response.WriteAsync("Level1/Level2A"));

                });

                level1App.Map("/level2b", level2BApp => {
                    level2BApp.Run(context => context.Response.WriteAsync("Level1/Level2A"));

                });
                }
            );
            */
          /*   app.Use(async (context, next) => {
                var method = context.Request.Method;
                await context.Response.WriteAsync(method);
                await next.in
            });*/

          app.Map("/hello", appbuilder => {
                appbuilder.MapWhen(
                    context => context.Request.Query.ContainsKey("name"),
                    (appB) => {
                        appB.Run(async newContext => {
                            await newContext.Response.WriteAsync($"Hello { newContext.Request.Query["name"] }!!");
                        });
                    }
                );
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();
            app.UseMvc();
           /*  app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });*/
        }
    }
}