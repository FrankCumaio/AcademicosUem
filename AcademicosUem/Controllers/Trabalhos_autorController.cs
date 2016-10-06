using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AcademicosUem.Models;

namespace AcademicosUem.Controllers
{
    public class Trabalhos_autorController : Controller
    {
        private AcademicosUemDbContext db = new AcademicosUemDbContext();

        // GET: Trabalhos_autor
        public ActionResult Index()
        {
            var trabalhos_autor = db.Trabalhos_autor.Include(t => t.Autor);
            return View(trabalhos_autor.ToList());
        }

        // GET: Trabalhos_autor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabalhos_autor trabalhos_autor = db.Trabalhos_autor.Find(id);
            if (trabalhos_autor == null)
            {
                return HttpNotFound();
            }
            return View(trabalhos_autor);
        }

        // GET: Trabalhos_autor/Create
        public ActionResult Create()
        {
            ViewBag.AutorID = new SelectList(db.Autor, "Id", "Nome");
            return View();
        }

        // POST: Trabalhos_autor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrabalhosID,AutorID")] Trabalhos_autor trabalhos_autor)
        {
            if (ModelState.IsValid)
            {
                db.Trabalhos_autor.Add(trabalhos_autor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AutorID = new SelectList(db.Autor, "Id", "Nome", trabalhos_autor.AutorID);
            return View(trabalhos_autor);
        }

        // GET: Trabalhos_autor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabalhos_autor trabalhos_autor = db.Trabalhos_autor.Find(id);
            if (trabalhos_autor == null)
            {
                return HttpNotFound();
            }
            ViewBag.AutorID = new SelectList(db.Autor, "Id", "Nome", trabalhos_autor.AutorID);
            return View(trabalhos_autor);
        }

        // POST: Trabalhos_autor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrabalhosID,AutorID")] Trabalhos_autor trabalhos_autor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trabalhos_autor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AutorID = new SelectList(db.Autor, "Id", "Nome", trabalhos_autor.AutorID);
            return View(trabalhos_autor);
        }

        // GET: Trabalhos_autor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabalhos_autor trabalhos_autor = db.Trabalhos_autor.Find(id);
            if (trabalhos_autor == null)
            {
                return HttpNotFound();
            }
            return View(trabalhos_autor);
        }

        // POST: Trabalhos_autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trabalhos_autor trabalhos_autor = db.Trabalhos_autor.Find(id);
            db.Trabalhos_autor.Remove(trabalhos_autor);
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
    }
}
