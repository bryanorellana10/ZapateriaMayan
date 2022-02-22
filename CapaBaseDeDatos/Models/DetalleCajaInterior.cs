using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class DetalleCajaInterior
    {
        public int Id { get; set; }
        public int CajaInteriorId { get; set; }
        public int? VentasInteriorId { get; set; }
        public string Descripcion { get; set; }

        public bool Entrega { get; set; }

        public decimal Gasto { get; set; }
        public decimal Ingreso { get; set; }



        public CajaInterior CajaInterior { get; set; }
        public VentasInterior VentasInterior { get; set; }

    }
}
