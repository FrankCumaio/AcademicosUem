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
   
    public class TrabalhoFilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dashboard
        [Authorize]
        public ActionResult Dashboard()
        {
             ViewBag.CatFilesID = new SelectList(db.catFiles, "Id", "designacao");
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "username");
            ViewBag.DocentesID = new SelectList(db.Docente, "Id", "nome");
            string[] userRoles = Roles.GetRolesForUser(User.Identity.Name);
            for (var i = 0; i < userRoles.Length; i++)
            { }
                
                if (Roles.IsUserInRole("Estudante"))
                {
                    string id = User.Identity.GetUserId();
                    return View("dashboard", db.TrabalhoFiles.Where(t => t.Trabalho.ApplicationUser.Id == id));
                }
                else
                if (Roles.IsUserInRole("RA"))
            {
                return View("dashboard", db.TrabalhoFiles.Where(t => t.EstadoTrabalhoFile.Designacao.Equals("Pendente")));
            }
            if (Roles.IsUserInRole("CC"))
            {
                List<TrabalhoFiles> lst = new List<TrabalhoFiles>();
                var query = from o in db.TrabalhoFiles
                            join c in db.EstadosDoTrabalho on o.TrabalhoID equals c.TrabalhoID where c.EstadoTrabalhoID == 3 && o.CatFiles.Designacao =="Tese" && c.isActual==true
                            select new
                            {
                                TrabalhoFilesId = o.Id,
                                Path = o.Path,
                                TrabalhoID = o.TrabalhoID,
                                Trabalho = o.Trabalho,
                                CatFilesID = o.CatFilesID,
                                CatFiles = o.CatFiles,
                                EstadoTrabalhoFileID = o.EstadoTrabalhoFileID,
                                EstadoTrabalhoFile = o.EstadoTrabalhoFile,
                                Data = o.Data,
                            };
                foreach (var item in query)
                {
                    var trabalhoFiles = new TrabalhoFiles()
                    {
                        Id = item.TrabalhoFilesId,
                        Path = item.Path,
                        TrabalhoID = item.TrabalhoID,
                        Trabalho = item.Trabalho,
                        CatFilesID = item.CatFilesID,
                        CatFiles = item.CatFiles,
                        EstadoTrabalhoFileID = item.EstadoTrabalhoFileID,
                        EstadoTrabalhoFile = item.EstadoTrabalhoFile,
                        Data = item.Data,
                    };
                    lst.Add(trabalhoFiles);
                }
                    
                    return View("dashboard",lst);
            }
            else {
                return Redirect("/Account/Login"); }
               
        }

        public ActionResult ListaTrabalhos()
        {
            return View("listaTrabalhos", db.TrabalhoFiles.Where(t => t.EstadoTrabalhoFile.Designacao.Equals("Aprovado")));
        }

        public ActionResult DetalhesTrabalho(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrabalhoFiles trabalhoFile = db.TrabalhoFiles.Find(id);
            if (trabalhoFile == null)
            {
                return HttpNotFound();
            }
            return View("detalhesTrabalho", trabalhoFile);
        }
        [Authorize]
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
            ViewBag.CatFilesID = new SelectList(db.catFiles, "Id", "designacao");
            ViewBag.DocentesID = new SelectList(db.Docente, "Id", "nome");
            trabalhoFiles.EstadoTrabalhoFileID = db.EstadoTrabalhoFile.Where(a=>a.Designacao == "Aprovado").FirstOrDefault().Id;
            db.Entry(trabalhoFiles).State = EntityState.Modified;
            db.SaveChanges();
            return View("dashboard", db.TrabalhoFiles.Where(t => t.EstadoTrabalhoFile.Designacao.Equals("Pendente")));
            }
        [Authorize]
        // GET: TrabalhoFiles
        public ActionResult Index()
        {
            var trabalhoFiles = db.TrabalhoFiles.Include(t => t.CatFiles).Include(t => t.Trabalho);
            return View(trabalhoFiles.ToList());
        }
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Path,TrabalhoID,CatFilesID,Data")] TrabalhoFiles trabalhoFiles)
        {
            if (ModelState.IsValid)
            {
                db.TrabalhoFiles.Add(trabalhoFiles);
                db.SaveChanges();
                return RedirectToAction("/trabalhofiles/dashboard");
            }

            ViewBag.CatFilesID = new SelectList(db.catFiles, "Id", "Designacao", trabalhoFiles.CatFilesID);
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo", trabalhoFiles.TrabalhoID);
            return View("/trabalhofiles/dashboard");
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
