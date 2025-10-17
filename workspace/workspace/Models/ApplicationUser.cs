using Microsoft.AspNetCore.Identity;

namespace workspace.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Telephone { get; set; }
        public string? Adresse { get; set; }
        public string ActiveToken { get; set; } = Guid.NewGuid().ToString();
        public DateTime TokenExpiration { get; set; }

        // Relations
        // Make this property virtual to enable lazy loading
        public virtual Enseignant Enseignant { get; set; }


        // Ensure this navigation property is marked as 'virtual'
        public virtual Etudiant Etudiant { get; set; }// Relation 1-1 avec Étudiant
    }
}
