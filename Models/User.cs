using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace story_brook_api.Models
{
    public class User
    {
        [Key]
        [Column("id")]
        [MaxLength(36)]
        public string? UserId { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("password")]
        public string? Password { get; set; }
    }
}
