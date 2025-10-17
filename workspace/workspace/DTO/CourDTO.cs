namespace workspace.DTO
{
    public class CourDTO
    {
        public string Chapitre { get; set; }
        public string Description { get; set; }
        public DateTime? DateHeure { get; set; }
        public string NomMatiere { get; set; } // Nom de la Matière pour la recherche
        public string EnseignantNom { get; set; } // Nom de l'Enseignant pour la recherche
     
    }
}
