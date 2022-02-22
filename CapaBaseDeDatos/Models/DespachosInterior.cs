using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class DespachosInterior
    {

        public DespachosInterior()
        {
            EnviosInteriors = new List<EnviosInterior>();
            VentasInteriors = new List<VentasInterior>();
        }

        public int Id { get; set; }
        public int PedidoId { get; set; }

        [Required(ErrorMessage ="*Se requiere un número de guía")]
        public string NumeroGuia { get; set; }
        public decimal Peso { get; set; }
        public DateTime InicioInterior { get; set; }

        public string Comentario { get; set; }
        public bool EstadoInteriorDespacho { get; set; }
        public bool Liquidado { get; set; }
        public Pedido Pedido { get; set; }

        public ICollection<EnviosInterior> EnviosInteriors { get; set; }
        public ICollection<VentasInterior> VentasInteriors { get; set; }

    }

    }
