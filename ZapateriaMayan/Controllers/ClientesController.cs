using CapaBaseDeDatos.Data;
using System;
using System.Net;
using System.Web.Mvc;
using ZapateriaMayan.ViewModels;

namespace ZapateriaMayan.Controllers
{
    public class ClientesController : Controller
    {
        private ClientesRepository _clienteRepository = null;


        public ClientesController(ClientesRepository clientesRepository)
        {
            _clienteRepository = clientesRepository;
        }

        public ActionResult Contactos()
        {
            var listaClientes = _clienteRepository.GetList(); 
            return View(listaClientes);
        }

        public ActionResult NuevoContacto()
        {
            var viewModel = new ClienteViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult NuevoContacto(ClienteViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var cliente = viewModel.Cliente;
                cliente.FechaCreacion = DateTime.Now;
                _clienteRepository.Add(cliente);
                TempData["Message"] = "¡El cliente se ha INSERTADO con éxito!";
                return RedirectToAction("Contactos");
            }
            return View(viewModel);
        }


        public ActionResult ModificarContacto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var clientes = _clienteRepository.Get((int)id, includeRelatedEntities: false);

            if (clientes == null)
            {
                return HttpNotFound();
            }

            //necesito capturar los  datos y almacenarlos en viewmodel
            var viewModel = new ClientesEditViewModel()
            {
                Cliente = clientes
            };


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ModificarContacto(ClientesEditViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var cliente = viewModel.Cliente;
                _clienteRepository.ModificarCliente(cliente);
                TempData["Message"] = "¡El cliente se ha MODIFICADO con éxito!";
                return RedirectToAction("Contactos", "Clientes");

            }

            return View(viewModel);
        }

        public ActionResult EliminarContacto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var clientes = _clienteRepository.Get((int)id, includeRelatedEntities: false);

            if (clientes == null)
            {
                return HttpNotFound();
            }

            //necesito capturar los  datos y almacenarlos en viewmodel
            var viewModel = new ClientesEditViewModel()
            {
                Cliente = clientes
            };


            return View(viewModel);
        }

        public ActionResult Eliminar(int? id)
        {
            var cliente = _clienteRepository.Get((int)id);
            cliente.Estado = true;
            _clienteRepository.ModificarCliente(cliente);
            TempData["Message"] = "¡El cliente se ha ELIMINADO con éxito!";
            return RedirectToAction("Contactos", "Clientes");

        }

        public JsonResult RetornarDatosCliente(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var cliente = _clienteRepository.Get((int)id, includeRelatedEntities: false);
            return Json(cliente, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtenerListaClientes()
        {
            var lista = _clienteRepository.GetList();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

    }
}