using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace membership.Models
{
    public class RedemptionItem
    {
        [Key] // Mark CustomerId as the Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Automatically generate CustomerId (auto-increment)
        public int RedemptionItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RequiredPoints { get; set; }
        public Level Level { get; set; } //reference to level

        public static List<RedemptionItem> Redemable(IEnumerable<RedemptionItem> items, int levelid)
        {

            return items.Where(item => item.Level.LevelId <= levelid).ToList();
        }

        public static List<RedemptionItem> Nonredeamable(IEnumerable<RedemptionItem> items, int levelid)
        {
            return items.Where(item => item.Level.LevelId > levelid).ToList();
        }

    }
}
