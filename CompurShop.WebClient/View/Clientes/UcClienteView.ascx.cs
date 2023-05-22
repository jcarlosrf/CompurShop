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
        private readonly CombosService _comboService;
               

        public UcClienteView()
        {
            _comboService = DependencyConfig.Resolve<CombosService>();            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        public void CarregarCombo(List<Uf> ufs)
        {
            dropEstados.DataSource = ufs;
            dropEstados.DataBind();
            dropEstados.SelectedIndex = -1;
        }


        protected void btnSave_Click1(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "EditCliente", "$(document).ready(function(){ closeModal('#modalDiv'); }); ", true);
                updCliente.Update();
            }
        }
    }
}