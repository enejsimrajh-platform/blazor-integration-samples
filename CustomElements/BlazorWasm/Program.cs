using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWasm.Components;
using BlazorWasm.Identity;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.RegisterCustomElement<Counter>("wasm-counter");
builder.RootComponents.RegisterCustomElement<Weather>("wasm-weather");
builder.RootComponents.RegisterCustomElement<Auth>("wasm-auth");

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();

builder.Services.AddTransient<CookieHandler>();
var host = builder.Configuration["Host"]
    ?? throw new InvalidOperationException("Missing 'Host' configuration value.");
builder.Services.AddHttpClient("Host", client =>
    {
        client.BaseAddress = new(host);
    })
    .AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();
