using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CompurShop.Domain.Services
{
    public static class Criptografia
    {
        public static string CriptografiaSenha(string senha)
        {
            senha = FormsAuthentication.HashPasswordForStoringInConfigFile(senha, "MD5");

            return senha;
        }
    }
}