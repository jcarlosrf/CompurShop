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

namespace CompurShop.WebClient.View.Clientes
{
    public partial class UcClienteView : System.Web.UI.UserControl
    {
        private readonly ClienteService _clienteService;
        private readonly UsuarioService _userService;

        // Delegate
        public delegate void GravouClienteEventHandler(object sender, string nome);
        // Event
        public event GravouClienteEventHandler GravouCliente;

        public Cliente VsCliente
        {
            get
            {
                if (ViewState["VS_Cliente"] == null)
                    ViewState["VS_Uf"] = new Cliente();
                return (Cliente)ViewState["VS_Cliente"];
            }
            set
            {
                ViewState["VS_Cliente"] = value;
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

        public UcClienteView()
        {
            _clienteService = DependencyConfig.Resolve<ClienteService>();
            _userService = DependencyConfig.Resolve<UsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Label pageTItle = Page.Master.FindControl("LabelTitlePage") as Label;

            if (pageTItle != null)
            {
                pageTItle.Text = Page.Title;
                VsCliente = new Cliente();
            }
        }

        public void CarregarDados(Cliente cliente, List<Uf> ufs)
        {
            VsCliente = cliente;

            dropUF.DataSource = ufs;            
            dropUF.DataBind();

            textNome.Text = cliente.Nome;
            textCpf.Text = cliente.CPFCNPJ;
            textTelefone.Text = cliente.Telefone;
            textEmail.Text = cliente.Email;
            textLogradouro.Text = cliente.Logradouro;
            textNumnero.Text = cliente.Numero;
            textComplemento.Text = cliente.Complemento;
            textCidade.Text = cliente.Cidade;
            textBairro.Text = cliente.Bairro;
            textCep.Text = cliente.CEP;

            textLogin.Text = string.Empty;
            txtSenha.Text = string.Empty;
            txtSenha2.Text = string.Empty;

            // Carregar o valor no dropdown de estados
            if (VsCliente.UF != null)
                dropUF.SelectedValue = VsCliente.UF;
            else
            {
                dropUF.ClearSelection();                
            }
        }


        protected void btnSave_Click1(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Cliente cliente = VsCliente;

                cliente.Nome = textNome.Text;
                cliente.CPFCNPJ = textCpf.Text;
                cliente.Telefone = textTelefone.Text;
                cliente.Email = textEmail.Text;
                cliente.Logradouro = textLogradouro.Text;
                cliente.Numero = textNumnero.Text;
                cliente.Complemento = textComplemento.Text;
                cliente.Cidade = textCidade.Text;
                cliente.Bairro = textBairro.Text;
                cliente.CEP = textCep.Text;
                cliente.UF = dropUF.SelectedValue;

                _clienteService.GravarCliente(cliente);

                int codcliente = cliente.Id;

                Usuario usuario = Vs_User;
                usuario.Nome = textLogin.Text;

                usuario.IdCliente = codcliente;
                usuario.IdNivel = codcliente > 0 ? 1 : 2;
                usuario.Senha = txtSenha.Text;

                _userService.Gravar(usuario);

                GravouCliente(sender, cliente.Nome);
            }
        }
    }
}