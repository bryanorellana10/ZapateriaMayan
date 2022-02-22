using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class EnviosInterior
    {
        public int Id { get; set; }
        public int EstadoEnvioInteriorId { get; set; }
        public int DespachosInteriorId { get; set; }


        public EstadoEnvioInterior EstadoEnvioInterior { get; set; }

        public DespachosInterior  DespachosInterior {get; set;}
    }
}
