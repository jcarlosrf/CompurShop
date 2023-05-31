using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scire.Arquivos.Infra
{
    public class ListaService
    {
        private readonly IListaReporsitory _ListaRepository;
        private readonly IListaArquivoRepository _ListaArquivosRepository;
        private readonly ICpfsRepository _CpfRepository;


        public ListaService(IListaReporsitory listaRepository, ICpfsRepository cpfsRepository, IListaArquivoRepository listaArquivoRepository)
        {
            _ListaRepository = listaRepository;
            _CpfRepository = cpfsRepository;
            _ListaArquivosRepository = listaArquivoRepository;
        }       

        public Lista GetListaById(int idLista)
        {
            return _ListaRepository.GetEntity(idLista);
        }

        public Lista PreparaLista(string nome)
        {
            Lista minhalista = new Lista{ Nome = nome };
                                    
           // minhalista.QtdeCpfs = _CpfRepository.GetQtdeCpfInFile(nome);
            minhalista.CpfsLista = nome.Replace("\r\n", ";");
            minhalista.Datahora = DateTime.Now;
            return minhalista;
        }
        public List<Cpf> CpfPorLista(int idlista)
        {
            return _CpfRepository.GetCpfLista(idlista);
        }

        public int GravarLista(Lista lista)
        {
            string cpfs = lista.CpfsLista;

            int idlista = _ListaRepository.SaveLista(lista);

            int qtdecpfs = _CpfRepository.SaveCPFS(cpfs, idlista);

            qtdecpfs = _CpfRepository.GetQtdeCpfLista(lista.Id);

            return qtdecpfs;
        }

        public Lista UpdateStatus(int idlista, int status)
        {
            var lista = _ListaRepository.GetEntity(idlista);

            lista.Status = status;

            _ListaRepository.SaveLista(lista);

            return lista;
        }


        public List<Lista> GetListaByStatus(int status)
        {
            var listas = _ListaRepository.GetListas().Where(l => l.Status.Equals(status)).ToList();
            return listas;
        }

        public int GravarListaArquivo(List<ListaArquivo> listasArquivos, int idlista, List<string> criticas)
        {
            string criticasString = string.Join(";", criticas);

            return _ListaArquivosRepository.SaveListas(listasArquivos, idlista, criticasString);
        }
    }
}
