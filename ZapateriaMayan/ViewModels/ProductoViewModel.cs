using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;

namespace ZapateriaMayan.ViewModels
{
    public class ProductoViewModel
    {

        public SelectList ListaCategorias { get; set; }
        public Producto Producto { get; set; } = new Producto();
        [Required(ErrorMessage = "*Se necesita una imagen.")]
        public HttpPostedFileBase Image { get; set; }

         

        
        public void Iniciar(ProductosRepository productosRepository)
        {
            ListaCategorias = new SelectList(
               productosRepository.GetListCategoria (), "Id" , "NombreC"

                );


        }

       

    }
}