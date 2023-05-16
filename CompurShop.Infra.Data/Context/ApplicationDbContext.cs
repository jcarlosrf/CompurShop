using Npgsql;
using System.Configuration;
using System.Data.Entity;

namespace CompurShop.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
                     : base(GetConnectionString())
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
        }

        private static string GetConnectionString()
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = "192.168.15.220",
                Port = 5432,
                Database = "compurshop",
                Username = "postgres",
                Password = "1234"
            };

            return builder.ToString();
        }
    }
}
