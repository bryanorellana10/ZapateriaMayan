using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Data
{
    public class CajaInteriorRepository
    {
        private readonly Context _context = null;

        public CajaInteriorRepository (Context context)
        {
            _context = context;
        }


        public IList<CajaInterior> GetList()
        {
            return _context.CajaInteriors
                .Include(a => a.DetalleCajaInterior)
                .OrderByDescending(a => a.FechaApertura).ToList();
        }

        public void Add(DetalleCajaInterior detalleCajaInterior, bool saveChanges = true)
        {
            _context.DetalleCajaInteriors.Add(detalleCajaInterior);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public CajaInterior GetCajaInterior(int id, bool includerelated = true)
        {
            var caja = _context.CajaInteriors.AsQueryable();

            if (includerelated)
            {
                caja = caja.Include(a => a.DetalleCajaInterior)
                           .Include(a => a.DetalleCajaInterior.Select(vnt => vnt.VentasInterior));
                    
            }

            return caja.Where(s => s.Id == id).SingleOrDefault();
        }

        public void AddDetail(DetalleCajaInterior detalleCajaInterior, bool saveChanges = true)
        {
            _context.DetalleCajaInteriors.Add(detalleCajaInterior);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }


        public void ModCaja(CajaInterior cajaInterior, bool saveChanges = true)
        {   
            _context.Entry(cajaInterior).State = EntityState.Modified;

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }


        //voy a comer dejo esto para mientras
        // usa repositorio para interior mejor aparte
        //public CajaInterior GetCajaInterior(int id, bool includeRelated = true)
        //{
        //    var caja = _context.CajaInteriors.AsQueryable();
        //}

    }
}
