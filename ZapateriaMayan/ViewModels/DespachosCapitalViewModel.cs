using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZapateriaMayan.ViewModels
{
    public class DespachosCapitalViewModel : DespachosBaseViewModel
    {
        public EnviosCapital EnviosCapital { get; set; } = new EnviosCapital();
        public DespachosCapital DespachosCapital { get; set; } = new DespachosCapital();

        public Despachador Despachador { get; set; } = new Despachador();

        public SelectList ListaRutas { get; set; }
        public SelectList ListaMensajeros { get; set; }


        public void Iniciar(RutasRepository rutasRepository)
        {
            ListaRutas = new SelectList(
                rutasRepository.RutasList(), "Id", "Descripcion"
                );

            ListaMensajeros = new SelectList(
                rutasRepository.MensajerosList(), "Id", "Nombres"
                );

        }
    }
}