using Scire.Arquivos.Infra;
using Unity;

namespace Scire.Arquivos
{
    public static class DependencyConfig
    {
        private static readonly UnityContainer container;
        static DependencyConfig()
        {
            container = new UnityContainer();

            // Registrando as dependências
            container.RegisterType<ScireWsDbContext>();

            //servicos
            container.RegisterType<ListaService>();


            // Repositorios
            container.RegisterType<IListaReporsitory, ListaReporsitory>();
            container.RegisterType<IListaArquivoRepository, ListaArquivoRepository>();
            container.RegisterType<ICpfsRepository, CpfsRepository>();
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
