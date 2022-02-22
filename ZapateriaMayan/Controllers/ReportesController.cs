using CapaBaseDeDatos.Data;
using Microsoft.Ajax.Utilities;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZapateriaMayan.ViewModels;

namespace ZapateriaMayan.Controllers
{
    public class ReportesController : Controller
    {
        private readonly VentasRepository _ventasRepository = null;

        public ReportesController(VentasRepository ventasRepository)
        {
            _ventasRepository = ventasRepository;
        }
        
        // Reportes ventas
        public ActionResult VentasCapital()
        {
            return View();
        }


        [HttpPost]
        public ActionResult VentasCapital(string date, bool CheckBoxCapital, bool CheckBoxInterior)
        {
            var dates = date.Split('-');
            
            if (dates == null) return RedirectToAction("Ventas");

            if (CheckBoxCapital == false || CheckBoxInterior == false) return RedirectToAction("Ventas");

            return new ActionAsPdf("ReportesVentas", new { Inicio = dates[0], Final = dates[0], Checkboxcapital = CheckBoxCapital, Checkboxinterior = CheckBoxInterior });

        }

        public ActionResult ReportesVentas(DateTime Inicio, DateTime Final, bool Checkboxcapital, bool Checkboxinterior)
        {
            if(Checkboxcapital && Checkboxinterior)
            {
                var listadoVentasCapital = _ventasRepository.GetListCapitalRangos(Inicio, Final.AddDays(1));
                var listadoVentasInterior = _ventasRepository.GetListInteriorRangos(Inicio, Final.AddDays(1));

                var viewmodel = new ReportesVentasViewModel()
                {
                    ventasCapitals = listadoVentasCapital,
                    ventasInteriors = listadoVentasInterior,
                };

                return View(viewmodel);
    
            }
            else if(!Checkboxcapital && Checkboxinterior)
            {
                var listadoVentasCapital = _ventasRepository.GetListCapitalRangos(Inicio, Final.AddDays(1));

                var viewmodel = new ReportesVentasViewModel()
                {
                    ventasCapitals = listadoVentasCapital,
                };

                return View(viewmodel);

            }
            return View();


        }
    }
}