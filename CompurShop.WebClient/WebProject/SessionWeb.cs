using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompurShop.WebClient.WebProject
{
    [Serializable]
    public class SessionWEB
    {
        private string usuarioLogado = string.Empty;
        private int idnivel = 0;
        private int idcliente = 0;
        private string postMessages = string.Empty;
        public const string SessSessionWEB = "SSessionWEB";

        /// <summary>
        /// Armazena o nome da pessoa ou o login do         
        /// usuário logado no sistema
        /// </summary>
        public string UsuarioLogado
        {
            get
            {
                return usuarioLogado;
            }
            set
            {
                usuarioLogado = value;
            }
        }

        public int IdNivel
        {
            get
            {
                return idnivel;
            }
            set
            {
                idnivel = value;
            }
        }
        public int IdCliente
        {
            get
            {
                return idcliente;
            }
            set
            {
                idcliente = value;
            }
        }



        /// <summary>
        /// Recebe e atribui mensagens a serem transferidas em um post ou redirect.
        /// Ao ler a mensagens enviada a propriedade é automaticamente limpada.
        /// </summary>
        public string PostMessages
        {
            get
            {
                try
                {
                    return postMessages;
                }
                finally
                {
                    postMessages = string.Empty;
                }
            }
            set
            {
                postMessages = value;
            }
        }
    }
}