using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CompurShop.Infra.Data.Repositories
{
    public class UfRepository : IUfRepository
    {
        private readonly ScireDbContext _context;

        public UfRepository(ScireDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Uf> GetAll()
        {
            var query = _context.Ufs.AsQueryable();
            return query.ToList();
        }
    }
}
