using System;
using System.Collections.Generic;
using CompurShop.Domain.Interfaces;
using CompurShop.Domain.Entities;
using System.Linq;

namespace CompurShop.Domain.Services
{
    public class ListaService
    {
        private readonly IListaReporsitory _ListaRepository;
        private readonly IListaArquivoRepository _ListaArquivosRepository;
        private readonly ICpfsRepository _CpfRepository;
        private readonly IClienteRepository _ClienteRepository;


        public ListaService(IListaReporsitory listaRepository, ICpfsRepository cpfsRepository, IClienteRepository clienteRepository, IListaArquivoRepository listaArquivoRepository)
        {
            _ListaRepository = listaRepository;
            _CpfRepository = cpfsRepository;
            _ClienteRepository = clienteRepository;
            _ListaArquivosRepository = listaArquivoRepository;
        }
        public IEnumerable<Lista> BuscarListas(bool carregarCliente, int idcliente)
        {
            var listas = _ListaRepository.GetListasByCliente(idcliente);
            
            foreach (var lista in listas)
            {
                //lista.QtdeCpfs = _CpfRepository.GetQtdeCpfLista(lista.Id);
                if (lista.IdCliente > 0 && carregarCliente)
                {
                    lista.Cliente = _ClienteRepository.GetEntity(lista.IdCliente);
                }
            }

            return listas;
        }

        #region Buscas de Listas

        public Lista GetListaById(int idLista)
        {
            return _ListaRepository.GetEntity(idLista);
        }

        public List<Lista> GetListaByStatus(int status)
        {
            var listas = _ListaRepository.GetListas().Where(l => l.Status.Equals(status)).ToList();
            return listas;
        }

        #endregion

        #region Buscas CPF
        public List<Cpf> CpfPorLista(int idlista)
        {
            return _CpfRepository.GetCpfLista(idlista);
        }

        #endregion

        #region Gravacao Lista

        public Lista PreparaLista(string nome)
        {
            Lista minhalista = new Lista{ Nome = nome };
                                    
           // minhalista.QtdeCpfs = _CpfRepository.GetQtdeCpfInFile(nome);
            minhalista.CpfsLista = nome.Replace("\r\n", ";");
            minhalista.Datahora = DateTime.Now;
            return minhalista;
        }
        
        public int GravarLista(Lista lista)
        {
            string cpfs = lista.CpfsLista;

            int idlista = _ListaRepository.SaveLista(lista);

            int qtdecpfs = _CpfRepository.SaveCPFS(cpfs, idlista);

            qtdecpfs = _CpfRepository.GetQtdeCpfLista(lista.Id);

            return qtdecpfs;
        }

        public int UpdateStatus(int idlista, int status)
        {
            var lista = _ListaRepository.GetEntity(idlista);

            lista.Status = status;

            return _ListaRepository.SaveLista(lista);
        }

        #endregion

        #region ListaArquivos

        public int GravarListaArquivo(ListaArquivo lista)
        {
            return _ListaArquivosRepository.SaveLista(lista);
        }

        public List<ListaArquivo> BuscarArquivosPorLista(int idlista)
        {
            var arquivos = _ListaArquivosRepository.GetByIdLista(idlista);
            return arquivos;
        }

        #endregion
    }
}
