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
    public class CajaCapitalRepository
    {
        private readonly Context _context = null;

        public CajaCapitalRepository (Context context)
        {
            _context = context;
        }


        public IList<CajaCapital> GetList()
        {
            return _context.CajaCapitals
                .Include(a => a.DetalleCajaCapitals)                
                .OrderByDescending(a => a.FechaApertura)
                .ToList();
        }


        public void Add(DetalleCajaCapital detalleCajaCapital, bool saveChanges = true)
        {
            _context.DetalleCajaCapitals.Add(detalleCajaCapital);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }



        public CajaCapital Getcajacapital(int id, bool includerelated = true)
        {
            var caja = _context.CajaCapitals.AsQueryable();

            if (includerelated)
            {
                caja = caja.Include(a => a.DetalleCajaCapitals.Select(z => z.VentasCapital.DespachosCapital.EnviosCapitals.Select(c => c.Despachador)))
                    .Include(a => a.DetalleCajaCapitals.Select(z => z.VentasCapital.DespachosCapital.Pedido))
                    .Include(a => a.DetalleCajaCapitals.Select(z => z.VentasCapital.DespachosCapital.Pedido.Cliente))
                    .Include(a => a.DetalleCajaCapitals.Select(z => z.VentasCapital.DespachosCapital.EnviosCapitals.Select(c => c.Rutas)));
            }

            return caja.Where(s => s.Id == id).SingleOrDefault();
        }

        public DetalleCajaCapital GetcajacapitalDetalle(int id)
        {

            return _context.DetalleCajaCapitals.Where(s => s.Id == id).SingleOrDefault();
        }

        public void DeleteDetalleCajaCapital(int id, bool saveChanges = true)
        {
            var set = _context.Set<DetalleCajaCapital>();
            var entity = set.Find(id);
            set.Remove(entity);
            if (saveChanges)
            {
                _context.SaveChanges();

            }
        }


        public void Update(CajaCapital cajaCapital, bool saveChanges = true)
        {
            _context.Entry(cajaCapital).State = EntityState.Modified;

            if (true)
            {
                _context.SaveChanges();

            }

        }

        public void UpdateDetail(DetalleCajaCapital detallecajaCapital, bool saveChanges = true)
        {
            _context.Entry(detallecajaCapital).State = EntityState.Modified;

            if (true)
            {
                _context.SaveChanges();

            }

        }



    }
}
