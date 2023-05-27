using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.Arquivos.Infra
{
    [Serializable]
    [Table("cpfs", Schema = "public")]
    public class Cpf
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("cpf")]
        public string Nome { get; set; }

        [Required]
        [Column("idlista")]
        public int IdLista { get; set; }
    }
}
