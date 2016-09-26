namespace WebApplication2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Temas> Temas { get; set; }
        public virtual DbSet<Trabalho> Trabalho { get; set; }
        public virtual DbSet<Trabalhos_autor> Trabalhos_autor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Area>()
                .HasMany(e => e.Trabalho)
                .WithRequired(e => e.Area)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Autor>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Autor>()
                .Property(e => e.Telefone)
                .IsUnicode(false);

            modelBuilder.Entity<Autor>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Autor>()
                .HasMany(e => e.Temas)
                .WithRequired(e => e.Autor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Curso>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Curso>()
                .HasMany(e => e.Area)
                .WithRequired(e => e.Curso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Curso>()
                .HasMany(e => e.Autor)
                .WithRequired(e => e.Curso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Temas>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Temas>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Trabalho>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Trabalho>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Trabalho>()
                .Property(e => e.Data_Publicacao)
                .IsUnicode(false);

            modelBuilder.Entity<Trabalho>()
                .Property(e => e.Grau_Academico)
                .IsUnicode(false);

            modelBuilder.Entity<Trabalho>()
                .Property(e => e.Data_Defesa)
                .IsFixedLength();

            modelBuilder.Entity<Trabalho>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Trabalho>()
                .Property(e => e.DirectorioDoc)
                .IsUnicode(false);

            modelBuilder.Entity<Trabalho>()
                .HasMany(e => e.Trabalhos_autor)
                .WithOptional(e => e.Trabalho)
                .HasForeignKey(e => e.TrabalhosID);
        }
    }
}
