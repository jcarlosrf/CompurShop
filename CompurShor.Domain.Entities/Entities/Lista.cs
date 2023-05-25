﻿using System;
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

        [Column("idcliente")]
        public int IdCliente { get; set; }


        [Column("status")]
        public int Status { get; set; }

        [NotMapped]
        public Cliente Cliente { get; set; }


        [NotMapped] // Ignorar a coluna Tipo no mapeamento da tabela
        public int QtdeCpfs { get; set; }

        [NotMapped] // Ignorar a coluna Tipo no mapeamento da tabela
        public string CpfsLista { get; set; }

        [NotMapped] // Ignorar a coluna Tipo no mapeamento da tabela
        public Boolean btnProcessar 
        {
            get
            {
                return Status == 0;
            }
        }

        [NotMapped] // Ignorar a coluna Tipo no mapeamento da tabela
        public Boolean iconeProcessar
        {
            get
            {
                return Status == 1;
            }
        }

        [NotMapped] // Ignorar a coluna Tipo no mapeamento da tabela
        public Boolean btnArquivo
        {
            get
            {
                return Status == 2;
            }
        }
    }
}
