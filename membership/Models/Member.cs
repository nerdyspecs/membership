using membership.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using membership.Data;
using Microsoft.AspNetCore.Http.HttpResults;


namespace membership.Models
{
    public class Member
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int TotalPoints { get; set; }
        public bool IsAdmin { get; set; } = false;
        public virtual ICollection<MemberRedemption> MemberRedemptions { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Transaction> Transactions { get; set; }

        public Level getLevel(List<Transaction> transactions, List<Level>levels) {
            double totalTransaction = 0;
            foreach (var transaction in transactions) {
                totalTransaction += transaction.Total;
            }
            var level = levels.FirstOrDefault(l => (totalTransaction >= l.MinTotalTransaction && totalTransaction <= l.MaxTotalTransaction));
            if (level == null) {
                var maxLevel = levels.OrderByDescending(l => l.MaxTotalTransaction).FirstOrDefault();

                if (totalTransaction > maxLevel.MaxTotalTransaction)
                {
                    return maxLevel;
                }
            }
            if (level == null)
            {
                return null;
            }
            else {
                return level;

            }
        }
    }
}