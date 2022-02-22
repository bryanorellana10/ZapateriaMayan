using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZapateriaMayan.ViewModels
{
    public class ProductoEditViewModel :ProductoViewModel
    {


        public int Id
        {

            get { return Producto.Id; }
            set { Producto.Id = value; }
        }

        public DateTime FechaCreacions
        {

            get { return Producto.FechaCreacion; }
            set { Producto.FechaCreacion = value; }
        }

        public bool Estado
        {

            get { return Producto.Estado; }
            set { Producto.Estado = value; }
        }
    }
}