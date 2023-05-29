using CompurShop.WebClient.WebProject;
using System;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CompurShop.WebClient
{
    public partial class SiteMaster : MasterPage
    {
        public SessionWEB __SessionWEB
        {
            get
            {
                return (SessionWEB)Session[SessionWEB.SessSessionWEB];
            }
            set
            {
                Session[SessionWEB.SessSessionWEB] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
#if (DEBUG)

            if(__SessionWEB == null)
            __SessionWEB = new SessionWEB() { IdNivel = 1, IdCliente = 0, UsuarioLogado = "#debug" };
            
#endif

            if (__SessionWEB == null)
            {
                try
                {
                    __SessionWEB = new SessionWEB();
                    HttpContext.Current.Response.Redirect("~\\Login\\login.aspx", true);
                }
                catch (ThreadAbortException)
                {
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            else if (String.IsNullOrEmpty(__SessionWEB.UsuarioLogado))
            {
                try
                {
                    __SessionWEB = new SessionWEB();
                    HttpContext.Current.Response.Redirect("~\\Login\\login.aspx", true);
                }
                catch (ThreadAbortException)
                {
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (__SessionWEB == null)
                    __SessionWEB = new SessionWEB();

                if (String.IsNullOrEmpty(__SessionWEB.UsuarioLogado))
                {
                    labelUser.Text = string.Empty;
                }
                else
                {
                    labelUser.Text = __SessionWEB.UsuarioLogado;
                    labelUserDetalhe.Text = __SessionWEB.UsuarioLogado;
                    labelNivel.Text = string.Format("Nivel: {0} - Cliente: {1}", __SessionWEB.IdNivel, __SessionWEB.IdCliente);
                }

                BuildMenuFromSiteMap();
            }
        }

        private void BuildMenuFromSiteMap()
        {
            SiteMapNode rootNode = SiteMap.RootNode;

            if (rootNode != null)
            {
                var sidebarDiv = new HtmlGenericControl("aside");
                sidebarDiv.ID = "sidebar";
                sidebarDiv.Attributes.Add("class", "sidebar");

                var sidebarNavPanel = new Panel();
                sidebarNavPanel.ID = "sidebar-nav";
                sidebarNavPanel.CssClass = "sidebar-nav";

                sidebarDiv.Controls.Add(sidebarNavPanel);

                BuildMenuNodes(rootNode.ChildNodes, sidebarNavPanel);

                mainForm.Controls.Add(sidebarDiv);
            }
        }

        private void BuildMenuNodes(SiteMapNodeCollection nodes, Control parentControl)
        {
            foreach (SiteMapNode node in nodes)
            {
                int role = int.Parse(node.Roles[0].ToString());
                if (role < __SessionWEB.IdNivel)
                    continue;


                if (node != null && node.Url != null)
                {
                    string iditem = node.Description;
                    string iconmenu = node.ResourceKey;                   

                    var listItem = new HtmlGenericControl("li");
                    listItem.Attributes.Add("class", "nav-item");

                    var link = new HyperLink();
                    link.CssClass = "nav-link collapsed";
                    link.Attributes.Add("data-bs-target", "#" + iditem);
                    link.Attributes.Add("data-bs-toggle", "collapse");
                    link.NavigateUrl = node.Url;

                    var icon = new HtmlGenericControl("i");
                    icon.Attributes.Add("class", iconmenu);

                    var span = new HtmlGenericControl("span");
                    span.InnerText = node.Title;

                    var chevronIcon = new HtmlGenericControl("i");
                    chevronIcon.Attributes.Add("class", "bi bi-chevron-down ms-auto");

                    link.Controls.Add(icon);
                    link.Controls.Add(span);
                    link.Controls.Add(chevronIcon);

                    listItem.Controls.Add(link);

                    if (node.HasChildNodes)
                    {
                        var childUl = new HtmlGenericControl("ul");
                        childUl.Attributes.Add("id", iditem);
                        childUl.Attributes.Add("class", "nav-content collapse");
                        childUl.Attributes.Add("data-bs-parent", "#sidebar-nav");

                        listItem.Controls.Add(childUl);

                        BuildChildMenuNodes(node.ChildNodes, childUl);
                    }

                    parentControl.Controls.Add(listItem);
                }
            }
        }

        private void BuildChildMenuNodes(SiteMapNodeCollection nodes, Control parentControl)
        {
            foreach (SiteMapNode node in nodes)
            {
                int role = int.Parse(node.Roles[0].ToString());
                if (role < __SessionWEB.IdNivel)
                    continue;

                if (node != null && node.Url != null)
                {
                    var listItem = new HtmlGenericControl("li");
                    
                    var link = new HyperLink();
                    link.NavigateUrl = node.Url;
                    link.Attributes.Add("class", "nav-link");

                    var icon = new HtmlGenericControl("i");
                    icon.Attributes.Add("class", "bi bi-circle");

                    var span = new HtmlGenericControl("span");
                    span.InnerText = node.Title;

                    link.Controls.Add(icon);
                    link.Controls.Add(span);

                    listItem.Controls.Add(link);
                    parentControl.Controls.Add(listItem);
                }
            }
        }



    }
}