using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Data
{
    public class TallaRepository
    {
        private Context _context = null;

        public TallaRepository(Context context)
        {
             
            _context = context;
        }

        public void AddSinGuardar(Talla model)
        {
            _context.Tallas.Add(model);
        }

        public void DetalleSinGuardar(DetalleTalla model)
        {
            _context.DetalleTallas.Add(model);

        }

        public DetalleTalla GetStock( int id, bool incluideRelatedEntites = true)
        {
            var stock = _context.DetalleTallas.AsQueryable();

            if (incluideRelatedEntites)
            {



                stock = stock

                    .Include(s => s.Talla);


            }


            return stock
                .Where(s => s.Id == id).SingleOrDefault();

        }
        public Talla GetTa(int id, bool incluideRelatedEntites = true)
        {

            var tallas = _context.Tallas.AsQueryable();


            if (incluideRelatedEntites)
            {
                tallas = tallas
                    .Include(s => s.DetalleTallas.Select(a => a.Talla));
            }


            return tallas
                .Where(s => s.Id == id).SingleOrDefault();

        }


        public DetalleTalla GetSt(int id)
        {
            return _context.DetalleTallas.Where(s => s.TallaId == id).SingleOrDefault();
        }



        public void Guardar ()
        {
            _context.SaveChanges();
        }


        public Producto VerificarExTalla(int id, bool related = true)
        {
            var productos = _context.Productos.AsQueryable();

            if (related)
            {
                productos = productos
                    .Include(s => s.Categoria)
                    .Include(s => s.DetalleTallas)
                    .Include(s => s.DetalleTallas.Select(a => a.Talla));
            }

            return productos
                .Where(s => s.Id == id)
                .SingleOrDefault();

        }

        //public bool verificarExistenciasMod(int id)
        //{
        //    return _context.Productos
        //            .Include(s => s.Categoria)
        //            .Include(s => s.DetalleTallas)
        //            .Include(s => s.DetalleTallas.Select(a => a.Talla))
        //            .Where(s => s.Id == id).FirstOrDefault(s => s.DetalleTallas.Select(a => a.Talla.NombreTalla) == " ")
                    
                    
        //}



        public void ModificarTalla3(Talla model, bool save = true)
        {

            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void ModificarTalla2(DetalleTalla model, bool save = true)
        {

            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }



        public void ModificarTalla(Talla model, bool guardar = true)
        {

            _context.Entry(model).State = EntityState.Modified;

            if(guardar)
            { 
               
                _context.SaveChanges();

            }

      
            
        }

    }
}
