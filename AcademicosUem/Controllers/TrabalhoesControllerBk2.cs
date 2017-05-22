//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using AcademicosUem.Models;
//using System.IO;
//using System.Diagnostics;

//namespace AcademicosUem.Controllers
//{
//    public class TrabalhoesController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: Trabalhoes
//        public ActionResult Index()
//        {
//            var trabalho = db.Trabalho.Include(t => t.Estudante);
//            return View(trabalho.ToList());
//        }

//        // GET: Trabalhoes
//        public ActionResult Dashboard()
//        {
//            var trabalho = db.Trabalho.Include(t => t.Estudante);
//            return View("dashboard");
//        }

//        // GET: Trabalhoes/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Trabalho trabalho = db.Trabalho.Find(id);
//            if (trabalho == null)
//            {
//                return HttpNotFound();
//            }
//            return View(trabalho);
//        }

//        // GET: Trabalhoes/Create
//        public ActionResult Create()
//        {
//            ViewBag.EstudanteID = new SelectList(db.Estudante, "Id", "apelido");
//            return View();
//        }

//        // POST: Trabalhoes/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,Titulo,Descricao,Grau_Academico,EstudanteID")] Trabalho trabalho, HttpPostedFileBase ficheiro)
//        {
//            TrabalhoFiles files = new TrabalhoFiles();
//            if (ModelState.IsValid)
//            {
//                db.Trabalho.Add(trabalho);
//                files.Path = trabalho.Id.ToString() + ".pdf";
//                files.TrabalhoID = trabalho.Id;
//                files.CatFilesID = db.catFiles.Where(u => u.Designacao.Equals("Protocolo", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().Id;
//                db.TrabalhoFiles.Add(files);
//                db.SaveChanges();
//                var last_insert_id = trabalho.Id.ToString() + ".pdf";
//                if (ficheiro != null && ficheiro.ContentLength > 0)
//                {
//                    // extract only the filename
//                    var fileName = Path.GetFileName(ficheiro.FileName);
//                    Debug.WriteLine(fileName);
//                    // store the file inside ~/App_Data/uploads folder
//                    var path = Path.Combine(Server.MapPath("~/uploads"), last_insert_id);
//                    ficheiro.SaveAs(path);
//                }
//                return RedirectToAction("Index");
//            }

//            ViewBag.EstudanteID = new SelectList(db.Estudante, "Id", "apelido", trabalho.EstudanteID);
//            return View(trabalho);
//        }

//        // GET: Trabalhoes/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Trabalho trabalho = db.Trabalho.Find(id);
//            if (trabalho == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.EstudanteID = new SelectList(db.Estudante, "Id", "apelido", trabalho.EstudanteID);
//            return View(trabalho);
//        }

//        // POST: Trabalhoes/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,Titulo,Descricao,Grau_Academico,EstudanteID,Data")] Trabalho trabalho)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(trabalho).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.EstudanteID = new SelectList(db.Estudante, "Id", "apelido", trabalho.EstudanteID);
//            return View(trabalho);
//        }

//        // GET: Trabalhoes/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Trabalho trabalho = db.Trabalho.Find(id);
//            if (trabalho == null)
//            {
//                return HttpNotFound();
//            }
//            return View(trabalho);
//        }

//        // POST: Trabalhoes/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Trabalho trabalho = db.Trabalho.Find(id);
//            db.Trabalho.Remove(trabalho);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
