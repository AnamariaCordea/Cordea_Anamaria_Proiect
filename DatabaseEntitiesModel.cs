using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DatabaseModel
{
    public partial class DatabaseEntitiesModel : DbContext
    {
        public DatabaseEntitiesModel()
            : base("name=DatabaseEntitiesModel")
        {
        }

        public virtual DbSet<Animale> Animals { get; set; }
        public virtual DbSet<Medici> Medics { get; set; }
        public virtual DbSet<Programari> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animale>()
                .HasMany(e => e.Appointments)
                .WithOptional(e => e.Animale)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Medici>()
                .HasMany(e => e.Appointments)
                .WithOptional(e => e.Medici)
                .WillCascadeOnDelete();
        }
    }
}
