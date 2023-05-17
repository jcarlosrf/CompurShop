using CompurShop.Domain.Entities;
using System.Collections.Generic;

namespace CompurShop.Domain.Interfaces
{
    public interface IClienteRepository
    {
        void SaveCliente(Cliente cliente);
        IEnumerable<Cliente> GetClientesByNome(string nome, string cpf, int pagina, int registrosPorPagina);
        void DeleteCliente(int clienteId);
    }
}

