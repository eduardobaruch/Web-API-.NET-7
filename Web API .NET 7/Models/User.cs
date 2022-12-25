using System.ComponentModel.DataAnnotations;

namespace Web_API_.NET_7.Models
{
    public class User
    {
        public int Id { get; set; }
        public String Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public List<Character>? Characters { get; set; }
        [Required]
        public string Role { get; set; } = "Player";
    }
}
