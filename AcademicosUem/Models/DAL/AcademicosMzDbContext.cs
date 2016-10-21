namespace AcademicosUem.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AcademicosMzDbContext : DbContext
    {
        public AcademicosMzDbContext()
            : base("name=AcademicosMzDbContext")
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Temas> Temas { get; set; }
        public virtual DbSet<Trabalho> Trabalho { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SCOTT");

            modelBuilder.Entity<Autor>()
                .HasMany(e => e.Trabalhos)
                .WithMany(e => e.Autor)
                .Map(m => m.ToTable("TrabalhoAutors", "SCOTT"));
        }
    }
}
