namespace AcademicosUem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trabalho")]
    public partial class Trabalho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trabalho()
        {
            Autor = new HashSet<Autor>();
        }

        public int Id { get; set; }
        [StringLength(255, MinimumLength = 3, ErrorMessage = "My Error Message")]
        public string Titulo { get; set; }
        [StringLength(255, MinimumLength = 3, ErrorMessage = "My Error Message")]
        public string Descricao { get; set; }
        [StringLength(255, MinimumLength = 3, ErrorMessage = "My Error Message")]
        public string _Data_Publicacao = DateTime.Now.ToString();
        [StringLength(255, MinimumLength = 3, ErrorMessage = "My Error Message")]
        public string Data_Publicacao {
            get {
                return _Data_Publicacao;
            }
            set{ _Data_Publicacao = value; }
        }
        [StringLength(255, MinimumLength = 3, ErrorMessage = "My Error Message")]
        public string Grau_Academico { get; set; }
        [StringLength(255, MinimumLength = 3, ErrorMessage = "My Error Message")]
        public string Estado { get; set; }

        public string DirectorioDoc { get; set; }

        public int AreaID { get; set; }
            
        public virtual Area Area { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Autor> Autor { get; set; }
    }
}
