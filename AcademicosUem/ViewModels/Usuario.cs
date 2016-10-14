using AcademicosUem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AcademicosUem.ViewModels
{
    public class Usuario
    {

            public IList<ApplicationUser> users { get; set; }
            public IList<string> roles { get; set; }

    }
}