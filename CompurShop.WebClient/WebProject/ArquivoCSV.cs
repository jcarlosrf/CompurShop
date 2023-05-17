using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Collections.Generic;
using CompurShop.Domain.Entities;

namespace CompurShop.WebClient.WebProject
{
    public class ArquivoCsv
    {
        public static bool ExportarParaCSV(List<Cpf> cpfs, string fileName)
        {
            try
            {
                string caminhoArquivo = HttpContext.Current.Server.MapPath("~/Download/" + fileName);

                // Criar o arquivo e escrever o cabeçalho
                using (StreamWriter sw = new StreamWriter(caminhoArquivo, false, Encoding.UTF8))
                {
                    sw.WriteLine("Cpf");

                    // Escrever cada objeto Contidos na lista como uma linha no arquivo CSV
                    foreach (Cpf contido in cpfs)
                    {
                        sw.WriteLine($"{contido.Nome};");
                    }
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public static void DownloadCSV(string filename)
        {
            // Definir o caminho e nome do arquivo CSV            
            string caminhoArquivo = HttpContext.Current.Server.MapPath("~/Download/" + filename);

            // Preparar a resposta HTTP para download
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", $"attachment; filename=\"{filename}\"");

            // Escrever o conteúdo do arquivo CSV na resposta HTTP
            using (FileStream fs = new FileStream(caminhoArquivo, FileMode.Open))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, bytesRead);
                    HttpContext.Current.Response.Flush();
                }
            }

            HttpContext.Current.Response.End();
        }
    }
}