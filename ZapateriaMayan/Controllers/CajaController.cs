using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;
using CapaBaseDeDatos.Security;
using Microsoft.AspNet.Identity;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZapateriaMayan.ViewModels;

namespace ZapateriaMayan.Controllers
{
    public class CajaController : Controller
    {
        private readonly CajaCapitalRepository _cajaCapitalRepository = null;
        private readonly CajaInteriorRepository _cajaInteriorRepository = null;
        private readonly ApplicationUserManager _userManager;



        public CajaController(CajaCapitalRepository capitalRepository, CajaInteriorRepository cajaInteriorRepository, ApplicationUserManager applicationUserManager)
        {
            _cajaCapitalRepository = capitalRepository;
            _cajaInteriorRepository = cajaInteriorRepository;
            _userManager = applicationUserManager;

        }


        public ActionResult IndexCajaCapital()
        {
            if (ComprobarEstadoCajaCapital(out CajaCapital Caja))
            {
                ViewBag.Caja = Caja;
                ViewBag.EstadoCaja = false;
            }
            else
            {
                ViewBag.Caja = Caja;
                ViewBag.EstadoCaja = true;
            }

            var cajasCapitalList = _cajaCapitalRepository.GetList();


            var viewModel = new CajaCapitalViewModel()
            {
                CajaCapitalsList = cajasCapitalList
            };

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult IndexCajaCapital(CajaCapitalViewModel viewModel) 
        {

            if (ComprobarEstadoCajaCapital(out CajaCapital caja))
            {
                ModelState.AddModelError("ViewBag.Error", "ERROR, No se puede tener varias cajas abiertas. Cierre la caja que tiene abierta antes de abrir otra.");
            }


            if (ModelState.IsValid)
            {
                var abrirCaja = new CajaCapital()
                {
                    MontoApertura = viewModel.CajaCapital.MontoApertura,
                    FechaApertura = viewModel.CajaCapital.FechaApertura,
                    EstadoCaja = true,
                    Responsable = _userManager.FindById(User.Identity.GetUserId()).Name


                };

                var nuevoDetalle = new DetalleCajaCapital()
                {
                    CajaCapital = abrirCaja,
                    Descripcion = "Apertura de Caja",
                    Entrega = false,
                    Gasto = 0.00M,
                    Ingreso = abrirCaja.MontoApertura
                };


                _cajaCapitalRepository.Add(nuevoDetalle);
                TempData["Message"] = "¡La caja se ha abierto con éxito!";
                return RedirectToAction("IndexCajaCapital");
            }

            var cajasCapitalList = _cajaCapitalRepository.GetList();
            viewModel.CajaCapitalsList = cajasCapitalList;

            return View(viewModel);
        }



        public ActionResult VerDetalleCajaCapital(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var caja = _cajaCapitalRepository.Getcajacapital((int)id);

            if (caja == null)
            {
                return HttpNotFound();
            }

            var viewmodel = new CajaCapitalViewModel()
            {
                CajaCapital = caja
            };


            return View(viewmodel);
        }


        /// <summary>
        /// Comprueba si hay cajas abiertas.
        /// </summary>
        /// <returns></returns>
        //public bool ComprobarEstadoCajaCapital()
        //{
        //    var cajas = _cajaCapitalRepository.GetList();

        //    foreach (var item in cajas)
        //    {
        //        if (item.EstadoCaja == true) return true;
        //    }

        //    return false;
        //}

        public bool ComprobarEstadoCajaCapital(out CajaCapital caja)
        {
            var cajas = _cajaCapitalRepository.GetList();

            foreach (var item in cajas)
            {

                if (item.EstadoCaja == true)
                {
                    caja = item;
                    return true;
                }
            }
            caja = null;
            return false;
        }




        public bool ComprobarEstadoCajaInterior(out CajaInterior caja)
        {
            var cajas = _cajaInteriorRepository.GetList();

            foreach (var item in cajas)
            {

                if (item.EstadoCaja == true) {
                    caja = item;                
                return true;
                }
            }
            caja = null;
            return false;
        }


        public ActionResult IndexCajaInterior()
        {
            if (ComprobarEstadoCajaInterior(out CajaInterior Caja))
            {
                ViewBag.Caja = Caja;
                ViewBag.EstadoCaja = false;
            }
            else{
                ViewBag.Caja = Caja;
                ViewBag.EstadoCaja = true;
            }

            var cajasInteriorList = _cajaInteriorRepository.GetList();


            var viewModel = new CajaInteriorViewModel()
            {
                CajaInteriorsList = cajasInteriorList
            };

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult IndexCajaInterior(CajaInteriorViewModel viewModel)
        {

            if (ComprobarEstadoCajaInterior(out CajaInterior Caja))
            {
                ViewBag.Caja = Caja;
                ViewBag.EstadoCaja = false;
            }
            else
            {
                ViewBag.Caja = Caja;
                ViewBag.EstadoCaja = true;
            }


            if (ModelState.IsValid)
            {
                var abrirCaja = new CajaInterior()
                {
                    MontoApertura = viewModel.CajaInterior.MontoApertura,
                    FechaApertura = viewModel.CajaInterior.FechaApertura,
                    EstadoCaja = true

                };

                var nuevoDetalle = new DetalleCajaInterior()
                {
                    CajaInterior = abrirCaja,
                    Descripcion = "Apertura de Caja",
                    Entrega = false,
                    Gasto = 0.00M,
                    Ingreso = abrirCaja.MontoApertura
                };


                _cajaInteriorRepository.Add(nuevoDetalle);
                TempData["Message"] = "¡La caja se ha abierto con éxito!";
                return RedirectToAction("IndexCajaInterior");
            }

            var cajasInteriorList = _cajaInteriorRepository.GetList();
            viewModel.CajaInteriorsList = cajasInteriorList;

            return View(viewModel);
        }

        public ActionResult VerDetalleCajaInterior(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var caja = _cajaInteriorRepository.GetCajaInterior((int)id);

            if (caja == null)
            {
                return HttpNotFound();
            }

            var viewmodel = new CajaInteriorViewModel()
            {
                CajaInterior = caja
            };


            return View(viewmodel);
        }



        public ActionResult AgregarIngreso(decimal monto, string descripcion)
        {
            ComprobarEstadoCajaInterior(out CajaInterior Caja);
            DetalleCajaInterior detalleCaja = new DetalleCajaInterior();
            detalleCaja.CajaInterior = Caja;
            detalleCaja.Descripcion = descripcion;
            detalleCaja.Ingreso = monto;

            _cajaInteriorRepository.AddDetail(detalleCaja);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AgregarEgreso(decimal monto, string descripcion)
        {
            ComprobarEstadoCajaInterior(out CajaInterior Caja);
            DetalleCajaInterior detalleCaja = new DetalleCajaInterior();
            detalleCaja.CajaInterior = Caja;
            detalleCaja.Descripcion = descripcion;
            detalleCaja.Gasto = monto;

            _cajaInteriorRepository.AddDetail(detalleCaja);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CerrarCaja()
        {
            ComprobarEstadoCajaInterior(out CajaInterior Caja);
            Caja.EstadoCaja = false;
            
            _cajaInteriorRepository.ModCaja(Caja);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }


        //public ActionResult IngresoCajaCapital(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }



        //    var viewmodel = new CajaCapitalViewModel()
        //    {
        //        Id = (int)id
        //    };


        //    return View(viewmodel);
        //}


        //[HttpPost]
        //public ActionResult IngresoCajaCapital(CajaCapitalViewModel viewModel)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var nuevoDetalle = new DetalleCajaCapital()
        //        {
        //            CajaCapitalId = viewModel.Id,
        //            Descripcion = viewModel.DetalleCajaCapital.Descripcion,
        //            Gasto = 0.00M,
        //            Ingreso = viewModel.DetalleCajaCapital.Ingreso
        //        };

        //        _cajaCapitalRepository.Add(nuevoDetalle);
        //        return RedirectToAction("VerDetalleCajaCapital", new { viewModel.Id});
        //    }

        //        return View(viewModel);

        //}



        public ActionResult IngresoCajaCapital(decimal monto, string descripcion)
        {
            ComprobarEstadoCajaCapital(out CajaCapital Caja);

            DetalleCajaCapital detalleCaja = new DetalleCajaCapital{
                CajaCapital = Caja,
                Descripcion = descripcion,
                Ingreso = monto,
                Gasto = 0.00M,
                Responsable = _userManager.FindById(User.Identity.GetUserId()).Name

            };

            _cajaCapitalRepository.Add(detalleCaja);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult IngresoCajaCapitalEdit(decimal monto, string descripcion, int id)
        {
            ComprobarEstadoCajaCapital(out CajaCapital Caja);
            var detalle = _cajaCapitalRepository.GetcajacapitalDetalle(id);
            detalle.Ingreso = monto;
            detalle.Descripcion = descripcion;

            _cajaCapitalRepository.UpdateDetail(detalle);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult EgresoCajaCapitalEdit(decimal monto, string descripcion, int id)
        {
            ComprobarEstadoCajaCapital(out CajaCapital Caja);
            var detalle = _cajaCapitalRepository.GetcajacapitalDetalle(id);
            detalle.Gasto = monto;
            detalle.Descripcion = descripcion;

            _cajaCapitalRepository.UpdateDetail(detalle);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }



        public ActionResult EgresoCajaCapital(decimal monto, string descripcion)
        {
            ComprobarEstadoCajaCapital(out CajaCapital Caja);

            DetalleCajaCapital detalleCaja = new DetalleCajaCapital
            {
                CajaCapital = Caja,
                Descripcion = descripcion,
                Ingreso = 0.00M,
                Gasto = monto,
                Responsable = _userManager.FindById(User.Identity.GetUserId()).Name
            };

            _cajaCapitalRepository.Add(detalleCaja);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CerrarCajaCapital()
        {

            ComprobarEstadoCajaCapital(out CajaCapital Caja);

            Caja.EstadoCaja = false;

            _cajaCapitalRepository.Update(Caja);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }


        public ActionResult EliminarDetalleCapital(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            //var detalleCajaCapital = _cajaCapitalRepository.GetcajacapitalDetalle((int)id);

            _cajaCapitalRepository.DeleteDetalleCajaCapital((int)id);

            return Json("Se ha eliminado el elemento de la caja.", JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult ReporteCaja(string fechaInicio, string fechaFin)
        {
            DateTime.TryParseExact(fechaInicio.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var Inicio);
            DateTime.TryParseExact(fechaFin.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var Fin);


            return new ActionAsPdf("Reporte", new { Inicio, Fin });

            
        }
        // vemos que pedo

        [AllowAnonymous]
        public ActionResult Reporte(DateTime Inicio, DateTime Fin)
        {
            var cajasCapitalList = _cajaCapitalRepository.GetList().Where(f => f.FechaApertura >= Inicio && f.FechaApertura <= Fin.AddDays(1)).ToList();

            var viewModel = new CajaCapitalViewModel()
            {
                CajaCapitalsList = cajasCapitalList
            };

            return View(viewModel);
        }
        public ActionResult ReporteCajaDetail(int id)
        {
            
            return new ActionAsPdf("ReporteDetail", new { id });


        }

        [AllowAnonymous]
        public ActionResult ReporteDetail(int id)
        {
            var cajasCapitalList = _cajaCapitalRepository.Getcajacapital(id);

            var viewModel = new CajaCapitalViewModel()
            {
                CajaCapital = cajasCapitalList
            };

            return View(viewModel);
        }

    }
}