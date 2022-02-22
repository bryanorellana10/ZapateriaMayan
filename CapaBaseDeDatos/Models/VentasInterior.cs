using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class VentasInterior
    {
        public int Id { get; set; }
        public int DespachosInteriorId { get;set;}
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }

        public DateTime FechaVenta { get; set; }

        public DespachosInterior DespachosInterior { get; set; }
    }
}
