using System.ComponentModel.DataAnnotations;

namespace GestionnaireRecettes.Models.dto
{
    public class IngredientDto
    {
        [Required, StringLength(100)]
        public string Nom { get; set; }

        public double Quantite { get; set; }

        public string Unité { get; set; }

        public int RecetteId { get; set; }
    }
}
