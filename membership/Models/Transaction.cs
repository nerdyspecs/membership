using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace membership.Models
{
    public class Transaction
    {
        [Key] // Mark CustomerId as the Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Automatically generate CustomerId (auto-increment)
        public int TransactionId { get; set; }
        public string TransactionTitle { get; set; }
        public double Total { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;

        // foreignkey
        public int? MemberId { get; set; }
        public Member Member { get; set; }
    }
}
    