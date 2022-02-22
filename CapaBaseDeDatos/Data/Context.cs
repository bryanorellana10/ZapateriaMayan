using CapaBaseDeDatos.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Data
{
    public class Context : IdentityDbContext<User>
    {
        public Context() : base("Context", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new DatabaseInitializer());
        }

        // aqui creamos las tablas, el contexto se encarga de generar la tabla en la bd

        //tabla cliente
        public DbSet<Cliente> Clientes { get; set; }


        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<ReservasProduc> ReservasProducs { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Talla> Tallas { get; set; }

        public DbSet<DetalleTalla> DetalleTallas { get; set; }

        public DbSet<Solicitudes> Solicitudes { get; set; }

        public DbSet<DetalleSolicitud> DetalleSolicituds { get; set; }

        public DbSet<Produccion> Produccions { get; set; }

        public DbSet<EstadosProducto> EstadosProductos { get; set; }


        // todo sobre despachos
        public DbSet<DespachosInterior> DespachosInteriors { get; set; }
        public DbSet<EstadoEnvioInterior> EstadoEnviosInteriors { get; set; }
        public DbSet<EnviosInterior> EnviosInteriors { get; set; }


        public DbSet<DespachosCapital> DespachosCapitals { get; set; }
        public DbSet<EstadoEnvioCapital> EstadoEnvioCapitals { get; set; }
        public DbSet<EnviosCapital>  EnviosCapitals { get; set; }

        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<Despachador> Despachadors { get; set; }


        //ventas
        public DbSet<VentasInterior> VentasInteriors { get; set; }
        public DbSet<VentasCapital> VentasCapitals { get; set; }


        // caja
        public DbSet<CajaCapital> CajaCapitals { get; set; }
        public DbSet<DetalleCajaCapital> DetalleCajaCapitals { get; set; }

        public DbSet<CajaInterior> CajaInteriors { get; set; }
        public DbSet<DetalleCajaInterior> DetalleCajaInteriors { get; set; }


        // no tocar esto 
        // es critico
        // la BD tiene relaciones circulares.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetalleSolicitud>()
                .HasRequired(c => c.ReservasProduc)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            
        }

    }

}