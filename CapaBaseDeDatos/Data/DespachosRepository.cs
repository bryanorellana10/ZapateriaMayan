using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaBaseDeDatos.Models;

namespace CapaBaseDeDatos.Data
{
    public class DespachosRepository
    {
        private Context _contexts = null;

        public DespachosRepository(Context context)
        {

            _contexts = context;
        }
        public void Add(DespachosCapital model)
        {
            _contexts.DespachosCapitals.Add(model);
            _contexts.SaveChanges();
        }



        public IList<DespachosCapital> DespachosCapitalesGet()
        {
            return _contexts.DespachosCapitals.Where(s => s.EstadoCapitalDespacho == true).ToList();

        }

        public IList<DespachosInterior> DespachosInteriorGetI()
        {
            return _contexts.DespachosInteriors.Where(s => s.EstadoInteriorDespacho == true).ToList();

        }

        public DespachosCapital GetDatos(int id, bool incluideRels)
        {
            var despachos = _contexts.DespachosCapitals.AsQueryable();

            if (incluideRels)
            {
                despachos = despachos
                    .Include(s => s.Pedido);

            }
            return despachos.Where(s => s.Id == id).SingleOrDefault();

        }



        public IList<DespachosCapital> ListaCapitalesDesp()
        {
            return _contexts.DespachosCapitals
                .Include(s => s.Pedido)
                //.Where(d => d.Pedido.Region == "Capital")
                .ToList();
        }

        // interior
        public IList<EnviosInterior> ListaInternosDespachos()
        {
            return _contexts.EnviosInteriors
                .Include(d => d.DespachosInterior)
                .Include(d => d.EstadoEnvioInterior)
                .OrderByDescending(a => a.DespachosInterior.InicioInterior)
                .ToList();
        }

        public IList<EnviosInterior> ListaPendientesDeRecoleccion()
        {
            return _contexts.EnviosInteriors
                .Include(d => d.DespachosInterior)
                .Include(d => d.EstadoEnvioInterior)
                .Where(d => d.EstadoEnvioInterior.Estado == "Pendiente de recoleccion")
                .OrderByDescending(a => a.DespachosInterior.InicioInterior)
                .ToList();
        }

        public IList<EnviosInterior> ListaRecolectados()
        {
            return _contexts.EnviosInteriors
                .Include(d => d.DespachosInterior)
                .Include(d => d.EstadoEnvioInterior)
                .Where(d => d.EstadoEnvioInterior.Estado == "Recolectado")
                .OrderByDescending(a => a.DespachosInterior.InicioInterior)
                .ToList();
        }

        public IList<EnviosInterior> ListaEntregadosInterior()
        {
            return _contexts.EnviosInteriors
                .Include(d => d.DespachosInterior)
                .Include(d => d.EstadoEnvioInterior)
                .Where(d => d.EstadoEnvioInterior.Estado == "Entregado")
                .OrderByDescending(a => a.DespachosInterior.InicioInterior)
                .ToList();
        }

        public IList<EnviosInterior> ListaDevolucionInterior()
        {
            return _contexts.EnviosInteriors
                .Include(d => d.DespachosInterior)
                .Include(d => d.EstadoEnvioInterior)
                .Where(d => d.EstadoEnvioInterior.Estado == "Devolucion")
                .OrderByDescending(a => a.DespachosInterior.InicioInterior)
                .ToList();
        }

        public IList<EnviosInterior> ListaLiquidadosInterior()
        {
            return _contexts.EnviosInteriors
                .Include(d => d.DespachosInterior)
                .Include(d => d.EstadoEnvioInterior)
                .Where(d => d.EstadoEnvioInterior.Estado == "Liquidado")
                .OrderByDescending(a => a.DespachosInterior.InicioInterior)
                .ToList();
        }


        // capital

        public IList<EnviosCapital> ListaCapitalDespachos()
        {
            return _contexts.EnviosCapitals
                .Include(d => d.DespachosCapital)
                .Include(d => d.EstadoEnvioCapital)
                .OrderByDescending(a => a.DespachosCapital.InicioCapital)
                .ToList();
        }

        public IList<EnviosCapital> ListaCapitalDespachosEsperandoEnviar()
        {
            return _contexts.EnviosCapitals
                .Include(d => d.DespachosCapital)
                .Include(d => d.EstadoEnvioCapital)
                .Where(d => d.EstadoEnvioCapital.Estado == "Esperando Enviar")
                .OrderByDescending(a => a.DespachosCapital.InicioCapital)
                .ToList();
        }

        public IList<EnviosCapital> ListaCapitalEsperandoEntrega()
        {
            return _contexts.EnviosCapitals
                .Include(d => d.DespachosCapital)
                .Include(d => d.EstadoEnvioCapital)
                .Where(d => d.EstadoEnvioCapital.Estado == "Esperando Entrega")
                .OrderByDescending(a => a.DespachosCapital.InicioCapital)
                .ToList();
        }

        public IList<EnviosCapital> ListaCapitalEntregados()
        {
            return _contexts.EnviosCapitals
                .Include(d => d.DespachosCapital)
                .Include(d => d.EstadoEnvioCapital)
                .Where(d => d.EstadoEnvioCapital.Estado == "Entregado")
                .OrderByDescending(a => a.DespachosCapital.InicioCapital)
                .ToList();
        }

        public IList<EnviosCapital> ListaCapitalDevolucion()
        {
            return _contexts.EnviosCapitals
                .Include(d => d.DespachosCapital)
                .Include(d => d.EstadoEnvioCapital)
                .Where(d => d.EstadoEnvioCapital.Estado == "Devolucion")
                .OrderByDescending(a => a.DespachosCapital.InicioCapital)
                .ToList();
        }

        public IList<EnviosCapital> ListaCapitalLiquidados()
        {
            return _contexts.EnviosCapitals
                .Include(d => d.DespachosCapital)
                .Include(d => d.EstadoEnvioCapital)
                .Where(d => d.EstadoEnvioCapital.Estado == "Liquidado")
                .OrderByDescending(a => a.DespachosCapital.InicioCapital)
                .ToList();
        }

        public void Add(EnviosInterior enviosInterior, bool guardar = true)
        {
            _contexts.EnviosInteriors.Add(enviosInterior);

            if (guardar)
            {
                _contexts.SaveChanges();

            }

        }

        public void Add(EnviosCapital enviosCapital, bool guardar = true)
        {
            _contexts.EnviosCapitals.Add(enviosCapital);

            if (guardar)
            {
                _contexts.SaveChanges();

            }

        }

        public bool VerificarSiExisteEnDespachoCapital(int PedidoId)
        {
            return _contexts.DespachosCapitals.Any(a => a.PedidoId == PedidoId);
        }

        public bool VerificarSiExisteEnDespachoInterior(int PedidoId)
        {
            return _contexts.DespachosInteriors.Any(a => a.PedidoId == PedidoId);
        }


        public void ModificarSinGuardar(EnviosInterior enviosInterior)
        {
            _contexts.Entry(enviosInterior).State = EntityState.Modified;

        }

        public void ModificarSinGuardar(EnviosCapital enviosCapital)
        {
            _contexts.Entry(enviosCapital).State = EntityState.Modified;

        }

        public void ModificarSinGuardar(DespachosInterior interior)
        {
            _contexts.Entry(interior).State = EntityState.Modified;

        }

        public void ModificarSinGuardar(DespachosCapital capital)
        {
            _contexts.Entry(capital).State = EntityState.Modified;

        }


        public void Add(VentasInterior ventasInterior, bool guardar = true)
        {
            _contexts.VentasInteriors.Add(ventasInterior);

            if (guardar)
            {
                _contexts.SaveChanges();

            }

        }

        public void Add(VentasCapital ventasCapital, bool guardar = true)
        {
            _contexts.VentasCapitals.Add(ventasCapital);

            if (guardar)
            {
                _contexts.SaveChanges();

            }

        }

        public IList<EnviosInterior> ListaLiquidados()
        {
            return _contexts.EnviosInteriors
                .Include(d => d.DespachosInterior)
                .Include(d => d.EstadoEnvioInterior)
                .Where(a => a.EstadoEnvioInterior.Estado == "Liquidado")
                .ToList();
        }
    }
}
