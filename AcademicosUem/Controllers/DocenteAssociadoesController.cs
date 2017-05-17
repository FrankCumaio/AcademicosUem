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
    public class DocenteAssociadoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DocenteAssociadoes
        public ActionResult Index()
        {
            var docenteAssociado = db.DocenteAssociado.Include(d => d.Docente).Include(d => d.Funcao).Include(d => d.Trabalho);
            return View(docenteAssociado.ToList());
        }

        // GET: DocenteAssociadoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocenteAssociado docenteAssociado = db.DocenteAssociado.Find(id);
            if (docenteAssociado == null)
            {
                return HttpNotFound();
            }
            return View(docenteAssociado);
        }

        // GET: DocenteAssociadoes/Create
        public ActionResult Create()
        {
            ViewBag.DocenteID = new SelectList(db.Docente, "Id", "apelido");
            ViewBag.FuncaoID = new SelectList(db.Funcao, "Id", "Designacao");
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo");
            return View();
        }

        // POST: DocenteAssociadoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrabalhoID,DocenteID,FuncaoID,Data")] DocenteAssociado docenteAssociado)
        {
            if (ModelState.IsValid)
            {
                db.DocenteAssociado.Add(docenteAssociado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocenteID = new SelectList(db.Docente, "Id", "apelido", docenteAssociado.DocenteID);
            ViewBag.FuncaoID = new SelectList(db.Funcao, "Id", "Designacao", docenteAssociado.FuncaoID);
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo", docenteAssociado.TrabalhoID);
            return View(docenteAssociado);
        }

        // GET: DocenteAssociadoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocenteAssociado docenteAssociado = db.DocenteAssociado.Find(id);
            if (docenteAssociado == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocenteID = new SelectList(db.Docente, "Id", "apelido", docenteAssociado.DocenteID);
            ViewBag.FuncaoID = new SelectList(db.Funcao, "Id", "Designacao", docenteAssociado.FuncaoID);
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo", docenteAssociado.TrabalhoID);
            return View(docenteAssociado);
        }

        // POST: DocenteAssociadoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrabalhoID,DocenteID,FuncaoID,Data")] DocenteAssociado docenteAssociado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docenteAssociado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocenteID = new SelectList(db.Docente, "Id", "apelido", docenteAssociado.DocenteID);
            ViewBag.FuncaoID = new SelectList(db.Funcao, "Id", "Designacao", docenteAssociado.FuncaoID);
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo", docenteAssociado.TrabalhoID);
            return View(docenteAssociado);
        }

        // GET: DocenteAssociadoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocenteAssociado docenteAssociado = db.DocenteAssociado.Find(id);
            if (docenteAssociado == null)
            {
                return HttpNotFound();
            }
            return View(docenteAssociado);
        }

        // POST: DocenteAssociadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocenteAssociado docenteAssociado = db.DocenteAssociado.Find(id);
            db.DocenteAssociado.Remove(docenteAssociado);
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
