using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaBaseDeDatos.Models;
using System.Data.Entity;


namespace CapaBaseDeDatos.Data
{
    public class RutasRepository
    {
        private Context _context = null;


        public RutasRepository(Context context)
        {

            _context = context;
        }


        public IList<Ruta> RutasList()
        {

            return _context.Rutas
                .Where(a => a.Inactivo == false)
                .OrderBy(a => a.Descripcion)
                .ToList();

        }

        public IList<Despachador> MensajerosList()
        {

            return _context.Despachadors
                .Where(a => a.Estado == false)
                .OrderBy(a => a.Nombres)
                .ToList();

        }

        public void Add(Ruta ruta, bool saveChanges = true)
        {
            _context.Rutas.Add(ruta);

            if(saveChanges)
            {
                _context.SaveChanges();
            }
        }



    }
}

