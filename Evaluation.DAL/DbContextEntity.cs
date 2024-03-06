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
        /// DbSet for Evenement.
        /// </summary>
        public virtual DbSet<Evenement> Evenement { get; set; }

        /// <summary>
        /// OnModelCreating builder.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.EvenementTableInstantiate(modelBuilder);
        }

        /// <summary>
		/// Evenement table instantiation.
		/// </summary>
		/// <param name="modelBuilder"></param>
		private void EvenementTableInstantiate(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evenement>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Titre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.DateEvent)
                    .IsRequired();

                entity.Property(e => e.TimeEvent)
                    .IsRequired();

                entity.Property(e => e.Lieu)
                     .IsRequired()
                     .HasMaxLength(150);
            });
        }

    }
}
