using CompurShop.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CompurShop.Infra.Data
{
    public class ClienteDbContext : ApplicationDbContext
    {
        public DbSet<Clientes> Clientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Clientes>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Clientes>()
                .Property(c => c.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Clientes>()
                .Property(c => c.Nome)
                .HasColumnName("Nome");

            modelBuilder.Entity<Clientes>()
                .Property(c => c.CpfCnpj)
                .HasColumnName("CpfCnpj");

            modelBuilder.Entity<Clientes>()
                .Property(c => c.Tipo)
                .HasColumnName("Tipo");

            modelBuilder.Entity<Clientes>()
                .Property(c => c.Telefone)
                .HasColumnName("Telefone");

            // Configurações adicionais para a entidade Cliente, se necessário
        }
    }
}
