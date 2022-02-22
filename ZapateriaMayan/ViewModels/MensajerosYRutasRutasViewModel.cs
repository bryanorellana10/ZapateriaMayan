using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZapateriaMayan.ViewModels
{
    public class MensajerosYRutasRutasViewModel : MensajerosYRutasBaseViewModel
    {
        public Ruta Ruta { get; set; } = new Ruta();

        public int Id
        {

            get { return Ruta.Id; }
            set { Ruta.Id = value; }
        }
    }
}