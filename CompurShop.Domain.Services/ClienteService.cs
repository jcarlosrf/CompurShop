using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CompurShop.Domain.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IEnumerable<Cliente> BuscarClientesPorNome(string nome)
        {
            return _clienteRepository.GetClientesByNome(nome);
        }

        public void GravarCliente(Cliente cliente)
        {
            // Implemente a lógica desejada, se necessário
            // Por exemplo, validações ou manipulações dos dados

            _clienteRepository.SaveCliente(cliente);
        }
    }
}

