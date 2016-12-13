using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicosUem.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Local { get; set; }
        public string Agenda { get; set; }
        public int Telefone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public Boolean IsPublished { get; set; }
        public Boolean IsPublic { get; set; }
        public DateTime publishDate { get; set; }
        public int TrabalhoID { get; set; }
        public int EventoCategoriaID { get; set; }
        public virtual Trabalho Trabalho { get; set; }
        public virtual EventoCategoria EventoCategoria { get; set; }

    }
}