using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CapaBaseDeDatos.Data
{
    public class PedidosRepository
    {
        private Context _context = null;

        public PedidosRepository(Context context)
        {
            _context = context;
        }

        // pedidos de hoy
        public IList<Pedido> PedidosRecientes()
        {
            return _context.Pedidos.Where(p => p.FechaPedido == DateTime.Today).ToList();
        }

        //public IList<BorradorPedidos> BorradorPedidosList()
        //{
           // return _context.BorradorPedidos.Include(s => s.Pedido.Cliente).ToList();
        //}



        public IList<Pedido> ListaPedidos()
        {
            return _context.Pedidos
                .Include(d => d.DetallePedidos.Select(s => s.DetalleTalla))
                .Include(d => d.Cliente)
                .Where(d => (d.EnBorrador == false && d.Estado == false))
                .OrderByDescending(d => d.FechaPedido)
                .ToList();
        }

        public IList<Pedido> ListaCapitales(bool showCancel = false)
        {
            return _context.Pedidos
                    .Include(d => d.DetallePedidos.Select(s => s.DetalleTalla))
                    .Include(d => d.Cliente)
                    .Where(d => d.Region == "Capital")
                    .Where(d => d.EnBorrador == false)
                    .Where(d => d.Estado == showCancel)
                    .OrderByDescending(d => d.FechaPedido)
                    .ToList();
            
        }

        public IList<Pedido> ListaInternos(bool showCancel = false)
        {
            return _context.Pedidos
                    .Include(d => d.DetallePedidos.Select(s => s.DetalleTalla))
                    .Include(d => d.Cliente)
                    .Where(d => d.Region == "Interior")
                    .Where(d => d.EnBorrador == false)
                    .Where(d => d.Estado == showCancel)
                    .OrderByDescending(d => d.FechaPedido)
                    .ToList();
            
        }

        public IList<Pedido> EnBorrador()
        {
            return _context.Pedidos
               .Include(d => d.DetallePedidos.Select(s => s.DetalleTalla))
               .Include(d => d.Solicitudes.Select(a => a.DetalleSolicituds))                             
               .Include(d => d.Cliente)
               //.Where(d => d.Region == "Interior")
               .Where(d => d.EnBorrador == true)
               .OrderByDescending(d => d.FechaPedido)
               .ToList();
        }

        public void AgregarSinGuardar(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
        }

        public void ModificarSinGuardar(Pedido pedido)
        {
            _context.Entry(pedido).State = EntityState.Modified;

        }

        public void AgregarSinGuardar(DetallePedido detallePedido)
        { 
            _context.DetallePedidos.Add(detallePedido);

        }

        public void AgregarSinGuardar(ReservasProduc detalleReserva)
        {
            _context.ReservasProducs.Add(detalleReserva);

        }

        public void AgregarSolicitud(Solicitudes solicitudes)
        {
            _context.Solicitudes.Add(solicitudes);
        }


        public void AgregarSinGuargarDetalleSolicitud(DetalleSolicitud detalleSolicitud)
        {
            _context.DetalleSolicituds.Add(detalleSolicitud);
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }

        public Pedido Get(int id, bool incluideRelatedEntities = true)
        {
            var pedidos = _context.Pedidos.AsQueryable();

            if (incluideRelatedEntities)
            {
                pedidos = pedidos
                    .Include(s => s.Cliente)
                    .Include(s => s.DetallePedidos.Select(t => t.DetalleTalla).Select(p => p.Producto))
                    .Include(s => s.DetallePedidos.Select(t => t.DetalleTalla).Select(p => p.Talla))
                    .Include(s => s.DespachosInteriors.Select(t => t.EnviosInteriors.Select(a => a.EstadoEnvioInterior)))
                    .Include(s => s.DespachosCapitals.Select(t => t.EnviosCapitals.Select(a => a.EstadoEnvioCapital)))
                    .Include(s => s.DespachosCapitals.Select(t => t.EnviosCapitals.Select(a => a.Rutas)))
                .Include(s => s.DespachosCapitals.Select(t => t.EnviosCapitals.Select(a => a.Despachador)));



            }

            return pedidos.Where(s => s.Id == id).SingleOrDefault();
        }

        public Pedido GetPedidoDetalle(int id, bool incluideRelatedEntities = true)
        {
            var pedidos = _context.Pedidos.AsQueryable();

            if (incluideRelatedEntities)
            {
                pedidos = pedidos
                    .Include(s => s.Cliente)
                    .Include(s => s.DetallePedidos.Select(t => t.DetalleTalla).Select(p => p.Producto))
                    .Include(s => s.DetallePedidos.Select(t => t.DetalleTalla).Select(p => p.Talla))
                    .Include(s => s.Cliente)
                    .Include(s => s.Solicitudes.Select(a => a.EstadosProducto))
                    .Include(s => s.Solicitudes.Select(a => a.DetalleSolicituds.Select(z => z.Produccion)))
                    .Include(s => s.ReservasProducs.Select(a => a.DetalleTalla.Producto))
                    .Include(s => s.ReservasProducs.Select(a => a.DetalleTalla.Talla));
                
                //.Include(s => s.Solicitudes.Select(a => a.DetalleSolicituds))
                //.Include(s => s.Solicitudes.Select(a => a.DetalleSolicituds.Select(x => x.EstadosProduccion)))
                //.Include(s => s.Solicitudes.Select(a => a.DetalleSolicituds.Select(x => x.Produccion)));

            }

            return pedidos.Where(s => s.Id == id).SingleOrDefault();
        }

        public Pedido GetPedidoReservaPro(int id)
        {
            return _context.Pedidos
                .Include(a => a.ReservasProducs)
                .Include(a => a.Solicitudes.Select(z => z.DetalleSolicituds))
                .Where(a => a.Id == id)
                .SingleOrDefault();
        }


        public IList<Pedido> PedidosSegunCliente(int clienteId, int pedidoId) 
        {
            var pedidos = _context.Pedidos.AsQueryable();

            return pedidos.Include(s => s.Cliente).Where(c => c.Cliente.Id == clienteId)
                .Where(i => i.Id != pedidoId && i.EnBorrador == false && i.EstadoDespacho == false)
                .OrderByDescending(a => a.FechaPedido)
                .ToList();
        }

        public DetalleTalla GetDT(int id)
        {
            return _context.DetalleTallas.Where(s => s.Id == id).SingleOrDefault();
        }

        public void ModificarDT(DetalleTalla detalleTalla)
        {
            _context.Entry(detalleTalla).State = EntityState.Modified;

        }

        public void Update(DetalleTalla entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

        }

        public void ModificarPedido(Pedido entity, bool guardar = true)
        {
            _context.Entry(entity).State = EntityState.Modified;

            if (guardar)
            {
                _context.SaveChanges();

            }

        }

        public void EliminarPedido(Pedido entity, bool guardar = true)
        {
            _context.Entry(entity).State = EntityState.Deleted;

            if (guardar)
            {
                _context.SaveChanges();

            }

        }
    }
}
