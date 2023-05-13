using CompurShop.Domain.Entities;
using System.Collections.Generic;



namespace CompurShop.Domain.Interfaces
{
    public interface IClienteRepository
    {
        void SaveCliente(Clientes cliente);
        IEnumerable<Clientes> GetClientesByNome(string nome);
        void DeleteCliente(int clienteId);
    }
}

