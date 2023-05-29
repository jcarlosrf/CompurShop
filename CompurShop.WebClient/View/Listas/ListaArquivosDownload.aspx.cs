using CompurShop.Domain.Entities;
using CompurShop.Domain.Services;
using CompurShop.WebClient.App_Start;
using CompurShop.WebClient.WebProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompurShop.WebClient.View.Listas
{
    public partial class ListaArquivosDownload : System.Web.UI.Page
    {
        private readonly ListaService _ListaService;
        public ListaArquivosDownload()
        {
            _ListaService = DependencyConfig.Resolve<ListaService>();
        }

        private List<ListaArquivo> VS_ListaArquivos
        {
            get
            {
                if (ViewState["VS_ListaArquivos"] == null)
                    ViewState["VS_ListaArquivos"] = new List<ListaArquivo>();
                return (List<ListaArquivo>)ViewState["VS_ListaArquivos"];
            }
            set
            {
                ViewState["VS_ListaArquivos"] = value;
            }
        }

        private Lista VS_Lista
        {
            get
            {
                if (ViewState["VS_Lista"] == null)
                    ViewState["VS_Lista"] = new Lista();
                return (Lista)ViewState["VS_Lista"];
            }
            set
            {
                ViewState["VS_Lista"] = value;
            }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label pageTItle = Page.Master.FindControl("LabelTitlePage") as Label;

                if (pageTItle != null)
                {
                    pageTItle.Text = Page.Title;
                }

                var lista = __SessionWEB.PostObject;
                if (lista!=null)
                    VS_Lista = (Lista)lista;
            }

            lblTitulo.Text = string.Format("{0} - {1}", VS_Lista.Id, VS_Lista.Nome);
        }

        protected void gridListas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DownloadDocumentos(e);
            }
            catch
            {
                labelMessage.Text = AlertMessage.GetMessage("Não foi possível gerar arquivo", AlertMessage.TipoMensagem.Erro);
            }
        }

        private async void DownloadDocumentos(GridViewCommandEventArgs e)
        {
            try
            {
                int idLista = await Task.Run(() => Convert.ToInt32(e.CommandArgument));
                var lista = VS_ListaArquivos.FirstOrDefault(l => l.Id == idLista);

                string sourceFilePath = Properties.Settings.Default.PastaArquivos + lista.NomeArquivo;
                string destinationFilePath = Server.MapPath("~/download/" + lista.NomeArquivo);

                if (File.Exists(sourceFilePath))
                {
                    File.Copy(sourceFilePath, destinationFilePath, true);
                    ArquivoCsv.Download(lista.NomeArquivo);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                // Ignorar a exceção ThreadAbortException
            }
            catch
            {
                labelMessage.Text = AlertMessage.GetMessage("Não foi possível gerar arquivo", AlertMessage.TipoMensagem.Erro);
            }
        }
            // The return type can be changed to IEnumerable, however to support
            // paging and sorting, the following parameters must be added:
            //     int maximumRows
            //     int startRowIndex
            //     out int totalRowCount
            //     string sortByExpression
        public IEnumerable<CompurShop.Domain.Entities.ListaArquivo> gridListas_GetData()
        {
            VS_ListaArquivos = _ListaService.BuscarArquivosPorLista(VS_Lista.Id);

            return VS_ListaArquivos;
        }
    }
}