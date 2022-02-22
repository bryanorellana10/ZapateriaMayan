using CapaBaseDeDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZapateriaMayan.ViewModels;

namespace ZapateriaMayan.Controllers
{
    public class MensajeroYRutasController : Controller
    {
        private readonly MensajeroRepository _mensajeroRepository = null;
        public MensajeroYRutasController(MensajeroRepository mensajeroRepository)
        {
            _mensajeroRepository = mensajeroRepository;
        }

        // todos los mensajeros
        public ActionResult TodosLosMensajeros()
        {
            var mensajeros = _mensajeroRepository.GetList();
            return View(mensajeros);
        }

        public ActionResult TodasLasRutas()
        {
            var rutas = _mensajeroRepository.GetListRutas();
            return View(rutas);
        }


        public ActionResult NuevoMensajero()
        {

            return View();
        }

        public ActionResult NuevaRuta()
        {

            return View();
        }


        [HttpPost]
        public ActionResult NuevaRuta(MensajerosYRutasRutasViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var ruta = viewModel.Ruta;
                _mensajeroRepository.Add(ruta);
                TempData["Message"] = "¡La ruta se ha INSERTADO con éxito!";
                return RedirectToAction("TodasLasRutas");
            }
            return View(viewModel);
        }



        [HttpPost]
        public ActionResult NuevoMensajero(MensajeroYRutasMensajeroViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var mensajero = viewModel.Despachador;
                _mensajeroRepository.Add(mensajero);
                TempData["Message"] = "¡El mensajero se ha INSERTADO con éxito!";
                return RedirectToAction("TodosLosMensajeros");
            }
            return View(viewModel);
        }

        public ActionResult EditarMensajero(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var mensajero = _mensajeroRepository.Get((int)id);

            if (mensajero == null)
            {
                return HttpNotFound();
            }

            //necesito capturar los  datos y almacenarlos en viewmodel
            var viewModel = new MensajeroYRutasMensajeroViewModel()
            {
                Despachador = mensajero

            };

            return View(viewModel);
        }

        public ActionResult EditarRuta(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var ruta = _mensajeroRepository.GetRuta((int)id);

            if (ruta == null)
            {
                return HttpNotFound();
            }

            //necesito capturar los  datos y almacenarlos en viewmodel
            var viewModel = new MensajerosYRutasRutasViewModel()
            {
                Ruta = ruta

            };

            return View(viewModel);
        }



        [HttpPost]
        public ActionResult EditarMensajero(MensajeroYRutasMensajeroViewModel model)
        {
           if(ModelState.IsValid)
           {
                var mensajero = model.Despachador;
                _mensajeroRepository.Modify(mensajero);
                TempData["Message"] = "¡El Producto se ha MODIFICADO con éxito!";
                return RedirectToAction("TodosLosMensajeros");

            }

            return View(model);
        }


        [HttpPost]
        public ActionResult EditarRuta(MensajerosYRutasRutasViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ruta = model.Ruta;
                _mensajeroRepository.ModifyRuta(ruta);
                TempData["Message"] = "¡La Ruta se ha MODIFICADO con éxito!";
                return RedirectToAction("TodasLasRutas");

            }

            return View(model);
        }

        public ActionResult EliminarMensajero(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var mensajero = _mensajeroRepository.Get((int)id);
            mensajero.Estado = true; // desactivado == true

            _mensajeroRepository.Modify(mensajero);

            TempData["Message"] = "¡El Producto se ha desactivado con éxito!";

            return RedirectToAction("TodosLosMensajeros");
        }

        public ActionResult EliminarRuta(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var ruta = _mensajeroRepository.GetRuta((int)id);
            ruta.Inactivo = true; // desactivado == true

            _mensajeroRepository.ModifyRuta(ruta);

            TempData["Message"] = "¡La Ruta se ha desactivado con éxito!";

            return RedirectToAction("TodasLasRutas");
        }

    }
}