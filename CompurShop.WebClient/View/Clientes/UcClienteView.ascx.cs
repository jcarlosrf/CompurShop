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

        public UcClienteView()
        {
            _clienteService = DependencyConfig.Resolve<ClienteService>();
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
                
                GravouCliente(sender, cliente.Nome);
            }
        }
    }
}