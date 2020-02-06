using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BE;
using DAL;

namespace UI.Controllers
{
    public class NuevoEquipoController : Controller
    {
        private QuePedimosContext db = new QuePedimosContext();

        // GET: NuevoEquipo
        public ActionResult Index()
        {
            var equipo = db.Equipo.Select(x => new
                                                {   Id = x.Id,
                                                    Lider = x.Lider,
                                                    Integrantes = x.Integrantes,
                                                    FechaCreado = x.FechaCreado,
                                                    FechaUltimaModificacion = x.FechaUltimaModificacion }).ToList()
                                  .Select(x => new Equipo()
                                                {   Id = x.Id,
                                                    Lider = x.Lider,
                                                    FechaCreado = x.FechaCreado,
                                                    FechaUltimaModificacion = x.FechaUltimaModificacion,
                                                    Integrantes = x.Integrantes }).ToList();
            return View(equipo);
        }

        // GET: NuevoEquipo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipo.Include("Integrantes")
                                     .FirstOrDefault(e => e.Id == id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // GET: NuevoEquipo/Create
        public ActionResult Create()
        {
            ViewBag.istaUsuarios = db.Usuario.ToList();
            return View();
        }

        // POST: NuevoEquipo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaCreado,FechaUltimaModificacion")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Equipo.Add(equipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipo);
        }

        // GET: NuevoEquipo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // POST: NuevoEquipo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaCreado,FechaUltimaModificacion")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipo);
        }

        // GET: NuevoEquipo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // POST: NuevoEquipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipo equipo = db.Equipo.Find(id);
            db.Equipo.Remove(equipo);
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
