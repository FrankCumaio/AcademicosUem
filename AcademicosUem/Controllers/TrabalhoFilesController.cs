﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AcademicosUem.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace AcademicosUem.Controllers
{
    [Authorize]
    public class TrabalhoFilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            string[] userRoles = Roles.GetRolesForUser(User.Identity.Name);
            for (var i = 0; i < userRoles.Length; i++)
            { }
                
                if (Roles.IsUserInRole("Estudante"))
                {
                    object id = User.Identity.GetUserId();
                    return View("dashboard", db.TrabalhoFiles.Where(t => t.Trabalho.ApplicationUser.Id == id));
                }
                else
                if (Roles.IsUserInRole("RA"))
            {
                return View("dashboardRA", db.TrabalhoFiles.Where(t => t.EstadoTrabalhoFile.Designacao.Equals("Pendente")));
            }
            if (Roles.IsUserInRole("CC"))
            {
                return View("dashboardCC", db.TrabalhoFiles.Where(t => t.EstadoTrabalhoFile.Designacao.Equals("Aprovado")));
            }
            else {
                return Redirect("/Account/Login"); }
               
        }

        public ActionResult AprovarDoc(int? id)
        {
              if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TrabalhoFiles trabalhoFiles = db.TrabalhoFiles.Find(id);
                if (trabalhoFiles == null)
                {
                    return HttpNotFound();
                }
            
            trabalhoFiles.EstadoTrabalhoFileID = db.EstadoTrabalhoFile.Where(a=>a.Designacao == "Aprovado").FirstOrDefault().Id;
            db.Entry(trabalhoFiles).State = EntityState.Modified;
            db.SaveChanges();
            return View("dashboard", db.TrabalhoFiles.Where(t => t.EstadoTrabalhoFile.Designacao.Equals("Pendente")));
            }

            // GET: TrabalhoFiles
            public ActionResult Index()
        {
            var trabalhoFiles = db.TrabalhoFiles.Include(t => t.CatFiles).Include(t => t.Trabalho);
            return View(trabalhoFiles.ToList());
        }

        // GET: TrabalhoFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrabalhoFiles trabalhoFiles = db.TrabalhoFiles.Find(id);
            if (trabalhoFiles == null)
            {
                return HttpNotFound();
            }
            return View(trabalhoFiles);
        }

        // GET: TrabalhoFiles/Create
        public ActionResult Create()
        {
            ViewBag.CatFilesID = new SelectList(db.catFiles, "Id", "Designacao");
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo");
            return View();
        }

        // POST: TrabalhoFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Path,TrabalhoID,CatFilesID,Data")] TrabalhoFiles trabalhoFiles)
        {
            if (ModelState.IsValid)
            {
                db.TrabalhoFiles.Add(trabalhoFiles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatFilesID = new SelectList(db.catFiles, "Id", "Designacao", trabalhoFiles.CatFilesID);
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo", trabalhoFiles.TrabalhoID);
            return View(trabalhoFiles);
        }

        // GET: TrabalhoFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrabalhoFiles trabalhoFiles = db.TrabalhoFiles.Find(id);
            if (trabalhoFiles == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatFilesID = new SelectList(db.catFiles, "Id", "Designacao", trabalhoFiles.CatFilesID);
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo", trabalhoFiles.TrabalhoID);
            return View(trabalhoFiles);
        }

        // POST: TrabalhoFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Path,TrabalhoID,CatFilesID,Data")] TrabalhoFiles trabalhoFiles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trabalhoFiles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatFilesID = new SelectList(db.catFiles, "Id", "Designacao", trabalhoFiles.CatFilesID);
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo", trabalhoFiles.TrabalhoID);
            return View(trabalhoFiles);
        }

        // GET: TrabalhoFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrabalhoFiles trabalhoFiles = db.TrabalhoFiles.Find(id);
            if (trabalhoFiles == null)
            {
                return HttpNotFound();
            }
            return View(trabalhoFiles);
        }

        // POST: TrabalhoFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrabalhoFiles trabalhoFiles = db.TrabalhoFiles.Find(id);
            db.TrabalhoFiles.Remove(trabalhoFiles);
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
