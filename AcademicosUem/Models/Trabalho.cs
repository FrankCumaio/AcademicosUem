namespace WebApplication2.Models
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
            Trabalhos_autor = new HashSet<Trabalhos_autor>();
        }

        public int Id { get; set; }

        [StringLength(250)]
        public string Titulo { get; set; }

        [StringLength(250)]
        public string Descricao { get; set; }

        [StringLength(250)]
        public string Data_Publicacao { get; set; }

        [StringLength(10)]
        public string Grau_Academico { get; set; }

        [StringLength(10)]
        public string Data_Defesa { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        [StringLength(100)]
        public string DirectorioDoc { get; set; }

        public int AreaID { get; set; }

        public virtual Area Area { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trabalhos_autor> Trabalhos_autor { get; set; }
    }
}
