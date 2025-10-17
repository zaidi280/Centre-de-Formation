namespace workspace.DTO
{
    public class CreerEtudiantDTO
    {
        public string UserName { get; set; } // Nom d'utilisateur
        public string Email { get; set; } // Adresse email
        public string Nom { get; set; } // Nom de famille
        public string Prenom { get; set; } // Prénom
        public string Telephone { get; set; } // Numéro de téléphone
        public string Adresse { get; set; } // Adresse
        public string Classe { get; set; } // Classe
        public string Niveau { get; set; } // Niveau
        public DateTime? DateInscription { get; set; } // DateInscription
        public string Password { get; set; } // Mot de passe
    }
}
