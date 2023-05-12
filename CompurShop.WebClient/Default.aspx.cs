using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompurShop.WebClient
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Defina o título da página
            //Page.Title = "Home";

            // Defina o título da página no ContentPlaceHolder
            Label pageTItle = Page.Master.FindControl("LabelTitlePage") as Label;
            if (pageTItle != null)
            {
                pageTItle.Text = Page.Title;
            }


        }
    }
}