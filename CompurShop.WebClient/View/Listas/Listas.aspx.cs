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

        private List<Lista> VS_Listas
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Label pageTItle = Page.Master.FindControl("LabelTitlePage") as Label;

                if (pageTItle != null)
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

            listClientes.DataSource = Vs_Clientes;
            listClientes.ClearSelection();
            listClientes.DataBind();

            if (__SessionWEB.IdCliente > 0)
            {
                listClientes.SelectedValue = __SessionWEB.IdCliente.ToString();
                listClientes.Enabled = false;
            }
        }


        protected void gridListas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnFile")
            {
                try
                {
                    int idLista = Convert.ToInt32(e.CommandArgument);

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
                        ArquivoCsv.DownloadCSV(FileName);
                    }
                    else
                    {
                        labelMessage.Text = AlertMessage.GetMessage("Não foi possível gerar arquivo", AlertMessage.TipoMensagem.Erro);
                    }

                }
                catch (Exception ex)
                {
                    labelMessage.Text = AlertMessage.GetMessage("Não foi possível gerar arquivo" + Environment.NewLine + ex.Message, AlertMessage.TipoMensagem.Erro);
                    return;
                }
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
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IEnumerable<Lista> GridListas_GetData()
        {
            _ = int.TryParse(listClientes.SelectedValue, out int idcliente);
            VS_Listas = _ListaService.BuscarListas(false, idcliente).ToList() ;

            return VS_Listas;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridListas.DataBind();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Lista lista = new Lista { Id = 0 };
            UcLista.CarregarDados(lista, Vs_Clientes);

            //ScriptManager.RegisterStartupScript(this, GetType(), "EditLista", "$(document).ready(function(){ openModal('#modalDiv'); }); ", true);            
        }

        private void UcLista_GravouLista(string mensagem)
        {
            labelMessage.Text = AlertMessage.GetMessage("Lista Gravada: " + mensagem, AlertMessage.TipoMensagem.Sucesso);
            gridListas.DataBind();
        }
    }
}