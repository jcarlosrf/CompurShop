using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompurShop.Domain.Entities
{
    [Serializable]
    [Table("usuarios", Schema = "public")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nome")]
        public string Nome { get; set; }

        [Required]
        [Column("senha")]
        public string Senha { get; set; }

        [Required]
        [Column("idcliente")]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        [Required]
        [Column("idnivel")]
        public int IdNivel { get; set; }


        [NotMapped]
        public string NomeCliente
        {
            get
            {
                if (Cliente == null)
                    return string.Empty;
                return Cliente.Nome;
            }
        }

    }
}
