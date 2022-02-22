using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZapateriaMayan.ViewModels
{
    public class CajaCapitalViewModel : CajaBaseViewModel
    {
        public CajaCapitalViewModel()
        {
            CajaCapital.FechaApertura = DateTime.Now;
        }


        public int Id { get; set; }

        public IList<CajaCapital> CajaCapitalsList { get; set; }
        public CajaCapital CajaCapital { get; set; } = new CajaCapital();
        public DetalleCajaCapital DetalleCajaCapital { get; set; } = new DetalleCajaCapital();
    }
}