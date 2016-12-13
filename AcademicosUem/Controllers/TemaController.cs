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
    public class TemaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tema
        public ActionResult Index()
        {
            var temas = db.Temas.Include(t => t.Area).Include(t => t.Perfil);
            return View(temas.ToList());
        }

        // GET: Tema/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temas temas = db.Temas.Find(id);
            ViewBag.temasRelacionados = db.Temas.ToList();
            ViewBag.Perfiles = db.Perfil.ToList();

            if (temas == null)
            {
                return HttpNotFound();
            }
            return View(temas);
        }

        // GET: Tema/Create
        public ActionResult Create()
        {
            ViewBag.AreaID = new SelectList(db.Area, "Id", "Nome");
            ViewBag.PerfilID = new SelectList(db.Perfil, "Id", "Nome");
            return PartialView("Create", new AcademicosUem.Models.Temas());
        }

        // POST: Tema/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descricao,PerfilID,AreaID,userId")] Temas temas)
        {
            temas.Data_Publicacao = DateTime.Now.ToString();

            if (ModelState.IsValid)
            {
                db.Temas.Add(temas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AreaID = new SelectList(db.Area, "Id", "Nome", temas.AreaID);
            ViewBag.PerfilID = new SelectList(db.Perfil, "Id", "Nome", temas.PerfilID);
            return View(temas);
        }

        // GET: Tema/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temas temas = db.Temas.Find(id);
            if (temas == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.Area, "Id", "Nome", temas.AreaID);
            ViewBag.PerfilID = new SelectList(db.Perfil, "Id", "Nome", temas.PerfilID);
            return View(temas);
        }

        // POST: Tema/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descricao,PerfilID,AreaID")] Temas temas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(temas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaID = new SelectList(db.Area, "Id", "Nome", temas.AreaID);
            ViewBag.PerfilID = new SelectList(db.Perfil, "Id", "Nome", temas.PerfilID);
            return View(temas);
        }

        // GET: Tema/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temas temas = db.Temas.Find(id);
            if (temas == null)
            {
                return HttpNotFound();
            }
            return View(temas);
        }

        // POST: Tema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Temas temas = db.Temas.Find(id);
            db.Temas.Remove(temas);
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
