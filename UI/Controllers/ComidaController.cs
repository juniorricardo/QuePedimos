using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BE;
using BL;

namespace UI.Controllers
{
    public class ComidaController : Controller
    {
        private ComidaBL comidaBL = new ComidaBL();

        // GET: Comida
        public async Task<ActionResult> Index()
        {
            return View(await comidaBL.ListaComidas());
        }

        // GET: Comida/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comida comida = await comidaBL.BuscarComidaPorId(id);
            if (comida == null)
            {
                return HttpNotFound();
            }
            return View(comida);
        }

        // GET: Comida/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comida/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombre")] Comida comida)
        {
            if (ModelState.IsValid)
            {
                comidaBL.AgregarComida(comida);
                return RedirectToAction("Index");
            }

            return View(comida);
        }

        // GET: Comida/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comida comida = await comidaBL.BuscarComidaPorId(id);
            if (comida == null)
            {
                return HttpNotFound();
            }
            return View(comida);
        }

        // POST: Comida/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre")] Comida comida)
        {
            if (ModelState.IsValid)
            {
                comidaBL.ActualizarComida(comida);
                return RedirectToAction("Index");
            }
            return View(comida);
        }

        // GET: Comida/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comida comida = await comidaBL.BuscarComidaPorId(id);
            if (comida == null)
            {
                return HttpNotFound();
            }
            return View(comida);
        }

        // POST: Comida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Comida comida = await comidaBL.BuscarComidaPorId(id);
            comidaBL.EliminarComida(comida);
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
