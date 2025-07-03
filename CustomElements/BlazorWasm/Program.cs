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
builder.Services.AddAuthenticationStateDeserialization();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();

builder.Services.AddTransient<CookieHandler>();
builder.Services.AddHttpClient("Server", client =>
    {
        client.BaseAddress = new Uri("https://localhost:44345/");
    })
    .AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();
