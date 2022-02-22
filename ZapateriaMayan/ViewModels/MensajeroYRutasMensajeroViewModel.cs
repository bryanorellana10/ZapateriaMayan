using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZapateriaMayan.ViewModels
{
    public class MensajeroYRutasMensajeroViewModel: MensajerosYRutasBaseViewModel
    {
        public Despachador Despachador { get; set; } = new Despachador();

        public int Id
        {

            get { return Despachador.Id; }
            set { Despachador.Id = value; }
        }
    }
}