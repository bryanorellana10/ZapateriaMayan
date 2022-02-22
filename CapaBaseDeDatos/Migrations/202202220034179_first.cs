namespace CapaBaseDeDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CajaCapitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MontoApertura = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontoCierre = c.Decimal(precision: 18, scale: 2),
                        Responsable = c.String(),
                        FechaApertura = c.DateTime(nullable: false),
                        FechaCierre = c.DateTime(),
                        EstadoCaja = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetalleCajaCapitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CajaCapitalId = c.Int(nullable: false),
                        VentasCapitalId = c.Int(),
                        Descripcion = c.String(),
                        Entrega = c.Boolean(nullable: false),
                        Gasto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ingreso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Responsable = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CajaCapitals", t => t.CajaCapitalId, cascadeDelete: true)
                .ForeignKey("dbo.VentasCapitals", t => t.VentasCapitalId)
                .Index(t => t.CajaCapitalId)
                .Index(t => t.VentasCapitalId);
            
            CreateTable(
                "dbo.VentasCapitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DespachosCapitalId = c.Int(nullable: false),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaVenta = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DespachosCapitals", t => t.DespachosCapitalId, cascadeDelete: true)
                .Index(t => t.DespachosCapitalId);
            
            CreateTable(
                "dbo.DespachosCapitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        EstadoCapitalDespacho = c.Boolean(nullable: false),
                        Comentario = c.String(),
                        InicioCapital = c.DateTime(nullable: false),
                        Liquidado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pedidoes", t => t.PedidoId, cascadeDelete: true)
                .Index(t => t.PedidoId);
            
            CreateTable(
                "dbo.EnviosCapitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DespachosCapitalId = c.Int(nullable: false),
                        EstadoEnvioCapitalId = c.Int(nullable: false),
                        RutasId = c.Int(nullable: false),
                        DespachadorId = c.Int(nullable: false),
                        EgresoEntrega = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Despachadors", t => t.DespachadorId, cascadeDelete: true)
                .ForeignKey("dbo.DespachosCapitals", t => t.DespachosCapitalId, cascadeDelete: true)
                .ForeignKey("dbo.EstadoEnvioCapitals", t => t.EstadoEnvioCapitalId, cascadeDelete: true)
                .ForeignKey("dbo.Rutas", t => t.RutasId, cascadeDelete: true)
                .Index(t => t.DespachosCapitalId)
                .Index(t => t.EstadoEnvioCapitalId)
                .Index(t => t.RutasId)
                .Index(t => t.DespachadorId);
            
            CreateTable(
                "dbo.Despachadors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombres = c.String(),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EstadoEnvioCapitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rutas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Inactivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        Direccion = c.String(),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Inicio = c.DateTime(nullable: false),
                        Final = c.DateTime(nullable: false),
                        Region = c.String(),
                        Observacion = c.String(),
                        ContactoPrincipal = c.String(),
                        Telefonoc = c.String(),
                        FechaPedido = c.DateTime(nullable: false),
                        EstadoDespacho = c.Boolean(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        EnBorrador = c.Boolean(nullable: false),
                        Devolucion = c.Boolean(nullable: false),
                        Vendedor = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombres = c.String(nullable: false),
                        Telefono1 = c.String(nullable: false),
                        Telefono2 = c.String(),
                        Direccion = c.String(nullable: false),
                        Whatsapp = c.String(),
                        Facebook = c.String(),
                        Instagram = c.String(),
                        Region = c.String(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        ContactoPrincipal = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DespachosInteriors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        NumeroGuia = c.String(nullable: false),
                        Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InicioInterior = c.DateTime(nullable: false),
                        Comentario = c.String(),
                        EstadoInteriorDespacho = c.Boolean(nullable: false),
                        Liquidado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pedidoes", t => t.PedidoId, cascadeDelete: true)
                .Index(t => t.PedidoId);
            
            CreateTable(
                "dbo.EnviosInteriors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EstadoEnvioInteriorId = c.Int(nullable: false),
                        DespachosInteriorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DespachosInteriors", t => t.DespachosInteriorId, cascadeDelete: true)
                .ForeignKey("dbo.EstadoEnvioInteriors", t => t.EstadoEnvioInteriorId, cascadeDelete: true)
                .Index(t => t.EstadoEnvioInteriorId)
                .Index(t => t.DespachosInteriorId);
            
            CreateTable(
                "dbo.EstadoEnvioInteriors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VentasInteriors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DespachosInteriorId = c.Int(nullable: false),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaVenta = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DespachosInteriors", t => t.DespachosInteriorId, cascadeDelete: true)
                .Index(t => t.DespachosInteriorId);
            
            CreateTable(
                "dbo.DetallePedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        DetalleTallaId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DetalleTallas", t => t.DetalleTallaId, cascadeDelete: true)
                .ForeignKey("dbo.Pedidoes", t => t.PedidoId, cascadeDelete: true)
                .Index(t => t.PedidoId)
                .Index(t => t.DetalleTallaId);
            
            CreateTable(
                "dbo.DetalleTallas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductoId = c.Int(nullable: false),
                        TallaId = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Productoes", t => t.ProductoId, cascadeDelete: true)
                .ForeignKey("dbo.Tallas", t => t.TallaId, cascadeDelete: true)
                .Index(t => t.ProductoId)
                .Index(t => t.TallaId);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoriaId = c.Int(nullable: false),
                        NombreProducto = c.String(nullable: false),
                        Modelo = c.String(),
                        Observaciones = c.String(),
                        Estado = c.Boolean(nullable: false),
                        Imagen = c.String(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecioOferta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaCreacion = c.DateTime(nullable: false),
                        Categoria1 = c.String(),
                        Categoria2 = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NombreC = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReservasProducs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        DetalleTallaId = c.Int(nullable: false),
                        CantidadSolicitada = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cantidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CantidadReserva = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DetalleTallas", t => t.DetalleTallaId, cascadeDelete: true)
                .ForeignKey("dbo.Pedidoes", t => t.PedidoId, cascadeDelete: true)
                .Index(t => t.PedidoId)
                .Index(t => t.DetalleTallaId);
            
            CreateTable(
                "dbo.DetalleSolicituds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SolicitudesId = c.Int(nullable: false),
                        ProduccionId = c.Int(nullable: false),
                        ReservasProducId = c.Int(nullable: false),
                        Observacion = c.String(),
                        Prioridad = c.Boolean(nullable: false),
                        EnBodega = c.Boolean(nullable: false),
                        FaltaDeTextil = c.Boolean(nullable: false),
                        FaltaDeHilo = c.Boolean(nullable: false),
                        FaltaDeForro = c.Boolean(nullable: false),
                        OtrosAlistados = c.Boolean(nullable: false),
                        FaltaDeSuela = c.Boolean(nullable: false),
                        FaltaDePegamento = c.Boolean(nullable: false),
                        CorteEnMalEstado = c.Boolean(nullable: false),
                        OtrosEnsuelados = c.Boolean(nullable: false),
                        ReservasProduc_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produccions", t => t.ProduccionId, cascadeDelete: true)
                .ForeignKey("dbo.ReservasProducs", t => t.ReservasProducId)
                .ForeignKey("dbo.Solicitudes", t => t.SolicitudesId, cascadeDelete: true)
                .ForeignKey("dbo.ReservasProducs", t => t.ReservasProduc_Id)
                .Index(t => t.SolicitudesId)
                .Index(t => t.ProduccionId)
                .Index(t => t.ReservasProducId)
                .Index(t => t.ReservasProduc_Id);
            
            CreateTable(
                "dbo.Produccions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Area = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Solicitudes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        EstadosProductoId = c.Int(nullable: false),
                        FechaSolicitud = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EstadosProductoes", t => t.EstadosProductoId, cascadeDelete: true)
                .ForeignKey("dbo.Pedidoes", t => t.PedidoId, cascadeDelete: true)
                .Index(t => t.PedidoId)
                .Index(t => t.EstadosProductoId);
            
            CreateTable(
                "dbo.EstadosProductoes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tallas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreTalla = c.String(),
                        EstadoTallaa = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CajaInteriors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MontoApertura = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontoCierre = c.Decimal(precision: 18, scale: 2),
                        Responsable = c.String(),
                        FechaApertura = c.DateTime(nullable: false),
                        FechaCierre = c.DateTime(),
                        EstadoCaja = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetalleCajaInteriors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CajaInteriorId = c.Int(nullable: false),
                        VentasInteriorId = c.Int(),
                        Descripcion = c.String(),
                        Entrega = c.Boolean(nullable: false),
                        Gasto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ingreso = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CajaInteriors", t => t.CajaInteriorId, cascadeDelete: true)
                .ForeignKey("dbo.VentasInteriors", t => t.VentasInteriorId)
                .Index(t => t.CajaInteriorId)
                .Index(t => t.VentasInteriorId);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        IsInactive = c.Boolean(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.DetalleCajaInteriors", "VentasInteriorId", "dbo.VentasInteriors");
            DropForeignKey("dbo.DetalleCajaInteriors", "CajaInteriorId", "dbo.CajaInteriors");
            DropForeignKey("dbo.DetalleCajaCapitals", "VentasCapitalId", "dbo.VentasCapitals");
            DropForeignKey("dbo.VentasCapitals", "DespachosCapitalId", "dbo.DespachosCapitals");
            DropForeignKey("dbo.DetallePedidoes", "PedidoId", "dbo.Pedidoes");
            DropForeignKey("dbo.DetalleTallas", "TallaId", "dbo.Tallas");
            DropForeignKey("dbo.ReservasProducs", "PedidoId", "dbo.Pedidoes");
            DropForeignKey("dbo.ReservasProducs", "DetalleTallaId", "dbo.DetalleTallas");
            DropForeignKey("dbo.DetalleSolicituds", "ReservasProduc_Id", "dbo.ReservasProducs");
            DropForeignKey("dbo.Solicitudes", "PedidoId", "dbo.Pedidoes");
            DropForeignKey("dbo.Solicitudes", "EstadosProductoId", "dbo.EstadosProductoes");
            DropForeignKey("dbo.DetalleSolicituds", "SolicitudesId", "dbo.Solicitudes");
            DropForeignKey("dbo.DetalleSolicituds", "ReservasProducId", "dbo.ReservasProducs");
            DropForeignKey("dbo.DetalleSolicituds", "ProduccionId", "dbo.Produccions");
            DropForeignKey("dbo.DetalleTallas", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.Productoes", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.DetallePedidoes", "DetalleTallaId", "dbo.DetalleTallas");
            DropForeignKey("dbo.VentasInteriors", "DespachosInteriorId", "dbo.DespachosInteriors");
            DropForeignKey("dbo.DespachosInteriors", "PedidoId", "dbo.Pedidoes");
            DropForeignKey("dbo.EnviosInteriors", "EstadoEnvioInteriorId", "dbo.EstadoEnvioInteriors");
            DropForeignKey("dbo.EnviosInteriors", "DespachosInteriorId", "dbo.DespachosInteriors");
            DropForeignKey("dbo.DespachosCapitals", "PedidoId", "dbo.Pedidoes");
            DropForeignKey("dbo.Pedidoes", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.EnviosCapitals", "RutasId", "dbo.Rutas");
            DropForeignKey("dbo.EnviosCapitals", "EstadoEnvioCapitalId", "dbo.EstadoEnvioCapitals");
            DropForeignKey("dbo.EnviosCapitals", "DespachosCapitalId", "dbo.DespachosCapitals");
            DropForeignKey("dbo.EnviosCapitals", "DespachadorId", "dbo.Despachadors");
            DropForeignKey("dbo.DetalleCajaCapitals", "CajaCapitalId", "dbo.CajaCapitals");
            DropIndex("dbo.IdentityUserLogins", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropIndex("dbo.DetalleCajaInteriors", new[] { "VentasInteriorId" });
            DropIndex("dbo.DetalleCajaInteriors", new[] { "CajaInteriorId" });
            DropIndex("dbo.Solicitudes", new[] { "EstadosProductoId" });
            DropIndex("dbo.Solicitudes", new[] { "PedidoId" });
            DropIndex("dbo.DetalleSolicituds", new[] { "ReservasProduc_Id" });
            DropIndex("dbo.DetalleSolicituds", new[] { "ReservasProducId" });
            DropIndex("dbo.DetalleSolicituds", new[] { "ProduccionId" });
            DropIndex("dbo.DetalleSolicituds", new[] { "SolicitudesId" });
            DropIndex("dbo.ReservasProducs", new[] { "DetalleTallaId" });
            DropIndex("dbo.ReservasProducs", new[] { "PedidoId" });
            DropIndex("dbo.Productoes", new[] { "CategoriaId" });
            DropIndex("dbo.DetalleTallas", new[] { "TallaId" });
            DropIndex("dbo.DetalleTallas", new[] { "ProductoId" });
            DropIndex("dbo.DetallePedidoes", new[] { "DetalleTallaId" });
            DropIndex("dbo.DetallePedidoes", new[] { "PedidoId" });
            DropIndex("dbo.VentasInteriors", new[] { "DespachosInteriorId" });
            DropIndex("dbo.EnviosInteriors", new[] { "DespachosInteriorId" });
            DropIndex("dbo.EnviosInteriors", new[] { "EstadoEnvioInteriorId" });
            DropIndex("dbo.DespachosInteriors", new[] { "PedidoId" });
            DropIndex("dbo.Pedidoes", new[] { "ClienteId" });
            DropIndex("dbo.EnviosCapitals", new[] { "DespachadorId" });
            DropIndex("dbo.EnviosCapitals", new[] { "RutasId" });
            DropIndex("dbo.EnviosCapitals", new[] { "EstadoEnvioCapitalId" });
            DropIndex("dbo.EnviosCapitals", new[] { "DespachosCapitalId" });
            DropIndex("dbo.DespachosCapitals", new[] { "PedidoId" });
            DropIndex("dbo.VentasCapitals", new[] { "DespachosCapitalId" });
            DropIndex("dbo.DetalleCajaCapitals", new[] { "VentasCapitalId" });
            DropIndex("dbo.DetalleCajaCapitals", new[] { "CajaCapitalId" });
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.DetalleCajaInteriors");
            DropTable("dbo.CajaInteriors");
            DropTable("dbo.Tallas");
            DropTable("dbo.EstadosProductoes");
            DropTable("dbo.Solicitudes");
            DropTable("dbo.Produccions");
            DropTable("dbo.DetalleSolicituds");
            DropTable("dbo.ReservasProducs");
            DropTable("dbo.Categorias");
            DropTable("dbo.Productoes");
            DropTable("dbo.DetalleTallas");
            DropTable("dbo.DetallePedidoes");
            DropTable("dbo.VentasInteriors");
            DropTable("dbo.EstadoEnvioInteriors");
            DropTable("dbo.EnviosInteriors");
            DropTable("dbo.DespachosInteriors");
            DropTable("dbo.Clientes");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Rutas");
            DropTable("dbo.EstadoEnvioCapitals");
            DropTable("dbo.Despachadors");
            DropTable("dbo.EnviosCapitals");
            DropTable("dbo.DespachosCapitals");
            DropTable("dbo.VentasCapitals");
            DropTable("dbo.DetalleCajaCapitals");
            DropTable("dbo.CajaCapitals");
        }
    }
}
