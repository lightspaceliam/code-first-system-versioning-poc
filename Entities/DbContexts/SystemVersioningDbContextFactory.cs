using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Entities.DbContexts
{
    public class SystemVersioningDbContextFactory : IDesignTimeDbContextFactory<SystemVersioningDbContext>
    {
        public SystemVersioningDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SystemVersioningDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=SystemVersioningDbContext;Integrated Security=True;MultipleActiveResultSets=True");

            return new SystemVersioningDbContext(optionsBuilder.Options);
        }
    }
}
