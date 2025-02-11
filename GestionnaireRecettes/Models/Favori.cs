using System.ComponentModel.DataAnnotations;

namespace GestionnaireRecettes.Models
{
    public class Favori
    {
        [Key]
        public int Id { get; set; }

        // Foreign Key to User
        public string UserID { get; set; }
        public virtual User User { get; set; }

        // Foreign Key to Recipe
        public int RecetteID { get; set; }
        public virtual Recette Recette { get; set; }
    }
}
