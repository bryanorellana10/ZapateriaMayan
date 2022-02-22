using CapaBaseDeDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZapateriaMayan.ViewModels;

namespace ZapateriaMayan.Controllers
{
    public class DashboardController : Controller
    {
        private DashboardRepository _dashboard = null;
        public DashboardController(DashboardRepository dashboard)
        {
            _dashboard = dashboard;
        }


        public ActionResult TodasGraficas()
        {

            return View(); 
        }

        public ActionResult Index()
        {
            var TotalVentasInteriorHoy = _dashboard.VentasHoyInterior();
            var viewmodel = new DashboardViewModel()
            {
                TotalVentasInterior = TotalVentasInteriorHoy
            };
            return View(viewmodel);
        }
    }
}