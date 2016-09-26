namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Autor")]
    public partial class Autor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Autor()
        {
            Temas = new HashSet<Temas>();
            Trabalhos_autor = new HashSet<Trabalhos_autor>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(250)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Telefone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Nr_estudante { get; set; }

        public int CursoID { get; set; }

        public virtual Curso Curso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Temas> Temas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trabalhos_autor> Trabalhos_autor { get; set; }
    }
}
