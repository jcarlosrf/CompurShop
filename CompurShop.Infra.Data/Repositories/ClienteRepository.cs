using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace CompurShop.Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteDbContext _context;

        public ClienteRepository(ClienteDbContext context)
        {
            _context = context;
        }

        public void SaveCliente(Cliente cliente)
        {
            if (cliente.Id == 0)
            {
                _context.Clientes.Add(cliente);
            }
            else
            {
                var existingCliente = _context.Clientes.Find(cliente.Id);
                if (existingCliente != null)
                {
                    existingCliente.Nome = cliente.Nome;
                    existingCliente.CPFCNPJ = cliente.CPFCNPJ;
                    existingCliente.Telefone = cliente.Telefone;
                    existingCliente.Logradouro = cliente.Logradouro;
                    existingCliente.Numero = cliente.Numero;
                    existingCliente.Complemento = cliente.Complemento;
                    existingCliente.Cidade = cliente.Cidade;
                    existingCliente.UF = cliente.UF;
                    existingCliente.CEP = cliente.CEP;
                }
            }

            _context.SaveChanges();
        }

        public IEnumerable<Cliente> GetClientesByNome(string nome)
        {

            var clientes = _context.Clientes.ToList();

            return clientes;
        }

        public void DeleteCliente(int clienteId)
        {
            var cliente = _context.Clientes.Find(clienteId);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
        }
    }
}