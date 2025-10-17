using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workspace.Models
{
    public class Salle
    {
        [Key]
        public string IdSalle { get; set; } = Guid.NewGuid().ToString();
        public string? NomSalle { get; set; }
        public int? Capacite { get; set; }
        public string? TypeSalle { get; set; }
        public string? Equipement {  get; set; }
        public virtual ICollection<Matiere> Matieres { get; set; } = new List<Matiere>();
       

    }
}
