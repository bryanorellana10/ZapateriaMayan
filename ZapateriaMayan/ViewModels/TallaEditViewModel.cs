using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZapateriaMayan.ViewModels
{
    public class TallaEditViewModel : TallaViewModel
    {
        public int IdProd { get; set; }


        public int Id
        {
            get { return Talla.Id; }
            set { Talla.Id = value; }

        }

        public bool EstadoTalla
        {
            get { return Talla.EstadoTallaa; }
            set { Talla.EstadoTallaa = value; }

        }


        public new int Stock
        {
            get { return DetalleTalla.Stock; }
            set { DetalleTalla.Stock = value; }

        }


    }
}
