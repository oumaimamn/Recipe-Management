using GestionnaireRecettes.data;
using GestionnaireRecettes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GestionnaireRecettes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RecettesAppContext _context;

        public HomeController(RecettesAppContext context, ILogger<HomeController> logger)
        {
            this._context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var recettes = _context.Recettes.
             Include(r => r.Auteur)
            .OrderByDescending(r => r.DatePublication)  // Trier par date
            .ToList();  // Exécuter la requête

            ViewBag.recettes = recettes;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
