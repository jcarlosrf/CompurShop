using System;

namespace Scire.Arquivos.Infra
{
    public class ListaArquivoRepository : IListaArquivoRepository
    {
        private readonly ScireWsDbContext _context;

        public ListaArquivoRepository(ScireWsDbContext context)
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
