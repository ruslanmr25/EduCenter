using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using UI;
using UI.Clients;
using UI.Handlers;
using UI.Providers;

var apiBaseAddress = "http://localhost:5026";
var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<MessageHandler>();

builder.Services.AddAuthorizationCore();

builder
    .Services.AddHttpClient(
        "AuthClient",
        client =>
        {
            client.BaseAddress = new Uri(apiBaseAddress);
        }
    )
    .AddHttpMessageHandler<MessageHandler>();

builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("AuthClient");
});
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddScoped<TokenProvider>();

builder.Services.AddMudServices();

builder.Services.AddScoped<TokenProvider>();

builder.Services.AddScoped<AuthClient>();

builder.Services.AddScoped<TeacherClient>();

builder.Services.AddScoped<ScienceClient>();

builder.Services.AddScoped<GroupClient>();

builder.Services.AddScoped<StudentClient>();

builder.Services.AddScoped<StudentPaymentSystemClient>();

await builder.Build().RunAsync();
