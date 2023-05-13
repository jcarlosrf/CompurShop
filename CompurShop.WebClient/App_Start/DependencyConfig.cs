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
            container.RegisterType<IClienteRepository, ClienteRepository>();
            container.RegisterType<ClienteDbContext>();
            container.RegisterType<ClienteService>();

            // Registrando o container no contexto do aplicativo
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}


