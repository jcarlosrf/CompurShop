using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scire.Arquivos.Infra
{
    public class ListaArquivoRepository : IListaArquivoRepository
    {
        private readonly ScireWsDbContext _context;

        public ListaArquivoRepository(ScireWsDbContext context)
        {
            _context = context;
        }

        public int SaveListas(List<ListaArquivo> listas, int idlista, string criticas)
        {
            try
            {
                using (var contexto = new ScireWsDbContext())
                {
                    foreach (var lista in listas) { 

                        contexto.ListasArquivos.Add(lista);
                    }

                    var existingCliente = contexto.Listas.Find(idlista);
                    if (existingCliente != null)
                    {                        
                        existingCliente.Status = 2;
                    }

                    contexto.SaveChanges();
                    AtualizarCriticas(criticas, idlista, contexto);
                }

                return idlista;
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }

        private int AtualizarCriticas(string cpfscriticas, int idlista, ScireWsDbContext contexto)
        {
            var filePathParameter = new Npgsql.NpgsqlParameter("@cpfs_text", NpgsqlTypes.NpgsqlDbType.Text)
            {
                Value = cpfscriticas
            };

            var idListaParameter = new Npgsql.NpgsqlParameter("@pidlista", NpgsqlTypes.NpgsqlDbType.Integer)
            {
                Value = idlista
            };

            // Chama a função count_cpfs_in_file do PostgreSQL
            using (var command = _context.Database.Connection.CreateCommand())
            {
                if (command.Connection.State != System.Data.ConnectionState.Open)
                    command.Connection.Open();

                command.CommandText = "SELECT scire_cpfs_updatecritica(@cpfs_text, @pidlista)";
                command.Parameters.Add(filePathParameter);
                command.Parameters.Add(idListaParameter);
                if (_context.Database.Connection.State == System.Data.ConnectionState.Closed)
                    _context.Database.Connection.Open();

                if (command.Connection.State != System.Data.ConnectionState.Open)
                    command.Connection.Open();

                var result = command.ExecuteNonQuery();
                                
                return result;
            }
        }
    }

}
