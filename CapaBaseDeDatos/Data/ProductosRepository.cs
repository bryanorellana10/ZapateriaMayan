using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Data
{
    public class ProductosRepository
    {

        private Context _contexts = null;

        public ProductosRepository(Context context)
        {

            _contexts = context;
        }

        public void Add(Producto model)
        {
            _contexts.Productos.Add(model);
            _contexts.SaveChanges();

        }

        public IList<Producto> GetList()
        {
            return _contexts.Productos
                .Include(c => c.Categoria)
                .Include(c=> c.DetalleTallas)
                .Include(c => c.DetalleTallas.Select(m => m.Talla))
                .Where(c => c.Estado == true).ToList();


        }
        public IList<Categoria> GetListCategoria()
        {
            return _contexts.Categorias.ToList();

        }


        public Producto Get(int id, bool incluideRelatedEntities = true)
        {
            var productos = _contexts.Productos.AsQueryable();

            if (incluideRelatedEntities)
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

        public DetalleTalla GetTalla(int id)
        {
            var tallas = _contexts.DetalleTallas;

            return tallas.Where(t => t.Id == id).SingleOrDefault();
        }

        public void ModificarProducto(Producto model)
        {

            _contexts.Entry(model).State = EntityState.Modified;
            _contexts.SaveChanges();
        }

        //public ReservasProduc GetTallaReserva(int id)
        //{
        //    var tallas = _contexts.ReservasProducs;

        //    return tallas.Where(t => t.DetalleTallaId == id).SingleOrDefault();
        //}

        public void GuardarReservaProduc(ReservasProduc reservas)
        {
            _contexts.Entry(reservas).State = EntityState.Modified;
        }


        public void AddCategory(Categoria model)
        {
            _contexts.Categorias.Add(model);
            _contexts.SaveChanges();

        }

    }
}
