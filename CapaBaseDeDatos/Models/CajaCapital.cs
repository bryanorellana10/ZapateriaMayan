using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class CajaCapital
    {
        public CajaCapital()
        {
            DetalleCajaCapitals = new List<DetalleCajaCapital>();
        }


        public int Id { get; set; }
        [Required(ErrorMessage = "*Se necesita un monto de apertura.")]
        public decimal MontoApertura { get; set; }
        public decimal? MontoCierre { get; set; } // creo que es ineccesario, veamos que pasa
        public string Responsable { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }
        public bool EstadoCaja { get; set; }


        public ICollection<DetalleCajaCapital> DetalleCajaCapitals { get; set; }
    }
}
