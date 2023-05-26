using CompurShop.Domain.Entities;
using CompurShop.Domain.Services;
using CompurShop.WebClient.App_Start;
using CompurShop.WebClient.WebProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CompurShop.WebClient.View.Listas
{
    public partial class UcLista : System.Web.UI.UserControl
    {
        private readonly ListaService _ListaService;
        private readonly CombosService _CombosService;

        public UcLista()
        {
            _ListaService = DependencyConfig.Resolve<ListaService>();
            _CombosService = DependencyConfig.Resolve<CombosService>();
        }

        // Delegate
        public delegate void GravouListaEventHandler(string mensagem);
        // Event
        public event GravouListaEventHandler GravouLista;


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

        }

        protected bool PrepararLista(out int linhas)
        {
            if (fileUpload.HasFile)
            {
                // Verifique se o arquivo é um arquivo CSV
                if (Path.GetExtension(fileUpload.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    // Salve o arquivo em algum diretório
                    string filePath = Server.MapPath("~/Arquivos/") + fileUpload.FileName;
                    fileUpload.SaveAs(filePath);

                    // Processar o arquivo CSV (exemplo: ler as linhas)
                    string[] lines = File.ReadAllLines(filePath);

                    var texto = File.ReadAllText(filePath);

                    VS_Lista = _ListaService.PreparaLista(texto);

                    linhas = lines.Length;
                    return true;
                }
            }
            linhas = 0;
            // arquivo não encontrado
            return false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var processouarquivo = PrepararLista(out int linhas);
                        
            if (!processouarquivo)
            {
                labelMessage.Text = AlertMessage.GetMessage("Nenhum arquivo seleciona ou arquivo inválido. Tente novamente"
                    , AlertMessage.TipoMensagem.Sucesso);
                return;
            }                       

            VS_Lista.Nome = txtNome.Text;
            VS_Lista.IdCliente = int.Parse(listClientesNew.SelectedValue);
            VS_Lista.QtdeCpfs =_ListaService.GravarLista(VS_Lista);

            int repetidos = linhas - VS_Lista.QtdeCpfs;

            string mensagem = string.Format("Qtde lidos: {0} - Repetidos {1} ", VS_Lista.QtdeCpfs, repetidos);

            GravouLista(mensagem);
        }

        public void CarregarDados(Lista lista, List<Cliente> clientes)
        {
            VS_Lista = lista;
            Vs_Clientes = clientes;
            
            txtNome.Text = lista.Nome;
            CarregarCombos();
        }

        private async void CarregarCombos()
        {
            listClientesNew.DataSource = Vs_Clientes;
            await Task.Run(()=> listClientesNew.ClearSelection());
            listClientesNew.DataBind();

            if (__SessionWEB.IdCliente > 0)
            {
                listClientesNew.SelectedValue = __SessionWEB.IdCliente.ToString();
                listClientesNew.Enabled = false;
            }
        }
    }
}