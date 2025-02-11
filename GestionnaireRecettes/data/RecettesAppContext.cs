using GestionnaireRecettes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace GestionnaireRecettes.data
{

 
    public class RecettesAppContext : IdentityDbContext<User>
    {


        public DbSet<User> Users { get; set; }
        public DbSet<Recette> Recettes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Step> Steps { get; set; }


        public DbSet<Favori> Favoris { get; set; }

        public DbSet<Avis> Avis { get; set; }


        public RecettesAppContext(DbContextOptions<RecettesAppContext> options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurer les clés étrangères avec des relations appropriées
            modelBuilder.Entity<Avis>()
                .HasOne(a => a.User)
                .WithMany(u => u.Avis)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Avis>()
                .HasOne(a => a.Recette)
                .WithMany(r => r.Avis)
                .HasForeignKey(a => a.RecetteID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Favori>()
           .HasOne(f => f.User)
           .WithMany(u => u.Favoris)
           .HasForeignKey(f => f.UserID)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Favori>()
                .HasOne(a => a.Recette)
                .WithMany(r => r.Favoris)
                .HasForeignKey(a => a.RecetteID)
                .OnDelete(DeleteBehavior.NoAction);

            // Autres configurations (relations, index, etc.)
        }

    }
}
