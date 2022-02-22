using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class EstadoEnvioInterior
    {
        public EstadoEnvioInterior()
        {
            EnviosInteriors = new List<EnviosInterior>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Estado { get; set; }

        public ICollection<EnviosInterior> EnviosInteriors { get; set; }
    }
}
