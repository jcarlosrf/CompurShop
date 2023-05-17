using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompurShop.Domain.Entities
{
    [Serializable]
    [Table("listas", Schema = "public")]
    public class Lista
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nome")]
        public string Nome { get; set; }

        [Required]
        [Column("datahora")]
        public DateTime Datahora{ get; set; }

        [NotMapped] // Ignorar a coluna Tipo no mapeamento da tabela
        public int QtdeCpfs
        {
            get;set;
        }

        [NotMapped] // Ignorar a coluna Tipo no mapeamento da tabela
        public string CpfsLista
        {
            get; set;
        }

        
    }
}
