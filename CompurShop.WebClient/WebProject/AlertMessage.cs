using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace CompurShop.WebClient.WebProject
{
    public static class AlertMessage
    {
        public enum TipoMensagem
        {
            Sucesso
           ,
            Erro
           ,
            Alerta
           ,
            Informacao
           ,
            Nenhuma
        }

        public static string GetMessage(string message, TipoMensagem tipoMensagem)
        {
            // Pega o tema da página que chamou.
            string tema = HttpContext.Current.Handler is Page ?
                        ((Page)HttpContext.Current.Handler).Theme ??
                        "Default" :
                        "Default";

            string estilo = string.Empty;
            string imagemEstilo = string.Empty;

            switch (tipoMensagem)
            {
                case TipoMensagem.Alerta:
                    {
                        imagemEstilo += "bi bi-exclamation-triangle me-1";
                        estilo = "alert alert-warning alert-dismissible fade show";
                        break;
                    }
                case TipoMensagem.Erro:
                    {
                        imagemEstilo += "bi bi-exclamation-octagon me-1";
                        estilo = "alert alert-danger alert-dismissible fade show";
                        break;
                    }
                case TipoMensagem.Sucesso:
                    {
                        imagemEstilo += "bi bi-check-circle me-1";
                        estilo = "alert alert-success alert-dismissible fade show";
                        break;
                    }
                case TipoMensagem.Informacao:
                    {
                        imagemEstilo += "bi bi-info-circle me-1";
                        estilo = "alert alert-info alert-dismissible fade show";
                        break;
                    }
                default:
                    {
                        imagemEstilo = "";
                        estilo = "";
                        break;
                    }
            }

            return GetMessage(message, imagemEstilo, estilo);
        }

        private static string GetMessage(string message, string imagemEstilo, string estilo)
        {
            StringBuilder sb = new StringBuilder();
                        
            sb.Append("<div class=\"" + estilo + "\" " + "role = \"alert\">");
            sb.Append("<i class=\"" + imagemEstilo + "\"></i>");            
            sb.Append(message);
            sb.Append("<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>");
            sb.Append("</div>");

            return sb.ToString();
        }
    }
}