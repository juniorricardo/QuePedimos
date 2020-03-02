using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public PedidoBL pedidoBL = new PedidoBL();
        [Authorize]
        public ActionResult Index()
        {
            pedidoBL.VerificarPedidos();
            return View(pedidoBL.ListarPedidos());
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}