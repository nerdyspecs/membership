using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace membership.Models
{
    public class MemberRedemption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RedemptionId { get; set; }
        public Member Member { get; set; }
        public RedemptionItem RedemptionItem { get; set; }
        public int PointsDeducted { get; set; }
        public int PointsRemaining { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
    }
}
