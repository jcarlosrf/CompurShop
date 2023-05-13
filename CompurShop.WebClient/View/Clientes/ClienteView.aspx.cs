using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompurShop.WebClient.View.Clientes
{
    public partial class ClienteView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label pageTItle = Page.Master.FindControl("LabelTitlePage") as Label;
            if (pageTItle != null)
            {
                pageTItle.Text = Page.Title;
            }

        }

    }
}