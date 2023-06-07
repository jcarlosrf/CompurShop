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
namespace CompurShop.WebClient.View.Usuarios
{
    public partial class Usuarios : System.Web.UI.Page
    {
        private readonly UsuarioService _userService;
        private readonly CombosService _CombosService;

        protected List<Usuario> Vs_Users
        {
            get
            {
                if (ViewState["Vs_Users"] == null)
                    ViewState["Vs_Users"] = new List<Cliente>();
                return (List<Usuario>)ViewState["Vs_Users"];
            }
            set
            {
                ViewState["Vs_Users"] = value;
            }
        }

        protected List<Cliente> Vs_Clientes
        {
            get
            {
                if (ViewState["Vs_Clientes"] == null)
                    ViewState["Vs_Clientes"] = new List<Uf>();
                return (List<Cliente>)ViewState["Vs_Clientes"];
            }
            set
            {
                ViewState["Vs_Clientes"] = value;
            }
        }

        public Usuarios()
        {
            _userService = DependencyConfig.Resolve<UsuarioService>();
            _CombosService = DependencyConfig.Resolve<CombosService>();
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


            UcUsuario.GravouUser += UcUsuario_GravouUser;
            UcMensagem1.MensagemConfirmou += UcMensagem1_MensagemConfirmou;
        }

        private void UcUsuario_GravouUser(object sender, string nome)
        {
            labelMessage.Text = AlertMessage.GetMessage(string.Format("Usuário {0} gravado com sucesso.", nome), AlertMessage.TipoMensagem.Sucesso);
            gridUsuarios.DataBind();
        }

        private async void CarregarCombos()
        {
            Vs_Clientes = await _CombosService.GetClientes();            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                gridUsuarios.PageIndex = 0;
                gridUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                labelMessage.Text = AlertMessage.GetMessage("Erro ao realizar pesquisa. Tente novamente" + Environment.NewLine + ex.Message, AlertMessage.TipoMensagem.Alerta);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Usuario cliente = new Usuario { Id = 0 };
            UcUsuario.CarregarDados(cliente, Vs_Clientes);
            ScriptManager.RegisterStartupScript(this, GetType(), "EditUsuario", "$(document).ready(function(){ openModal('#modalDiv'); }); ", true);
        }

        public IEnumerable<CompurShop.Domain.Entities.Usuario> gridUsuarios_GetData()
        {
            try
            {
                Vs_Users = _userService.GetbyNome(txtNome.Text.Trim()).ToList();

                return Vs_Users;
            }
            catch (Exception ex)
            {
                labelMessage.Text = AlertMessage.GetMessage("Erro ao realizar pesquisa. Tente novamente" + Environment.NewLine + ex.Message, AlertMessage.TipoMensagem.Alerta);
                return new List<Usuario>();
            }
        }

        protected void gridUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].CssClass = "icon-column";
                e.Row.Cells[1].CssClass = "icon-column";
            }
        }

        protected void gridUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Usuario cliente = new Usuario();
            int rowID = int.Parse(e.CommandArgument.ToString());
            cliente = Vs_Users.FirstOrDefault(c => c.Id == rowID);

            switch (e.CommandName)
            {
                case "cmdEdit":
                    UcUsuario.CarregarDados(cliente, Vs_Clientes);
                    ScriptManager.RegisterStartupScript(this, GetType(), "EditCliente", "$(document).ready(function(){ openModal('#modalDiv'); }); ", true);

                    break;

                case "cmdDelete":

                    UcMensagem1.CarregarMensagem(string.Format("Confirma exclusão do usuário {0} - {1}?", cliente.Id, cliente.Nome)
                        , cliente, "Exclusão");
                    ScriptManager.RegisterStartupScript(this, GetType(), "DeleteCliente", "$(document).ready(function(){ openModal('#modalDivMensagem'); }); ", true);

                    break;
            }
        }

        private void UcMensagem1_MensagemConfirmou(object sender, string identificador)
        {
            try
            {
                if (identificador == "Exclusão")
                {
                    Usuario cliente = (Usuario)sender;
                    _userService.Apagar(cliente.Id);
                    labelMessage.Text = AlertMessage.GetMessage("Usuário excluído.", AlertMessage.TipoMensagem.Sucesso);
                    gridUsuarios.DataBind();
                }
            }
            catch (Exception ex)
            {
                labelMessage.Text = AlertMessage.GetMessage("Erro: " + ex.Message, AlertMessage.TipoMensagem.Erro);
            }
        }
    }
}