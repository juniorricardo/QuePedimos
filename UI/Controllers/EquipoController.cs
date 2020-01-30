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
using DAL;

namespace UI.Controllers
{
    public class EquipoController : Controller
    {
        private QuePedimosContext db = new QuePedimosContext();
        private EquipoBL equipoBL = new EquipoBL();
        private UsuarioBL usuarioBL = new UsuarioBL();

        // GET: NuevoEquipo
        public ActionResult Index()
        {
            // De esta manera se muesta error de Disposed
            // The ObjectContext instance has been disposed and can no longer be used for operations that require a connection.'
            // en el model para listar integrantes de un equipo, no tiene acceso a la lista de los integrantes  
            var listaEquipos = equipoBL.ListaEquipos();
            return View(db.Equipo.ToList());
        }

        // GET: NuevoEquipo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = equipoBL.BuscarEquipoPorId(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.listaIntegrantes = equipoBL.ListarIntegranteEquipo((int)id);
            return View(equipo);
        }

        // GET: NuevoEquipo/Create
        public ActionResult Create()
        {
            ViewBag.listaUsuarios = usuarioBL.ListaUsuarios();
            return View();
        }

        // POST: NuevoEquipo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Empleados")]int[] Empleados)
        {
            if (ModelState.IsValid)
            {
                equipoBL.AgregarEquipo(Empleados);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: NuevoEquipo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = equipoBL.BuscarEquipoPorId(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.listaUsuarios = equipoBL.ListarIntegranteEquipo((int)id).ToList();
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
                equipoBL.ActualizarEquipo(equipo);
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
            Equipo equipo = equipoBL.BuscarEquipoPorId(id);
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
            equipoBL.EliminarEquipo(id);
            return RedirectToAction("Index");
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
