using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class Solicitudes
    {

        public Solicitudes()
        {
            DetalleSolicituds = new List<DetalleSolicitud>();
        }

        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int EstadosProductoId { get; set; }

        public DateTime FechaSolicitud { get; set; } 
        public Pedido Pedido { get; set; }
        public EstadosProducto EstadosProducto { get; set; }

        public ICollection<DetalleSolicitud> DetalleSolicituds { get; set; }
    }
}
