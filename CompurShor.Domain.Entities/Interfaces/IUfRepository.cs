using CompurShop.Domain.Entities;
using System.Collections.Generic;

namespace CompurShop.Domain.Interfaces
{
    public interface IUfRepository
    {
        IEnumerable<Uf> GetAll();
    }
}