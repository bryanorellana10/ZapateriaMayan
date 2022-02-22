using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class Ruta
    {
        public Ruta()
        {
            EnviosCapitals = new List<EnviosCapital>();
        }

        public int Id { get; set; }
        public string Descripcion{ get; set; }
        public decimal Costo { get; set; }
        public bool Inactivo { get; set; }
        

        public ICollection<EnviosCapital> EnviosCapitals { get; set; }
    }
}
