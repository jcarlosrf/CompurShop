using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CompurShop.Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ScireDbContext _context;

        public ClienteRepository(ScireDbContext context)
        {
            _context = context;
        }

        public Cliente GetEntity(int id)
        {
            return _context.Clientes.FirstOrDefault(c => c.Id.Equals(id));
        }

        public int SaveCliente(Cliente cliente)
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
                    existingCliente.Email = cliente.Email;
                    existingCliente.Bairro = cliente.Bairro;
                }
            }

            return _context.SaveChanges();
        }
        public IEnumerable<Cliente> GetClientesByNome(string nome, string cpf, int startRowIndex, int registrosPorPagina, out int totalRowCount)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            if (!string.IsNullOrEmpty(cpf))
                query = query.Where(c => c.CPFCNPJ.Contains(cpf));

            totalRowCount = query.Count();

            if (registrosPorPagina < 1 || startRowIndex < 0)
                return query.ToList();
            
            int totalPaginas = (int)Math.Ceiling((double)totalRowCount / registrosPorPagina);

            // Realiza a paginação
            var clientesPaginados = query.OrderBy(c => c.Nome)
                .Skip(startRowIndex)
                .Take(registrosPorPagina)
                .ToList();

            return clientesPaginados;
        }
                
        public int DeleteCliente(int clienteId)
        {
            var cliente = _context.Clientes.Find(clienteId);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                return _context.SaveChanges();
            }

            return 0;
        }
    }
}