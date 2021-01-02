using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ikl.web.Client.Shared;

namespace ikl.web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton(sp => new HttpClient
                {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            builder.Services.AddSingleton<DataService>();
            var host = builder.Build();

            var dataService = host.Services.GetService<DataService>();
            await dataService.Initialize();
            await host.RunAsync();
        }
    }
}
