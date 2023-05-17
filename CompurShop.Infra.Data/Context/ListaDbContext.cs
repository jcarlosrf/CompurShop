using CompurShop.Domain.Entities;
using Npgsql;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CompurShop.Infra.Data
{
    public class ListaDbContext : DbContext
    {
        public ListaDbContext() : base(GetConnectionString())
        {
        }
        public DbSet<Lista> Listas { get; set; }

        private static string GetConnectionString()
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost",
                Port = 5432,
                Database = "compurshop",
                Username = "postgres",
                Password = "1234"
            };

            return builder.ToString();
        }
    }
}
