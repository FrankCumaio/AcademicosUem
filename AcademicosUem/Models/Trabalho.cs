﻿namespace AcademicosUem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trabalho")]
    public partial class Trabalho
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Grau_Academico { get; set; }
        public string EstudanteID { get; set; }
        public virtual Estudante Estudante { get; set; }
        //Permite criar timestamp logo na bd
        public DateTime Data { get; set; }
        public Trabalho()
        {
            Data = DateTime.Now;
        }
    }

}
