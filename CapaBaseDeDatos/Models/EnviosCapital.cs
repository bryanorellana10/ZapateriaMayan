using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class EnviosCapital
    {
        public int Id { get; set; }

        public int DespachosCapitalId { get; set; }
        public int EstadoEnvioCapitalId { get; set; }

        [Required(ErrorMessage = "* se requiere de una Ruta.")]
        public int RutasId { get; set; }

        [Required(ErrorMessage = "* se requiere de un mensajero.")]
        public int DespachadorId { get; set; }

        public decimal EgresoEntrega { get; set; }

        public DespachosCapital DespachosCapital { get; set; }
        public EstadoEnvioCapital EstadoEnvioCapital { get; set; }
        public Ruta Rutas { get; set; }
        public Despachador Despachador { get; set; }
    }
}
