using AppBlazor.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


namespace AppBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7110/") });

            builder.Services.AddScoped<LibroService>();

            builder.Services.AddScoped<TipoLibroServices>();

            builder.Services.AddScoped<AutorService>();

            await builder.Build().RunAsync();
        }
    }
}
