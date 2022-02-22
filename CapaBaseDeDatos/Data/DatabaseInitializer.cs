using CapaBaseDeDatos.Models;
using CapaBaseDeDatos.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Data
{
    internal class DatabaseInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            // categorias
            // categorias

            var userStore = new UserStore<User>(context);
            var roleStore = new RoleStore<Role>(context);

            var userManager = new ApplicationUserManager(userStore);
            var roleManager = new ApplicationRoleManager(roleStore);


            var Role1 = new Role
            {
                Name = "Administrador"
            };
            roleManager.Create(Role1);
            var Role2 = new Role
            {
                Name = "Vendedor"
            };
            roleManager.Create(Role2);
            var Role3 = new Role
            {
                Name = "Alistador"
            };
            roleManager.Create(Role3);
            var Role4 = new Role
            {
                Name = "Ensuelador"
            };
            roleManager.Create(Role4);
            var Role5 = new Role
            {
                Name = "Despacho/Bodega"
            };
            roleManager.Create(Role5);
            var Role6 = new Role
            {
                Name = "Envios Capital"
            };
            roleManager.Create(Role6);
            var Role7 = new Role
            {
                Name = "Envios Interior"
            };
            roleManager.Create(Role7);

            var Role8 = new Role
            {
                Name = "Seguimientos"
            };
            roleManager.Create(Role8);



            var admin1 = new User()
            {
                UserName = "admin@sistema.com",
                Name = "Administrador"
            };

            userManager.Create(admin1, "Maya1821");
            var currentUser = userManager.FindByName(admin1.UserName);
            userManager.AddToRole(currentUser.Id, Role1.Name);

            var categoria1 = new Categoria()
            {
                Id = 1,
                NombreC = "Caballero"
            };

            context.Categorias.AddOrUpdate(categoria1);


            var categoria2 = new Categoria()
            {
                Id = 2,
                NombreC = "Dama"
            };

            context.Categorias.AddOrUpdate(categoria2);


            var categoria3 = new Categoria()
            {
                Id = 3,
                NombreC = "Niño"
            };

            context.Categorias.AddOrUpdate(categoria3);

            var categoria4 = new Categoria()
            {
                Id = 4,
                NombreC = "Niña"
            };

            context.Categorias.AddOrUpdate(categoria4);



            // Areas

            var area1 = new Produccion()
            {
                Id = 1,
                Area = "Alistado"
            };

            context.Produccions.AddOrUpdate(area1);

            var area2 = new Produccion()
            {
                Id = 2,
                Area = "Ensuelado"
            };

            context.Produccions.AddOrUpdate(area2);

            var area3 = new Produccion()
            {
                Id = 3,
                Area = "En Revision"
            };

            context.Produccions.AddOrUpdate(area3);

            var area4 = new Produccion()
            {
                Id = 4,
                Area = "En Bodega"
            };

            context.Produccions.AddOrUpdate(area4);

            var area5 = new Produccion()
            {
                Id = 5,
                Area = "Aceptado"
            };

            context.Produccions.AddOrUpdate(area5);

            var area6 = new Produccion()
            {
                Id = 6,
                Area = "No Aceptado"
            };

            context.Produccions.AddOrUpdate(area6);


            ////Estado de cada producto

            //var estadosolicitud1 = new EstadosProduccion()
            //{
            //    Id = 1,
            //    Estado = "En Espera"
            //};

            //context.EstadosProduccions.AddOrUpdate(estadosolicitud1);


            //var estadosolicitud2 = new EstadosProduccion()
            //{
            //    Id = 2,
            //    Estado = "En Produccion"
            //};

            //context.EstadosProduccions.AddOrUpdate(estadosolicitud2);

            //var estadosolicitud3 = new EstadosProduccion()
            //{
            //    Id = 3,
            //    Estado = "Demorado"
            //};

            //context.EstadosProduccions.AddOrUpdate(estadosolicitud3);


            //var estadosolicitud4 = new EstadosProduccion()
            //{
            //    Id = 4,
            //    Estado = "Con Problema"
            //};

            //context.EstadosProduccions.AddOrUpdate(estadosolicitud4);

            //var estadosolicitud5 = new EstadosProduccion()
            //{
            //    Id = 5,
            //    Estado = "Confirmado"
            //};

            //context.EstadosProduccions.AddOrUpdate(estadosolicitud5);


            //var estadosolicitud6 = new EstadosProduccion()
            //{
            //    Id = 6,
            //    Estado = "Finalizado"
            //};

            //context.EstadosProduccions.AddOrUpdate(estadosolicitud6);



            // estados de las solicitudes 

            var estadosproducto1 = new EstadosProducto()
            {
                Id = 1,
                Estado = "En espera"

            };

            context.EstadosProductos.AddOrUpdate(estadosproducto1);


            var estadosproducto2 = new EstadosProducto()
            {
                Id = 2,
                Estado = "En Produccion"

            };

            context.EstadosProductos.AddOrUpdate(estadosproducto2);


            var estadosproducto3 = new EstadosProducto()
            {
                Id = 3,
                Estado = "Falta de materiales"

            };

            context.EstadosProductos.AddOrUpdate(estadosproducto3);


            var estadosproducto4 = new EstadosProducto()
            {
                Id = 4,
                Estado = "Finalizado"

            };

            context.EstadosProductos.AddOrUpdate(estadosproducto4);


            var estadosproducto5 = new EstadosProducto()
            {
                Id = 5,
                Estado = "Confirmado"

            };

            context.EstadosProductos.AddOrUpdate(estadosproducto5);


            var estadosproducto6 = new EstadosProducto()
            {
                Id = 6,
                Estado = "Finalizado"

            };

            context.EstadosProductos.AddOrUpdate(estadosproducto6);



            // estados envios interior
            var estadoEnvio1 = new EstadoEnvioInterior()
            {
                Id = 1,
                Estado = "Pendiente de recoleccion" // Estado Inicial
            };

            context.EstadoEnviosInteriors.AddOrUpdate(estadoEnvio1);



            var estadoEnvio2 = new EstadoEnvioInterior()
            {
                Id = 2,
                Estado = "Recolectado"
            };

            context.EstadoEnviosInteriors.AddOrUpdate(estadoEnvio2);


            var estadoEnvio3 = new EstadoEnvioInterior()
            {
                Id = 3,
                Estado = "Entregado"
            };

            context.EstadoEnviosInteriors.AddOrUpdate(estadoEnvio3);


            var estadoEnvio4 = new EstadoEnvioInterior()
            {
                Id = 4,
                Estado = "Devolucion"
            };

            context.EstadoEnviosInteriors.AddOrUpdate(estadoEnvio4);

            var estadoEnvio5 = new EstadoEnvioInterior()
            {
                Id = 5,
                Estado = "Liquidado"
            };

            context.EstadoEnviosInteriors.AddOrUpdate(estadoEnvio5);





            // estados envios capital
            var estadoEnvioc1 = new EstadoEnvioCapital()
            {
                Id = 1,
                Estado = "Esperando Enviar" // Estado Inicial
            };

            context.EstadoEnvioCapitals.AddOrUpdate(estadoEnvioc1);



            var estadoEnvioc2 = new EstadoEnvioCapital()
            {
                Id = 2,
                Estado = "Esperando Entrega"
            };

            context.EstadoEnvioCapitals.AddOrUpdate(estadoEnvioc2);


            var estadoEnvioc3 = new EstadoEnvioCapital()
            {
                Id = 3,
                Estado = "Entregado"
            };

            context.EstadoEnvioCapitals.AddOrUpdate(estadoEnvioc3);


            var estadoEnvioc4 = new EstadoEnvioCapital()
            {
                Id = 4,
                Estado = "Devolucion"
            };

            context.EstadoEnvioCapitals.AddOrUpdate(estadoEnvioc4);

            var estadoEnvioc5 = new EstadoEnvioCapital()
            {
                Id = 5,
                Estado = "Liquidado"
            };

            context.EstadoEnvioCapitals.AddOrUpdate(estadoEnvioc5);






            context.SaveChanges();

        }

    }
    
}
