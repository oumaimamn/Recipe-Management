using GestionnaireRecettes.data;
using GestionnaireRecettes.Models;
using GestionnaireRecettes.Models.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionnaireRecettes.Controllers
{
    [Authorize]
    public class IngredientController : Controller
    {
        private readonly RecettesAppContext _context;

        public IngredientController(RecettesAppContext context)
        {
            _context = context;
        }

        // List all ingredients
        [HttpGet]
        public IActionResult Index(int recetteId)
        {
            var ingredients = _context.Ingredients.Where(i => i.RecetteID == recetteId).ToList();
            ViewBag.RecetteId = recetteId;
            ViewBag.ingredients = ingredients;


            return View();
        }


        // Display form to create a new ingredient
        // Handle form submission to create a new ingredient
        
        [HttpPost]
        public IActionResult Create(IngredientDto ingredientDto)
        {
            if (!ModelState.IsValid)
            {
                return View(ingredientDto);
            }

            Ingredient ingredient = new Ingredient()
            {
                Nom = ingredientDto.Nom,
                Quantite = ingredientDto.Quantite,
                Unité = ingredientDto.Unité,    
                RecetteID = ingredientDto.RecetteId,
            };

            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();

            return RedirectToAction("Index", new { recetteId = ingredientDto.RecetteId });


            //return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var ingredient = _context.Ingredients.Find(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            _context.SaveChanges();

            // Redirect to the list of ingredients or a relevant page after deletion
            return RedirectToAction("Index", new { recetteId = ingredient.RecetteID });
        }

    }
}
