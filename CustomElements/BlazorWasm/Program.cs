using BlazorWasm.Components;
using BlazorWasm.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.RegisterCustomElement<Counter>("wasm-counter");
builder.RootComponents.RegisterCustomElement<Weather>("wasm-weather");
builder.RootComponents.RegisterCustomElement<Auth>("wasm-auth");

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();

var baseAddress = new Uri(builder.HostEnvironment.BaseAddress);
builder.Services.AddScoped<HttpClient>(sp => new() { BaseAddress = baseAddress });
builder.Services.AddScoped<CookieHandler>();
builder.Services.AddHttpClient("Auth", client => client.BaseAddress = baseAddress)
    .AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();
