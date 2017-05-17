using System;

namespace AcademicosUem.Models
{
    public class Estudante
    {
        public int Id { get; set; }
        public string apelido { get; set; }
        public string nome { get; set; }
        public DateTime dataNasc { get; set; }
        public string morada { get; set; }
        public string seccao { get; set; }
        public string curso { get; set; }

    }
}