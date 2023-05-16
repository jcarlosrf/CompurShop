using CompurShop.Domain.Services;
using CompurShop.WebClient.App_Start;
using System;
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
            Label pageTItle = Page.Master.FindControl("LabelTitlePage") as Label;
            if (pageTItle != null)
            {
                pageTItle.Text = Page.Title;
            }           
        }


        protected void gridClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gridClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].CssClass = "icon-column";
                e.Row.Cells[1].CssClass = "icon-column";
            }

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    // Estilizar o link de edição
            //    LinkButton editLink = (LinkButton)e.Row.Cells[0].Controls[0];
            //    editLink.Text = "";
            //    editLink.CssClass = "icon-link bi-pencil";

            //    // Estilizar o link de exclusão
            //    LinkButton deleteLink = (LinkButton)e.Row.Cells[0].Controls[2];
            //    deleteLink.Text = "";
            //    deleteLink.CssClass = "icon-link bi-trash";
            //}
        }

        protected void gridClientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            var clientes = _clienteService.BuscarClientesPorNome("");
            gridClientes.DataSource = clientes;
            gridClientes.DataBind();
        }

        protected void gridClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
                {
                case "cmdEdit":
                    var rowIndex = e.CommandArgument;
                    
                    
                    // GridViewRow row = gridClientes.Rows[rowIndex];


                    ScriptManager.RegisterStartupScript(this, GetType(), "EditCliente", "$(document).ready(function(){ openModalDanger('#modalDiv'); }); ", true);


                    break;
                case "cmdDelete":
                    break;

            }
        }
    }
}