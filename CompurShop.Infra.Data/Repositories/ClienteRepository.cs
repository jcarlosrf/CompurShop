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

        public void SaveCliente(Clientes cliente)
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
                    existingCliente.CpfCnpj = cliente.CpfCnpj;
                    existingCliente.Tipo = cliente.Tipo;
                    existingCliente.Telefone = cliente.Telefone;
                }
            }

            _context.SaveChanges();
        }

        public IEnumerable<Clientes> GetClientesByNome(string nome)
        {
            return _context.Clientes.Where(c => c.Nome.Contains(nome)).ToList();
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