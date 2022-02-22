using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class ReservasProduc
    {

        public ReservasProduc()
        {
            DetalleSolicituds = new List<DetalleSolicitud>();
        }

        public int Id { get; set; }

        public int PedidoId { get; set; }
        public int DetalleTallaId { get; set; }

        public int CantidadSolicitada { get; set; } // cantidad solicitada.

        public decimal Precio { get; set; }

        public decimal Descuento { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }

        public decimal Cantidad { get; set; } // stock libre

        public decimal CantidadReserva { get; set; } // es la que se reservo

        public Pedido Pedido { get; set; }
        public DetalleTalla DetalleTalla { get; set; }

        public ICollection<DetalleSolicitud> DetalleSolicituds { get; set; }
    }
}
