using Blazored.LocalStorage;
using Frontend;
using Frontend.Clients;
using Frontend.Handlers;
using Frontend.Providers;
using Frontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<TokenService>();

builder.Services.AddScoped<AuthClientHandler>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddCascadingAuthenticationState();
var apiBaseAddress = "http://localhost:5026";

builder
    .Services.AddHttpClient(
        "AuthClient",
        client =>
        {
            client.BaseAddress = new Uri(apiBaseAddress);
        }
    )
    .AddHttpMessageHandler<AuthClientHandler>();

builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("AuthClient");
});

builder.Services.AddScoped<AuthClient>();
builder.Services.AddScoped<CenterAdminClient>();
builder.Services.AddScoped<CenterClient>();

builder.Services.AddScoped<ScienceClient>();

builder.Services.AddScoped<GroupClient>();

builder.Services.AddScoped<TeacherClient>();

builder.Services.AddScoped<StudentClient>();

await builder.Build().RunAsync();
