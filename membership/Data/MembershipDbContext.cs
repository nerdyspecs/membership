using Microsoft.EntityFrameworkCore;
using membership.Models;

namespace membership.Data
{
    public class MembershipDbContext : DbContext
    {
        public MembershipDbContext(DbContextOptions<MembershipDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Transaction> Transactions{ get; set; }
        public DbSet<Level> Levels{ get; set; }
        public DbSet<RedemptionItem> RedemptionItems{ get; set; }   
        public DbSet<MemberRedemption> MemberRedemptions{ get; set; }
    }
}
