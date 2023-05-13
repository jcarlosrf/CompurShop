using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompurShop.Domain.Entities
{
    [Table("Clientes")]
    public class Clientes
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("CpfCnpj")]
        public string CpfCnpj { get; set; }

        [Column("Tipo")]
        public string Tipo { get; set; }

        [Column("Telefone")]
        public string Telefone { get; set; }
    }
}
