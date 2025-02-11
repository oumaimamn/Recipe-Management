using System.ComponentModel.DataAnnotations;

namespace GestionnaireRecettes.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; }

        [Required]
        public double Quantite { get; set; }

        [Required]
        [StringLength(20)]
        public string Unité { get; set; }

        // Foreign Key to Recipe
        public int RecetteID { get; set; }
        public virtual Recette Recette { get; set; }
    }
}
