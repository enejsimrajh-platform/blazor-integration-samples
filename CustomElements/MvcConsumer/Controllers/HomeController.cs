using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcConsumer.Models;

namespace MvcConsumer.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _http;

    public HomeController(ILogger<HomeController> logger, HttpClient http)
    {
        _logger = logger;
        _http = http;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Counter()
    {
        return View();
    }

    public IActionResult WeatherEncapsulated()
    {
        return View();
    }

    public async Task<IActionResult> WeatherExternal()
    {
        var weatherForecasts = await _http.GetFromJsonAsync<WeatherViewModel.WeatherForecast[]>("WeatherForecast");
        return View(new WeatherViewModel { Data = weatherForecasts });
    }

    public IActionResult Auth()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
