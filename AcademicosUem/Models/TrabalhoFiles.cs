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
        public int TrabalhoID { get; set; }
        public virtual Trabalho Trabalho { get; set; }
        public int CatFilesID { get; set; }
        public virtual catFiles CatFiles { get; set; }
        public int EstadoTrabalhoFileID { get; set; }
        public virtual EstadoTrabalhoFile EstadoTrabalhoFile { get; set; }
        //Permite criar timestamp logo na bd
        public DateTime Data { get; set; }
        public TrabalhoFiles()
        {
            Data = DateTime.Now;
        }
    }
}