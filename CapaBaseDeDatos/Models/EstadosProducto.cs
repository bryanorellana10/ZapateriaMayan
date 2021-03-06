using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class EstadosProducto
    {
        public EstadosProducto()
        {
            Solicitudes = new List<Solicitudes>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Estado { get; set; }


        public ICollection<Solicitudes> Solicitudes { get; set; }
    }
}
