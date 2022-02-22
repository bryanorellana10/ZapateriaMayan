using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZapateriaMayan.ViewModels
{
    public class SolicitudesAlistado : SolicitudesViewModel
    {
        public IList<DetalleSolicitud> DetalleSolicituds;

        public override void Init(SolicitudesRepository solicitudesRepository)
        {
            base.Init(solicitudesRepository);
            Areas = new SelectList(solicitudesRepository.GetProduccions_Areas("Alistado"), "Id", "Area");
        }
        
    }
} 