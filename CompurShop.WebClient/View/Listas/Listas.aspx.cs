using CompurShop.Domain.Entities;
using CompurShop.Domain.Services;
using CompurShop.WebClient.App_Start;
using CompurShop.WebClient.WebProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI;
using System.Web;
using System.Threading.Tasks;

namespace CompurShop.WebClient.View.Listas
{
    public partial class Listas : System.Web.UI.Page
    {
        private readonly ListaService _ListaService;
        private readonly CombosService _CombosService;
        public Listas()
        {
            _ListaService = DependencyConfig.Resolve<ListaService>();
            _CombosService = DependencyConfig.Resolve<CombosService>();
        }

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

        protected List<Lista> VS_Listas
        {
            get
            {
                if (ViewState["VS_Listas"] == null)
                    ViewState["VS_Listas"] = new List<Lista>();
                return (List<Lista>)ViewState["VS_Listas"];
            }
            set
            {
                ViewState["VS_Listas"] = value;
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

        protected bool Vs_Pesquisar
        {
            get
            {
                if (ViewState["Vs_Pesquisar"] == null)
                    ViewState["Vs_Pesquisar"] = true;
                return (bool)ViewState["Vs_Pesquisar"];
            }

            set
            {
                ViewState["Vs_Pesquisar"] = value;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Page.Master.FindControl("LabelTitlePage") is Label pageTItle)
                {
                    pageTItle.Text = Page.Title;
                }

                CarregarCombos();
            }

            UcLista.GravouLista += UcLista_GravouLista;
        }       

        private async void CarregarCombos()
        {
            Vs_Clientes = await _CombosService.GetClientes();
            if (__SessionWEB.IdCliente > 0)
            {
                Vs_Clientes = Vs_Clientes.Where(c => c.Id == __SessionWEB.IdCliente).ToList();
            }

            listClientes.DataSource = Vs_Clientes;
            listClientes.ClearSelection();
            listClientes.DataBind();

            listClientes.SelectedValue = "0";

            if (__SessionWEB.IdCliente > 0)
            {
                listClientes.SelectedValue = __SessionWEB.IdCliente.ToString();
                listClientes.Enabled = false;
            }
        }

        private async void DownaloadCsv(GridViewCommandEventArgs e)
        {
            try
            {
                int idLista = await Task.Run(()=> Convert.ToInt32(e.CommandArgument));

                Lista MinhaLista = _ListaService.GetListaById(idLista);
                if (MinhaLista == null)
                {
                    labelMessage.Text = AlertMessage.GetMessage("Não foi possível gerar arquivo", AlertMessage.TipoMensagem.Erro);
                    return;
                }

                List<Cpf> cpfs = _ListaService.CpfPorLista(MinhaLista.Id);

                string FileName = string.Format("Lista{0}_{1}.csv", MinhaLista.Nome, DateTime.Now.ToString("yyyyMMddHHmmss"));

                if (ArquivoCsv.ExportarParaCSV(cpfs, FileName))
                {
                    ArquivoCsv.Download(FileName);
                }
                else
                {
                    labelMessage.Text = AlertMessage.GetMessage("Não foi possível gerar arquivo", AlertMessage.TipoMensagem.Erro);
                }

            }
            catch (System.Threading.ThreadAbortException)
            {
                // Ignorar a exceção ThreadAbortException
            }
            catch (Exception ex)
            {
                labelMessage.Text = AlertMessage.GetMessage("Não foi possível gerar arquivo" + Environment.NewLine + ex.Message, AlertMessage.TipoMensagem.Erro);
                return;
            }
        }

        protected void gridListas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnFile")
            {
                DownaloadCsv(e);
            }
            else if (e.CommandName == "btnProcessar")
            {
                try
                {
                    int idLista = Convert.ToInt32(e.CommandArgument);
                    _ListaService.UpdateStatus(idLista, 1);

                    gridListas.DataBind();

                }
                catch (Exception ex)
                {
                    labelMessage.Text = AlertMessage.GetMessage("Não foi possível atualizar status " + Environment.NewLine + ex.Message, AlertMessage.TipoMensagem.Erro);
                    return;
                }
            }
            else if (e.CommandName == "btnFileDocumentos")
            {
                int idLista = Convert.ToInt32(e.CommandArgument);
                var lista = VS_Listas.FirstOrDefault(l => l.Id == idLista);

                __SessionWEB.PostMessages = "teste";
                __SessionWEB.PostObject = lista;

                Page page = (Page)HttpContext.Current.Handler;
                string script;
                script = @"window.open(""{0}"", ""{1}"");";

                script = String.Format(script, "ListaArquivosDownload.aspx", "_blank");
                ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
            }
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IEnumerable<Lista> GridListas_GetData()
        {
            if (IsPostBack)
            {
                gridListas.EmptyDataText = "Nenum registro encontrado";

                if (Vs_Pesquisar)
                {
                    _ = int.TryParse(listClientes.SelectedValue, out int idcliente);
                    VS_Listas = _ListaService.BuscarListas(false, idcliente).ToList();
                }

                Vs_Pesquisar = true;
                Timer1.Enabled = true;
                return VS_Listas;
            }
            gridListas.EmptyDataText = "Realize a pesquisa para carregar dados";
            return new List<Lista>();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridListas.DataBind();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Timer1.Enabled = false;
                Lista lista = new Lista { Id = 0 };
                UcLista.CarregarDados(lista, Vs_Clientes);

                ScriptManager.RegisterStartupScript(this, GetType(), "EditLista", "document.getElementById('collapseOne').classList.add('fade'); document.getElementById('collapseOne').classList.add('show');", true);
            }
            catch
            {
                Timer1.Enabled = true;
            }
        }

        private void UcLista_GravouLista(string mensagem)
        {
            Timer1.Enabled = true;            
            gridListas.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "EditLista", "document.getElementById('collapseOne').classList.remove('show');", true);

            if (!string.IsNullOrEmpty(mensagem))
                labelMessage.Text = AlertMessage.GetMessage("Lista Gravada: " + mensagem, AlertMessage.TipoMensagem.Sucesso);
        }

        protected async void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Timer1.Enabled = false;

                var listaProcessando = await Task.Run(() => VS_Listas.Where(l => l.Status == 1).ToList());
                bool atualizar = false;

                foreach (var lista in listaProcessando)
                {
                    var newlista = _ListaService.GetListaById(lista.Id);

                    if (newlista.Status != 1)
                    {
                        lista.Status = newlista.Status;
                        atualizar = true;
                    }
                }

                if (atualizar)
                {
                    Vs_Pesquisar = false;
                    gridListas.DataBind();
                }
            }
            finally
            {
                Timer1.Enabled = true;
            }
        }
    }
}