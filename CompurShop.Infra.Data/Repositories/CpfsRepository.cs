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

        public IQueryable<Cpf> GetCpfLista(int idlista)
        {
            var query = _context.Cpfs.Where(c => c.IdLista == idlista);

            return query;
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
