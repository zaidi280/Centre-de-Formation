using System.ComponentModel.DataAnnotations;

namespace workspace.Models
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        public string UserId { get; set; }
        public DateTime Expiration { get; set; }
    }
}
