using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaBaseDeDatos.Models
{
    public class DespachosCapital
    {
        public DespachosCapital()
        {
            EnviosCapitals = new List<EnviosCapital>();
            VentasCapitals = new List<VentasCapital>();
        }

        public int Id { get; set; }
        public int PedidoId { get; set; }
        //public string NumeroGuia { get; set; }
        public bool EstadoCapitalDespacho { get; set; }
        public string Comentario { get; set; }
        public DateTime InicioCapital { get; set; }
        public bool Liquidado { get; set; } 
        public Pedido Pedido { get; set; }

        public ICollection<EnviosCapital> EnviosCapitals { get; set; }
        public ICollection<VentasCapital> VentasCapitals { get; set; }


    }
}
