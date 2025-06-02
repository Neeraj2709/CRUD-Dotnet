using Microsoft.EntityFrameworkCore;
using BookManagementAPI.Models;

namespace BookManagementAPI.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
                .HasOne<User>()
                .WithMany(u => u.Books)
                .HasForeignKey(b => b.UserId);
        }
    }
}