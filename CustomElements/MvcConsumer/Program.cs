var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var blazorUrl = builder.Configuration["BlazorUrl"]
    ?? throw new InvalidOperationException("Missing 'BlazorUrl' configuration value.");
builder.Services.AddHttpClient("Blazor", client => client.BaseAddress = new Uri(blazorUrl));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapAreaControllerRoute(
    name: "blazorwasm",
    areaName: "BlazorWasm",
    pattern: "BlazorWasm/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
