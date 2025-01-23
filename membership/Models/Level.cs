using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace membership.Models
{
    public class Level
    {
        [Key] // Mark CustomerId as the Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Automatically generate CustomerId (auto-increment)
        public int LevelId { get; set; }
        public string LevelTitle { get; set; }
        public int MinTotalTransaction {  get; set; }
        public int MaxTotalTransaction { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;

    }
}
