[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(ZapateriaMayan.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace ZapateriaMayan.App_Start
{
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using CapaBaseDeDatos.Data;
    using CapaBaseDeDatos.Models;
    using CapaBaseDeDatos.Security;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;

    public class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {

            container.Register<Context>(Lifestyle.Scoped);
            container.Register<ClientesRepository>(Lifestyle.Scoped);
            container.Register<PedidosRepository>(Lifestyle.Scoped);
            container.Register<ProductosRepository>(Lifestyle.Scoped);
            container.Register<TallaRepository>(Lifestyle.Scoped);
            container.Register<SolicitudesRepository>(Lifestyle.Scoped);
            container.Register<DespachosRepository>(Lifestyle.Scoped);
            container.Register<RutasRepository>(Lifestyle.Scoped);
            container.Register<VentasRepository>(Lifestyle.Scoped);
            container.Register<DashboardRepository>(Lifestyle.Scoped);
            container.Register<MensajeroRepository>(Lifestyle.Scoped);
            container.Register<UsersRepository>(Lifestyle.Scoped);
            container.Register<CajaCapitalRepository>(Lifestyle.Scoped);
            container.Register<CajaInteriorRepository>(Lifestyle.Scoped);



            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);

            container.Register(() =>
                container.IsVerifying
                    ? new OwinContext().Authentication
                    : HttpContext.Current.GetOwinContext().Authentication,
                Lifestyle.Scoped);

            container.Register<IUserStore<User>>(() =>
                new UserStore<User>(container.GetInstance<Context>()),
                Lifestyle.Scoped);


            //container.Register<IRoleStore<Role, string>, RoleStore<Role>>(
            // Lifestyle.Scoped);

            //container.Register<IRoleStore<Role>>(() =>
            //    new RoleStore<Role>(container.GetInstance<Context>()),
            //    Lifestyle.Scoped);
        }
    }
}