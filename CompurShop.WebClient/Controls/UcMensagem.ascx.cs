using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompurShop.WebClient.Controls
{
    public partial class UcMensagem : System.Web.UI.UserControl
    {
        // Delegate
        public delegate void MensagemConfirmouEventHandler(object sender, string identificador);
        // Event
        public event MensagemConfirmouEventHandler MensagemConfirmou;

        public object VsObjeto
        {
            get
            {
                if (ViewState["VsObjeto"] == null)
                    ViewState["VsObjeto"] = new object();
                return (object)ViewState["VsObjeto"];
            }
            set
            {
                ViewState["VsObjeto"] = value;
            }
        }

        public string VsIdentificador
        {
            get
            {
                if (ViewState["VsIdentificador"] == null)
                    ViewState["VsIdentificador"] = string.Empty;
                return (string)ViewState["VsIdentificador"];
            }
            set
            {
                ViewState["VsIdentificador"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CarregarMensagem(string mensagem, object sender, string identificador)
        {
            VsObjeto = sender;
            labelTitulo.Text = mensagem;
            VsIdentificador = identificador;
        }
            
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            MensagemConfirmou(VsObjeto, VsIdentificador);
        }
    }
}