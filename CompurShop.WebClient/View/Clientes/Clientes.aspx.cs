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

        public Clientes()
        {
            _clienteService = DependencyConfig.Resolve<ClienteService>();
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
            }          
        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                gridClientes.PageIndex = 0;
                gridClientes.DataBind();
            }
            catch (Exception ex)
            {
                labelMessage.Text = ErrorMessage.GetErroMessage("Não foi possivel realizar pesquisa. Tente novamente", ErrorMessage.TipoMensagem.Alerta) ;
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
                return _clienteService.BuscarClientesPorNome(txtNome.Text.Trim(), txtCpf.Text.Trim(), startRowIndex, maximumRows, out totalRowCount);                
            }
            catch (Exception ex)
            {
                totalRowCount = 0;
                labelMessage.Text = ErrorMessage.GetErroMessage(ex.Message, ErrorMessage.TipoMensagem.Erro);
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
            switch (e.CommandName)
            {
                case "cmdEdit":
                    var rowIndex = e.CommandArgument;


                    // GridViewRow row = gridClientes.Rows[rowIndex];


                    ScriptManager.RegisterStartupScript(this, GetType(), "EditCliente", "$(document).ready(function(){ openModal('#modalDiv'); }); ", true);


                    break;
                case "cmdDelete":
                    break;

            }
        }
    }
}