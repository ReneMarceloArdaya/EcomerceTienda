using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using TiendaVirtual.Client;
using TiendaVirtual.Client.Services;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



// Configurar HttpClient con handler personalizado para manejar 401
builder.Services.AddScoped<AuthMessageHandler>();
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("http://localhost:32768/"); // Cambia según tu contenedor o puerto real
})
.AddHttpMessageHandler<AuthMessageHandler>()

;

// docker : http://localhost:5300/
// Local:  http://localhost:32777/
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Registrar HttpClient para inyección directa
builder.Services.AddScoped(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return httpClientFactory.CreateClient("API");
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<DialogService>();


var app = builder.Build();

var authService = app.Services.GetRequiredService<AuthService>();
await authService.InitializeAsync();

await app.RunAsync();