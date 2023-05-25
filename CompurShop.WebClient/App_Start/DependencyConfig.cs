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


            //servicos
            container.RegisterType<ClienteService>();
            container.RegisterType<ListaService>();
            container.RegisterType<CombosService>();
            container.RegisterType<UsuarioService>();


            // Repositorios
            container.RegisterType<IClienteRepository, ClienteRepository>();
            container.RegisterType<IListaReporsitory, ListaReporsitory>();
            container.RegisterType<ICpfsRepository, CpfsRepository>();
            container.RegisterType<IUfRepository, UfRepository>();
            container.RegisterType<IUsuarioRepository, UsuarioRepository>();
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}


