using Microsoft.EntityFrameworkCore;

namespace Entities.DbContexts
{
    public class SystemVersioningDbContext : DbContext
    {
        public SystemVersioningDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions) { }

        public SystemVersioningDbContext() { }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasIndex(e => e.FirstName);
            modelBuilder.Entity<Person>()
                .HasIndex(e => e.LastName);
            modelBuilder.Entity<Person>()
                .HasIndex(e => e.Email);
        }
    }
}
