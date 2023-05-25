using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompurShop.Domain.Services
{
    public class CombosService
    {

        private readonly IUfRepository _ufRepository;
        private readonly IClienteRepository _clienteRepository;

        public CombosService(IUfRepository ufRepository, IClienteRepository clienteRepository)
        {
            _ufRepository = ufRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<List<Uf>> GetEstados()
        {
            var estados = await Task.Run(() => _ufRepository.GetAll());

            return estados.ToList();
        }

        public async Task<List<Cliente>> GetClientes()
        {
            var clientes = await Task.Run(() => _clienteRepository.GetClientesByNome(string.Empty, string.Empty, 0, 0, out int registros));

            return clientes.OrderBy(c=>c.Nome).ToList();
        }
    }
}