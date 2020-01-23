using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BE;
using BL;

namespace UI.Controllers
{
    public class EquipoController : Controller
    {
        private EquipoBL dbEquipo = new EquipoBL();
        private UsuarioBL dbUsuario = new UsuarioBL();

        // GET: Equipo
        public ActionResult Index()
        {
            ObtenerListaUsuarios();
            return View(dbEquipo.ListaEquipos());
        }

        // GET: Equipo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = dbEquipo.BuscarEquipoPorId(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // GET: Equipo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaCreado,FechaUltimaModificacion")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                dbEquipo.AgregarEquipo(equipo);
                return RedirectToAction("Index");
            }

            return View(equipo);
        }

        // GET: Equipo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = dbEquipo.BuscarEquipoPorId(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // POST: Equipo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaCreado,FechaUltimaModificacion")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                dbEquipo.ActualizarUsuario(equipo);
                return RedirectToAction("Index");
            }
            return View(equipo);
        }

        // GET: Equipo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = dbEquipo.BuscarEquipoPorId(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // POST: Equipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipo equipo = dbEquipo.BuscarEquipoPorId(id);
            dbEquipo.EliminarEquipo(equipo);
            return RedirectToAction("Index");
        }

        public void ObtenerListaUsuarios()
        {
            var usuario = dbUsuario.ListaUsuarios().Find(u => u.Nombre == "Elias");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}
