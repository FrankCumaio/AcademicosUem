using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AcademicosUem.Models;
using AcademicosUem.ViewModels;
using System.IO;
using System.Diagnostics;

namespace AcademicosUem.Controllers
{
    public class TrabalhoController : Controller
    {
        private AcademicosMzDbContext db = new AcademicosMzDbContext();

        // GET: Trabalho
        public ActionResult Index()
        {
            var trabalho = db.Trabalho.Include(t => t.Area);
            return View(trabalho.ToList());
        }

        //public ActionResult Todos()
        //{
        //    ViewBag.Cursos = db.Curso.ToList();
        //    ViewBag.autores = db.Autor.ToList();
        //    var trabalho = db.Trabalho.Include(t => t.Area);
        //    return View(trabalho.ToList());
        //}

        //GET: Pesquisa

        public ActionResult Todos(string Area, string searchString)
        {
 
            if (searchString!=null && Area!=null)
            {
                var trabalhoRes = db.Trabalho.Include(t => t.Area).Where(t => t.Area.Curso.Area_conhecimento.Equals(Area) && t.Titulo.Equals(searchString, StringComparison.OrdinalIgnoreCase));
                    //from t in db.Trabalho
                    //           where t.Area.Curso.Area_conhecimento.Equals(Area) 
                    //           select t;
                return View(trabalhoRes.ToList());
            }
            else
            {
                ViewBag.Cursos = db.Curso.ToList();
                ViewBag.autores = db.Autor.ToList();
                var trabalho = db.Trabalho.Include(t => t.Area);
                return View(trabalho.ToList());
            }
            var GenreLst = new List<string>();
 
        }

        // GET: Trabalho/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabalho trabalho = db.Trabalho.Find(id);
            ViewBag.trabalhosRelacionados = db.Trabalho.ToList();

            if (trabalho == null)
            {  
                return HttpNotFound();
            }
            return View(trabalho);
        }

        // GET: Trabalho/Create
        public ActionResult Create()
        {
            ViewBag.AutorID = new SelectList(db.Autor, "Id", "Nome");
            ViewBag.AreaID = new SelectList(db.Area, "Id", "Nome");
            var trabalho = new Trabalho();
            trabalho.Autor = new List<Autor>();
            PopulateAssignedAutorData(trabalho);
            return PartialView("Create", new AcademicosUem.Models.Trabalho());
        }



        // POST: Trabalho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descricao,Data_Publicacao,Grau_Academico,Estado,DirectorioDoc,AreaID")] Trabalho trabalho,string[] selectedAutores, HttpPostedFileBase ficheiro)
        {

            if (selectedAutores != null)
            {
                trabalho.Autor = new List<Autor>();
                foreach (var autor in selectedAutores)
                {
                    var autorAdd = db.Autor.Find(int.Parse(autor));
                    trabalho.Autor.Add(autorAdd);
                }
            }
            // Verify that the user selected a file


            trabalho.Estado = "Registado";
            trabalho.Data_Publicacao = DateTime.Now.ToString();
            if (ModelState.IsValid)
            {

                db.Trabalho.Add(trabalho);
                db.SaveChanges();
                var last_insert_id = trabalho.Id.ToString() + ".pdf";
                if (ficheiro != null && ficheiro.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(ficheiro.FileName);
                    Debug.WriteLine(fileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine(Server.MapPath("~/uploads"), last_insert_id);
                    ficheiro.SaveAs(path);
                }
                return RedirectToAction("Index");
            }



            ViewBag.AreaID = new SelectList(db.Area, "Id", "Nome", trabalho.AreaID);
            return View(trabalho);
        }

        // GET: Trabalho/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabalho trabalho = db.Trabalho.Find(id);
            if (trabalho == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.Area, "Id", "Nome", trabalho.AreaID);
            return View(trabalho);
        }

        // POST: Trabalho/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descricao,Data_Publicacao,Grau_Academico,Data_Defesa,Estado,DirectorioDoc,AreaID")] Trabalho trabalho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trabalho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaID = new SelectList(db.Area, "Id", "Nome", trabalho.AreaID);
            return View(trabalho);
        }

        // GET: Trabalho/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabalho trabalho = db.Trabalho.Find(id);
            if (trabalho == null)
            {
                return HttpNotFound();
            }
            return View(trabalho);
        }

        // POST: Trabalho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trabalho trabalho = db.Trabalho.Find(id);
            db.Trabalho.Remove(trabalho);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateAssignedAutorData(Trabalho trabalho)
        {
            var allAutors = db.Autor;
            var trabalhoAutores = new HashSet<int>(trabalho.Autor.Select(c => c.Id));
            var viewModel = new List<AssignedAutorData>();
            foreach (var autor in allAutors)
            {
                viewModel.Add(new AssignedAutorData
                {
                    AutorID = autor.Id,
                    Nome = autor.Nome,
                    Assigned = trabalhoAutores.Contains(autor.Id)
                });
            }
            ViewBag.Autores = viewModel;
        }
    }
}
