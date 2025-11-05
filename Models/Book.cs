using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace story_brook_api.Models
{
    public class Book
    {
        [Key]
        [Column("id")]
        [MaxLength(20)]
        public string? Id { get; set; }
    }
}
