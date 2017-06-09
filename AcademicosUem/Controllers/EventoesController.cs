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
    public class EventoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Eventoes
        public ActionResult Index()
        {
            var evento = db.Evento.Where(e=>e.IsPublished==true).Include(e => e.EventoCategoria).Include(e => e.Trabalho);
            return View(evento.ToList());
        }

        // GET: Eventoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: Eventoes/Create
        public ActionResult Create()
        {
            ViewBag.EventoCategoriaID = new SelectList(db.EventoCategoria, "Id", "descricao");
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo");
            return View();
        }

        // POST: Eventoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(Evento evento)
        {
            //if (ModelState.IsValid)
            //{
            EstadosDoTrabalho estadoTrabalho = new EstadosDoTrabalho();


            evento.publishDate = DateTime.Today;
            evento.StartDateTime = DateTime.Parse(evento.StartDateTime.ToString());
            evento.EndDateTime = DateTime.Parse(evento.StartDateTime.ToString());
            evento.EventoCategoriaID = db.EventoCategoria.Where(e => e.descricao == "Defesa").FirstOrDefault().Id;
                evento.IsPublic = true;
            evento.IsPublished = true;

                db.Evento.Add(evento);
                db.SaveChanges();

            estadoTrabalho.TrabalhoID = evento.TrabalhoID;
            estadoTrabalho.EstadoTrabalhoID = db.EstadoTrabalho.Where(e => e.Designacao.Equals("Defesa Marcada", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().Id;
            estadoTrabalho.isActual = true;

            int estadojobcount = db.EstadosDoTrabalho.Where(e => e.TrabalhoID == evento.TrabalhoID && e.isActual == true).Count();
            for (int i = 0; i < estadojobcount; i++)
            {
                EstadosDoTrabalho oldActual = db.EstadosDoTrabalho.Where(o => o.TrabalhoID == evento.TrabalhoID && o.isActual == true).FirstOrDefault();
                oldActual.isActual = false;
                db.Entry(oldActual).State = EntityState.Modified;
                db.SaveChanges();
            }
            db.EstadosDoTrabalho.Add(estadoTrabalho);
            db.SaveChanges();
            return Json(evento);
            //}

            ViewBag.EventoCategoriaID = new SelectList(db.EventoCategoria, "Id", "descricao", evento.EventoCategoriaID);
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo", evento.TrabalhoID);
            return Json(evento);

        }

        public JsonResult EfectivarDefesa(int TrabID)
        {
            EstadosDoTrabalho estadoTrabalho = new EstadosDoTrabalho();

            estadoTrabalho.TrabalhoID = TrabID;
            estadoTrabalho.EstadoTrabalhoID = db.EstadoTrabalho.Where(e => e.Designacao.Equals("Publicado", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().Id;
            estadoTrabalho.isActual = true;

            int estadojobcount = db.EstadosDoTrabalho.Where(e => e.TrabalhoID == TrabID && e.isActual == true).Count();
            for (int i = 0; i < estadojobcount; i++)
            {
                EstadosDoTrabalho oldActual = db.EstadosDoTrabalho.Where(o => o.TrabalhoID == TrabID && o.isActual == true).FirstOrDefault();
                oldActual.isActual = false;
                db.Entry(oldActual).State = EntityState.Modified;
                db.SaveChanges();
            }
            int oldeventocount = db.Evento.Where(e => e.IsPublished == true && e.TrabalhoID == TrabID).Count();
            for (int i = 0; i < oldeventocount; i++)
            {
                Evento oldEvento = db.Evento.Where(e => e.IsPublished == true && e.TrabalhoID == TrabID).FirstOrDefault();
                oldEvento.IsPublished = false;
                db.Entry(oldEvento).State = EntityState.Modified;
                db.SaveChanges();
            }
            db.EstadosDoTrabalho.Add(estadoTrabalho);
            db.SaveChanges();
            return Json("Defesa Efectivada");
        }
        // GET: Eventoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventoCategoriaID = new SelectList(db.EventoCategoria, "Id", "descricao", evento.EventoCategoriaID);
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo", evento.TrabalhoID);
            return View(evento);
        }

        // POST: Eventoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDateTime,EndDateTime,Local,Agenda,Telefone,Email,Website,IsPublished,IsPublic,publishDate,TrabalhoID,EventoCategoriaID")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventoCategoriaID = new SelectList(db.EventoCategoria, "Id", "descricao", evento.EventoCategoriaID);
            ViewBag.TrabalhoID = new SelectList(db.Trabalho, "Id", "Titulo", evento.TrabalhoID);
            return View(evento);
        }

        // GET: Eventoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Eventoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = db.Evento.Find(id);
            db.Evento.Remove(evento);
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
