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
        [StringLength(255, MinimumLength = 3, ErrorMessage = "My Error Message")]
        public string Titulo { get; set; }
        [StringLength(255, MinimumLength = 3, ErrorMessage = "My Error Message")]
        public string Descricao { get; set; }

        public int AutorID { get; set; }

        public int? AreaID { get; set; }

        public string _Data_Publicacao = DateTime.Now.ToString();
        public string Data_Publicacao
        {
            get
            {
                return _Data_Publicacao;
            }
            set { _Data_Publicacao = value; }
        }

        public virtual Area Area { get; set; }

        public string userId { get; set; }

        public virtual Autor Autor { get; set; }
    }
}
