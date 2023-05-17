using CompurShop.Domain.Entities;
using CompurShop.Domain.Services;
using CompurShop.WebClient.App_Start;
using CompurShop.WebClient.WebProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using System.Linq;

namespace CompurShop.WebClient.View.Listas
{
    public partial class Listas : System.Web.UI.Page
    {
        private readonly ListaService _ListaService;
        private IEnumerable<Lista> VS_Listas
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

        public Listas()
        {
            _ListaService = DependencyConfig.Resolve<ListaService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            VS_Listas = _ListaService.BuscarListas();
            gridListas.DataSource = VS_Listas;
            gridListas.DataBind();
        }

        protected void gridListas_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
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

                    VS_Lista = _ListaService.PreparaLista(lines, texto);
                    

                    lblMensagem.Text = string.Format("Qtde lidos: {0} - Repetidos {1} ", lines.Length, VS_Lista.QtdeCpfs);
                    lblMensagem.Visible = true;

                    
                }
                else
                {
                    // O arquivo não é um arquivo CSV
                    Response.Write("Selecione um arquivo CSV.");
                }
            }           
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            VS_Lista.Nome = txtNome.Text;

            _ListaService.GravarLista(VS_Lista);

            VS_Listas = _ListaService.BuscarListas();
            gridListas.DataSource = VS_Listas;
            gridListas.DataBind();

        }

        protected void gridListas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnFile")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument); // obter índice da linha selecionada
                    int idArea = Convert.ToInt32(gridListas.DataKeys[index].Value);

                    Lista MinhaLista = _ListaService.BuscarListas().Where(l => l.Id == idArea).FirstOrDefault();
                    if (MinhaLista == null)
                    {
                        string script = "alert('Não foi possível gerar arquivo!');";

                        // Registre o script na página
                        ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
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
                        string script = "alert('Não foi possível gerar arquivo!');";

                        // Registre o script na página
                        ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
                        return;
                    }

                }
                catch (Exception ex)
                {
                    string script = "alert('Não foi possível gerar arquivo!');";

                    // Registre o script na página
                    ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
                    return;
                }

            }
        }
    }
}