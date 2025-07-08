using Microsoft.AspNetCore.Mvc;
using MvcConsumer.Areas.BlazorWasm.Models;

namespace MvcConsumer.Areas.BlazorWasm.Controllers;

public class HomeController : Controller
{
    public IActionResult Counter()
    {
        return View();
    }

    public IActionResult WeatherEncapsulated()
    {
        return View();
    }

    public async Task<IActionResult> WeatherExternal([FromServices] IHttpClientFactory httpFactory)
    {
        // TODO: Server-side HTTP client authentication
        var http = httpFactory.CreateClient("Blazor");
        var weatherForecasts = await http.GetFromJsonAsync<WeatherViewModel.WeatherForecast[]>("api/WeatherForecast");
        return View(new WeatherViewModel { Data = weatherForecasts });
    }

    public IActionResult Auth()
    {
        return View();
    }
}
