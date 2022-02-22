using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Data
{
    public class DashboardRepository
    {
        private Context _context = null;

        public DashboardRepository(Context context)
        {
            _context = context;
        }

        public int VentasHoyInterior()
        {
            return _context.VentasInteriors
                .Where(a => a.FechaVenta == DateTime.Today)
                .Count();
        }
    }
}
