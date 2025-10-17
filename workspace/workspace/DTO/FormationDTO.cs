namespace workspace.DTO
{
    public class FormationDTO
    {
        public string Titre { get; set; } // Ex : "2ème année"
        public string Description { get; set; } // Ex : "Licence"
        public int? Duree { get; set; }
        public float? Prix { get; set; }
        public DateTime? DateDebut { get; set; } // Date d'inscription
        public DateTime? DateFin { get; set; }

        // Propriétés pour les relations
        public List<string> Enseignants { get; set; } = new List<string>();
        public List<string> Etudiants { get; set; } = new List<string>();
        public List<string> Matieres { get; set; } = new List<string>();
    }
}
