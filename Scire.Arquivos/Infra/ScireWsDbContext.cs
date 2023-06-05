using Npgsql;
using System.Data.Entity;

namespace Scire.Arquivos.Infra
{
    public class ScireWsDbContext : DbContext
    {
        public ScireWsDbContext()
                     : base(CreateConnection())
        {
        }

       
        public DbSet<Lista> Listas { get; set; }
        public DbSet<ListaArquivo> ListasArquivos { get; set; }

        public DbSet<Cpf> Cpfs { get; set; }

        private static string CreateConnection()
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder
            {
                Host = "185.182.184.30",
                Port = 5432,
                Database = "compurshop",
                Username = "postgres",
                Password = "1234",
                ApplicationName = "Scire.Serviço"
            };

            return builder.ConnectionString;
        }

    }
}
