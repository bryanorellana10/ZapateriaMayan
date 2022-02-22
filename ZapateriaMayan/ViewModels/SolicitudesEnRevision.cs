using CapaBaseDeDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZapateriaMayan.ViewModels
{
    public class SolicitudesEnRevision : SolicitudesViewModel
    {
        public override void Init(SolicitudesRepository solicitudesRepository)
        {
            base.Init(solicitudesRepository);
            Areas = new SelectList(solicitudesRepository.GetProduccions_Areas("En Revision"), "Id", "Area");
        }
    }
}