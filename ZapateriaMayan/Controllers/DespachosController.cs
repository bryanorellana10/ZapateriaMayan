using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;
using ZapateriaMayan.ViewModels;

namespace ZapateriaMayan.Controllers
{
    public class DespachosController : Controller
    {

        private DespachosRepository _despachosRepository = null;
        private PedidosRepository _pedidosRepository = null;
        private RutasRepository _rutasRepository = null;
        private CajaInteriorRepository _cajaInteriorRepository = null;
        private CajaCapitalRepository _cajaCapitalRepository = null;



        public DespachosController(DespachosRepository despachosRepository, PedidosRepository pedidosRepository, RutasRepository rutasRepository, CajaInteriorRepository cajaInteriorRepository, CajaCapitalRepository capitalRepository)
        {
            _pedidosRepository = pedidosRepository;
            _despachosRepository = despachosRepository;
            _rutasRepository = rutasRepository;
            _cajaInteriorRepository = cajaInteriorRepository;
            _cajaCapitalRepository = capitalRepository;
        }

        public ActionResult General()
        {

            return View();
        }

        public ActionResult TodosCapital()
        {
            var ListaCapitalDespachos = _despachosRepository.DespachosCapitalesGet();
            return View(ListaCapitalDespachos);
        }

        public ActionResult TodosInterior()
        {
            var ListaInteriorDespachos = _despachosRepository.DespachosInteriorGetI();
            return View(ListaInteriorDespachos);
        }

        public ActionResult NuevoDespachoCapital(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();

            }

            var pedido = _pedidosRepository.Get((int)id);

            var viewModel = new DespachosCapitalViewModel()
            {
                Pedido = pedido

            };

            viewModel.Iniciar(_rutasRepository);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult NuevoDespachoCapital(DespachosCapitalViewModel viewModel)
        {
            // revisa si ya existe el despacho en envios, es para evitar errores

            if (_despachosRepository.VerificarSiExisteEnDespachoCapital(viewModel.Id))
            {
                TempData["Message"] = "Este pedido ya ha sido despachado. Error.";
                ModelState.AddModelError(string.Empty, "Error.");
            }

            if (ModelState.IsValid)
            {
                //pendiente
                var nuevoDespacho = new DespachosCapital()
                {
                    PedidoId = viewModel.Id,
                    InicioCapital = DateTime.Now,
                    Comentario = viewModel.DespachosCapital.Comentario
                };

                var nuevoEnvio = new EnviosCapital()
                {
                    EstadoEnvioCapitalId = 66, // 1 esperan
                    DespachosCapital = nuevoDespacho,
                    RutasId = viewModel.EnviosCapital.RutasId,
                    DespachadorId = viewModel.EnviosCapital.DespachadorId,
                    EgresoEntrega = viewModel.EnviosCapital.EgresoEntrega

                };

                _despachosRepository.Add(nuevoEnvio, false);


                var getPedido = _pedidosRepository.Get(viewModel.Id, false);
                getPedido.EstadoDespacho = true;

                _pedidosRepository.ModificarPedido(getPedido, false);

                _pedidosRepository.Guardar();

                TempData["Message"] = "¡El Pedido se ha despachado correctamente!";
                return RedirectToAction("DetallePedido", "Pedidos", new { viewModel.Id });
            }

            viewModel.Iniciar(_rutasRepository);

            return View(viewModel);
        }

        public ActionResult NuevoDespachoInterior(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();

            }
            var pedido = _pedidosRepository.Get((int)id);

            var viewModel = new DespachosInteriorViewModel()
            {
                Pedido = pedido

            };

            //view.IniciarRutas(_rutasRepository);
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult NuevoDespachoInterior(DespachosInteriorViewModel viewModel)
        {
            if (_despachosRepository.VerificarSiExisteEnDespachoInterior(viewModel.Id))
            {
                TempData["Message"] = "Este pedido ya ha sido despachado. Error.";
                ModelState.AddModelError(string.Empty, "Error.");
            }


            if (ModelState.IsValid)
            {

                var nuevoDespacho = new DespachosInterior()
                {
                    PedidoId = viewModel.Id,
                    NumeroGuia = viewModel.DespachosInterior.NumeroGuia,
                    Peso = viewModel.DespachosInterior.Peso,
                    InicioInterior = DateTime.Now,
                    Comentario = viewModel.DespachosInterior.Comentario
                };

                var nuevoEnvio = new EnviosInterior()
                {
                    EstadoEnvioInteriorId = 1, // 1 esperando entrega
                    DespachosInterior = nuevoDespacho

                };

                _despachosRepository.Add(nuevoEnvio, false);


                var getPedido = _pedidosRepository.Get(viewModel.Id, false);
                getPedido.EstadoDespacho = true;

                _pedidosRepository.ModificarPedido(getPedido, false);

                _pedidosRepository.Guardar();

                TempData["Message"] = "¡El Pedido se ha despachado correctamente!";
                return RedirectToAction("DetallePedido", "Pedidos", new { viewModel.Id });
            }

            return View(viewModel);
        }


        // sirve para reconocer si el pedido es de interior o capital
        // no es buena practica pero luego hay que corregirlo
        public ActionResult Redireccion(int id)
        {
            var pedido = _pedidosRepository.Get(id);

            if (pedido.Region == "Capital")
            {

                return RedirectToAction("NuevoDespachoCapital", new { pedido.Id });

            }
            else if (pedido.Region == "Interior")
            {
                return RedirectToAction("NuevoDespachoInterior", new { pedido.Id });

            }

            return View(pedido);

        }

        public ActionResult EnviosTodosCapital()
        {


            return View();
        }

        //public ActionResult EnviosCapital()
        //{
        //    var ListaCapital = _despachosRepository.ListaCapitalesDesp();
        //    return View(ListaCapital);
        //}

        // interior

        public ActionResult DespachosInterior()
        {
            var ListInterior = _despachosRepository.ListaInternosDespachos();

            return View(ListInterior);
        }

        public ActionResult PendienteRecoleccion()
        {
            var ListInterior = _despachosRepository.ListaPendientesDeRecoleccion();

            return View(ListInterior);
        }

        public ActionResult Recolectados()
        {
            var ListInterior = _despachosRepository.ListaRecolectados();

            return View(ListInterior);
        }

        public ActionResult EntregadosInterior()
        {
            var ListInterior = _despachosRepository.ListaEntregadosInterior();

            return View(ListInterior);
        }

        public ActionResult DevolucionesInterior()
        {
            var ListInterior = _despachosRepository.ListaDevolucionInterior();

            return View(ListInterior);
        }

        public ActionResult LiquidadosInterior()
        {
            var ListInterior = _despachosRepository.ListaLiquidadosInterior();
            return View(ListInterior);
        }


        // capital

        public ActionResult DespachosCapital()
        {
            var ListInterior = _despachosRepository.ListaCapitalDespachos();

            return View(ListInterior);
        }

        public ActionResult EsperandoEnviar()
        {
            var ListInterior = _despachosRepository.ListaCapitalDespachosEsperandoEnviar();

            return View(ListInterior);
        }

        public ActionResult EsperandoEntrega()
        {
            var ListInterior = _despachosRepository.ListaCapitalEsperandoEntrega();

            return View(ListInterior);
        }

        public ActionResult EntregadosCapital()
        {
            var ListInterior = _despachosRepository.ListaCapitalEntregados();

            return View(ListInterior);
        }

        public ActionResult DevolucionesCapital()
        {
            var ListInterior = _despachosRepository.ListaCapitalDevolucion();

            return View(ListInterior);
        }

        public ActionResult LiquidadosCapital()
        {
            var ListInterior = _despachosRepository.ListaCapitalLiquidados();

            return View(ListInterior);
        }




        public ActionResult EstadoLiquidadoInterior(int PedidoId)
        {
            // cambiar el estado de envio
            bool cajaAbierta = ComprobarEstadoCajaInterior(out var caja);
            if (!cajaAbierta) {
                return Json("No se ha podido liquidar el pedido, no hay ninguna caja abierta.", JsonRequestBehavior.AllowGet);
            }

            var pedido = _pedidosRepository.Get(PedidoId);
            var estado = pedido.DespachosInteriors.Select(a => a.EnviosInteriors.SingleOrDefault()).SingleOrDefault();
            estado.EstadoEnvioInteriorId = 5;

            _despachosRepository.ModificarSinGuardar(estado);

            // hay que cambiar la liquidacion a true
            var despachosInterior = pedido.DespachosInteriors.SingleOrDefault();
            despachosInterior.Liquidado = true;

            _despachosRepository.ModificarSinGuardar(despachosInterior);



            var nuevaventa = new VentasInterior()
            {
                DespachosInteriorId = despachosInterior.Id,
                SubTotal = pedido.SubTotal,
                Descuento = pedido.Descuento,
                Total = pedido.Total,
                FechaVenta = DateTime.Now

            };







            _despachosRepository.Add(nuevaventa, false);



            var cajaDetalle = new DetalleCajaInterior()
            {
                VentasInteriorId = nuevaventa.Id,
                CajaInterior = caja,
                Descripcion = "Venta realizada pedido #" + PedidoId + ", Numero de Guia " + despachosInterior.NumeroGuia,
                Entrega = true, // liquidado significa ya entregado el pedido o el paquete
                Ingreso = nuevaventa.Total,
                Gasto = 0.00M,
            };



            _cajaInteriorRepository.Add(cajaDetalle, false);
            _pedidosRepository.Guardar();


            return Json("Liquidación éxitosa", JsonRequestBehavior.AllowGet);
        }



        public ActionResult EstadoRecolectadoInterior(int PedidoId)
        {
            // cambiar el estado de envio
            var pedido = _pedidosRepository.Get(PedidoId);
            var estado = pedido.DespachosInteriors.Select(a => a.EnviosInteriors.SingleOrDefault()).SingleOrDefault();
            estado.EstadoEnvioInteriorId = 2; // 2 recolectado

            _despachosRepository.ModificarSinGuardar(estado);

            _pedidosRepository.Guardar();


            return Json("Se ha cambiado estado.", JsonRequestBehavior.AllowGet);
        }

        public ActionResult EstadoEntregadoInterior(int PedidoId)
        {
            // cambiar el estado de envio
            var pedido = _pedidosRepository.Get(PedidoId);
            var estado = pedido.DespachosInteriors.Select(a => a.EnviosInteriors.SingleOrDefault()).SingleOrDefault();
            estado.EstadoEnvioInteriorId = 3; // 3 entregado

            _despachosRepository.ModificarSinGuardar(estado);

            _pedidosRepository.Guardar();


            return Json("Se ha cambiado estado.", JsonRequestBehavior.AllowGet);
        }







        // para capital


        public ActionResult EstadoLiquidadoCapital(int PedidoId)
        {
            bool cajaAbierta = ComprobarEstadoCajaCapital(out var caja);
            if (!cajaAbierta)
            {
                return Json("No se ha podido liquidar el pedido, no hay ninguna caja abierta.", JsonRequestBehavior.AllowGet);
            }

            // cambiar el estado de envio
            var pedido = _pedidosRepository.Get(PedidoId);
            var estado = pedido.DespachosCapitals.Select(a => a.EnviosCapitals.SingleOrDefault()).SingleOrDefault();
            estado.EstadoEnvioCapitalId = 70;

            _despachosRepository.ModificarSinGuardar(estado);

            // hay que cambiar la liquidacion a true
            var despachosCapital = pedido.DespachosCapitals.SingleOrDefault();
            despachosCapital.Liquidado = true;

            _despachosRepository.ModificarSinGuardar(despachosCapital);



            var nuevaventa = new VentasCapital()
            {
                DespachosCapitalId = despachosCapital.Id,
                SubTotal = pedido.SubTotal,
                Descuento = pedido.Descuento,
                Total = pedido.Total,
                FechaVenta = DateTime.Now

            };

            _despachosRepository.Add(nuevaventa, false);


            // incluir nuevo detalle Caja

            var nuevoDetalleCajaCapital = new DetalleCajaCapital()
            {
                CajaCapital = caja,
                VentasCapitalId = nuevaventa.Id,
                Descripcion = "Venta realizada " + "Pedido #" + pedido.Id + ",  Mensajero: " + despachosCapital.EnviosCapitals.Select(z => z.Despachador.Nombres).SingleOrDefault() + ",  Cliente: " + pedido.Cliente.Nombres,
                Ingreso = nuevaventa.Total,
                Gasto = despachosCapital.EnviosCapitals.SingleOrDefault().EgresoEntrega,
                Entrega = true,
                Responsable = pedido.Vendedor
            };


            _cajaCapitalRepository.Add(nuevoDetalleCajaCapital, false);

            _pedidosRepository.Guardar();


            return Json("Liquidación éxitosa", JsonRequestBehavior.AllowGet);
        }



        public ActionResult EstadoEsperandoEntregaCapital(int PedidoId)
        {
            // cambiar el estado de envio
            var pedido = _pedidosRepository.Get(PedidoId);
            var estado = pedido.DespachosCapitals.Select(a => a.EnviosCapitals.SingleOrDefault()).SingleOrDefault();
            estado.EstadoEnvioCapitalId = 67; // 67 esperando entrega

            _despachosRepository.ModificarSinGuardar(estado);

            _pedidosRepository.Guardar();


            return Json("Se ha cambiado estado.", JsonRequestBehavior.AllowGet);
        }

        public ActionResult EstadoEntregadoCapital(int PedidoId)
        {
            // cambiar el estado de envio
            var pedido = _pedidosRepository.Get(PedidoId);
            var estado = pedido.DespachosCapitals.Select(a => a.EnviosCapitals.SingleOrDefault()).SingleOrDefault();
            estado.EstadoEnvioCapitalId = 68;

            _despachosRepository.ModificarSinGuardar(estado);

            _pedidosRepository.Guardar();


            return Json("Se ha cambiado estado.", JsonRequestBehavior.AllowGet);
        }


        public ActionResult EstadoDevolucionCapital(int PedidoId)
        {
            // cambiar el estado de envio
            var pedido = _pedidosRepository.Get(PedidoId);
            var estado = pedido.DespachosCapitals.Select(a => a.EnviosCapitals.SingleOrDefault()).SingleOrDefault();
            estado.EstadoEnvioCapitalId = 14; // 3 enviado

            _despachosRepository.ModificarSinGuardar(estado);

            _pedidosRepository.Guardar();


            return Json("Se ha cambiado estado.", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult CambioEstadoDevolucion(int PedidoId)
        {
            // cambiar el a devolucion
            var pedido = _pedidosRepository.Get(PedidoId);
            pedido.Devolucion = true;
            _pedidosRepository.ModificarSinGuardar(pedido);

            foreach (var item in pedido.DetallePedidos)
            {
                var talla = _pedidosRepository.GetDT(item.DetalleTalla.Id);
                talla.Stock += Convert.ToInt32(item.Cantidad);
                _pedidosRepository.ModificarDT(talla);

            }


            _pedidosRepository.Guardar();


            return Json("Se ha cambiado estado.", JsonRequestBehavior.AllowGet);
        }

        public bool ComprobarEstadoCajaInterior(out CajaInterior caja)
        {
            var cajas = _cajaInteriorRepository.GetList();

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

        [HttpPost]
        public ActionResult GetValueRuta(int value)
        {
            // https://localhost:44320/Despachos/NuevoDespachoCapital/108
            var ruta = _rutasRepository.RutasList().Where(a => a.Id == value);                        


            return Json(ruta, JsonRequestBehavior.AllowGet);

        }

    }
}