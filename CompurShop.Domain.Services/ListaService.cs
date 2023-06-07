using System;
using System.Collections.Generic;
using CompurShop.Domain.Interfaces;
using CompurShop.Domain.Entities;
using System.Linq;
using System.IO;
using System.IO.Compression;

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

        public IEnumerable<Lista> BuscarListas(bool carregarCliente, int idcliente, string cpf, string pasta)
        {
            if (!string.IsNullOrEmpty(cpf))

                return BuscarListaPorCPF(idcliente, cpf, pasta);

            var listas = _ListaRepository.GetListasByCliente(idcliente);
            
            foreach (var lista in listas)
            {                
                if (lista.IdCliente > 0 && carregarCliente)
                {
                    lista.Cliente = _ClienteRepository.GetEntity(lista.IdCliente);
                }
            }

            return listas;
        }

        public IEnumerable<Lista> BuscarListaPorCPF(int idcliente, string cpf, string pasta)
        {
            var listas = new List<Lista>();

            var pastaarquivos = Path.Combine(pasta, cpf);
            var arquivodestino  = Path.Combine(pasta, "ArquivosGerados", string.Format("{0}.zip", cpf));

            bool exist = Directory.Exists(pastaarquivos);
            if (!exist)
            {
                return listas;
            }

            bool arquivoExiste = File.Exists(arquivodestino);
            if (arquivoExiste)
                File.Delete(arquivodestino);

            using (var zipArchive = ZipFile.Open(arquivodestino, ZipArchiveMode.Create))
            {
                AdicionarDiretorioAoZipAsync(zipArchive, pastaarquivos, cpf);
            }

            Lista list = new Lista
            {
                Id = 1,
                Critica = false,
                IdCliente = idcliente,
                Datahora = DateTime.Now,
                Nome = arquivodestino,
                Status = 9
                , CpfsLista = cpf
            };

            listas.Add(list);

            return listas;
        }

        private void AdicionarDiretorioAoZipAsync(ZipArchive zipArchive, string diretorioOrigem, string nomeDiretorio)
        {
            foreach (string arquivo in Directory.GetFiles(diretorioOrigem))
            {
                string nomeArquivo = Path.GetFileName(arquivo);
                zipArchive.CreateEntryFromFile(arquivo, Path.Combine(nomeDiretorio, nomeArquivo), CompressionLevel.Fastest);
            }

            foreach (string subdiretorio in Directory.GetDirectories(diretorioOrigem))
            {
                string nomeSubdiretorio = Path.GetFileName(subdiretorio);
                AdicionarDiretorioAoZipAsync(zipArchive, subdiretorio, Path.Combine(nomeDiretorio, nomeSubdiretorio));
            }
        }



        #region Buscas de Listas

        public Lista GetListaById(int idLista)
        {
            return _ListaRepository.GetEntity(idLista);
        }

        #endregion

        #region Buscas CPF
        public List<Cpf> CpfPorLista(int idlista, bool criticas)
        {
            var retorno =  _CpfRepository.GetCpfLista(idlista);

            if (criticas)
                retorno = retorno.Where(c => c.critica);

            return retorno.ToList();

        }

        #endregion

        #region Gravacao Lista

        public Lista PreparaLista(string nome)
        {
            Lista minhalista = new Lista{ Nome = nome };
           
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
