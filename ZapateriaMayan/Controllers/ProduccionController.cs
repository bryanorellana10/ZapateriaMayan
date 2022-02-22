using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZapateriaMayan.ViewModels;

namespace ZapateriaMayan.Controllers
{
    public class ProduccionController : Controller
    {
        private SolicitudesRepository _solicitudesRepository = null;
        private ProductosRepository _productosRepository = null;


        public ProduccionController(SolicitudesRepository solicitudesRepository, ProductosRepository productosRepository)
        {
            _solicitudesRepository = solicitudesRepository;
            _productosRepository = productosRepository;
        }

        public ActionResult Solicitudes()
        {
            var solicitudes = _solicitudesRepository.ListaSolicitudes(); 
            return View(solicitudes);
        }

        public ActionResult DetalleSolicitud(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var solicitudes = _solicitudesRepository.GetSolicitudes((int)id);

            if (solicitudes == null)
            {
                return HttpNotFound();
            }

            var viewmodel = new SolicitudesViewModel()
            {
                Solicitudes = solicitudes
            };

            viewmodel.Init(_solicitudesRepository);

            return View(viewmodel);
        }

        public ActionResult NuevaSolicitud()
        {

            return View();
        }

        public ActionResult Todos()
        {
            return View();
        }

        public ActionResult EnProduccion()
        {
            return View();
        }

        public ActionResult Finalizados()
        {
            return View();
        }

        public ActionResult EnEspera()
        {

            return View();
        }

        public ActionResult Alistados()
        {
            var alistados = _solicitudesRepository.Alistado();
            var alistadosNoAceptados = _solicitudesRepository.AlistadosPrioridad();

            var viewmodel = new SolicitudesAlistado()
            {
                DetalleSolicitud = alistados,
                DetalleSolicituds = alistadosNoAceptados

            };

            viewmodel.Init(_solicitudesRepository);


            return View(viewmodel);
        }

        public ActionResult Ensuelados()
        {
            var ensuelados = _solicitudesRepository.Ensuelado();
            var ensueladosNoAceptados = _solicitudesRepository.EnsueladosPrioridad();



            var viewmodel = new SolicitudesEnsuelado()
            {
                DetalleSolicitud = ensuelados,
                DetalleSolicituds = ensueladosNoAceptados
            };

            viewmodel.Init(_solicitudesRepository);


            return View(viewmodel);
        }

        public ActionResult EnRevision()
        {
            var enrevision = _solicitudesRepository.EnRevision();

            var viewmodel = new SolicitudesEnRevision()
            {
                DetalleSolicitud = enrevision
            };

            viewmodel.Init(_solicitudesRepository);

            return View(viewmodel);
        }

        public ActionResult Aceptados()
        {
            var enBodega = _solicitudesRepository.EnBodega();
            return View(enBodega);
        }


        public ActionResult ConfirmarSolicitud(int? id)
        {
            var solicitud = _solicitudesRepository.GetSolicitudes((int)id);
            solicitud.EstadosProductoId = 5; //5 confirmados


            _solicitudesRepository.ModicarSolicitud(solicitud);

            return RedirectToAction("DetalleSolicitud", new { id});
        }

        public ActionResult EnProduccio(int? id)
        {
            var solicitud = _solicitudesRepository.GetSolicitudes((int)id);
            solicitud.EstadosProductoId = 2; //2 En produccion


            _solicitudesRepository.ModicarSolicitud(solicitud);

            return RedirectToAction("DetalleSolicitud", new { id });
        }



        public ActionResult ConProblema(int? id)
        {
            var solicitud = _solicitudesRepository.GetSolicitudes((int)id);
            solicitud.EstadosProductoId = 6; //6 Con problema


            _solicitudesRepository.ModicarSolicitud(solicitud);

            return RedirectToAction("DetalleSolicitud", new { id });
        }


        public ActionResult Demorado(int? id)
        {
            var solicitud = _solicitudesRepository.GetSolicitudes((int)id);
            solicitud.EstadosProductoId = 7; //7 Demorado


            _solicitudesRepository.ModicarSolicitud(solicitud);

            return RedirectToAction("DetalleSolicitud", new { id });
        }

        public ActionResult Finalizado(int? id)
        {
            var solicitud = _solicitudesRepository.GetSolicitudes((int)id);
            solicitud.EstadosProductoId = 4; //12 Finalizado


            _solicitudesRepository.ModicarSolicitud(solicitud);

            return RedirectToAction("DetalleSolicitud", new { id });
        }



        // traslado para ensuelado
        public JsonResult CambiarAreas(List<int> Ids)
        {

            if (Ids == null)
            {
                return Json("Error. Lista vacía.");
            }
            

            foreach (var item in Ids)
            {
                var Area = _solicitudesRepository.GetDetalleSolicitud(item);
                Area.ProduccionId = 2; // 2 ensuelado
                
                // regresar los atrasos por defecto a false del area anterior ( en este caso alistados)
                Area.FaltaDeTextil = false;
                Area.FaltaDeHilo = false;
                Area.FaltaDeForro = false;
                Area.OtrosAlistados = false;

                Area.FaltaDeSuela = false;
                Area.FaltaDePegamento = false;
                Area.CorteEnMalEstado = false;
                Area.OtrosEnsuelados = false;

                _solicitudesRepository.ModificarDetalle(Area);
            }

            _solicitudesRepository.Guardar();


            return Json("Se ha trasladado con éxito.", JsonRequestBehavior.AllowGet);
        }

        // traslado para Alistado
        public JsonResult CambiarAreasAAlistado(List<int> Ids)
        {

            if (Ids == null)
            {
                return Json("Error. Lista vacía.");
            }


            foreach (var item in Ids)
            {
                var Area = _solicitudesRepository.GetDetalleSolicitud(item);
                Area.ProduccionId = 1; // 1 alistado

                // checkbocks por defecto
                Area.FaltaDeTextil = false;
                Area.FaltaDeHilo = false;
                Area.FaltaDeForro = false;
                Area.OtrosAlistados = false;


                Area.FaltaDeSuela = false;
                Area.FaltaDePegamento = false;
                Area.CorteEnMalEstado = false;
                Area.OtrosEnsuelados = false;


                _solicitudesRepository.ModificarDetalle(Area);
            }

            _solicitudesRepository.Guardar();


            return Json("Se ha trasladado con éxito.", JsonRequestBehavior.AllowGet);
        }


        // traslado Para revision
        public JsonResult CambiarAreasARevision(List<int> Ids)
        {

            if (Ids == null)
            {
                return Json("Error. Lista vacía.");
            }


            foreach (var item in Ids)
            {
                var Area = _solicitudesRepository.GetDetalleSolicitud(item);
                Area.ProduccionId = 3; //3 en revision

                Area.FaltaDeTextil = false;
                Area.FaltaDeHilo = false;
                Area.FaltaDeForro = false;
                Area.OtrosAlistados = false;


                Area.FaltaDeSuela = false;
                Area.FaltaDePegamento = false;
                Area.CorteEnMalEstado = false;
                Area.OtrosEnsuelados = false;


                _solicitudesRepository.ModificarDetalle(Area);
            }

            _solicitudesRepository.Guardar();


            return Json("Se ha trasladado con éxito.", JsonRequestBehavior.AllowGet);
        }



        // traslado Para revision, pero cambiar la prioridad mientras esta en ensuelado no aceptado
        public JsonResult CambiarAreasARevisionPrioridad(List<int> Ids)
        {

            if (Ids == null)
            {
                return Json("Error. Lista vacía.");
            }


            foreach (var item in Ids)
            {
                var Area = _solicitudesRepository.GetDetalleSolicitud(item);
                Area.ProduccionId = 3; //3 en revision
                Area.Prioridad = false;


                Area.FaltaDeTextil = false;
                Area.FaltaDeHilo = false;
                Area.FaltaDeForro = false;
                Area.OtrosAlistados = false;


                Area.FaltaDeSuela = false;
                Area.FaltaDePegamento = false;
                Area.CorteEnMalEstado = false;
                Area.OtrosEnsuelados = false;



                _solicitudesRepository.ModificarDetalle(Area);
            }

            _solicitudesRepository.Guardar();


            return Json("Se ha trasladado con éxito.", JsonRequestBehavior.AllowGet);
        }

        // No aceptados en Revision, rechazar y mandar a Alistados
        // la unica diferencia esque cambia la prioridad
        public ActionResult RechazarYTrasladarAAlistados(List<int> Ids)
        {

            if (Ids == null)
            {
                return Json("Error. Lista vacía.");
            }


            foreach (var item in Ids)
            {
                var Area = _solicitudesRepository.GetDetalleSolicitud(item);
                Area.ProduccionId = 1; // 1 Alistados 
                Area.Prioridad = true;

                Area.FaltaDeTextil = false;
                Area.FaltaDeHilo = false;
                Area.FaltaDeForro = false;
                Area.OtrosAlistados = false;


                Area.FaltaDeSuela = false;
                Area.FaltaDePegamento = false;
                Area.CorteEnMalEstado = false;
                Area.OtrosEnsuelados = false;



                _solicitudesRepository.ModificarDetalle(Area);
            }

            _solicitudesRepository.Guardar();


            return Json("Se ha trasladado con éxito.", JsonRequestBehavior.AllowGet);
        }

        //No aceptados en Revision, rechazar y mandar a Ensuelados
        // la unica diferencia esque cambia la prioridad
        public ActionResult RechazarYTrasladarAEnsuelados(List<int> Ids)
        {

            if (Ids == null)
            {
                return Json("Error. Lista vacía.");
            }


            foreach (var item in Ids)
            {
                var Area = _solicitudesRepository.GetDetalleSolicitud(item);
                Area.ProduccionId = 2; // 2 Ensuelados 
                Area.Prioridad = true;


                Area.FaltaDeTextil = false;
                Area.FaltaDeHilo = false;
                Area.FaltaDeForro = false;
                Area.OtrosAlistados = false;


                Area.FaltaDeSuela = false;
                Area.FaltaDePegamento = false;
                Area.CorteEnMalEstado = false;
                Area.OtrosEnsuelados = false;




                _solicitudesRepository.ModificarDetalle(Area);
            }

            _solicitudesRepository.Guardar();


            return Json("Se ha trasladado con éxito.", JsonRequestBehavior.AllowGet);
        }




        public ActionResult AceptarBodega(List<int> Ids)
        {

            if (Ids == null)
            {
                return Json("Error. Lista Vacía.", JsonRequestBehavior.AllowGet);
            }



            foreach (var item in Ids)
            {
                var Area = _solicitudesRepository.GetDetalleSolicitud(item);
                Area.ProduccionId = 4; // 4 En bodega
                Area.EnBodega = true; // marcar que ya este en bodega


                Area.FaltaDeTextil = false;
                Area.FaltaDeHilo = false;
                Area.FaltaDeForro = false;
                Area.OtrosAlistados = false;


                Area.FaltaDeSuela = false;
                Area.FaltaDePegamento = false;
                Area.CorteEnMalEstado = false;
                Area.OtrosEnsuelados = false;



                _solicitudesRepository.ModificarDetalle(Area);
            }

            _solicitudesRepository.Guardar();





            //if (idReservaProd == 0)
            //{
            //    return Json(result, JsonRequestBehavior.AllowGet);

            //}

            //// cambiar estado
            //var detalleBorrador = _solicitudesRepository.GetDetalleSolicitud(id);

            //detalleBorrador.ProduccionId = 4; // en bodega
            //detalleBorrador.EnBodega = true;

            //_solicitudesRepository.ModificarDetalle(detalleBorrador, false);

            ////reservas

            //var detalleReserva = _solicitudesRepository.GetReservasProduc(idReservaProd);

            //detalleReserva.CantidadReserva -= 1;

            //_productosRepository.GuardarReservaProduc(detalleReserva);

            //_solicitudesRepository.Guardar();

            return Json("Se ha trasladado con éxito.", JsonRequestBehavior.AllowGet);
            
        }


        public ActionResult NoAceptadoAlistado(int? id, int? idReservaProd)
        {
            string result = "Ha ocurrido un error";


            if (id == null || idReservaProd == null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);

            }


            if (idReservaProd == 0)
            {
                return Json(result, JsonRequestBehavior.AllowGet);

            }

            // cambiar estado
            var detalleBorrador = _solicitudesRepository.GetDetalleSolicitud(id);

            detalleBorrador.ProduccionId = 1; // a alistado
            detalleBorrador.Prioridad = true;

            _solicitudesRepository.ModificarDetalle(detalleBorrador, false);

            //reservas

            //var detalleReserva = _solicitudesRepository.GetReservasProduc(idReservaProd);

            //detalleReserva.CantidadReserva -= 1;

            //_productosRepository.GuardarReservaProduc(detalleReserva);

            _solicitudesRepository.Guardar();

            return Json("Exito", JsonRequestBehavior.AllowGet);

        }



        public ActionResult NoAceptadoEnsuelado(int? id, int? idReservaProd)
        {
            string result = "Ha ocurrido un error";


            if (id == null || idReservaProd == null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);

            }


            if (idReservaProd == 0)
            {
                return Json(result, JsonRequestBehavior.AllowGet);

            }

            // cambiar estado
            var detalleBorrador = _solicitudesRepository.GetDetalleSolicitud(id);

            detalleBorrador.ProduccionId = 2; // a ensuelado
            detalleBorrador.Prioridad = true; 

            _solicitudesRepository.ModificarDetalle(detalleBorrador, false);

            //reservas
            //var detalleReserva = _solicitudesRepository.GetReservasProduc(idReservaProd);
            //detalleReserva.CantidadReserva -= 1;
            //_productosRepository.GuardarReservaProduc(detalleReserva);

            _solicitudesRepository.Guardar();

            return Json("Exito", JsonRequestBehavior.AllowGet);

        }


        public ActionResult ActualizarFaltasA(DetalleSolicitud datos)
        {
            var detalleSolicitudProducto = _solicitudesRepository.GetDetalleSolicitud(datos.Id);
            detalleSolicitudProducto.FaltaDeTextil = datos.FaltaDeTextil;
            detalleSolicitudProducto.FaltaDeHilo = datos.FaltaDeHilo;
            detalleSolicitudProducto.FaltaDeForro = datos.FaltaDeForro;
            detalleSolicitudProducto.OtrosAlistados = datos.OtrosAlistados;


            _solicitudesRepository.ModificarDetalle(detalleSolicitudProducto);

            return Json("EXITO", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActualizarFaltasE(DetalleSolicitud datos)
        {
            var detalleSolicitudProducto = _solicitudesRepository.GetDetalleSolicitud(datos.Id);
            detalleSolicitudProducto.FaltaDeSuela = datos.FaltaDeSuela;
            detalleSolicitudProducto.FaltaDePegamento = datos.FaltaDePegamento;
            detalleSolicitudProducto.CorteEnMalEstado = datos.CorteEnMalEstado;
            detalleSolicitudProducto.OtrosEnsuelados = datos.OtrosEnsuelados;

            _solicitudesRepository.ModificarDetalle(detalleSolicitudProducto);


            return Json("EXITO", JsonRequestBehavior.AllowGet);

        }


    }
}