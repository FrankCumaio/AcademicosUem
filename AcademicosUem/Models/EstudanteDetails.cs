using System;

namespace AcademicosUem.Models
{
    public class EstudanteDetails
    {
        public int Id { get; set; }
        public DateTime dataNasc { get; set; }
        public string morada { get; set; }
        public string seccao { get; set; }
        public string curso { get; set; }
        public int ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}