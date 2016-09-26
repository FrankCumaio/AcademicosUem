namespace AcademicosUem.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class AcademicosUemDbContext : DbContext
    {


        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Temas> Temas { get; set; }
        public virtual DbSet<Trabalho> Trabalho { get; set; }
        public virtual DbSet<Trabalhos_autor> Trabalhos_autor { get; set; }


    }
}