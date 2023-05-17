using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;



namespace CompurShop.Infra.Data.Repositories
{
    public class ListaReporsitory : IListaReporsitory
    {
        private readonly ListaDbContext _context;

        public ListaReporsitory(ListaDbContext context)
        {
            _context = context;
        }
        public int SaveCliente(Lista lista)
        {
            try
            {
                if (lista.Id == 0)
                {
                    _context.Listas.Add(lista);
                }
                else
                {
                    var existingCliente = _context.Listas.Find(lista.Id);
                    if (existingCliente != null)
                    {
                        existingCliente.Nome = lista.Nome;
                        existingCliente.Datahora = lista.Datahora;
                    }
                }

                _context.SaveChanges();

                return lista.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Lista> GetListas()
        {
            var query = _context.Listas.AsQueryable();
            return query.ToList();
        }
    }
}
