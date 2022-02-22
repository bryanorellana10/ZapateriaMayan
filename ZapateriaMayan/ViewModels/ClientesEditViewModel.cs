using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZapateriaMayan.ViewModels
{
    public class ClientesEditViewModel : ClienteViewModel
    {
        //public ClientesEditViewModel(DateTime time)
        //{
        //    FechaCreacions = time;
        //}

        public int Id
        {
            get { return Cliente.Id; }
            set { Cliente.Id = value; }
        }

        public DateTime FechaCreacions
        {
            get { return Cliente.FechaCreacion; }
            set { Cliente.FechaCreacion = value; }
        }

        public bool Estado
        {
            get { return Cliente.Estado; }
            set { Cliente.Estado = value; }
        }
    }
}