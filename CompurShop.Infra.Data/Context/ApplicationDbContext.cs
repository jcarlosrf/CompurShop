using Npgsql;
using System.Configuration;
using System.Data.Entity;

namespace CompurShop.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
                    : base(ConfigurationManager.ConnectionStrings["CompuConnectionString"].ConnectionString)
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
