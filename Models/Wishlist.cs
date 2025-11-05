using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace story_brook_api.Models
{
    public class Wishlist
    {
        [Key]
        [Column("id")]
        [MaxLength(36)]
        public string? Id { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
