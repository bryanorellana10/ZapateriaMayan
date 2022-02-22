using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZapateriaMayan.ViewModels
{
    public class TrasladarPedidoViewModel
    {
        public Pedido Id { get; set; }
        public Pedido PedidoActual { get; set; }
    }
}