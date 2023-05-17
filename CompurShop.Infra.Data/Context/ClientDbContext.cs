using CompurShop.Domain.Entities;
using Npgsql;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CompurShop.Infra.Data
{
    public class ClienteDbContext : DbContext
    {
        public ClienteDbContext() :  base(GetConnectionString())
        {
        }
        public DbSet<Cliente> Clientes { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasDefaultSchema("public");

        //    modelBuilder.Entity<Clientes>()
        //        .HasKey(c => c.Id);

        //    modelBuilder.Entity<Clientes>()
        //        .Property(c => c.Id)
        //        .HasColumnName("Id")
        //        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        //    modelBuilder.Entity<Clientes>()
        //        .Property(c => c.Nome)
        //        .HasColumnName("Nome");

        //    modelBuilder.Entity<Clientes>()
        //        .Property(c => c.CpfCnpj)
        //        .HasColumnName("CpfCnpj");

        //    modelBuilder.Entity<Clientes>()
        //        .Property(c => c.Telefone)
        //        .HasColumnName("Telefone");

        //    modelBuilder.Entity<Clientes>()
        //        .Property(c => c.Logradouro)
        //        .HasColumnName("Logradouro");

        //    modelBuilder.Entity<Clientes>()
        //        .Property(c => c.Numero)
        //        .HasColumnName("Numero");

        //    modelBuilder.Entity<Clientes>()
        //        .Property(c => c.Complemento)
        //        .HasColumnName("Complemento");

        //    modelBuilder.Entity<Clientes>()
        //        .Property(c => c.Cidade)
        //        .HasColumnName("Cidade");

        //    modelBuilder.Entity<Clientes>()
        //        .Property(c => c.UF)
        //        .HasColumnName("UF")
        //        .HasMaxLength(2);

        //    modelBuilder.Entity<Clientes>()
        //        .Property(c => c.Cep)
        //        .HasColumnName("CEP");


        //}

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
