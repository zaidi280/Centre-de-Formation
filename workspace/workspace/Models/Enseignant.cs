using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace workspace.Models
{
    public class Enseignant
    {
        [Key]
        public string IdEnseignant { get; set; } = Guid.NewGuid().ToString();

        public string? Specialite { get; set; }
        public int? AnneesExperience { get; set; }
        public string? Diplome { get; set; }
        public DateTime? DateEmbauche { get; set; }

        public virtual ICollection<Cour> Cours { get; set; } = new List<Cour>();
        // Clé étrangère pour ApplicationUser
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; } // Relation 1-1 avec lazy loading
        [ForeignKey("Formation")]
        public string? FormationId { get; set; }
        [JsonIgnore]
        public virtual Formation? Formation { get; set; }

    }
}
