using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace AcademicosUem.ViewModels
{
    public class Ficheiro
    {
            public HttpPostedFileBase File { get; set; }
    }
}