using System;
using System.Collections.Generic;
using CompurShop.Domain.Interfaces;
using CompurShop.Domain.Entities;

namespace CompurShop.Domain.Services
{
    public class ListaService
    {
        private readonly IListaReporsitory _ListaRepository;
        private readonly ICpfsRepository _CpfRepository;

        public ListaService(IListaReporsitory listaRepository, ICpfsRepository cpfsRepository)
        {
            _ListaRepository = listaRepository;
            _CpfRepository = cpfsRepository;
        }
        public IEnumerable<Lista> BuscarListas()
        {
            var listas = _ListaRepository.GetListas();
            
            foreach (var lista in listas)
            {
                lista.QtdeCpfs = _CpfRepository.GetQtdeCpfLista(lista.Id);
            }

            return listas;

        }

        public Lista PreparaLista(string[] cpfs, string nome)
        {
            Lista minhalista = new Lista{ Nome = nome };
                                    
            minhalista.QtdeCpfs = _CpfRepository.GetQtdeCpfInFile(nome);
            minhalista.CpfsLista = nome.Replace("\r\n", ";");
            minhalista.Datahora = DateTime.Now;
            //foreach (string cpf in cpfs)
            //{
            //    int qtde = _CpfRepository.GetQtdeCpf(cpf);
            //    if (qtde == 0)
            //        minhalista.CpfsLista.Add(cpf);
            //}

            return minhalista;
        }


        public void GravarLista(Lista lista)
        {
            // Implemente a lógica desejada, se necessário
            // Por exemplo, validações ou manipulações dos dados

            string cpfs = lista.CpfsLista;

            int idlista = _ListaRepository.SaveCliente(lista);

            _CpfRepository.SaveCPFS(cpfs, idlista);
            
        }

        public List<Cpf> CpfPorLista(int idlista)
        {
            return _CpfRepository.GetCpfLista(idlista);
        }

    }
}
