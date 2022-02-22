using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class VentasCapital
    {
        public VentasCapital()
        {
            DetalleCajaCapitals = new List<DetalleCajaCapital>();
        }

        public int Id { get; set; }
        public int DespachosCapitalId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }

        public DateTime FechaVenta { get; set; }

        public DespachosCapital DespachosCapital { get; set; }


        public ICollection<DetalleCajaCapital> DetalleCajaCapitals { get; set; }
    }
}
