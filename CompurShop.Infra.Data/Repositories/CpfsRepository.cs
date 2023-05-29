using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompurShop.Infra.Data.Repositories
{
    public class CpfsRepository : ICpfsRepository
    {
        private readonly ScireDbContext _context;

        public CpfsRepository(ScireDbContext context)
        {
            _context = context;
        }
        

        public int GetQtdeCpfLista(int idlista)
        {
            var query = _context.Cpfs.Where(c => c.IdLista == idlista);

            return query.Count(); 
        }


        public List<Cpf> GetCpfLista(int idlista)
        {
            var query = _context.Cpfs.Where(c => c.IdLista == idlista);

            return query.ToList();
        }

        

        public int GetQtdeCpf(string cpf)
        {
            var query = _context.Cpfs.Count(c => c.Nome == cpf);

            return query;
        }


        public int GetQtdeCpfInFile(string cpfFileContent)
        {            

            var filePathParameter = new Npgsql.NpgsqlParameter("@filePath", NpgsqlTypes.NpgsqlDbType.Text)
            {
                Value = cpfFileContent.Replace("\r\n", ";")
            };

            // Chama a função count_cpfs_in_file do PostgreSQL
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = "SELECT scire_count_cpfs_in_file(@filePath)";
                command.Parameters.Add(filePathParameter);
                if (_context.Database.Connection.State == System.Data.ConnectionState.Closed)
                    _context.Database.Connection.Open();

                Int32 valor = 0;

                //var result = command.ExecuteReader();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        valor = reader.GetInt32(0);                            
                    } 
                }

                return valor;
            }
        }


        public int SaveCPFS(string cpfs, int idlista)
        {
            var filePathParameter = new Npgsql.NpgsqlParameter("@cpfs_text", NpgsqlTypes.NpgsqlDbType.Text)
            {
                Value = cpfs
            };

            var idListaParameter = new Npgsql.NpgsqlParameter("@pidlista", NpgsqlTypes.NpgsqlDbType.Integer)
            {
                Value = idlista
            };

            // Chama a função count_cpfs_in_file do PostgreSQL
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = "SELECT scire_cpfs_save(@cpfs_text, @pidlista)";
                command.Parameters.Add(filePathParameter);
                command.Parameters.Add(idListaParameter);
                if (_context.Database.Connection.State == System.Data.ConnectionState.Closed)
                    _context.Database.Connection.Open();


                var result = command.ExecuteNonQuery();

                return result;
            }
        }
    }
}
