using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace ZapateriaMayan.ViewModels
{
    public class TallaViewModel
    {
        
        public Talla Talla { get; set; } = new Talla();
        public Producto Producto { get; set; } = new Producto();

        public DetalleTalla DetalleTalla { get; set; } = new DetalleTalla();


        public int Ids { get; set; }

        public int  Stock => Talla.DetalleTallas.Select(a => a.Stock).FirstOrDefault();
    


        public List<Talla> Tallitas { get; set; } = new List<Talla>()
        { new Talla() { }, new Talla() { }, new Talla() { } , new Talla() { } , new Talla() { }, new Talla() { }, new Talla() { }, new Talla() { }, new Talla() { }  };


        public List<DetalleTalla> Detallitos { get; set; } = new List<DetalleTalla>()
        { new DetalleTalla() { }, new DetalleTalla() { }, new DetalleTalla() { } , new DetalleTalla() { } , new DetalleTalla() { }, new DetalleTalla() { }, new DetalleTalla() { }, new DetalleTalla() { }, new DetalleTalla() { }  };



       
         
    }
}