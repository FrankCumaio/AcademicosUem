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
    public class TrabalhoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trabalhoes
        public ActionResult Index()
        {
            return View(db.Trabalho.ToList());
        }

        // GET: Trabalhoes/Details/5
        public ActionResult Details(int? id)
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

        // GET: Trabalhoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trabalhoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descricao,Grau_Academico,EstudanteID,Data")] Trabalho trabalho)
        {
            if (ModelState.IsValid)
            {
                db.Trabalho.Add(trabalho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trabalho);
        }

        // GET: Trabalhoes/Edit/5
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
            return View(trabalho);
        }

        // POST: Trabalhoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descricao,Grau_Academico,EstudanteID,Data")] Trabalho trabalho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trabalho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trabalho);
        }

        // GET: Trabalhoes/Delete/5
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

        // POST: Trabalhoes/Delete/5
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
    }
}
