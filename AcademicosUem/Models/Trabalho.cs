namespace AcademicosUem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Trabalho
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Grau_Academico { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        //Permite criar timestamp logo na bd
        public DateTime Data { get; set; }
        public Trabalho()
        {
            Data = DateTime.Now;
        }
    }

}
