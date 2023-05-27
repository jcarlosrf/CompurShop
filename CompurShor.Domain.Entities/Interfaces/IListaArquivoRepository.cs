using CompurShop.Domain.Entities;

namespace CompurShop.Domain.Interfaces
{
    public interface IListaArquivoRepository
    {
        int SaveLista(ListaArquivo lista);
    }
}