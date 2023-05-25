using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace CompurShop.Domain.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IEnumerable<Cliente> BuscarClientesPorNome(string nome, string cpf, int startRowIndex, int registrosPorPagina, out int totalRowCount)
        {            
            return _clienteRepository.GetClientesByNome(nome, cpf, startRowIndex, registrosPorPagina, out totalRowCount);
        }

        public void GravarCliente(Cliente cliente)
        {
            // Implemente a lógica desejada, se necessário
            // Por exemplo, validações ou manipulações dos dados

            _clienteRepository.SaveCliente(cliente);
        }

        public void Deletar(Cliente cliente)
        {
            _clienteRepository.DeleteCliente(cliente.Id);
        }
    }
}

