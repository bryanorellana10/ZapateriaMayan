using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaBaseDeDatos.Models
{
     public class Categoria
    {

        public Categoria()
        {
            Productos = new List<Producto>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] // hace que no sea automatico pero desps lo quitamos OK
        public int Id { get; set; }

        public string NombreC { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}
