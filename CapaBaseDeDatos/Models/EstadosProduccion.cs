using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class EstadosProduccion
    {
        public EstadosProduccion()
        {
            DetalleSolicituds = new List<DetalleSolicitud>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Estado { get; set; }

        public ICollection<DetalleSolicitud> DetalleSolicituds { get; set; }

    }
}
