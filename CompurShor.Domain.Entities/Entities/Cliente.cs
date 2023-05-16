using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CompurShop.Domain.Entities
{
    [Table("clientes", Schema = "public")]
    public class Cliente
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nome")]
        public string Nome { get; set; }

        [Required]
        [Column("cpfcnpj")]
        public string CPFCNPJ { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }

        [Column("logradouro")]
        public string Logradouro { get; set; }

        [Column("numero")]
        public string Numero { get; set; }

        [Column("complemento")]
        public string Complemento { get; set; }

        [Column("cidade")]
        public string Cidade { get; set; }

        [Column("uf")]
        public string UF { get; set; }

        [Column("cep")]
        public string CEP { get; set; }

        [NotMapped] // Ignorar a coluna Tipo no mapeamento da tabela
        public string TipoDocumento
        {
            get { return CPFCNPJ.Length <= 11 ? "CPF" : "CNPJ"; }
        }
    }

}
