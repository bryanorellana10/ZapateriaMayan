using CapaBaseDeDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZapateriaMayan.Controllers
{
    public class VentasController : Controller
    {
        private VentasRepository _ventasRepository = null;

        public VentasController(VentasRepository ventasRepository)
        {
            _ventasRepository = ventasRepository;
        }

        public ActionResult Interior()
        {
            var ventasInterior = _ventasRepository.GetList();
            return View(ventasInterior);
        }

        public ActionResult Capital()
        {
            var ventasCapitals = _ventasRepository.GetListC();
            return View(ventasCapitals);
        }
    }
}