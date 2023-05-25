using CompurShop.Domain.Services;
using CompurShop.WebClient.App_Start;
using CompurShop.WebClient.WebProject;
using System;
using System.Web;

namespace CompurShop.WebClient.Login
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly UsuarioService _usuarioService;
        public Login()
        {
            _usuarioService = DependencyConfig.Resolve<UsuarioService>();
        }

        public SessionWEB _SessionWEB
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                yourPassword.Text = "";
                yourUsername.Text = "";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string senhax = Criptografia.CriptografiaSenha(yourPassword.Text.Trim());
                string userx = yourUsername.Text.Trim();

                var usuario = _usuarioService.GetPermissao(userx, senhax);

                if (usuario == null)
                    throw new Exception("Usuário ou Password incorretos!");


                if (_SessionWEB == null)
                    _SessionWEB = new SessionWEB();
                _SessionWEB.UsuarioLogado = usuario.Nome;
                _SessionWEB.IdCliente = usuario.IdCliente;
                _SessionWEB.IdNivel = usuario.IdNivel;

                Response.Redirect("~\\Default.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            catch (Exception ex)
            {
                labelMessage.Text = AlertMessage.GetMessage(ex.Message, AlertMessage.TipoMensagem.Erro);
            }
        }
    }
}