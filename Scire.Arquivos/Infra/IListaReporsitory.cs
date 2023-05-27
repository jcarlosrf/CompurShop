using System.Linq;

namespace Scire.Arquivos.Infra
{
    public interface IListaReporsitory
    {
        Lista GetEntity(int id);

        IQueryable<Lista> GetListas();

        IQueryable<Lista> GetListasByCliente(int idcliente);

        int SaveLista(Lista lista);
    }
}