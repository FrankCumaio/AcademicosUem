namespace AcademicosUem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SCOTT.Temas")]
    public partial class Tema
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255, MinimumLength = 3, ErrorMessage = "Erro no tipo de dados String do Oracle")]
        public string Titulo { get; set; }

        [StringLength(255, MinimumLength = 3, ErrorMessage = "Erro no tipo de dados String do Oracle")]
        public string Descricao { get; set; }

        public int AutorID { get; set; }

        public int? AreaID { get; set; }

        public virtual Area Area { get; set; }

        public virtual Autor Autor { get; set; }
    }
}
