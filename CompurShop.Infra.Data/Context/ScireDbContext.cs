using CompurShop.Domain.Entities;
using Npgsql;
using System.Data.Entity;

namespace CompurShop.Infra.Data
{
    public class ScireDbContext : DbContext
    {
        public ScireDbContext()
                     : base("CompuConnectionString")
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cpf> Cpfs { get; set; }
        public DbSet<Lista> Listas { get; set; }
        public DbSet<Uf> Ufs { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
