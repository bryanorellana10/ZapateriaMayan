using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Data
{
    public class MensajeroRepository
    {
        private readonly Context _context = null;
        public MensajeroRepository(Context context)
        {
            _context = context;
        }

        public IList<Despachador> GetList()
        {
            return _context.Despachadors
                .Where(a => a.Estado == false)
                .OrderBy(a => a.Nombres).ToList();
        }

        public IList<Ruta> GetListRutas()
        {
            return _context.Rutas
                .Where(a => a.Inactivo == false)
                .OrderBy(a => a.Descripcion).ToList();
        }



        public void Add (Despachador despachador, bool saveChanges = true)
        {
            _context.Despachadors.Add(despachador);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
            
        }

        public void Add(Ruta ruta, bool saveChanges = true)
        {
            _context.Rutas.Add(ruta);

            if (saveChanges)
            {
                _context.SaveChanges();
            }

        }

        public void Modify (Despachador despachador, bool saveChanges = true)
        {
            _context.Entry(despachador).State = System.Data.Entity.EntityState.Modified;

            if(saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void ModifyRuta(Ruta ruta, bool saveChanges = true)
        {
            _context.Entry(ruta).State = System.Data.Entity.EntityState.Modified;

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }


        public Despachador Get(int id)
        {
            return _context.Despachadors.Where(a => a.Id == id).SingleOrDefault();
        }

        public Ruta GetRuta(int id)
        {
            return _context.Rutas.Where(a => a.Id == id).SingleOrDefault();
        }

    }
}
