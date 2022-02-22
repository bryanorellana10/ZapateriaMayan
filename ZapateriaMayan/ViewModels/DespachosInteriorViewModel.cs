using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZapateriaMayan.ViewModels
{
    public class DespachosInteriorViewModel : DespachosBaseViewModel
    {
        public DespachosInterior DespachosInterior { get; set; } = new DespachosInterior();

    }
}