using CompurShop.Domain.Entities;
using System.Collections.Generic;

namespace CompurShop.Domain.Interfaces
{
    public interface IListaReporsitory
    {
        IEnumerable<Lista> GetListas();
        int SaveCliente(Lista lista);
    }
}