using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class Cliente
    {
        public Cliente()
        {
            Pedidos = new List<Pedido>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "* Este campo es obligatorio.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "* Este campo es obligatorio.")]
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        [Required(ErrorMessage = "* Este campo es obligatorio.")]
        public string Direccion { get; set; }
        public string Whatsapp { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        [Required(ErrorMessage = "* Este campo es obligatorio.")]
        public string Region { get; set; }
        public bool Estado { get; set; }
        public string ContactoPrincipal { get; set; }
        public DateTime FechaCreacion { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }


        // de aqui para abajo no seran campos de bd
        //public string NombreYApellidos { get { return Nombres + " " + Apellidos; } }


    }
}
