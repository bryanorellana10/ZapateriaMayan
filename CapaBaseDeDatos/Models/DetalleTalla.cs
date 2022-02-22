using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class DetalleTalla
    {
        public DetalleTalla()
        {
            DetallePedidos = new List<DetallePedido>();
            ReservasProducs = new List<ReservasProduc>();
        }

        public int Id { get; set; }

        public int ProductoId { get; set; }

        public int TallaId { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "No se permite negativos.")]
        public int Stock { get; set; }

        public Talla Talla { get; set; }
        public Producto Producto { get; set; }
        public ICollection<DetallePedido> DetallePedidos { get; set; }
        public ICollection<ReservasProduc> ReservasProducs { get; set; }
    }
}
