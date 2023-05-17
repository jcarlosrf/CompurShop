using CompurShop.Domain.Entities;
using CompurShop.Domain.Services;
using CompurShop.WebClient.App_Start;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CompurShop.WebClient.View.Clientes
{
    public partial class Clientes : System.Web.UI.Page
    {
        private readonly ClienteService _clienteService;

        private IEnumerable<Cliente> VS_Clientes
        {
            get
            {
                if (ViewState["VS_Clientes"] == null)
                    ViewState["VS_Clientes"] = new List<Cliente>();
                return (List<Cliente>)ViewState["VS_Clientes"];
            }
            set
            {
                ViewState["VS_Clientes"] = value;
            }
        }
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

                gridClientes.DataSource = VS_Clientes;
                gridClientes.DataBind();
            }          
        }


        protected void gridClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridClientes.PageIndex = e.NewPageIndex;
            gridClientes.DataSource = VS_Clientes;
            gridClientes.DataBind();
        }

        protected void gridClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].CssClass = "icon-column";
                e.Row.Cells[1].CssClass = "icon-column";
            }            
        }
        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            try {
                VS_Clientes = _clienteService.BuscarClientesPorNome(txtNome.Text.Trim(), txtCpf.Text.Trim(), 0,0);
                gridClientes.DataSource = VS_Clientes;
                gridClientes.DataBind();

               
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
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