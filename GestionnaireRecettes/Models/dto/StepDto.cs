using System.ComponentModel.DataAnnotations;

namespace GestionnaireRecettes.Models.dto
{
    public class StepDto
    {
        [Required]
        public int StepNumber { get; set; }

        [Required,StringLength(500)]
        public string Description { get; set; }
        public int RecetteId { get; set; }
    }
}
