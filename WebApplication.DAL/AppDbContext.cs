using Microsoft.EntityFrameworkCore;
using WebApplication.DAL.Entities;

namespace WebApplication.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureBooks(modelBuilder);
        }

        private void ConfigureBooks(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Book>();

            entity.HasIndex(b => b.ISBN)
                  .IsUnique();
        }
    }
}
