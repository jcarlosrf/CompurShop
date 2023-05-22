using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompurShop.Domain.Entities
{
    [Serializable]
    [Table("ufs", Schema = "public")]
    public class Uf
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nome")]
        public string Nome { get; set; }

        [Required]
        [Column("sigla")]
        public string Sigla { get; set; }
    }
}
