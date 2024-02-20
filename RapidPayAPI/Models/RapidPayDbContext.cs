using Microsoft.EntityFrameworkCore;

namespace RapidPayAPI.Models
{
    public class RapidPayDbContext : DbContext
    {
        public RapidPayDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Card> Cards { get; set; }
        public  DbSet<Payment> Payments { get; set; }
    }
}
