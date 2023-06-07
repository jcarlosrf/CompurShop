<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcUsuario.ascx.cs" Inherits="CompurShop.WebClient.View.Usuarios.UcUsuario" %>

<div class="modal-dialog modal-dialog-scrollable" style="min-width: 900px">
    <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title">
                <asp:Label runat="server" ID="labelTitulo" Text="Cadastro de Usuários"></asp:Label>
            </h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
            </button>
        </div>

        <div class="modal-body">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <!-- Multi Columns Form -->

                        <div class="col-md-9">
                            <asp:Label ID="labelNome" runat="server" Text="Nome" AssociatedControlID="textNome" SkinID="labelItem"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="textNome"
                                ValidationGroup="VUsers" ErrorMessage="Nome é obrigatório." CssClass="text-danger" Display="Dynamic">
                                <i class="bi bi-exclamation-circle"></i>
                            </asp:RequiredFieldValidator>
                            <asp:TextBox ID="textNome" runat="server" SkinID="textInput"></asp:TextBox>
                        </div>

                        <div class="col-4">
                            <asp:Label runat="server" AssociatedControlID="txtSenha" class="form-label">Senha</asp:Label>
                            <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha"
                                ValidationGroup="VUsers" ErrorMessage="Password é obrigatório."
                                CssClass="text-danger" Display="Dynamic">
                                <i class="bi bi-exclamation-circle"></i>
                            </asp:RequiredFieldValidator>
                            <asp:TextBox runat="server" type="password" SkinID="textInput" ID="txtSenha"></asp:TextBox>

                        </div>
                        <div class="col-4">
                            <asp:Label runat="server" AssociatedControlID="txtSenha2" class="form-label">Confirmar Senha</asp:Label>
                            <asp:RequiredFieldValidator ID="rfvSenha1" runat="server" ControlToValidate="txtSenha2"
                                ValidationGroup="VUsers" ErrorMessage="Password é obrigatório."
                                CssClass="text-danger" Display="Dynamic"><i class="bi bi-exclamation-circle"></i>
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator runat="server" ID="cmpSenha" ControlToCompare="txtSenha" ControlToValidate="txtSenha2"
                                Operator="Equal" ErrorMessage="As senhas devem ser iguais" Display="Dynamic" ValidationGroup="VUsers"  CssClass="text-danger">
                                 <i class="bi bi-exclamation-circle"></i>
                            </asp:CompareValidator>
                            <asp:TextBox runat="server" type="password" SkinID="textInput" ID="txtSenha2"></asp:TextBox>
                        </div>

                        <div class="col-sm-12 col-md-12 ui-widget">
                            <asp:Label ID="labelCliente" runat="server" Text="Cliente" AssociatedControlID="listClientesUser" Width="100%"
                                SkinID="labelItem"></asp:Label>
                            <asp:ListBox runat="server" ID="listClientesUser" ClientIDMode="Static" AutoPostBack="false" CssClass="form-control" 
                                DataTextField="nome"  AppendDataBoundItems="true" DataValueField="id" ItemType="CompurShop.Domain.Entities.Cliente"                                
                                ></asp:ListBox>
                        </div>

                    </div>
                </div>

            </div>
        </div>
        <div class="modal-footer">
            <div class="input-group row">
                <div class="col-md-8">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="VUsers" Width="100%" />
                </div>
                <div class="col-md-4 d-flex align-items-end">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style="margin-left: auto; margin-right: 10px">Fechar</button>
                    <asp:Button runat="server" ID="btnSave" SkinID="btPrimary" Text="Gravar"
                        OnClick="btnSave_Click" CausesValidation="true" ValidationGroup="VUsers" />
                </div>
            </div>
        </div>
    </div>
</div>
