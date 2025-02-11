using System.ComponentModel.DataAnnotations;

namespace GestionnaireRecettes.Models.dto
{
    public class RecetteDto
    {

        public int? Id { get; set; }
        [Required, StringLength(100)]
        public string Titre { get; set; }

        public string? Description { get; set; }

        public string? InstructionsCuisson { get; set; }

        public int TempsPreparation { get; set; }

        public int TempsCuisson { get; set; }
        public IFormFile? ImageFile { get; set; }

        public string? Image { get; set; }
        public string UserId { get; set; }
        
    }
}
