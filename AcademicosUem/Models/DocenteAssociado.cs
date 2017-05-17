using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicosUem.Models
{
    public class DocenteAssociado
    {
        public int Id { get; set; }
        public int TrabalhoID { get; set; }
        public int DocenteID { get; set; }
        public int FuncaoID { get; set; }
        public virtual Docente Docente { get; set; }
        public virtual Funcao Funcao { get; set; }
        public Trabalho Trabalho  { get; set; }
        //Permite criar timestamp logo na bd
        public DateTime Data { get; set; }
        public DocenteAssociado()
        {
            Data = DateTime.Now;
        }
    }
}
