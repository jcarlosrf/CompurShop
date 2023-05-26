<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcLista.ascx.cs" Inherits="CompurShop.WebClient.View.Listas.UcLista" %>

<%@ Register Src="~/Controls/UcLoader.ascx" TagPrefix="uc1" TagName="UcLoader" %>

<div class="card" style="min-width: 900px">
    <div class="card-header">
         <asp:Label runat="server" ID="labelTitulo" SkinID=labelItem Text="Cadastrar Lista de CPFs"></asp:Label>
    </div>

    <div class="card-body">

        <asp:UpdatePanel ID="updListas" runat="server" UpdateMode="Always" >
            <ContentTemplate>
                
                <asp:Label ID="labelMessage" runat="server" EnableViewState="False"></asp:Label>
                <div class="row">
                    <div class="col-sm-12 col-md-4">
                        <asp:Label ID="labelNome" runat="server" Text="Nome" AssociatedControlID="txtNome"
                            SkinID="labelItem"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
                            ValidationGroup="VgLista" ErrorMessage="Nome da lista é obrigatório." CssClass="text-danger" Display="Dynamic">
                                        <i class="bi bi-exclamation-circle"></i>
                        </asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtNome" runat="server" aria-describedby="basic-addon1"
                            SkinID="textInput" ReadOnly="false"></asp:TextBox>
                    </div>

                    <div class="col-sm-12 col-md-4 ui-widget">
                        <asp:Label ID="label2" runat="server" Text="Cliente" AssociatedControlID="listClientesNew" Width="100%"
                            SkinID="labelItem"></asp:Label>
                        <asp:ListBox runat="server" ID="listClientesNew" ClientIDMode="Static" AutoPostBack="false" CssClass="form-control"
                            DataTextField="nome" AppendDataBoundItems="true" DataValueField="id" ItemType="CompurShop.Domain.Entities.Cliente"></asp:ListBox>
                    </div>


                    <div class="input-wrapper col-sm-12 col-md-4 d-flex align-items-end">
                        <asp:FileUpload ID="fileUpload" ClientIDMode="Static" runat="server" CssClass="fileload" AllowMultiple="false" Accept=".csv" style="margin-bottom: 15px" />
                        <input type="text" name="file" id="file" class="filetext" placeholder="Arquivo" readonly="readonly" style="margin-bottom: 15px" >
                        <asp:Button runat="server" CausesValidation="false" CssClass="btnfile" Text="Selecionar" ValidationGroup="VgLista" style="margin-bottom: 15px"
                            OnClientClick=" $('.fileload').trigger('click');return false; "></asp:Button>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="card-footer">
        <div class="input-group row">
            <div class="col-md-8">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="VgLista" Width="100%" />
            </div>
            <div class="col-md-4 d-flex align-items-end">
                <button type="button" class="btn btn-secondary" style="margin-left: auto; margin-right: 10px"
                    onclick="document.getElementById('collapseOne').classList.remove('show'); return false;">Fechar</button>
                <asp:Button runat="server" ID="btnSave" SkinID="btPrimary" Text="Gravar"
                    OnClick="btnSalvar_Click" CausesValidation="true" ValidationGroup="VgLista" />
            </div>
        </div>
    </div>
</div>
