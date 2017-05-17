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
    public class EstudantesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Estudantes
        public ActionResult Index()
        {
            return View(db.Estudante.ToList());
        }

        // GET: Estudantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudante estudante = db.Estudante.Find(id);
            if (estudante == null)
            {
                return HttpNotFound();
            }
            return View(estudante);
        }

        // GET: Estudantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estudantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,apelido,nome,dataNasc,morada,seccao,curso")] Estudante estudante)
        {
            if (ModelState.IsValid)
            {
                db.Estudante.Add(estudante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estudante);
        }

        // GET: Estudantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudante estudante = db.Estudante.Find(id);
            if (estudante == null)
            {
                return HttpNotFound();
            }
            return View(estudante);
        }

        // POST: Estudantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,apelido,nome,dataNasc,morada,seccao,curso")] Estudante estudante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estudante);
        }

        // GET: Estudantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudante estudante = db.Estudante.Find(id);
            if (estudante == null)
            {
                return HttpNotFound();
            }
            return View(estudante);
        }

        // POST: Estudantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estudante estudante = db.Estudante.Find(id);
            db.Estudante.Remove(estudante);
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
