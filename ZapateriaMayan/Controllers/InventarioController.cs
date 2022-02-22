using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using ZapateriaMayan.ViewModels;

namespace ZapateriaMayan.Controllers
{
    public class InventarioController : Controller
    {
        private ProductosRepository _productosRepository = null;
        private TallaRepository _tallaRepository = null;


        public InventarioController(ProductosRepository productosRepository, TallaRepository tallaRepository)
        {

            _productosRepository = productosRepository;
            _tallaRepository = tallaRepository;

        }


        // GET: Inventario
        public ActionResult Productos()
        {
            
            var listaProductos = _productosRepository.GetList();
            return View(listaProductos);
        }


        public ActionResult NuevoProducto()
        {
            var viewModel = new ProductoViewModel();
            viewModel.Iniciar(_productosRepository);
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult NuevoProducto(ProductoViewModel viewModel)
        {

            string path = "";
            HttpPostedFileBase archivo = Request.Files["Image"];

            if (ModelState.IsValid)
            {

                if (archivo != null && archivo.ContentLength > 0)
                {
                    path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(archivo.FileName));
                    archivo.SaveAs(path);

                }

                if (ModelState.IsValid) 
                { 
                var producto = viewModel.Producto;
                producto.FechaCreacion = DateTime.Now;
                producto.Estado = true;
                producto.Imagen = viewModel.Image.FileName;
                _productosRepository.Add(producto);
                TempData["Message"] = "¡El Producto se ha INSERTADO con éxito!";
                return RedirectToAction("Productos");
                }
            }

            viewModel.Iniciar(_productosRepository);
            return View(viewModel);

        }

        public ActionResult DetallesProducto(int? id )
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = _productosRepository.Get((int)id, incluideRelatedEntities: true);

            if (producto == null)
            {
                return HttpNotFound();
            }


            var viewModel = new ProductoViewModel()
            {

                Producto = producto
            };

            return View(producto);
  


        }

        public ActionResult TallasAgregar(int idProd)
        {
            var viewModel = new TallaViewModel()
            {
                Ids = idProd
                
            };


            return View(viewModel);

        }

        [HttpPost]
        public ActionResult TallasAgregar(TallaViewModel viewModel) 
        {
            ModelState.Clear();


            //evitar que se repitan las tallas en la insercion


            //for (int i = 0; i < 9; i++)
            //{
            //    for (int j = i + 1; j < 8; j++)
            //    {
            //        if (viewModel.Tallitas[j].NombreTalla == null)
            //        {
            //            continue;
            //        }


            //        if (viewModel.Tallitas[i].NombreTalla == viewModel.Tallitas[j].NombreTalla)
            //        {
            //            ModelState.AddModelError("", "Repetida ");

            //        }
            //    }
            //}


            var tallitas = _tallaRepository.VerificarExTalla(viewModel.Ids);

            //var tall = tallitas.DetalleTallas.Where(a => a.Talla.EstadoTallaa == false);

            for (int i = 0; i < viewModel.Tallitas.Count; i++)
            {


                if (viewModel.Tallitas[i].NombreTalla == null)
                {
                    continue;
                }

                //if (_tallaRepository.VerificarExTalla(viewModel.Tallitas[i], viewModel.Ids))
                //{
                //    ModelState.AddModelError("Tallitas[" + i + "].NombreTalla", "Ya existe la talla " + viewModel.Tallitas[i].NombreTalla);
                //}

                foreach (var talla in tallitas.DetalleTallas.Where(a => a.Talla.EstadoTallaa == false))
                {
                    if (talla.Talla.NombreTalla == viewModel.Tallitas[i].NombreTalla)
                    {
                        ModelState.AddModelError("Tallitas[" + i + "].NombreTalla", "Ya existe la talla " + viewModel.Tallitas[i].NombreTalla);
                    }
                }

            }

            if (ModelState.IsValid)
            {
                for (int i = 0; i < viewModel.Tallitas.Count; i++)
                {
                    var talla = viewModel.Tallitas[i];

                    if (talla.NombreTalla == null)
                    {
                        continue;
                    }


                    _tallaRepository.AddSinGuardar(talla);


                    var detalletalla = new DetalleTalla()
                    {
                        ProductoId = viewModel.Ids,
                        Stock = viewModel.Detallitos[i].Stock,
                        Talla = talla
                    };

                    _tallaRepository.DetalleSinGuardar(detalletalla);


                }
                _tallaRepository.Guardar();

            }
            else
            {
                return View(viewModel);
            }

            return RedirectToAction("DetallesProducto", new { id = viewModel.Ids });

        }


        [HttpPost]
        public ActionResult agregarCategoria(int id,string nombre)
        {
            var ctg = new Categoria()
            {
                Id = id,
                NombreC = nombre
            };

            _productosRepository.AddCategory(ctg);

            return Json("prueba", JsonRequestBehavior.AllowGet);

        }




        //public JsonResult DisponibilidadTallas([Bind(Prefix = "Talla.NombreTalla")] string NombreTalla)
        //{
        //    return Json(!_tallaRepository.VerificarExTalla(NombreTalla) , JsonRequestBehavior.AllowGet);
        //} 


        public ActionResult ModificarProducto(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = _productosRepository.Get((int)id, incluideRelatedEntities: false);

            if (producto == null)
            {
                return HttpNotFound();
            }

            //necesito capturar los  datos y almacenarlos en viewmodel
            var viewModel = new ProductoEditViewModel()
            {
                Producto = producto
            };
            viewModel.Iniciar(_productosRepository);

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult ModificarProducto(ProductoEditViewModel viewModel)
        {
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                var producto = viewModel.Producto;
                _productosRepository.ModificarProducto(producto);
                TempData["Message"] = "¡El Producto se ha MODIFICADO con éxito!";
                return RedirectToAction("DetallesProducto", "Inventario", new {viewModel.Id});

            }

            viewModel.Iniciar(_productosRepository);


            return View(viewModel);
        }

        public ActionResult EliminarProducto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = _productosRepository.Get((int)id, incluideRelatedEntities: false);

            if (producto == null)
            {
                return HttpNotFound();
            }

            //necesito capturar los  datos y almacenarlos en viewmodel
            var viewModel = new ProductoEditViewModel()
            {
                Producto = producto
            };


            return View(viewModel);
        }

        public ActionResult Eliminar(int? id)
        {
            var producto = _productosRepository.Get((int)id);
            producto.Estado = false;
            _productosRepository.ModificarProducto(producto);
            TempData["Message"] = "¡El Producto se ha ELIMINADO con éxito!";
            return  RedirectToAction("Productos", "Inventario");

        }


      // verificar las existencias, este no es un controlador no use ningun ActionResult
      //simplemente lo use como llamada de AJAX

        public JsonResult VerificarExistencias(int id, int cantidad)
        {
            int cant = Math.Abs(cantidad);
            var talla = _productosRepository.GetTalla(id);
            var resultante = 0;

            if (cant > talla.Stock)
            {
                resultante = cant - talla.Stock;
                return Json(resultante, JsonRequestBehavior.AllowGet);
            }
            else 
            {
            return Json(resultante, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult RetornarExistencia(int valuee)
        {
            var talla = _productosRepository.GetTalla(valuee);
            return Json( talla.Stock);
        }
        public ActionResult ModificarTalla(int? id, int idProd)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var talla = _tallaRepository.GetTa((int)id, incluideRelatedEntites: true);

            if (talla == null)
            {
                return HttpNotFound();
            }
            //necesito capturar los  datos y almacenarlos en viewmodel
            var viewModel = new TallaEditViewModel()
            {
                Talla = talla,
                IdProd = idProd
            };


            return View(viewModel);

        }

        [HttpPost]
        public ActionResult ModificarTalla(TallaEditViewModel viewModel)
        {

            //    var tallitas = _tallaRepository.VerificarExTalla(viewModel.IdProd);

            //    foreach (var talla in tallitas.DetalleTallas)
            //    {
            //        if (talla.Talla.NombreTalla == viewModel.Talla.NombreTalla)
            //        {
            //            ModelState.AddModelError("NombreTalla", "Ya existe la talla ");
            //            break;
            //        }
            //    }

            //bool pobrecito = _tallaRepository.verificarExistenciasMod(viewModel.IdProd);

            if (ModelState.IsValid)
            {
                _tallaRepository.ModificarTalla(viewModel.Talla, false);

                var getid = _tallaRepository.GetSt(viewModel.Talla.Id);
                getid.Stock = viewModel.Stock;

                _tallaRepository.ModificarTalla2(getid);

                _tallaRepository.Guardar();

                //_tallaRepository.ModificarTalla(talla, nuevaTalla);
                TempData["Message"] = "¡La Talla del Producto se ha MODIFICADO con éxito!";
                return RedirectToAction("DetallesProducto", new { id = viewModel.IdProd});

            }

            return View(viewModel);
        }

        //Eliminar Tallas

        public ActionResult EliminarTalla(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var talla = _tallaRepository.GetTa((int)id, incluideRelatedEntites: false);

            if (talla == null)
            {
                return HttpNotFound();
            }

            var viewModel = new TallaEditViewModel()
            {
                Talla = talla
            };

            return View(viewModel);
        }


        public ActionResult EliminarT(int? id)
        {
            var talla = _tallaRepository.GetTa((int)id);
            talla.EstadoTallaa = true;
            _tallaRepository.ModificarTalla3(talla);
            TempData["Message"] = "¡La Talla se ha ELIMINADO con éxito!";
            return RedirectToAction("Productos", "Inventario");
        }
    }
}