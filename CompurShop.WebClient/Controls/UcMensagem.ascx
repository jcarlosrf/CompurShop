<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcMensagem.ascx.cs" Inherits="CompurShop.WebClient.Controls.UcMensagem" %>

<div class="modal-dialog modal-dialog-scrollable" style="min-width: 500px">
    <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title">
                <asp:Label runat="server" ID="labelTitulo" Text=""></asp:Label>
            </h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
            </button>
        </div>
        <div class="modal-body">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <span class="bi bi-question-circle-fill bi-question-message"></span>
                        </div>
                        <div class="col-md-4 offset-md-2 d-flex align-items-center">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style="margin-left: auto; margin-right: 10px; margin-top: 10px">Cancelar</button>                        
                            <asp:Button runat="server" ID="btnConfirm" SkinID="btPrimary" Text="Confirmar" style="margin-top: 10px"
                                OnClick=btnConfirm_Click CausesValidation="true"  />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
