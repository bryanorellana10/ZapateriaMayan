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
    public class RutasController : Controller
    {

        private RutasRepository _rutasRepository = null;

        public RutasController(RutasRepository rutasRepository)
        {

            _rutasRepository = rutasRepository;
        }

        //public ActionResult TodasRutas()
        //{
        //    //var view = _rutasRepository.Geta();
        //    //return View(view);
        //}

        //public ActionResult CrearRuta()
        //{
        //    var viewModel = new RutaViewModel();
        //    return View(viewModel);
        //}

        ////[HttpPost]
        //public ActionResult CrearRuta(RutaViewModel viewModel)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    var Ruta = viewModel.RutaDespacho;
        //    //    var Despacho = viewModel.despachador;
        //    //    _rutasRepository.DespachadorSinGuardar(Despacho);
        //    //    _rutasRepository.AddSinGuardarRuta(Ruta);
        //    //    _rutasRepository.RutaGuardar();
        //    //    TempData["Message"] = "¡La Ruta  se ha Creado con éxito!";
        //    //    return RedirectToAction("TodasRutas");
        //    //}
        //    return View();
        //}

        //public ActionResult ModificarRuta(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var ruta = _rutasRepository.GetTa((int)id, incluideRelatedEntites: false);

        //    if (ruta == null)
        //    {
        //        return HttpNotFound();

        //    }
        //    var viewModel = new RutaEditViewModel()
        //    {
        //        Ruta = ruta

        //    };


        //    return View(viewModel);

        //}

        //[HttpPost]
        //public ActionResult ModificarRuta(RutaEditViewModel viewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var ruta = viewModel.RutaDespacho;
        //        _rutasRepository.ModificarRuta2(ruta);
        //        TempData["Message"] = "¡La Ruta se ha MODIFICADO con éxito!";
        //        return RedirectToAction("Productos", "Inventario");

        //    }

        //    return View(viewModel);
        //}



        //public ActionResult EliminarRuta(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var Ruta = _rutasRepository.GetTa((int)id, incluideRelatedEntites: false);

        //    if (Ruta == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var viewModel = new RutaEditViewModel()
        //    {
        //        Ruta = Ruta
        //    };

        //    return View(viewModel);
        //}

        //public ActionResult EliminarRut(int? id)
        //{
        //    var ruta = _rutasRepository.GetTa((int)id);
        //    ruta.EstadoRuta = true;
        //    _rutasRepository.ModificarRuta2(ruta);
        //    TempData["Message"] = "¡La Ruta se ha ELIMINADO con éxito!";
        //    return RedirectToAction("Productos", "Inventario");
        //}




    }
}