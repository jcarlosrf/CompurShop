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

        

        public List<Lista> GetListasByCliente(int idcliente)
        {
            var query = GetListas();

            if (idcliente > 0)
            {
                query = query.Where(l => l.IdCliente.Equals(idcliente));
            }

            var listas = query.ToList();

            var listaIds = listas.Select(l => l.Id).ToList();

            var querycount = from l in _context.Listas
                        join c in _context.Cpfs on l.Id equals c.IdLista
                        where l.IdCliente == idcliente|| idcliente  == 0
                        group c by l.Id into g
                        select new
                        {
                            Id = g.Key,
                            QtdeCpf = g.Count(),
                            Criticas = g.Any(c => c.critica == true)
                        };


            //var cpfsCount = _context.Cpfs
            //                    .Where(c => listaIds.Contains(c.IdLista))
            //                    .GroupBy(c => c.IdLista)
            //                    .Select(g => new { IdLista = g.Key, Count = g.Count() })
            //                    .ToList();           

            //var criticas = _context.Cpfs
            //                    .Where(c => listaIds.Contains(c.IdLista) && c.critica == true) 
            //                    .GroupBy(c => c.IdLista)
            //                    .Select(g => new { IdLista = g.Key, Count = g.Count() })
            //                    .ToList();

            var result = querycount.ToList();

            foreach (var lista in listas)
            {
                lista.QtdeCpfs = result.FirstOrDefault(r => r.Id == lista.Id)?.QtdeCpf ?? 0;
                lista.Critica = result.FirstOrDefault(r => r.Id == lista.Id)?.Criticas ?? false;
            }

            listas = listas.OrderByDescending(l => l.Datahora).ToList();

            return listas;

        }       
    }
}
