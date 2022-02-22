using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class Despachador
    {
        public Despachador()
        {
            EnviosCapitals = new List<EnviosCapital>();
        }

        public int Id { get; set; }
        public string Nombres { get; set; }
        public bool Estado { get; set; }

        public ICollection<EnviosCapital> EnviosCapitals { get; set; }
    }
}
