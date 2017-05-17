using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicosUem.Models
{
    public class EstadosDoEvento
    {
        public int Id { get; set; }
        public int EventoID { get; set; }
        public int EstadoEventoID { get; set; }
        public virtual Evento Evento { get; set; }
        public virtual EstadoEvento EstadoEvento { get; set; }
        //Permite criar timestamp logo na bd
        public DateTime Data { get; set; }
        public EstadosDoEvento()
        {
            Data = DateTime.Now;
        }
    }
}