using CompurShop.Domain.Services;
using CompurShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CompurShop.WebClient.App_Start;

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


            var clientes = _clienteService.BuscarClientesPorNome("");
            gridClientes.DataSource = clientes;
            gridClientes.DataBind();
        }

        protected void gridClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gridClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}