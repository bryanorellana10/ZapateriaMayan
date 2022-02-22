using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Data
{

    public class ClientesRepository
    {
        private Context _contexts = null;

        public ClientesRepository(Context context)
        {
            _contexts = context;
        }

        public void Add(Cliente model)
        {
            _contexts.Clientes.Add(model);
            _contexts.SaveChanges();
        }

        public void AddSinGuardar(Cliente model)
        {
            _contexts.Clientes.Add(model);

        }

        public IList<Cliente> GetList()
        {
            //select * from clientes
            return _contexts.Clientes.Where(c => c.Estado == false).ToList();

        }
        public Cliente Get(int id, bool includeRelatedEntities = true)
        {

            var clientes = _contexts.Clientes.AsQueryable();

            return clientes
                 .Where(s => s.Id == id)
                 .SingleOrDefault();
        }


        public void ModificarCliente (Cliente model)
        {
            _contexts.Entry(model).State = EntityState.Modified;
            _contexts.SaveChanges();

        }
    }
}
