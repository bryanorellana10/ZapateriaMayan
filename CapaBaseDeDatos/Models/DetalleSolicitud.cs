using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class DetalleSolicitud
    {
        public int Id { get; set; }

        public int SolicitudesId { get; set; } 
        public int ProduccionId { get; set; } // Area
        public int ReservasProducId { get; set; }

        public string Observacion { get; set; }

        public bool Prioridad { get; set; }
        public bool EnBodega { get; set; }

        // Mostrar segun Area
        // atrasos para Alistados
        public bool FaltaDeTextil { get; set; }
        public bool FaltaDeHilo { get; set; }
        public bool FaltaDeForro { get; set; }
        public bool OtrosAlistados { get; set; }

        //atrasos para Ensuelados
        public bool FaltaDeSuela { get; set; }
        public bool FaltaDePegamento { get; set; }
        public bool CorteEnMalEstado { get; set; }
        public bool OtrosEnsuelados { get; set; }


        public Solicitudes Solicitudes { get; set; }
        public Produccion Produccion { get; set; }
        public ReservasProduc ReservasProduc { get; set; }

    }
}
