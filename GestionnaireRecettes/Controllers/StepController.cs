using GestionnaireRecettes.data;
using GestionnaireRecettes.Models.dto;
using GestionnaireRecettes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GestionnaireRecettes.Controllers
{
    [Authorize]
    public class StepController : Controller
    {
        private readonly RecettesAppContext _context;

        public StepController(RecettesAppContext context)
        {
            _context = context;
        }

        // List all ingredients
        [HttpGet]

        public IActionResult Index(int recetteId)
        {
            var steps = _context.Steps.Where(i => i.RecetteId == recetteId)
                                      .OrderBy(s => s.StepNumber) // Order by stepNumber
                                      .ToList();
            ViewBag.RecetteId = recetteId;
            ViewBag.steps = steps;


            return View();
        }



        // Handle form submission to create a new ingredient
        [HttpPost]
        public IActionResult Create(StepDto stepDto)
        {
            if (!ModelState.IsValid)
            {
                return View(stepDto);
            }

            Step step = new Step()
            {
                StepNumber = stepDto.StepNumber,
                Description = stepDto.Description,
                RecetteId = stepDto.RecetteId,
            };

            _context.Steps.Add(step);
            _context.SaveChanges();

            return RedirectToAction("Index", new { recetteId = stepDto.RecetteId });


            //return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var step = _context.Steps.Find(id);

            if (step == null)
            {
                return NotFound();
            }

            _context.Steps.Remove(step);
            _context.SaveChanges();

            // Redirect to the list of ingredients or a relevant page after deletion
            return RedirectToAction("Index", new { recetteId = step.RecetteId });
        }
    }
}
