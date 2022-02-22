using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class DetalleCajaCapital
    {
        public int Id { get; set; }
        public int CajaCapitalId { get; set; }
        public int? VentasCapitalId { get; set; }
        public string Descripcion { get; set; }

        public bool Entrega { get; set; }

        public decimal Gasto { get; set; }
        public decimal Ingreso { get; set; }
        public string Responsable { get; set; } 


        public CajaCapital CajaCapital { get; set; }
        public VentasCapital VentasCapital { get; set; }

    }
}
