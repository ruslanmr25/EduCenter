using Blazored.LocalStorage;
using Frontend;
using Frontend.Clients;
using Frontend.Providers;
using Frontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<TokenService>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddCascadingAuthenticationState();
var apiBaseAddress = "http://localhost:5026";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseAddress) });

builder.Services.AddScoped<AuthClient>();

await builder.Build().RunAsync();
