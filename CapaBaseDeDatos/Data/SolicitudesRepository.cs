using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CapaBaseDeDatos.Data
{
    public class SolicitudesRepository
    {
        private Context _context = null;

        public SolicitudesRepository(Context context)
        {
            _context = context; 
        }

        public IList<Solicitudes> ListaSolicitudes()
        {
            return _context.Solicitudes
                .Include(a => a.EstadosProducto)
                .OrderByDescending(a => a.FechaSolicitud).ToList();
        }

        public IList<DetalleSolicitud> Alistado()
        {
            return _context.DetalleSolicituds
                .Include(a => a.Produccion)
                .Include(a => a.Solicitudes.Pedido)
                .Include(a => a.ReservasProduc.DetalleTalla.Producto)
                .Include(a => a.ReservasProduc.DetalleTalla.Talla)
                .Where(a => a.Produccion.Area == "Alistado")
                //.Where(a => a.Solicitudes.EstadosProducto.Estado != "En Espera")
                .Where(a => a.Prioridad == false)
                .OrderByDescending(a => a.Id)
                .ToList();
        }

        public IList <DetalleSolicitud> AlistadosPrioridad()
        {
            return _context.DetalleSolicituds
                .Include(a => a.Produccion)
                .Include(a => a.Solicitudes.Pedido)
                .Include(a => a.ReservasProduc.DetalleTalla.Producto)
                .Include(a => a.ReservasProduc.DetalleTalla.Talla)
                .Where(a => a.Produccion.Area == "Alistado")
                .Where(a => a.Prioridad == true)
                //.Where(a => a.Solicitudes.EstadosProducto.Estado != "En Espera")
                .OrderByDescending(a => a.Id)
                .ToList();
        }

        public IList<DetalleSolicitud> EnsueladosPrioridad()
        {
            return _context.DetalleSolicituds
                .Include(a => a.Produccion)
                .Include(a => a.Solicitudes.Pedido)
                .Include(a => a.ReservasProduc.DetalleTalla.Producto)
                .Include(a => a.ReservasProduc.DetalleTalla.Talla)
                .Where(a => a.Produccion.Area == "Ensuelado")
                .Where(a => a.Prioridad == true)
                .OrderByDescending(a => a.Id)
                .ToList();
        }


        public IList<DetalleSolicitud> Ensuelado()
        {
            return _context.DetalleSolicituds
                .Include(a => a.Produccion)
                .Include(a => a.Solicitudes.Pedido)
                .Include(a => a.ReservasProduc.DetalleTalla.Producto)
                .Include(a => a.ReservasProduc.DetalleTalla.Talla)
                .Where(a => a.Produccion.Area == "Ensuelado")
                .Where(a => a.Prioridad == false)
                .OrderByDescending(a => a.Id)
                .ToList();
        }

        public IList<DetalleSolicitud> EnRevision()
        {
            return _context.DetalleSolicituds
                .Include(a => a.Produccion)
                .Include(a => a.Solicitudes.Pedido)
                .Include(a => a.ReservasProduc.DetalleTalla.Producto)
                .Include(a => a.ReservasProduc.DetalleTalla.Talla)
                .Where(a => a.Produccion.Area == "En Revision")
                .OrderByDescending(a => a.Id)
                .ToList();
        }

        public IList<DetalleSolicitud> EnBodega()
        {
            return _context.DetalleSolicituds
                .Include(a => a.Produccion)
                .Include(a => a.Solicitudes.Pedido)
                .Include(a => a.ReservasProduc.DetalleTalla.Producto)
                .Include(a => a.ReservasProduc.DetalleTalla.Talla)
                .Where(a => a.Produccion.Area == "En Bodega")
                .OrderByDescending(a => a.Id)
                .ToList();
        }


        public Solicitudes GetSolicitudes(int id, bool includeRelated = true)
        {
            var solicitudes = _context.Solicitudes.AsQueryable();

            if(includeRelated)
            {
                solicitudes = solicitudes
                    .Include(s => s.EstadosProducto)
                    .Include(s => s.DetalleSolicituds.Select(a => a.Produccion))
                    .Include(s => s.Pedido.ReservasProducs.Select(a => a.DetalleTalla.Producto))
                    .Include(s => s.Pedido.ReservasProducs.Select(a => a.DetalleTalla.Talla));

            }

            return solicitudes
                .Where(s => s.Id == id).SingleOrDefault();
        }
       
    
        public void ModicarSolicitud(Solicitudes solicitudes)
        {
            _context.Entry(solicitudes).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void ModificarDetalle(DetalleSolicitud detalle, bool savechanges = true)
        {
            _context.Entry(detalle).State = EntityState.Modified;

            if (savechanges)
            {
                _context.SaveChanges();

            }


        }

        public IList<Produccion> GetProduccions_Areas(string tipo)
        {
            var areas = _context.Produccions.AsQueryable();

            switch(tipo)
            {

                case "Alistado":
                    areas = areas.Where(a => a.Area != "En Revision" && a.Area != "En Bodega" && a.Area !="Alistado" && a.Area != "Aceptado" && a.Area != "No Aceptado");
                    break;

                case "Ensuelado":
                    areas = areas.Where(a => a.Area != "En Bodega" && a.Area != "Ensuelado" && a.Area != "Aceptado" && a.Area != "No Aceptado");
                    break;

                case "En Revision":
                    areas = areas.Where(a => a.Area != "En Revision");
                    break;
            }


            return areas
                .OrderBy(a => a.Area)
                .ToList();
        }

        public IList<Produccion> GetProduccions_Areas_pri(string tipo)
        {
            var areas = _context.Produccions.AsQueryable();

            switch (tipo)
            {

                case "Alistado":
                    areas = areas.Where(a => a.Area != "En Revision" && a.Area != "En Bodega" && a.Area != "Alistado" && a.Area != "Aceptado" && a.Area != "No Aceptado");
                    break;

                case "Ensuelado":
                    areas = areas.Where(a => a.Area != "En Bodega" && a.Area != "Ensuelado" && a.Area != "Aceptado" && a.Area != "No Aceptado" && a.Area != "Alsitado");
                    break;

                case "En Revision":
                    areas = areas.Where(a => a.Area != "En Revision");
                    break;
            }


            return areas
                .OrderBy(a => a.Area)
                .ToList();
        }





        public DetalleSolicitud GetDetalleSolicitud (int? id)
        {
            return _context.DetalleSolicituds.Where(d => d.Id == id).SingleOrDefault();
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }

        public ReservasProduc GetReservasProduc(int? id)
        {
            return _context.ReservasProducs.Where(d => d.Id == id).SingleOrDefault();
        }

    }
}
