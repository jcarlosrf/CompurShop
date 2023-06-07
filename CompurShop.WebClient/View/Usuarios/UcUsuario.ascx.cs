using CompurShop.Domain.Entities;
using CompurShop.Domain.Services;
using CompurShop.WebClient.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompurShop.WebClient.View.Usuarios
{
    public partial class UcUsuario : System.Web.UI.UserControl
    {
        private readonly UsuarioService _userService;

        // Delegate
        public delegate void GravouUsuarioEventHandler(object sender, string nome);
        // Event
        public event GravouUsuarioEventHandler GravouUser;

        public UcUsuario()
        {
            _userService = DependencyConfig.Resolve<UsuarioService>();
        }

        protected List<Cliente> Vs_Clientes
        {
            get
            {
                if (ViewState["Vs_Clientes"] == null)
                    ViewState["Vs_Clientes"] = new List<Uf>();
                return (List<Cliente>)ViewState["Vs_Clientes"];
            }
            set
            {
                ViewState["Vs_Clientes"] = value;
            }
        }

        protected Usuario Vs_User
        {
            get
            {
                if (ViewState["Vs_User"] == null)
                    ViewState["Vs_User"] = new Usuario();
                return (Usuario)ViewState["Vs_User"];
            }
            set
            {
                ViewState["Vs_User"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Usuario cliente = Vs_User;

                cliente.Nome = textNome.Text;
                int.TryParse(listClientesUser.SelectedValue, out int codcliente);
                cliente.IdCliente = codcliente;
                cliente.IdNivel = codcliente > 0 ? 1 : 2;
                cliente.Senha = txtSenha.Text;

                _userService.Gravar(cliente);

                GravouUser(sender, cliente.Nome);
            }
        }

        public void CarregarDados(Usuario user, List<Cliente> clientes)
        {
            Vs_Clientes = clientes;
            Vs_User = user;

            textNome.Text = user.Nome;
            CarregarCombos();
        }

        private async void CarregarCombos()
        {
            listClientesUser.DataSource = Vs_Clientes;
            await Task.Run(() => listClientesUser.ClearSelection());
            listClientesUser.DataBind();
            listClientesUser.SelectedValue = "0";

            if (Vs_User.IdCliente > 0)
            {
                listClientesUser.SelectedValue = Vs_User.IdCliente.ToString();
            }
        }
    }
}