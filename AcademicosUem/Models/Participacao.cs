using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicosUem.Models
{
    public class Participacao
    {
        public int Id { get; set; }
        public Boolean IsNotified { get; set; }
        public Boolean IsConfirmed { get; set; }
        public  int PerfilID { get; set; }
        public int EventoID { get; set; }
        public int PapelID { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual Evento Evento { get; set; }
        public virtual Papel Papel { get; set; }

    }
}