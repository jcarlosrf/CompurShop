using Scire.Arquivos.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Unity;

namespace Scire.Arquivos
{
    class BotArquivos
    {
        private readonly string ArquivosPath = Scire.Arquivos.Properties.Settings.Default.Pasta;
        
        private readonly ListaService _ListaService;

        public BotArquivos()
        {
            // Configurar o Unity Container
        
            _ListaService = DependencyConfig.Resolve<ListaService>();
        }


        public void Executar()
        {
            var listas = _ListaService.GetListaByStatus(1); // status 1 --> processar
            
            foreach(Lista lista in listas)
            {
                var cpfs = _ListaService.CpfPorLista(lista.Id);
                int status = ProcessarCPFs(cpfs, lista.Id) ? 2 : 0;

                _ListaService.UpdateStatus(lista.Id, status);
            }
        }       

        private bool ProcessarCPFs(List<Cpf> Cpfs, int idlista)
        {
            try
            {
                int contadorArquivo = 0;
                int contadorCpfs = 0;
                bool criouArquivo = false;
                string nomeArquivoZip = string.Empty;


                foreach (var cpf in Cpfs)
                {
                    string nomePasta = ArquivosPath + cpf.Nome.Trim().PadLeft(11, '0');
                    Console.WriteLine(string.Format("Nome {0} Arquivos {1} CPFs {2}", nomePasta, contadorArquivo, contadorCpfs));
                    if (Directory.Exists(nomePasta))
                    {
                        if (!criouArquivo)
                        {
                            contadorArquivo++;
                            nomeArquivoZip = string.Format("Lista{0}-{1}.zip", idlista, contadorArquivo);

                            if (File.Exists(ArquivosPath + "ArquivosGerados\\" + nomeArquivoZip))
                                File.Delete(ArquivosPath + "ArquivosGerados\\" + nomeArquivoZip);

                            ZipArchive zipArchive = ZipFile.Open(ArquivosPath + "ArquivosGerados\\" +  nomeArquivoZip, ZipArchiveMode.Create);
                            zipArchive.Dispose();
                            criouArquivo = true;
                        }

                        using (ZipArchive zipArchive = ZipFile.Open(ArquivosPath +"ArquivosGerados\\" + nomeArquivoZip, ZipArchiveMode.Update))
                        {
                            string nomeDiretorio = Path.GetFileName(nomePasta);
                            AdicionarDiretorioAoZip(zipArchive, nomePasta, nomeDiretorio);

                            contadorCpfs++;
                        }

                        if (contadorCpfs == 20000)
                        {                           

                            var listaarquivo = new ListaArquivo
                            {
                                IdLista = idlista,
                                NomeArquivo = nomeArquivoZip,
                                QtdeCpfs = contadorCpfs
                            };
                                                    

                            _ListaService.GravarListaArquivo(listaarquivo);

                            criouArquivo = false;
                            contadorCpfs = 0;
                        }
                    }
                }

                if (contadorCpfs > 0)
                {
                    var listaarquivofinal = new ListaArquivo
                    {
                        IdLista = idlista,
                        NomeArquivo = nomeArquivoZip,
                        QtdeCpfs = contadorCpfs
                    };

                    _ListaService.GravarListaArquivo(listaarquivofinal);
                }

                if (contadorArquivo == 0)
                    return false;
               
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private void AdicionarDiretorioAoZip(ZipArchive zipArchive, string diretorioOrigem, string nomeDiretorio)
        {
            foreach (string arquivo in Directory.GetFiles(diretorioOrigem))
            {
                string nomeArquivo = Path.GetFileName(arquivo);
                zipArchive.CreateEntryFromFile(arquivo, Path.Combine(nomeDiretorio, nomeArquivo));
            }

            foreach (string subdiretorio in Directory.GetDirectories(diretorioOrigem))
            {
                string nomeSubdiretorio = Path.GetFileName(subdiretorio);
                AdicionarDiretorioAoZip(zipArchive, subdiretorio, Path.Combine(nomeDiretorio, nomeSubdiretorio));
            }
        }
    }
}

