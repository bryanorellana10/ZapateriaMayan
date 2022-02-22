using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaBaseDeDatos.Models
{
    public class Producto
    {
        public Producto()
            {
            DetalleTallas = new List<DetalleTalla>();
            }

        
        
        public int Id { get; set; }

        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "* Este campo es obligatorio.")]
        public string NombreProducto { get; set; }

        public string Modelo { get; set; }
 
        public string Observaciones { get; set; }

        public bool Estado { get; set; }
      
        public string Imagen { get; set; }

        [Required(ErrorMessage = "* Este campo es obligatorio.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "* Este campo es obligatorio.")]

        public decimal PrecioOferta { get; set; }
        public DateTime FechaCreacion { get; set; }

     
        public Categoria Categoria { get; set; }

        public string Categoria1 { get; set; }

        public string Categoria2 { get; set; }

        public ICollection<DetalleTalla> DetalleTallas { get; set; }
    }
}
