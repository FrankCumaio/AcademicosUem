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

namespace AcademicosUem.Controllers
{
    public class TrabalhoController : Controller
    {
        private AcademicosUemDbContext db = new AcademicosUemDbContext();

        // GET: Trabalho
        public ActionResult Index()
        {
            var trabalho = db.Trabalho.Include(t => t.Area);
            return View(trabalho.ToList());
        }

        public ActionResult Todos()
        {
            var trabalho = db.Trabalho.Include(t => t.Area);
            return View(trabalho.ToList());
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
            return View();
        }



        // POST: Trabalho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descricao,Data_Publicacao,Grau_Academico,Estado,DirectorioDoc,AreaID")] Trabalho trabalho,string[] selectedAutores,HttpPostedFileBase file)
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

            trabalho.Estado = "Registado";
            trabalho.Data_Publicacao = DateTime.Now.ToString();
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);
                file.SaveAs(path);
                trabalho.DirectorioDoc =path;
                db.Trabalho.Add(trabalho);
                db.SaveChanges();
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
