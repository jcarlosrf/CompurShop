using CompurShop.Domain.Entities;
using System.Collections.Generic;

namespace CompurShop.Domain.Interfaces
{
    public interface IClienteRepository
    {
        void SaveCliente(Cliente cliente);
        IEnumerable<Cliente> GetClientesByNome(string nome, string cpf, int startRowIndex, int registrosPorPagina, out int totalRowCount);
        void DeleteCliente(int clienteId);
    }
}

