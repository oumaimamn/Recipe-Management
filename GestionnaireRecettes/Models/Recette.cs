using System.ComponentModel.DataAnnotations;

namespace GestionnaireRecettes.Models
{
    public class Recette
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titre { get; set; }

        public string? Description { get; set; }

        public string? InstructionsCuisson { get; set; }

        public int TempsPreparation { get; set; }

        public int TempsCuisson { get; set; }

        // Date de publication avec valeur par défaut
        [DataType(DataType.Date)]
        public DateTime DatePublication { get; set; } = DateTime.Now;

        public string? Image { get; set; }

        // Foreign Key to User
        public string UserID { get; set; }
        public virtual User Auteur { get; set; }

        // Relation with RecipeIngredients
        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public virtual ICollection<Step> Steps { get; set; }

        public virtual ICollection<Favori> Favoris { get; set; }

        // les Avis
        public virtual ICollection<Avis> Avis { get; set; }
    }
}
