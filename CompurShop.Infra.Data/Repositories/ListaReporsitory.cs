using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;



namespace CompurShop.Infra.Data.Repositories
{
    public class ListaReporsitory : IListaReporsitory
    {
        private readonly ScireDbContext _context;

        public ListaReporsitory(ScireDbContext context)
        {
            _context = context;
        }
        public int SaveLista(Lista lista)
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
                        existingCliente.IdCliente = lista.IdCliente;
                        existingCliente.Status = lista.Status;
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

        public Lista GetEntity(int id)
        {
            return _context.Listas.FirstOrDefault(l => l.Id.Equals(id));           
        }

        public IQueryable<Lista> GetListas()
        {
            var query = _context.Listas.AsQueryable();
            return query;
        }

        public List<Lista> GetListasByCliente_OLD(int idcliente)
        {
            var query = _context.Listas.Include("Cpfs").AsEnumerable();

            if (idcliente > 0)
            {
                query = query.Where(l => l.IdCliente.Equals(idcliente));
            }

            var result = query.Select(l => new Lista
            {
                Id = l.Id,
                Nome = l.Nome,
                Datahora = l.Datahora,
                IdCliente = l.IdCliente,
                Status = l.Status,
                QtdeCpfs = l.Cpfs.Count()
            });

            var retorno = result.ToList();

            return retorno.OrderByDescending(l => l.Datahora).ToList();
        }

        public List<Lista> GetListasByCliente(int idcliente)
        {
            var query = GetListas();

            if (idcliente > 0)
            {
                query = query.Where(l => l.IdCliente.Equals(idcliente));
            }

            var listas = query.ToList();

            var listaIds = listas.Select(l => l.Id).ToList();
            var cpfsCount = _context.Cpfs
                                .Where(c => listaIds.Contains(c.IdLista))
                                .GroupBy(c => c.IdLista)
                                .Select(g => new { IdLista = g.Key, Count = g.Count() })
                                .ToList();

            foreach (var lista in listas)
            {
                var cpfsCountItem = cpfsCount.FirstOrDefault(c => c.IdLista == lista.Id);
                if (cpfsCountItem != null)
                {
                    lista.QtdeCpfs = cpfsCountItem.Count;
                }
            }

            listas = listas.OrderByDescending(l => l.Datahora).ToList();

            return listas;

        }
    }
}
