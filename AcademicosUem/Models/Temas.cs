namespace AcademicosUem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SCOTT.Temas")]
    public partial class Temas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public int AutorID { get; set; }

        public int? AreaID { get; set; }

        public virtual Area Area { get; set; }

        public virtual Autor Autor { get; set; }
    }
}
