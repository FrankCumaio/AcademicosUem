namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Temas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Titulo { get; set; }

        [StringLength(250)]
        public string Descricao { get; set; }

        public int AutorID { get; set; }

        public int? AreaID { get; set; }

        public virtual Area Area { get; set; }

        public virtual Autor Autor { get; set; }
    }
}
