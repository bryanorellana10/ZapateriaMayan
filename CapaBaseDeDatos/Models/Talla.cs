using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    [MetadataType(typeof(Talla))]
    public class Talla
    {
        public Talla()
        {
            DetalleTallas = new List<DetalleTalla>();

        }

        public int Id { get; set; }

        //[Remote("DisponibilidadTallas", "Inventario", ErrorMessage = "Ya existe la talla.")]
        public string NombreTalla { get; set; }

        public bool EstadoTallaa { get; set; }
        
        public ICollection<DetalleTalla> DetalleTallas { get; set; }

    }
}
