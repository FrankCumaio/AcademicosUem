namespace AcademicosUem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class Temas
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public int PerfilID { get; set; }

        public int? AreaID { get; set; }

        public string _Data_Publicacao = DateTime.Now.ToString();
        public string Data_Publicacao
        {
            get
            {
                return _Data_Publicacao;
            }
            set { _Data_Publicacao = value; }
        }

        public virtual Area Area { get; set; }

        public string userId { get; set; }

        public virtual Perfil Perfil { get; set; }
    }
}
