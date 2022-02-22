using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZapateriaMayan.ViewModels
{
    public class SolicitudesEnsuelado: SolicitudesViewModel
    {
        public IList<DetalleSolicitud> DetalleSolicituds;

        public SelectList AreasPrioridad { get; set; }


        public override void Init(SolicitudesRepository solicitudesRepository)
        {
            base.Init(solicitudesRepository);
            Areas = new SelectList(solicitudesRepository.GetProduccions_Areas("Ensuelado"), "Id", "Area");
            AreasPrioridad = new SelectList(solicitudesRepository.GetProduccions_Areas_pri("Ensuelado"), "Id", "Area");
        }
    }
}