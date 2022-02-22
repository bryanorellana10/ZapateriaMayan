using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;
using CapaBaseDeDatos.Security;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZapateriaMayan.ViewModels;

namespace ZapateriaMayan.Controllers
{
    public class BorradorController: Controller
    {
        private readonly PedidosRepository _pedidosRepository = null;
        private readonly ClientesRepository _clientesRepository = null;
        private readonly ProductosRepository _productosRepository = null;
        private readonly ApplicationUserManager _userManager;

        public BorradorController(PedidosRepository pedidosRepository, ClientesRepository clientesRepository, ProductosRepository productosRepository, ApplicationUserManager applicationUserManager)
        {
            _pedidosRepository = pedidosRepository;
            _clientesRepository = clientesRepository;
            _productosRepository = productosRepository;
            _userManager = applicationUserManager;
        }

        public ActionResult Todos()
        {
            var enborrador = _pedidosRepository.EnBorrador();
            return View(enborrador);
        }



        public ActionResult GuardarABorrador_cr(List<ReservasProduc> reservas, Pedido infopedido, string[]  observaciones)
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
                    EnBorrador = true,
                    Vendedor = _userManager.FindById(User.Identity.GetUserId()).Name

                };

                _pedidosRepository.AgregarSinGuardar(nuevopedido);


                var nuevasolicitud = new Solicitudes()
                {
                    Pedido = nuevopedido,
                    EstadosProductoId = 1, // 1 en espera
                    FechaSolicitud = DateTime.Now

                };

                _pedidosRepository.AgregarSolicitud(nuevasolicitud);
                var index = 0;
                foreach (var item in reservas)
                {
                    
                    var detallepedido = new ReservasProduc()
                    {
                        Pedido = nuevopedido,
                        DetalleTallaId = item.DetalleTallaId,
                        CantidadSolicitada = item.CantidadSolicitada,
                        Precio = item.Precio,
                        Descuento = item.Descuento,
                        Subtotal = item.Subtotal,
                        Total = item.Total,

                        Cantidad = item.Cantidad,
                        CantidadReserva = item.CantidadReserva

                    };

                    _pedidosRepository.AgregarSinGuardar(detallepedido);



                    var talla = _pedidosRepository.GetDT(detallepedido.DetalleTallaId);

                    talla.Stock -= Convert.ToInt32(detallepedido.Cantidad);

                    _pedidosRepository.ModificarDT(talla);


                    for (int i = 0; i < detallepedido.CantidadReserva; i++)
                    {
                        var detalleSolicitud = new DetalleSolicitud()
                        {
                            SolicitudesId = nuevasolicitud.Id,
                            ProduccionId = 1, // Alistado
                            ReservasProduc = detallepedido,
                            Observacion = observaciones[index]
                        };

                        _pedidosRepository.AgregarSinGuargarDetalleSolicitud(detalleSolicitud);
                    }

                    index++;

                }

                _pedidosRepository.Guardar();

                return Json("SE HA INSERTADO CON ÉXITO", JsonRequestBehavior.AllowGet);

            }

            return Json(result, JsonRequestBehavior.AllowGet);


        }

        public ActionResult GuardarABorrador_cn(List<ReservasProduc> reservas, Pedido infopedido, Cliente cliente)
        {
            string result = "SE HA PRODUCIDO UN ERROR, ASEGÚRENSE QUE LOS DATOS ESTEN CORRECTOS";


            if (ModelState.IsValid)
            {
                var nuevoCliente = new Cliente()
                {
                  Nombres = cliente.Nombres,
                  Telefono1 = cliente.Telefono1,
                  Telefono2 = cliente.Telefono2,
                  Direccion = cliente.Direccion,
                  Whatsapp = cliente.Whatsapp,
                  Facebook = cliente.Facebook,
                  Instagram = cliente.Instagram,
                  Region = cliente.Region,
                  ContactoPrincipal = cliente.ContactoPrincipal,
                  FechaCreacion = DateTime.Now
                };


                var nuevopedido = new Pedido()
                {
                    Cliente = nuevoCliente,
                    Direccion = infopedido.Direccion,
                    SubTotal = infopedido.SubTotal,
                    Descuento = infopedido.Descuento,
                    Total = infopedido.Total,
                    Inicio = infopedido.Inicio, 
                    Final = infopedido.Final,
                    Region = infopedido.Region,
                    Observacion = infopedido.Observacion,
                    ContactoPrincipal = infopedido.ContactoPrincipal,
                    Telefonoc = nuevoCliente.Telefono1,
                    FechaPedido = DateTime.Now,
                    EnBorrador = true,
                    Vendedor = _userManager.FindById(User.Identity.GetUserId()).Name

                };

                _pedidosRepository.AgregarSinGuardar(nuevopedido);


                var nuevasolicitud = new Solicitudes()
                {
                    Pedido = nuevopedido,
                    EstadosProductoId = 1, // 1 en espera
                    FechaSolicitud = DateTime.Now

                };

                _pedidosRepository.AgregarSolicitud(nuevasolicitud);


                foreach (var item in reservas)
                {
                    var detallepedido = new ReservasProduc()
                    {
                        Pedido = nuevopedido,
                        DetalleTallaId = item.DetalleTallaId,
                        CantidadSolicitada = item.CantidadSolicitada,
                        Precio = item.Precio,
                        Descuento = item.Descuento,
                        Subtotal = item.Subtotal,
                        Total = item.Total,

                        Cantidad = item.Cantidad,
                        CantidadReserva = item.CantidadReserva

                    };

                    _pedidosRepository.AgregarSinGuardar(detallepedido);


                    var talla = _pedidosRepository.GetDT(detallepedido.DetalleTallaId);

                    talla.Stock -= Convert.ToInt32(detallepedido.Cantidad);

                    _pedidosRepository.ModificarDT(talla);



                    for (int i = 0; i < detallepedido.CantidadReserva; i++)
                    {
                        var detalleSolicitud = new DetalleSolicitud()
                        {
                            SolicitudesId = nuevasolicitud.Id,
                            ProduccionId = 1, // Alistado
                            ReservasProduc = detallepedido,
                        };

                        _pedidosRepository.AgregarSinGuargarDetalleSolicitud(detalleSolicitud);
                    }

                }

                _pedidosRepository.Guardar();

                return Json("SE HA INSERTADO CON ÉXITO", JsonRequestBehavior.AllowGet);

            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        

        public ActionResult EditarBorrador (int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var cargarpedido = _pedidosRepository.GetPedidoDetalle((int)id);

            var viewModel = new PedidoViewModel()
            {
                Pedido = cargarpedido
            };

            viewModel.Init(_clientesRepository, _productosRepository);


            return View(viewModel);

        }


        public ActionResult AgregarProductosBorrador(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var cargarpedido = _pedidosRepository.GetPedidoDetalle((int)id);


            var viewModel = new PedidoViewModel()
            {
                Pedido = cargarpedido
            };

            viewModel.Init(_clientesRepository, _productosRepository);

            return View(viewModel);

        }

        public ActionResult GuardarABorrador_modificar(List<ReservasProduc> reservas, List<DetalleSolicitud> solicitudes, int PedidoId, string[] observaciones)
        {
            string result = "SE HA PRODUCIDO UN ERROR, ASEGÚRENSE QUE LOS DATOS ESTEN CORRECTOS";
                



                if (ModelState.IsValid)
            {
                var pedido = _pedidosRepository.Get(PedidoId, true);
                var nuevasolicitud = new Solicitudes()
                {
                    Pedido = pedido,
                    EstadosProductoId = 1,
                    FechaSolicitud = DateTime.Now

                };
                
                int index = 0 ;

                foreach (var item in reservas)
                {
                    var detallepedido = new ReservasProduc()
                    {
                        Pedido = pedido,
                        DetalleTallaId = item.DetalleTallaId,
                        CantidadSolicitada = item.CantidadSolicitada,
                        Precio = item.Precio,
                        Descuento = item.Descuento,
                        Subtotal = item.Subtotal,
                        Total = item.Total,
                        Cantidad = item.Cantidad,
                        CantidadReserva = item.CantidadReserva


                    };

                    _pedidosRepository.AgregarSinGuardar(detallepedido);


                    var producto = _pedidosRepository.GetDT(item.DetalleTallaId);

                    if (Convert.ToInt32(item.Cantidad) != 0) {
                        producto.Stock -= Convert.ToInt32(item.Cantidad);

                        _pedidosRepository.Update(producto);
                    }

                   
                        for (int i = 0; i < detallepedido.CantidadReserva; i++)
                        {
                            var detalleSolicitud = new DetalleSolicitud()
                            {
                                Solicitudes = nuevasolicitud,
                                ProduccionId = 1, // Alistado
                                ReservasProduc = detallepedido,
                                Observacion = observaciones[index],
                            };

                            _pedidosRepository.AgregarSinGuargarDetalleSolicitud(detalleSolicitud);
                        }
                    index++;

                }
               
                _pedidosRepository.Guardar();
                result = "¡SE HA INSERTADO CON ÉXITO!";
            }


            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}
