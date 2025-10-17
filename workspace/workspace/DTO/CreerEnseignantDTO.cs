namespace workspace.DTO
{
    public class CreerEnseignantDTO
    {
        public string UserName { get; set; } // Nom d'utilisateur
        public string Email { get; set; } // Adresse email
        public string Nom { get; set; } // Nom de famille
        public string Prenom { get; set; } // Prénom
        public string Telephone { get; set; } // Numéro de téléphone
        public string Adresse { get; set; } // Adresse
        public string Specialite { get; set; } // Spécialité
        public int? AnneesExperience { get; set; } // Années d'expérience
        public string Diplome { get; set; } // Diplôme
        public DateTime? DateEmbauche { get; set; } // Date d'embauche
        public string Password { get; set; } // Mot de passe


    }
}
