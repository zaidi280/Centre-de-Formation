using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using workspace;

namespace workspace.Models
{

    public class WorkspaceContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public WorkspaceContext(DbContextOptions<WorkspaceContext> options)
            : base(options)
        {
        }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Cour> Cours { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Salle> Salles { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relation 1-1 entre Enseignant et ApplicationUser
            builder.Entity<Enseignant>()
                .HasOne(e => e.User)
                .WithOne(u => u.Enseignant)
                .HasForeignKey<Enseignant>(e => e.UserId);

            // Relation 1-1 entre Étudiant et ApplicationUser
            builder.Entity<Etudiant>()
                .HasOne(e => e.User)
                .WithOne(u => u.Etudiant)
                .HasForeignKey<Etudiant>(e => e.UserId);

            builder.Entity<Matiere>()
                .HasOne(e => e.Salle)
                .WithMany(u => u.Matieres)
                .HasForeignKey(e => e.SalleId);

            builder.Entity<Cour>()
                .HasOne(e => e.Matiere)
                .WithMany(u => u.Cours)
                .HasForeignKey(e => e.MatiereId);

            builder.Entity<Cour>()
                .HasOne(e => e.Enseignant)
                .WithMany(u => u.Cours)
                .HasForeignKey(e => e.EnseignantId);

            builder.Entity<Enseignant>()
                .HasOne(e => e.Formation)
                .WithMany(u => u.ListeEnseignants)
                .HasForeignKey(e => e.FormationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Etudiant>()
                .HasOne(e => e.Formation)
                .WithMany(u => u.ListeEtudiants)
                .HasForeignKey(e => e.FormationId);

            builder.Entity<Matiere>()
                .HasOne(e => e.Formation)
                .WithMany(u => u.ListeMatieres)
                .HasForeignKey(e => e.FormationId);

    

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}