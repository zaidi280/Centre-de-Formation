using System.ComponentModel.DataAnnotations;

namespace workspace.DTO
{
    public class InscriptionformetudDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Titre { get; set; }

    }
}
