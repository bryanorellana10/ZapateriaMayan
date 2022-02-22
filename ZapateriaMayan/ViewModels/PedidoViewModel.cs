using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZapateriaMayan.ViewModels
{
    public class PedidoViewModel
    {
        public PedidoViewModel()
        {
            Pedido.Inicio = DateTime.Today;
            Pedido.Final = DateTime.Today;
        }


        public ReservasProduc ReservasProduc { get; set; } = new ReservasProduc();
        public Pedido Pedido { get; set; } = new Pedido();
        public Cliente Cliente { get; set; } = new Cliente();
        public Solicitudes Solicitudes { get; set; } = new Solicitudes();

        public SelectList ListaClietnes { get; set; }

        public SelectList TipoContactoList { get; set; }

        public List<Producto> Productos { get; set;}
        public SelectList ListaEstadosPedido { get; set; }

        public List<Pedido> PedidosSegunCliente { get; set; }

        public void Init(ClientesRepository clientesRepository, ProductosRepository productosRepository)
        {
            Productos = new List<Producto>(productosRepository.GetList()) { };


            ListaClietnes = new SelectList(
                clientesRepository.GetList(), "Id", "Nombres"
                );


            TipoContactoList = new SelectList(
                new List<SelectListItem> {
                    new SelectListItem{ Text = "Facebook", Value = "Facebook"},
                    new SelectListItem{ Text = "Instagram", Value = "Instagram"},
                    new SelectListItem{ Text = "Whatsapp", Value = "Whatsapp"},
                    new SelectListItem{ Text = "Teléfono", Value = "Teléfono"},

                }, "Value", "Text"
                );
        }

        public void CargarPedidosSegunCliente(PedidosRepository pedidosRepository, int clienteId, int pedidoId)
        {
            PedidosSegunCliente = new List<Pedido>(pedidosRepository.PedidosSegunCliente(clienteId, pedidoId));
        }

       

    }
}