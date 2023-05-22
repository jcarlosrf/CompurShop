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

        public CombosService(IUfRepository ufRepository)
        {
            _ufRepository = ufRepository;
        }

        public async Task<List<Uf>> GetEstados()
        {
            var estados = await Task.Run(() => _ufRepository.GetAll());

            return estados.ToList();
        }
    }
}