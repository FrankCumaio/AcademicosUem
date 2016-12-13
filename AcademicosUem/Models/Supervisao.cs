using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicosUem.Models
{
    public class Supervisao
    {
        public int Id { get; set; }
        public int PerfilID { get; set; }
        public int TrabalhoID { get; set; }
        public int PapelID { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual Trabalho Trabalho { get; set; }
        public virtual Papel Papel { get; set; }

    }
}