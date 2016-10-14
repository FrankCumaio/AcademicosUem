namespace AcademicosUem.Testes.Models
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

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Autor> Autors { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Tema> Temas { get; set; }
        public virtual DbSet<Trabalho> Trabalho { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>()
                .HasMany(e => e.Trabalhoes)
                .WithMany(e => e.Autors)
                .Map(m => m.ToTable("TrabalhoAutors", "SCOTT"));
        }
    }
}
