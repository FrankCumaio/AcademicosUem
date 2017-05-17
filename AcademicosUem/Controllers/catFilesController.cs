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
    public class catFilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: catFiles
        public ActionResult Index()
        {
            return View(db.catFiles.ToList());
        }

        // GET: catFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catFiles catFiles = db.catFiles.Find(id);
            if (catFiles == null)
            {
                return HttpNotFound();
            }
            return View(catFiles);
        }

        // GET: catFiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: catFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Designacao,Desc")] catFiles catFiles)
        {
            if (ModelState.IsValid)
            {
                db.catFiles.Add(catFiles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(catFiles);
        }

        // GET: catFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catFiles catFiles = db.catFiles.Find(id);
            if (catFiles == null)
            {
                return HttpNotFound();
            }
            return View(catFiles);
        }

        // POST: catFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Designacao,Desc")] catFiles catFiles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catFiles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catFiles);
        }

        // GET: catFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catFiles catFiles = db.catFiles.Find(id);
            if (catFiles == null)
            {
                return HttpNotFound();
            }
            return View(catFiles);
        }

        // POST: catFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            catFiles catFiles = db.catFiles.Find(id);
            db.catFiles.Remove(catFiles);
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
