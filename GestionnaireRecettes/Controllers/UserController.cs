using GestionnaireRecettes.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using GestionnaireRecettes.Models;

namespace GestionnaireRecettes.Controllers
{
    public class UserController : Controller
    {

        private readonly RecettesAppContext _context;

        public UserController(RecettesAppContext context)
        {
            this._context = context;
        }
        public IActionResult Index(string id)
        {
            //var username = User.Identity.Name;  // Retrieves the name claim
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var user = _context.Users
                                 .Where(r => r.Id == id)
                                 .FirstOrDefault();

            var recettes = _context.Recettes
             .Where(r => r.UserID == id)
             .OrderByDescending(r => r.DatePublication)
            .ToList();

            ViewBag.user = user;
            return View(recettes);
        }



        [HttpGet]
        public IActionResult Profile()
        {

           
            return View();
        }
    }
}
