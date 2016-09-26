namespace AcademicosUem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Trabalhos_autor
    {
        public int Id { get; set; }

        public int? TrabalhosID { get; set; }

        public int? AutorID { get; set; }

        public virtual Autor Autor { get; set; }

        public virtual Trabalho Trabalho { get; set; }
    }
}
