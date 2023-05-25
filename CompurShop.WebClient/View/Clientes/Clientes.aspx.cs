using CompurShop.Domain.Entities;
using CompurShop.Domain.Services;
using CompurShop.WebClient.App_Start;
using CompurShop.WebClient.WebProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CompurShop.WebClient.View.Clientes
{
    public partial class Clientes : System.Web.UI.Page
    {
        private readonly ClienteService _clienteService;
        private readonly CombosService _comboService;

        protected List<Uf> Vs_Uf
        {
            get
            {
                if (ViewState["VS_Uf"] == null)
                    ViewState["VS_Uf"] = new List<Uf>();
                return (List<Uf>)ViewState["VS_Uf"];
            }
            set
            {
                ViewState["VS_Uf"] = value;                
            }
        }

        protected List<Cliente> Vs_Clientes
        {
            get
            {
                if (ViewState["Vs_Clientes"] == null)
                    ViewState["Vs_Clientes"] = new List<Cliente>();
                return (List<Cliente>)ViewState["Vs_Clientes"];
            }
            set
            {
                ViewState["Vs_Clientes"] = value;
            }
        }

        public Clientes()
        {
            _clienteService = DependencyConfig.Resolve<ClienteService>();
            _comboService = DependencyConfig.Resolve<CombosService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                Label pageTItle = Page.Master.FindControl("LabelTitlePage") as Label;

                if (pageTItle != null)
                {
                    pageTItle.Text = Page.Title;
                }

                CarregarCombos();
            }
            UcClienteView.GravouCliente += UcClienteView_GravouCliente;
            UcMensagem1.MensagemConfirmou += UcMensagem_MensagemConfirmou;
        }


        private void UcClienteView_GravouCliente(object sender, string nome)
        {
            labelMessage.Text = AlertMessage.GetMessage(string.Format("Cliente {0} gravado com sucesso.", nome) , AlertMessage.TipoMensagem.Sucesso);
            gridClientes.DataBind();
        }

        private async void CarregarCombos()
        {
            Vs_Uf = await _comboService.GetEstados();           
        }

        protected void BtnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                gridClientes.PageIndex = 0;
                gridClientes.DataBind();
            }
            catch (Exception ex)
            {
                labelMessage.Text = AlertMessage.GetMessage("Erro ao realizar pesquisa. Tente novamente" + Environment.NewLine + ex.Message, AlertMessage.TipoMensagem.Alerta) ;
            }
        }


        protected void gridClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridClientes.PageIndex = e.NewPageIndex;
        }

        protected void gridClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].CssClass = "icon-column";
                e.Row.Cells[1].CssClass = "icon-column";
            }            
        }
        

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IEnumerable<CompurShop.Domain.Entities.Cliente> gridClientes_GetData(int startRowIndex, int maximumRows, out int totalRowCount)
        {
            try
            {
                startRowIndex = gridClientes.PageIndex * maximumRows;   
                Vs_Clientes  = _clienteService.BuscarClientesPorNome(txtNome.Text.Trim(), txtCpf.Text.Trim(), startRowIndex, maximumRows, out totalRowCount).ToList();

                return Vs_Clientes;
            }
            catch (Exception ex)
            {
                totalRowCount = 0;
                labelMessage.Text = AlertMessage.GetMessage(ex.Message, AlertMessage.TipoMensagem.Erro);
                return new List<Cliente>();
            }
        }

        protected void DropLinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int registros = int.Parse(dropLinhas.SelectedValue);
            gridClientes.PageSize = registros;
            gridClientes.DataBind();
        }

        protected void gridClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Cliente cliente = new Cliente();
            int rowID = int.Parse(e.CommandArgument.ToString());
            cliente = Vs_Clientes.FirstOrDefault(c => c.Id == rowID);

            switch (e.CommandName)
            {                
                case "cmdEdit":
                    UcClienteView.CarregarDados(cliente, Vs_Uf);
                    ScriptManager.RegisterStartupScript(this, GetType(), "EditCliente", "$(document).ready(function(){ openModal('#modalDiv'); }); ", true);

                    break;

                case "cmdDelete":

                    UcMensagem1.CarregarMensagem(string.Format("Confirma exclusão do cliente {0} - {1}?", cliente.CPFCNPJ, cliente.Nome)
                        , cliente, "Exclusão");
                    ScriptManager.RegisterStartupScript(this, GetType(), "DeleteCliente", "$(document).ready(function(){ openModal('#modalDivMensagem'); }); ", true);

                    break;
            }
        }

        private void UcMensagem_MensagemConfirmou(object sender, string identificador)
        {
            try
            {
                if (identificador == "Exclusão")
                {
                    Cliente cliente = (Cliente)sender;
                    _clienteService.Deletar(cliente);
                    labelMessage.Text = AlertMessage.GetMessage("Cliente excluído.", AlertMessage.TipoMensagem.Sucesso);
                    gridClientes.DataBind();
                }
            }
            catch (Exception ex)
            {
                labelMessage.Text = AlertMessage.GetMessage("Erro: " + ex.Message, AlertMessage.TipoMensagem.Erro);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente { Id = 0 };
            UcClienteView.CarregarDados(cliente, Vs_Uf);
            ScriptManager.RegisterStartupScript(this, GetType(), "EditCliente", "$(document).ready(function(){ openModal('#modalDiv'); }); ", true);

        }
    }
}