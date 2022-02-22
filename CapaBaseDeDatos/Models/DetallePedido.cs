using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class DetallePedido
    {

        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int DetalleTallaId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public DetalleTalla DetalleTalla { get; set; }
        public Pedido Pedido { get; set; }

    }
}
