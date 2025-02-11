using System.ComponentModel.DataAnnotations;

namespace GestionnaireRecettes.Models
{
    public class Avis
    {
        [Key]
        public int Id { get; set; }

        // Clé étrangère vers l'utilisateur qui donne l'avis
        public string UserID { get; set; }
        public virtual User User { get; set; }

        // Clé étrangère vers la recette concernée par l'avis
        public int RecetteID { get; set; }
        public virtual Recette Recette { get; set; }

        // Note de l'avis
        [Range(1, 5)]
        public int Note { get; set; }

        // Commentaire associé à l'avis
        [StringLength(500)]
        public string? Commentaire { get; set; }
    }
}
