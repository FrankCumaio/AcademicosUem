using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicosUem.Models
{
    public class EstadosDoTrabalho
    {
        public int Id { get; set; }
        public Boolean isActual { get; set; }
        public int TrabalhoID { get; set; }
        public int EstadoTrabalhoID { get; set; }
        public virtual Trabalho Trabalho { get; set; }
        public virtual EstadoTrabalho EstadoTrabalho { get; set; }
        //Permite criar timestamp logo na bd
        public DateTime Data { get; set; }
        public EstadosDoTrabalho()
        {
            Data = DateTime.Now;
        }
    }
}