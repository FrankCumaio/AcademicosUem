using AcademicosUem.Models;
using AcademicosUem.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Web.Mvc;

namespace AcademicosUem.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            var account = new AccountController();
            var users = account.UserManager.Users;
            var roles = new List<string>();
            foreach (var user in users)
            {
                string str = "";
                foreach (var role in account.UserManager.GetRoles(user.Id))
                {
                    str = (str == "") ? role.ToString() : str + " - " + role.ToString();
                }
                roles.Add(str);
            }
            var model = new Usuario()
            {
                users = users.ToList(),
                roles = roles.ToList()
            };

            return View(model);
        }
    }
}