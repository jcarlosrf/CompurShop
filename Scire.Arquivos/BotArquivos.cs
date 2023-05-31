using Scire.Arquivos.Infra;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;


namespace Scire.Arquivos
{
    class BotArquivos
    {
        private readonly string ArquivosPath = Properties.Settings.Default.Pasta;
        private readonly int PastasPorArquivo = Properties.Settings.Default.QtdePorArqiovo;
        
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
                PastasEncontradas(cpfs, lista.Id);
            }
        }
        
        private void PastasEncontradas(List<Cpf> Cpfs, int idlista)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Console.WriteLine($"Iniciando {DateTime.Now.ToLongTimeString()}");
            List<string> criticas = new List<string>();

            try
            {              
                _ListaService.UpdateStatus(idlista, 3); // joga 3 - se outro loop do servico entrar não vai pegar o mesmo lote de arquivos

                var pastas = Directory.GetDirectories(ArquivosPath).ToList();
                var dictionary = new ConcurrentDictionary<long, string>();

                Parallel.ForEach(pastas, pasta =>
                {
                    string nomePasta = Path.GetFileName(pasta);
                    if (long.TryParse(nomePasta, out long chave))
                    {
                        dictionary[chave] = pasta;
                    }
                });

                var cpfs = Cpfs.Select(c => c.Nome).ToList();

                int takecpf = (int)Math.Ceiling((double)cpfs.Count / 10);

                var tasksPastas = new List<Task<(List<string>, List<string>)>>();

                Parallel.ForEach(Partitioner.Create(0, 10), range =>
                {
                    var batchIndex = range.Item1;

                    var cpfs1 = cpfs.Skip(batchIndex * takecpf).Take(takecpf).ToList();
                    var task = TestarPastas(cpfs1, dictionary, batchIndex);
                    tasksPastas.Add(task);
                });

                Task.WaitAll(tasksPastas.ToArray());

                // Verificar o resultado de cada tarefa
                var retornoPastas = tasksPastas
                         .Select(task => task.Result)
                         .ToList();

                var pastasFiltradas = new List<string>();

                foreach (var ret in retornoPastas)
                {
                    pastasFiltradas.AddRange(ret.Item1);
                    criticas.AddRange(ret.Item2);
                }
                Console.WriteLine($"Tempo de execução (Leitura das pastas): {stopwatch.Elapsed}");
                /*
                 NESSE PONTO JA TENHO QUAIS PASTAS PRECISAM SER ZIPADAS 
                        pastasfiltras --> list<string> somente com as pastas selecionadas
                 */


                /*
                 VAI COMEÇAR A ZIPAR OS ARQUIVOS
                 */

                int totalBatches = (int)Math.Ceiling((double)pastasFiltradas.Count / PastasPorArquivo);  // quantidade de lotes conforme nro de arquivos por zip

                var tasks = new List<Task<ListaArquivo>>();
                                
                for (int batchIndex = 0; batchIndex < totalBatches; batchIndex++)
                {
                    var lote = pastasFiltradas.Skip(batchIndex * PastasPorArquivo).Take(PastasPorArquivo).ToList();
                    var task = ProcessarCPFs(lote, idlista, batchIndex);
                    tasks.Add(task);
                }

                Task.WaitAll(tasks.ToArray());

                // Verificar o resultado de cada tarefa
                List<ListaArquivo> listaArquivos = tasks
                         .Select(task => task.Result)
                         .ToList();


                _ListaService.GravarListaArquivo(listaArquivos, idlista, criticas);
            }
            catch (Exception ex)
            {
                _ListaService.UpdateStatus(idlista, 1);
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"Tempo de execução (Total): {stopwatch.Elapsed}");
                Console.ReadLine();
                Console.ReadLine();
            }
        }

        private async Task<(List<string>, List<string>)> TestarPastas(List<string> cpfs, ConcurrentDictionary<long, string> pastas, int tarefa)
        {
            Stopwatch stopwatchTask = Stopwatch.StartNew();
            try
            {
                var pastasFiltradas = new List<string>();
                var criticas = new List<string>();

                await Task.Run(() =>
                {
                    foreach (var cpf in cpfs)
                    {
                        if (long.TryParse(cpf, out long lcpf))
                        {
                            if (pastas.TryGetValue(lcpf, out string valor))
                            {
                                pastasFiltradas.Add(valor);
                            }
                            else
                            {
                                criticas.Add(cpf);
                            }
                        }
                    }
                });

                return (pastasFiltradas, criticas);
            }
            finally
            {
                stopwatchTask.Stop();
                Console.WriteLine($"Tempo - Tarefa {tarefa}: {stopwatchTask.Elapsed}");
            }
        }

        private async Task<ListaArquivo> ProcessarCPFs(List<string> PastasCpf, int idlista, int NroLote)
        {
            Stopwatch stopwatchTask = Stopwatch.StartNew();
            Console.WriteLine($"Iniciando - Zip {NroLote} - Qtde CPFS {PastasCpf.Count}");

            int contadorCpfs = 0;
            
            try
            {
                string nomeArquivoZip = string.Format("Lista{0}-{1}.zip", idlista, NroLote);
                string caminhoArquivoZip = Path.Combine(ArquivosPath, "ArquivosGerados", nomeArquivoZip);  // Caminho completo do destino .zip

                bool arquivoExiste = File.Exists(caminhoArquivoZip);
                if (arquivoExiste)
                    File.Delete(caminhoArquivoZip);

                using (var zipArchive = ZipFile.Open(caminhoArquivoZip, ZipArchiveMode.Create))
                {
                    foreach (var nomePasta in PastasCpf)
                    {
                        string nomeDiretorio = Path.GetFileName(nomePasta);  // somente o nome do diretorio , sem a raiz por ex: c:\... --> na pratica é o CPF
                        await AdicionarDiretorioAoZipAsync(zipArchive, nomePasta, nomeDiretorio);
                        
                        contadorCpfs++;
                    }
                }

                var listaarquivofinal = new ListaArquivo
                {
                    IdLista = idlista,
                    NomeArquivo = nomeArquivoZip,
                    QtdeCpfs = contadorCpfs
                };

                return listaarquivofinal;

            }
            catch (Exception ex)
            {
                var listaarquivofinal = new ListaArquivo
                {
                    IdLista = idlista,
                    NomeArquivo = string.Empty,
                    QtdeCpfs = 0
                };

                return listaarquivofinal;
            }
            finally
            {
                stopwatchTask.Stop();
                Console.WriteLine($"Tempo - Zip {NroLote}: {stopwatchTask.Elapsed}");
            }
        }

        private async Task AdicionarDiretorioAoZipAsync(ZipArchive zipArchive, string diretorioOrigem, string nomeDiretorio)
        {
            foreach (string arquivo in Directory.GetFiles(diretorioOrigem))
            {              
                string nomeArquivo = Path.GetFileName(arquivo);
                zipArchive.CreateEntryFromFile(arquivo, Path.Combine(nomeDiretorio, nomeArquivo), CompressionLevel.Fastest );                
            }

            foreach (string subdiretorio in Directory.GetDirectories(diretorioOrigem))
            {
                string nomeSubdiretorio = Path.GetFileName(subdiretorio);
                await AdicionarDiretorioAoZipAsync(zipArchive, subdiretorio, Path.Combine(nomeDiretorio, nomeSubdiretorio));
            }
        }
    }
}

