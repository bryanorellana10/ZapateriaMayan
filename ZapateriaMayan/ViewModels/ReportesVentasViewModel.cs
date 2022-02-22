using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZapateriaMayan.ViewModels
{
    public class ReportesVentasViewModel : ReportesBaseViewModel
    {
        public IList<VentasCapital> ventasCapitals;
        public IList<VentasInterior> ventasInteriors;

    }
}