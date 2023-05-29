using CompurShop.Domain.Entities;
using System.Collections.Generic;

namespace CompurShop.Domain.Interfaces
{
    public interface IListaArquivoRepository
    {
        int SaveLista(ListaArquivo lista);

        List<ListaArquivo> GetByIdLista(int idlista);
    }
}