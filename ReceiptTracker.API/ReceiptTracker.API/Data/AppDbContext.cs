using Microsoft.EntityFrameworkCore;
using ReceiptTracker.Shared.Models;

namespace ReceiptTracker.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Receipt> Receipts { get; set; } //this sets up the receipts table in db

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Receipt>() //id is the primary key
                   .HasKey(x => x.Id);

            modelBuilder.Entity<Receipt>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("LOWER(HEX(RANDOMBLOB(16)))");
        }
    }
}
