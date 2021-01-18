using System;
using ikl.web.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ikl.web.Server
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

            services.AddControllersWithViews();
            services.AddRazorPages();
            var drawingsJson = File.ReadAllText("drawings.json");
            var drawings = JsonSerializer.Deserialize<Drawing[]>(drawingsJson);

            var customersJson = File.ReadAllText("customers.json");
            var customers = JsonSerializer.Deserialize<Customer[]>(customersJson);
            foreach (var item in customers)
            {
                item.Year += 1900;
            }

            foreach (var drawing in drawings)
            {
                foreach (var ratio in drawing.Ratios)
                {
                    drawing.Tags.Add(ratio);
                }
                if (drawing.Title.Contains("chair", StringComparison.InvariantCultureIgnoreCase) ||
                    drawing.Title.Contains("stol", StringComparison.InvariantCultureIgnoreCase) || 
                    drawing.Title.Contains("Hocker", StringComparison.InvariantCultureIgnoreCase) ||
                    drawing.Title.Contains("Sessel", StringComparison.InvariantCultureIgnoreCase) ||
                    drawing.Title.Contains("stuhl", StringComparison.InvariantCultureIgnoreCase))
                {
                    drawing.Tags.Add("stol");
                    drawing.Tags.Add("chair");
                }

                if (drawing.Title.Contains("tisch", StringComparison.InvariantCultureIgnoreCase) ||
                    drawing.Title.Contains("bord", StringComparison.InvariantCultureIgnoreCase) ||
                    drawing.Title.Contains("table", StringComparison.InvariantCultureIgnoreCase)
                    )
                {
                    drawing.Tags.Add("bord");
                    drawing.Tags.Add("table");
                }
                
                if (drawing.Title.Contains("gestell", StringComparison.InvariantCultureIgnoreCase))
                {
                    drawing.Tags.Add("ramme");
                }
            }
            services.AddSingleton(sp => new Data(drawings, customers));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
