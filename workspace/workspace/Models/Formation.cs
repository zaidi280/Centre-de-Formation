using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workspace.Models
{
    public class Formation
    {
        [Key]
        public string IdFormation { get; set; } = Guid.NewGuid().ToString();

        public string? Titre { get; set; } // Ex : "2ème année"
        public string? Description { get; set; } // Ex : "Licence"
        public int? Duree {  get; set; }
        public float? Prix { get; set; }
        public DateTime? DateDebut { get; set; } // Date d'inscription
        public DateTime? DateFin { get; set; }


        public virtual List<Enseignant> ListeEnseignants { get; set; } = new List<Enseignant>();

        public virtual List<Etudiant> ListeEtudiants { get; set; } = new List<Etudiant>();

    
        public virtual List<Matiere> ListeMatieres { get; set; } = new List<Matiere>();

    }
}
