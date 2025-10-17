using System.ComponentModel.DataAnnotations;

namespace workspace.DTO
{
    public class InscriptionDTO
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Adresse { get; set; }

        // Ajout des propriétés spécifiques à l'étudiant

        [Required]
        public string? Classe { get; set; } // Ex : "2ème année"
        [Required]
        public string? Niveau { get; set; } // Ex : "Licence"
        [Required]
        public DateTime? DateInscription { get; set; } // Date d'inscription


       
   
    }
}
