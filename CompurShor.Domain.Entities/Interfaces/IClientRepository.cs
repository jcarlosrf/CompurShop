using CompurShop.Domain.Entities;
using System.Collections.Generic;



namespace CompurShop.Domain.Interfaces
{
    public interface IClienteRepository
    {
        void SaveCliente(Cliente cliente);
        IEnumerable<Cliente> GetClientesByNome(string nome);
        void DeleteCliente(int clienteId);
    }
}

