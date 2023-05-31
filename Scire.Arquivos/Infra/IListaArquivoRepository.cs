using System.Collections.Generic;

namespace Scire.Arquivos.Infra
{
    public interface IListaArquivoRepository
    {
        int SaveListas(List<ListaArquivo> lista, int idlista, string criticas);
    }
}