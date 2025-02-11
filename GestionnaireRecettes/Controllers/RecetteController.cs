using GestionnaireRecettes.data;
using GestionnaireRecettes.Models;
using GestionnaireRecettes.Models.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionnaireRecettes.Controllers
{
    [Authorize]
    public class RecetteController : Controller
    {
        private readonly RecettesAppContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<User> _userManager;


        public RecetteController(RecettesAppContext context, IWebHostEnvironment environment, UserManager<User> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;

        }

        [Authorize]
        public IActionResult Index()
        {
            var recettes = _context.Recettes.ToList();
            return View(recettes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(RecetteDto recetteDto)
        {

            if(!ModelState.IsValid) {
                return View(recetteDto);
            }

            string newFileName = "";

            if (recetteDto.ImageFile != null)
            {

                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(recetteDto.ImageFile!.FileName);


                string imageFullPath = _environment.WebRootPath + "/recettes/" + newFileName;

                using(var stream = System.IO.File.Create(imageFullPath)) {
                    recetteDto.ImageFile.CopyTo(stream);
                } 

            }


            Recette recette = new Recette()
            {
                Titre = recetteDto.Titre,
                Description = recetteDto.Description,
                InstructionsCuisson = null,
                TempsPreparation = recetteDto.TempsPreparation,
                TempsCuisson = recetteDto.TempsCuisson,
                Image = newFileName,
                UserID = recetteDto.UserId,
             };



            _context.Recettes.Add(recette);
            _context.SaveChanges();

            return RedirectToAction("Index","ingredient", new { recetteId = recette.Id });
        }

        [AllowAnonymous]
        public IActionResult Show(int id)
        {
            // Retrieve the recipe with its associated ingredients and steps
            var recette = _context.Recettes
                                  .Include(r => r.Auteur)
                                  .Where(r => r.Id == id)
                                  .FirstOrDefault();

            if (recette == null)
            {
                return NotFound(); // Return 404 if the recipe doesn't exist
            }

            // Load the related ingredients and steps, and ensure steps are ordered
            _context.Entry(recette).Collection(r => r.Ingredients).Load(); // Load ingredients
            recette.Steps = _context.Steps
                                     .Where(s => s.RecetteId == id)
                                     .OrderBy(s => s.StepNumber) // Sort by StepNumber
                                     .ToList(); // Convert to a list

            return View(recette);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var recette = _context.Recettes.Find(id);  

            if (recette == null)
            {
                return NotFound();  
            }

            var recetteDto = new RecetteDto
            {
                Id = recette.Id,
                Titre = recette.Titre,
                Description = recette.Description,
                TempsPreparation = recette.TempsPreparation,
                TempsCuisson = recette.TempsCuisson,
                UserId = recette.UserID,
                Image = recette.Image  
            };

            return View(recetteDto);  
        }


        [Authorize]
        [HttpPost]
        public IActionResult Edit(RecetteDto recetteDto)
        {
            if (!ModelState.IsValid)
            {
                return View(recetteDto);
            }

            var recette = _context.Recettes.Find(recetteDto.Id);

            if (recette == null)
            {
                return NotFound();
            }

            // supprimer l'ancienne image
            if (recetteDto.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(recette.Image))
                {
                    var oldImagePath = Path.Combine(_environment.WebRootPath, "recettes", recette.Image);

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(recetteDto.ImageFile.FileName);

                var imageFullPath = Path.Combine(_environment.WebRootPath, "recettes", newFileName);

                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    recetteDto.ImageFile.CopyTo(stream);
                }

                recette.Image = newFileName;
            }

            recette.Titre = recetteDto.Titre;
            recette.Description = recetteDto.Description;
            recette.TempsPreparation = recetteDto.TempsPreparation;
            recette.TempsCuisson = recetteDto.TempsCuisson;

            _context.SaveChanges();

            return RedirectToAction("Edit", new {id = recette.Id}); 
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            if (user == null)
            {
                return Unauthorized();
            }

            var recette = _context.Recettes.FirstOrDefault(r => r.Id == id);

            if (recette == null)
            {
                return NotFound();  // S 404
            }

            if (recette.UserID != user.Id)
            {
                return Forbid();  //   403
            }

            // Supprimer la recette
            _context.Recettes.Remove(recette);
            _context.SaveChanges(); 

            return RedirectToAction("Index", "User", new { id = user.Id });
        }
    }
}
