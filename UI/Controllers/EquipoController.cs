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
using DAL;

namespace UI.Controllers
{
    public class EquipoController : Controller
    {
        private EquipoBL equipoBL = new EquipoBL();
        private UsuarioBL usuarioBL = new UsuarioBL();
        private PedidoBL pedidoBL = new PedidoBL();
        private ComidaBL comidaBL = new ComidaBL();

        // GET: NuevoEquipo
        public async Task<ActionResult> Index()
        {
            /*
             Generar registro en 'Pedido' 
             */

            ViewBag.Pedidos = pedidoBL.ListarPedidos();
            return View(await equipoBL.ListaEquipos());
        }

        // GET: NuevoEquipo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = await equipoBL.BuscarEquipoPorId(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // GET: NuevoEquipo/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.ListaComidas = await comidaBL.ListaComidas();
            return View(await usuarioBL.ListaUsuarios());
        }

        // POST: NuevoEquipo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Empleados,Comidas")]int[] Empleados, int[] Comidas)
        {
            if (ModelState.IsValid)
            {
                equipoBL.AgregarEquipo(Empleados, Comidas);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: NuevoEquipo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = await equipoBL.BuscarEquipoPorId(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Usuarios = await usuarioBL.ListaUsuarios();

            #region Editar-Doble Lista
            /*  Doble lista
                var integrantes = equipoBL.ListarIntegranteEquipo((int)id);
                var tablaUsuario = usuarioBL.ListaUsuarios();
                var noIntegrantes = (from noInte in tablaUsuario
                                     where !(from b in integrantes
                                             select b.Id)
                                             .Contains(noInte.Id)
                                     select noInte).ToList();
                ViewBag.noIntegrantes = noIntegrantes;
                ViewBag.listaIntegrantes = integrantes;
                 */
            #endregion

            return View(equipo);
        }

        // POST: NuevoEquipo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, int[] Empleados)
        {
            if (ModelState.IsValid)
            {
                equipoBL.ActualizarEquipo(Id, Empleados);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: NuevoEquipo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = await equipoBL.BuscarEquipoPorId(id);
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
