using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZapateriaMayan.ViewModels
{
    public class CajaInteriorViewModel
    {
        public CajaInteriorViewModel()
        {
            CajaInterior.FechaApertura = DateTime.Now;
        }


        public IList<CajaInterior> CajaInteriorsList { get; set; }
        public CajaInterior CajaInterior { get; set; } = new CajaInterior();
    }
}