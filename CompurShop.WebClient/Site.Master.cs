using CompurShop.WebClient.WebProject;
using System;
using System.Threading;
using System.Web;
using System.Web.UI;

namespace CompurShop.WebClient
{
    public partial class SiteMaster : MasterPage
    {
        public SessionWEB __SessionWEB
        {
            get
            {
                return (SessionWEB)Session[SessionWEB.SessSessionWEB];
            }
            set
            {
                Session[SessionWEB.SessSessionWEB] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (__SessionWEB == null)
            {
                try
                {
                    __SessionWEB = new SessionWEB();
                    HttpContext.Current.Response.Redirect("~\\Login\\login.aspx", true);
                }
                catch (ThreadAbortException)
                {
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            else if (String.IsNullOrEmpty(__SessionWEB.UsuarioLogado))
            {
                try
                {
                    __SessionWEB = new SessionWEB();
                    HttpContext.Current.Response.Redirect("~\\Login\\login.aspx", true);
                }
                catch (ThreadAbortException)
                {
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (__SessionWEB == null)
                    __SessionWEB = new SessionWEB();

                if (String.IsNullOrEmpty(__SessionWEB.UsuarioLogado))
                {
                    labelUser.Text = string.Empty;
                }
                else
                {
                    labelUser.Text = __SessionWEB.UsuarioLogado;
                    labelUserDetalhe.Text = __SessionWEB.UsuarioLogado;
                    labelNivel.Text = string.Format("Nivel: {0} - Cliente: {1}", __SessionWEB.IdNivel, __SessionWEB.IdCliente);
                }
            }
        }

    }
}