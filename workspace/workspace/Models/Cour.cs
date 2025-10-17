using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workspace.Models
{
    public class Cour
    {
        [Key]
        public string IdCour { get; set; } = Guid.NewGuid().ToString();
        public string? Chapitre { get; set; }
        public string? Description { get; set; }
        public DateTime? DateHeure { get; set; }

        [ForeignKey("Matiere")]
        public string MatiereId { get; set; }
        public virtual Matiere Matiere { get; set; }
        [ForeignKey("Enseignant")]
        public string EnseignantId { get; set; }
        public virtual Enseignant Enseignant { get; set; }
    
    }
}
