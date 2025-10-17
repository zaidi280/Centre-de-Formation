using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace workspace.Models
{
    public class Matiere
    {
        [Key]
        public string IdMatiere { get; set; } = Guid.NewGuid().ToString();
        public string? NomMatiere { get; set; }
        public string? Description { get; set; }
        public int? VolumeHoraire { get; set; }
        public virtual ICollection<Cour> Cours { get; set; } = new List<Cour>();
        [ForeignKey("Formation")]
        public string? FormationId { get; set; }
        [JsonIgnore]
        public virtual Formation? Formation { get; set; }
        [ForeignKey("Salle")]
        public string SalleId { get; set; }
        [JsonIgnore]
        public virtual Salle Salle { get; set; }
    }
}
