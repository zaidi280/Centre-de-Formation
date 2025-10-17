using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace workspace.Models
{
    public class Etudiant
    {
        [Key]
        public string IdEtudiant { get; set; } = Guid.NewGuid().ToString();

        public string? Classe { get; set; } // Ex : "2ème année"
        public string? Niveau { get; set; } // Ex : "Licence"
        public DateTime? DateInscription { get; set; } // Date d'inscription

        // Clé étrangère pour ApplicationUser
        [ForeignKey("User")]
        public string UserId { get; set; }
        //public ApplicationUser User { get; set; } // Relation 1-1 avec ApplicationUser
        public virtual ApplicationUser User { get; set; } // Relation 1-1 avec lazy loading
        [ForeignKey("Formation")]
        public string? FormationId { get; set; }
        [JsonIgnore]
        public virtual Formation? Formation { get; set; }
    }
}
