using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicosUem.Models
{
    public class TrabalhoFiles
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Tipo { get; set; }
        public int TrabalhoID { get; set; }
        public virtual Trabalho Trabalho { get; set; }
    }
}