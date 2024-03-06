using Microsoft.EntityFrameworkCore;

namespace Evaluation.DAL
{
    public class DbContextEntity : DbContext
    {
        /// <summary>
        /// Call the base constructor.
        /// </summary>
        /// <param name="options"></param>
        public DbContextEntity(DbContextOptions<DbContextEntity> options) : base(options) { }

    }
}
