<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcLoader.ascx.cs" Inherits="CompurShop.WebClient.Controls.UcLoader" %>

<asp:UpdateProgress ID="upgProgress" runat="server" DisplayAfter="10">
    <ProgressTemplate>
        <div class="loader">
            <div class="spinner-border text-primary" role="status" style="width: 35px; height: 35px; font-weight: bold" aria-hidden="true">
            </div>
            <h4>Processando...</h4>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
