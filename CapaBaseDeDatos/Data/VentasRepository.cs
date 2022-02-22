using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CapaBaseDeDatos.Data
{
    public class VentasRepository
    {
        private Context _context = null;

        public VentasRepository(Context context)
        {
            _context = context;
        }

        public IList<VentasInterior> GetList()
        {
            return _context.VentasInteriors
                .Include(a => a.DespachosInterior.Pedido)
                .OrderByDescending(a => a.FechaVenta).ToList();
        }

        public IList<VentasCapital> GetListC()
        {
            return _context.VentasCapitals
                .Include(a => a.DespachosCapital.Pedido)
                .OrderByDescending(a => a.FechaVenta).ToList();
        }

        public IList<VentasCapital> GetListCapitalRangos(DateTime Inicio, DateTime Final)
        {
            return _context.VentasCapitals
                .Include(a => a.DespachosCapital.Pedido)
                .OrderByDescending(a => a.FechaVenta)
                .Where(a => a.FechaVenta > Inicio && a.FechaVenta < Final)
                .ToList();
        }

        public IList<VentasInterior> GetListInteriorRangos(DateTime Inicio, DateTime Final)
        {
            return _context.VentasInteriors
                .Include(a => a.DespachosInterior.Pedido)
                .OrderByDescending(a => a.FechaVenta)
                .Where(a => a.FechaVenta > Inicio && a.FechaVenta < Final)
                .ToList();
        }
    }
}
