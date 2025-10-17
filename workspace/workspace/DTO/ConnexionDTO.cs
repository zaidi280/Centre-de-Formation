using System.ComponentModel.DataAnnotations;

namespace workspace.DTO
{
    public class ConnexionDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
