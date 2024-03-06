using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Evaluation.DAL
{
    public class DbContextEntityFactory : IDesignTimeDbContextFactory<DbContextEntity>
    {
        public DbContextEntity CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContextEntity>();

            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("EvalAPIConnectionString"));

            return new DbContextEntity(optionsBuilder.Options);
        }
    }
}
