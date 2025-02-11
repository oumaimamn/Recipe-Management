using System.ComponentModel.DataAnnotations;

namespace GestionnaireRecettes.Models
{
    public class Step
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StepNumber { get; set; } // Indicates the order of the step

        [Required]
        [StringLength(500)]
        public string Description { get; set; } // Step description

        // Foreign key to Recette
        public int RecetteId { get; set; }

        // Navigation property to Recette
        public virtual Recette Recette { get; set; }
    }
}
