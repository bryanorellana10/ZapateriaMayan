using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;
using CapaBaseDeDatos.Security;
using Microsoft.AspNet.Identity;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZapateriaMayan.ViewModels;

namespace ZapateriaMayan.Controllers
{
    public class PedidosController : Controller
    {
        private ClientesRepository _clientesRepository = null;
        private ProductosRepository _productosRepository = null;
        private  PedidosRepository _pedidosRepository = null;
        private readonly ApplicationUserManager _userManager;


        public PedidosController(ClientesRepository clientesRepository, ProductosRepository productosRepository, PedidosRepository pedidosRepository, ApplicationUserManager applicationUserManager)
        {
            _clientesRepository = clientesRepository;
            _productosRepository = productosRepository;
            _pedidosRepository = pedidosRepository;
            _userManager = applicationUserManager;
        }

        public ActionResult PedidosHoy()
        {
            return View();
        }

        public ActionResult NuevoPedido()
        {
            var viewModel = new PedidoViewModel();
            viewModel.Init(_clientesRepository, _productosRepository);
            return View(viewModel);
        }

        public ActionResult Todos()
        {
            var pedidos = _pedidosRepository.ListaPedidos();
            return View(pedidos);
        }

        public ActionResult ListaCapitales()
        {
            var pedidosCapital = _pedidosRepository.ListaCapitales();
            return View(pedidosCapital);
        }

        public ActionResult ListaInternos()
        {
            var pedidosCapital = _pedidosRepository.ListaInternos();
            return View(pedidosCapital);
        }

        public ActionResult ListaCapitalesCancelados()
        {
            var pedidosCapital = _pedidosRepository.ListaCapitales(true);
            return View(pedidosCapital);
        }

        public ActionResult ListaInternosCancelados()
        {
            var pedidosCapital = _pedidosRepository.ListaInternos(true);
            return View(pedidosCapital);
        }


        public ActionResult DetallePedido(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pedido = _pedidosRepository.Get((int)id, incluideRelatedEntities: true);

            if (pedido == null)
            {
                return HttpNotFound();
            }

            var viewmodel = new PedidoViewModel()
            {
                Pedido = pedido
            };

            viewmodel.CargarPedidosSegunCliente(_pedidosRepository, pedido.ClienteId, pedido.Id);

            return View(viewmodel);
        }

        public ActionResult Procesar(List<DetallePedido> pedido, Pedido infopedido)
        {
            string result = "SE HA PRODUCIDO UN ERROR, ASEGÚRENSE QUE LOS DATOS ESTEN CORRECTOS";


            if (ModelState.IsValid)
            {
                var nuevopedido = new Pedido()
                {
                    ClienteId = infopedido.ClienteId,
                    Direccion = infopedido.Direccion,
                    SubTotal = infopedido.SubTotal,
                    Descuento = infopedido.Descuento,
                    Total = infopedido.Total,
                    Inicio = infopedido.Inicio,
                    Final = infopedido.Final,
                    Region = infopedido.Region,
                    Observacion = infopedido.Observacion,
                    ContactoPrincipal = infopedido.ContactoPrincipal,
                    Telefonoc = infopedido.Telefonoc,
                    FechaPedido = DateTime.Now,
                    Vendedor = _userManager.FindById(User.Identity.GetUserId()).Name
                };

                _pedidosRepository.AgregarSinGuardar(nuevopedido);

                foreach (var item in pedido)
                {
                    var detallepedido = new DetallePedido()
                    {
                        Pedido = nuevopedido,
                        DetalleTallaId = item.DetalleTallaId,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio,
                        Descuento = item.Descuento,
                        Subtotal = item.Subtotal,
                        Total = item.Total
                    };

                    _pedidosRepository.AgregarSinGuardar(detallepedido);

                    var talla = _pedidosRepository.GetDT(detallepedido.DetalleTallaId);

                    talla.Stock -= Convert.ToInt32(detallepedido.Cantidad);

                    _pedidosRepository.ModificarDT(talla);


                }

                _pedidosRepository.Guardar();
                result = "¡SE HA INSERTADO CON ÉXITO!";

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Procesarcn(DetallePedido[] pedido, Cliente cliente, Pedido infopedido)
        {
            string result = "SE HA PRODUCIDO UN ERROR, ASEGÚRENSE QUE LOS DATOS ESTEN CORRECTOS";


            if (ModelState.IsValid)
            {
                var nuevocliente = new Cliente()
                {
                    Nombres = cliente.Nombres,
                    Telefono1 = cliente.Telefono1,
                    Telefono2 = cliente.Telefono2,
                    Direccion = cliente.Direccion,
                    Whatsapp = cliente.Whatsapp,
                    Facebook = cliente.Facebook,
                    Instagram = cliente.Instagram,
                    Region = cliente.Region,
                    FechaCreacion = DateTime.Now

                };

                _clientesRepository.AddSinGuardar(nuevocliente);


                var nuevopedido = new Pedido()
                {
                    Direccion = nuevocliente.Direccion,
                    SubTotal = infopedido.SubTotal,
                    Total = infopedido.Total,
                    Descuento = infopedido.Descuento,
                    Inicio = infopedido.Inicio,
                    Final = infopedido.Final,
                    Region = nuevocliente.Region,
                    Observacion = infopedido.Observacion,
                    ContactoPrincipal = infopedido.ContactoPrincipal,
                    Telefonoc = nuevocliente.Telefono1,
                    FechaPedido = DateTime.Now,
                    Cliente = nuevocliente,
                    Vendedor = _userManager.FindById(User.Identity.GetUserId()).Name
                };

                _pedidosRepository.AgregarSinGuardar(nuevopedido);


                foreach (var item in pedido)
                {
                    var detallepedido = new DetallePedido()
                    {
                        Pedido = nuevopedido,
                        DetalleTallaId = item.DetalleTallaId,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio,
                        Descuento = item.Descuento,
                        Subtotal = item.Subtotal,
                        Total = item.Total
                    };

                    _pedidosRepository.AgregarSinGuardar(detallepedido);


                    _pedidosRepository.AgregarSinGuardar(detallepedido);

                    var talla = _pedidosRepository.GetDT(detallepedido.DetalleTallaId);

                    talla.Stock -= Convert.ToInt32(detallepedido.Cantidad);

                    _pedidosRepository.ModificarDT(talla);

                }

                try
                {
                    _pedidosRepository.Guardar();
                    result = "¡SE HA INSERTADO CON ÉXITO!";


                }
                catch (Exception)
                {
                    result = "ERROR CRÍTICO. NO HAY EXISTENCIAS.";

                }
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditarPedido(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var cargarpedido = _pedidosRepository.GetPedidoDetalle((int)id);


            var viewModel = new PedidoEditViewModel()
            {
                Pedido = cargarpedido
            };

            viewModel.Init(_clientesRepository, _productosRepository);


            return View(viewModel);
        }

       
        public ActionResult ActualizarBorradorAPedido(int Id)
        {

            var pedido = _pedidosRepository.GetPedidoReservaPro(Id);
            pedido.EnBorrador = false;

            _pedidosRepository.ModificarSinGuardar(pedido);

            // verificar si todos los productos estan en bodega 
            // esto es una mala practica pero no se me ocurrio de otra manera
            // pero funciona bien




            var existePedido = (from solicitud in pedido.Solicitudes from enbodega in solicitud.DetalleSolicituds
                                where enbodega.EnBodega == false
                                select enbodega);

            if (existePedido.Count() > 0) {
                return Json("Aún hay productos en produccion. ERROR. ", JsonRequestBehavior.AllowGet);
            }

            // CODIGO FOREACH
            //foreach (var item in pedido.Solicitudes)
            //{
            //    foreach (var det in item.DetalleSolicituds)
            //    {
            //        if (det.EnBodega == false)
            //        {
                        

            //        }
            //    }
            //}

            //var verificar = prueba.Any(a => a.EnBodega == true);


            foreach (var item in pedido.ReservasProducs) 
            {
                var nuevodetalle = new DetallePedido()
                {
                    PedidoId = pedido.Id,
                    DetalleTallaId = item.DetalleTallaId,
                    Cantidad = item.CantidadSolicitada,
                    Precio = item.Precio,
                    Descuento = item.Descuento,
                    Subtotal = item.Subtotal,
                    Total = item.Total
                };

                _pedidosRepository.AgregarSinGuardar(nuevodetalle);

            }

            _pedidosRepository.Guardar();

            return Json( pedido.Id ,JsonRequestBehavior.AllowGet); 

        }

     
        public ActionResult ActualizarDatosPedido(Pedido p)
        {
            if (ModelState.IsValid)
            {
                var pedido = p;

                var ped = _pedidosRepository.Get(pedido.Id, false);
                ped.Direccion = pedido.Direccion;
                ped.Inicio = pedido.Inicio;
                ped.Final = pedido.Final;
                ped.Region = pedido.Region;
                ped.Observacion = pedido.Observacion;
                ped.ContactoPrincipal = pedido.ContactoPrincipal;
                ped.Telefonoc = pedido.Telefonoc;

                _pedidosRepository.ModificarPedido(ped);


            }

            return Json("ÉXITO", JsonRequestBehavior.AllowGet);

        }

        public ActionResult DetallePedidoPDF(int? id) 
        {
            var pedido = _pedidosRepository.Get((int)id, incluideRelatedEntities: true);

         
            var viewmodel = new PedidoViewModel()
            {
                Pedido = pedido
            };


            viewmodel.CargarPedidosSegunCliente(_pedidosRepository, pedido.ClienteId, pedido.Id);

            var productos = viewmodel.Pedido.DetallePedidos.GroupBy(det => det.DetalleTalla.Producto,(prd) => new { productos = prd});
            ViewBag.productos = productos.Select(a => a.Key);

            return new ActionAsPdf("DetallePedidoPDF",  viewmodel)
            {
                FileName = "pedido-" + viewmodel.Pedido.Id + ".pdf",
                PageSize = Rotativa.Options.Size.A3,
                PageOrientation = Rotativa.Options.Orientation.Portrait
            };
        

            //return View(viewmodel);
        }

        [HttpPost]
        public ActionResult AsociarPedido(int padre,int hijo)
        {
            var pedidoPadre = _pedidosRepository.Get(padre, incluideRelatedEntities: true);
            var pedidoHijo = _pedidosRepository.Get(hijo, incluideRelatedEntities: true);

            foreach (var dtl in pedidoHijo.DetallePedidos) {
                pedidoPadre.DetallePedidos.Add(dtl);
            }

            _pedidosRepository.ModificarSinGuardar(pedidoPadre);
            _pedidosRepository.EliminarPedido(pedidoHijo,false);
            _pedidosRepository.Guardar();




            return Json("ÉXITO" + padre + " " + hijo, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult CancelarPedido(int PedidoId)
        {
            
            var pedido = _pedidosRepository.Get(PedidoId);
            pedido.Estado = true;
            _pedidosRepository.ModificarSinGuardar(pedido);

            foreach (var item in pedido.DetallePedidos)
            {
                var talla = _pedidosRepository.GetDT(item.DetalleTalla.Id);
                talla.Stock += Convert.ToInt32(item.Cantidad);
                _pedidosRepository.ModificarDT(talla);

            }


            _pedidosRepository.Guardar();


            return Json("Se ha cancelado el pedido exitosamente.", JsonRequestBehavior.AllowGet);
        }




    }
}