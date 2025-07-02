using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWasm.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.RegisterCustomElement<Counter>("wasm-counter");
builder.RootComponents.RegisterCustomElement<Weather>("wasm-weather");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44345/") });

await builder.Build().RunAsync();
