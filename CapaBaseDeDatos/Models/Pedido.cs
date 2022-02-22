using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class Pedido
    {
        public Pedido()
        {
            DetallePedidos = new List<DetallePedido>();
            Solicitudes = new List<Solicitudes>();
            ReservasProducs = new List<ReservasProduc>();
            DespachosInteriors = new List<DespachosInterior>();
            DespachosCapitals = new List<DespachosCapital>();
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Direccion { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Descuento { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Final { get; set; }
        public string Region { get; set; }
        public string Observacion { get; set; }
        public string ContactoPrincipal { get; set; }
        public string Telefonoc { get; set; }
        public DateTime FechaPedido { get; set; }
        public bool EstadoDespacho { get; set; } // 1 despachado, 0 no despachado
        public bool Estado { get; set; } // 1 eliminado, 0 no eliminado
        public bool EnBorrador { get; set; } // 1 en borrador, 0 no borrador
        public bool Devolucion { get; set; } // 1 devuelto, 0 no devuelto (defecto)

        public string Vendedor { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<DetallePedido> DetallePedidos { get; set; }

        public ICollection<Solicitudes> Solicitudes { get; set; }

        public ICollection<ReservasProduc> ReservasProducs { get; set; }


        public ICollection<DespachosInterior> DespachosInteriors { get; set; }
        public ICollection <DespachosCapital> DespachosCapitals { get; set; }

        public string FechaEntrega { get { return Inicio + "-" + Final; } }


    }
}
