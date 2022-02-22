using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZapateriaMayan.ViewModels
{
    public class SolicitudesViewModel
    {
        public SolicitudesViewModel()
        {

        }


        public Solicitudes Solicitudes { get; set; } = new Solicitudes();
        public EstadosProduccion EstadosProduccion { get; set; } = new EstadosProduccion();

    
        public SelectList Areas { get; set; }

        public IList<DetalleSolicitud> DetalleSolicitud { get; set; }



        public string EstadoDetalle { get; set; }

        public virtual void Init(SolicitudesRepository solicitudesRepository)
        {
        }
    
    
    }


}