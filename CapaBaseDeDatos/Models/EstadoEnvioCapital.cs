using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class EstadoEnvioCapital
    {
        public EstadoEnvioCapital()
        {
            EnviosCapitals = new List<EnviosCapital>();
        }

        public int Id { get; set; }
        public string Estado { get; set; }

        public ICollection<EnviosCapital> EnviosCapitals { get; set; }
    }
}
