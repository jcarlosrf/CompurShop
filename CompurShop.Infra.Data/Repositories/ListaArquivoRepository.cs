using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System;
using System.Linq;

namespace CompurShop.Infra.Data.Repositories
{
    public class ListaArquivoRepository : IListaArquivoRepository
    {
        private readonly ScireDbContext _context;

        public ListaArquivoRepository(ScireDbContext context)
        {
            _context = context;
        }

        public int SaveLista(ListaArquivo lista)
        {
            try
            {
                if (lista.Id == 0)
                {
                    _context.ListasArquivos.Add(lista);
                }
                else
                {
                    var existingCliente = _context.ListasArquivos.Find(lista.Id);
                    if (existingCliente != null)
                    {
                        existingCliente.NomeArquivo = lista.NomeArquivo;
                        existingCliente.IdLista = lista.IdLista;
                        existingCliente.QtdeCpfs = lista.QtdeCpfs;
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
    }
}
