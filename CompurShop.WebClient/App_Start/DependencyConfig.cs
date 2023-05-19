using CompurShop.Domain.Interfaces;
using CompurShop.Domain.Services;
using CompurShop.Infra.Data;
using CompurShop.Infra.Data.Repositories;
using System.Web;
using Unity;

namespace CompurShop.WebClient.App_Start
{
    public static class DependencyConfig
    {
        private static readonly UnityContainer container;
        static DependencyConfig()
        {
            container = new UnityContainer();

            // Registrando as dependências
            container.RegisterType<ScireDbContext>();


            container.RegisterType<IClienteRepository, ClienteRepository>();
            container.RegisterType<ClienteService>();

            container.RegisterType<IListaReporsitory, ListaReporsitory>();
            container.RegisterType<ListaService>();

            container.RegisterType<ICpfsRepository, CpfsRepository>();
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}


