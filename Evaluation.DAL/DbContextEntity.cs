using Evaluation.Entities;
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

        /// <summary>
        /// DbSet for Model1.
        /// </summary>
        public virtual DbSet<Evenement> Model1 { get; set; }

        /// <summary>
        /// OnModelCreating builder.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.Model1TableInstantiate(modelBuilder);
        }

        /// <summary>
		/// Model1 table instantiation.
		/// </summary>
		/// <param name="modelBuilder"></param>
		private void Model1TableInstantiate(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evenement>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }

    }
}
