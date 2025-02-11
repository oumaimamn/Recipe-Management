using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GestionnaireRecettes.Models
{
    public class User : IdentityUser
    {
 



        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        public string? Bio { get; set; }

        // Relation with Recipes
        public virtual ICollection<Recette> Recettes { get; set; }

        // Relation with Les Avis
        public virtual ICollection<Avis> Avis { get; set; }

        // Relation with Favorites
        public virtual ICollection<Favori> Favoris { get; set; }
    }
}
