using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AcademicosUem.Models;
using System.IO;
using System.Diagnostics;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace AcademicosUem.Controllers
{
    [Authorize]
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

        //Json verfica existencia de trabalhos
        public ActionResult verficaexistencia()
        {
            string id = User.Identity.GetUserId();
            int hastrabalho = (int)db.Trabalho.Where(t => t.ApplicationUserID == id).Count();
            return Json(hastrabalho, JsonRequestBehavior.AllowGet); ;
        }
            // GET: Trabalhoes/Create
            public ActionResult Create()
        {
            ViewBag.CatFilesID = new SelectList(db.catFiles, "Id", "designacao");
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "username");
            ViewBag.DocentesID = new SelectList(db.Docente, "Id", "nome");
            string id = User.Identity.GetUserId();
            ViewBag.hastrabalho = (int)db.Trabalho.Where(t => t.ApplicationUserID == id).Count();

            //if (hastrabalho >= 1)
            //{
            //    return View("/trabalhofiles/create");

            //}
            //else
            //{
    
            //}
            
                        return View();
        }

        // POST: Trabalhoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descricao,Grau_Academico,ApplicationUserID,Data")] Trabalho trabalho, HttpPostedFileBase ficheiro,int DocentesID, int CatFilesID)
        {
            int trabID = new int();
            var last_insert_id = "";
            int fileID = new int();
            TrabalhoFiles files = new TrabalhoFiles();
            DocenteAssociado docentes = new DocenteAssociado();
            string id = User.Identity.GetUserId();
            int hastrabalho = (int)db.Trabalho.Where(t => t.ApplicationUserID == id).Count();
            if (hastrabalho > 0)
            {
                trabID = db.Trabalho.Where(t => t.ApplicationUserID == id).FirstOrDefault().Id;
               
               
                 last_insert_id = files.Id + "Protocolo.pdf";
                files.Path = last_insert_id;
                files.TrabalhoID = trabID;
                if (Roles.IsUserInRole("Estudante"))
                {
                    files.CatFilesID = CatFilesID;
                    if (db.catFiles.Where(c => c.Id == CatFilesID).FirstOrDefault().Designacao == "Tese")
                    {
                        files.EstadoTrabalhoFileID = db.EstadoTrabalhoFile.Where(u => u.Designacao.Equals("Aprovado", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().Id;

                    }
                    else
                    {
                        files.EstadoTrabalhoFileID = db.EstadoTrabalhoFile.Where(u => u.Designacao.Equals("Pendente", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().Id;

                    }
                }
                else
                {
                    files.CatFilesID = db.catFiles.Where(u => u.Designacao.Equals("Protocolo", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().Id;

                }
                if (ficheiro != null && ficheiro.ContentLength > 0)
                {
                    // extract only the filename
                     last_insert_id = files.Id + "Protocolo.pdf";
                    
                    var fileName = Path.GetFileName(ficheiro.FileName);
                    Debug.WriteLine(fileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine(Server.MapPath("~/uploads"), last_insert_id);
                    ficheiro.SaveAs(path);
                }
                db.TrabalhoFiles.Add(files);
                db.SaveChanges();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Gravacao do trabalho
                    if (Roles.IsUserInRole("Estudante"))
                    {
                        trabalho.ApplicationUserID = User.Identity.GetUserId();
                        trabalho.Grau_Academico = "Licenciatura";
                    }
                    db.Trabalho.Add(trabalho);
                    db.SaveChanges();

                    //associacao do documento
                   
                    files.TrabalhoID = trabalho.Id;
                    if (Roles.IsUserInRole("Estudante"))
                    {
                        files.CatFilesID = CatFilesID;
                        if (db.catFiles.Where(c => c.Id == CatFilesID).FirstOrDefault().Designacao == "Tese")
                        {
                            files.EstadoTrabalhoFileID = db.EstadoTrabalhoFile.Where(u => u.Designacao.Equals("Aprovado", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().Id;

                        }
                        else
                        {
                            files.EstadoTrabalhoFileID = db.EstadoTrabalhoFile.Where(u => u.Designacao.Equals("Pendente", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().Id;

                        }
                    }
                    else
                    {
                        files.CatFilesID = db.catFiles.Where(u => u.Designacao.Equals("Protocolo", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().Id;

                    }
                    if (ficheiro != null && ficheiro.ContentLength > 0)
                    {
                        // extract only the filename
                        last_insert_id = fileID.ToString() + "Protocolo.pdf";
                        files.Path = last_insert_id;
                        var fileName = Path.GetFileName(ficheiro.FileName);
                        Debug.WriteLine(fileName);
                        // store the file inside ~/App_Data/uploads folder
                        var path = Path.Combine(Server.MapPath("~/uploads"), files.Path);
                        ficheiro.SaveAs(path);
                    }
                    db.TrabalhoFiles.Add(files);

                    //Associacao do supervisor
                    docentes.DocenteID = DocentesID;
                    docentes.FuncaoID = db.Funcao.Where(u => u.Designacao.Equals("Supervisor", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().Id;
                    docentes.TrabalhoID = trabalho.Id;
                    db.DocenteAssociado.Add(docentes);
                    db.SaveChanges();
                    fileID = files.Id;

                    return RedirectToAction("dashboard", "trabalhofiles");
                }

            }


            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "username");
           
            return RedirectToAction("dashboard", "trabalhofiles");
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
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "username");

            return View(trabalho);
        }

        // POST: Trabalhoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descricao,Grau_Academico,ApplicationUserID,Data")] Trabalho trabalho)
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
