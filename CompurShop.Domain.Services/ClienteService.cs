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

        public IEnumerable<Clientes> BuscarClientesPorNome(string nome)
        {
            // Implemente a lógica desejada, se necessário
            // Por exemplo, validações ou manipulações dos dados


            List<Clientes> clientes = new List<Clientes>();
            clientes.Add(new Clientes { Id = 1, Nome = "Jorge", CpfCnpj = "123456" });
            clientes.Add(new Clientes { Id = 2, Nome = "Francisco", CpfCnpj = "321564" });
            clientes.Add(new Clientes { Id = 3, Nome = "Ana", CpfCnpj = "789456" });

            clientes = clientes.Where(c => c.Nome.Contains(nome) || string.IsNullOrEmpty(nome)).ToList();

            return clientes;


            return _clienteRepository.GetClientesByNome(nome);

        }

        public void GravarCliente(Clientes cliente)
        {
            // Implemente a lógica desejada, se necessário
            // Por exemplo, validações ou manipulações dos dados

            _clienteRepository.SaveCliente(cliente);
        }
    }
}

