using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;



namespace ZapateriaMayan.ViewModels
{
    public class DespachosBaseViewModel
    {

        public DespachosBaseViewModel()
        {

        }

        public Pedido Pedido { get; set; } = new Pedido();

        public int Id
        {
            get { return Pedido.Id; }
            set { Pedido.Id = value; }

        }


        public int GetId => Pedido.DetallePedidos.Select(a => a.PedidoId).SingleOrDefault();


    }
}