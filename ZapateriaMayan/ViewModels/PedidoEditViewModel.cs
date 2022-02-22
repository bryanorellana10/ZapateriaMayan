using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZapateriaMayan.ViewModels
{
    public class PedidoEditViewModel : PedidoViewModel
    {
        public int Id
        {
            get { return Pedido.Id; }
            set { Pedido.Id = value; }
        }

    }
}