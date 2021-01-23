using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ikl.web.Client.Shared;
using ikl.web.Shared;
using MatBlazor;

namespace ikl.web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddMatBlazor();
            
            var httpClient = new HttpClient
                {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)};
            
            var data = await httpClient.GetFromJsonAsync<Data>("data");
            builder.Services.AddSingleton(httpClient);
            if (data != null)
            {
                builder.Services.AddSingleton(data);
            }
            builder.Services.AddSingleton<DataService>();
            var host = builder.Build();
            await host.RunAsync();
        }
    }
}
