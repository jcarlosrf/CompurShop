using CompurShop.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CompurShop.Domain.Interfaces
{
    public interface IListaReporsitory
    {
        Lista GetEntity(int id);

        IQueryable<Lista> GetListas();

        List<Lista> GetListasByCliente(int idcliente);


        int SaveLista(Lista lista);
    }
}