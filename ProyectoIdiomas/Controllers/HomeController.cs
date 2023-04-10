using Microsoft.AspNetCore.Mvc;
using ProyectoIdiomas.Models;
using System.Diagnostics;

using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

namespace ProyectoIdiomas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private readonly IStringLocalizer<HomeController> _obtenerRecurso;

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> obtenerRecurso)
        {
            _logger = logger;
            _obtenerRecurso = obtenerRecurso;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["textoPrueba"] = _obtenerRecurso["textoPrueba"];
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult EstablecerCultura(string nuevaCultura, string retornarUrl) {
            Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(nuevaCultura)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(5) }
                );

            return LocalRedirect(retornarUrl);
        
        }
    }
}