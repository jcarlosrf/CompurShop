<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcClienteView.ascx.cs" Inherits="CompurShop.WebClient.View.Clientes.UcClienteView" %>

<div class="modal-dialog modal-dialog-scrollable" style="min-width: 900px">
    <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title">
                <asp:Label runat="server" ID="labelTitulo" Text="Cadastro de Cliente"></asp:Label>
            </h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
            </button>
        </div>

        <div class="modal-body">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <!-- Multi Columns Form -->
                        <div class="col-md-4">
                            <asp:Label ID="labelCpf" runat="server" Text="CPF / CNPJ" AssociatedControlID="textCpf" SkinID="labelItem"></asp:Label>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="textCpf"
                                ValidationGroup="Clientes" ErrorMessage="CPF é obrigatório." CssClass="text-danger" Display="Dynamic">
                                        <i class="bi bi-exclamation-circle"></i>
                            </asp:RequiredFieldValidator>--%>
                            <asp:TextBox ID="textCpf" runat="server" SkinID="textInput" ValidateRequestMode="Enabled"></asp:TextBox>

                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="labelNome" runat="server" Text="Nome" AssociatedControlID="textNome" SkinID="labelItem"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="textNome"
                                ValidationGroup="Clientes" ErrorMessage="Nome é obrigatório." CssClass="text-danger" Display="Dynamic">
                                        <i class="bi bi-exclamation-circle"></i>
                            </asp:RequiredFieldValidator>
                            <asp:TextBox ID="textNome" runat="server" SkinID="textInput"></asp:TextBox>

                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="labelTelefone" runat="server" Text="Telefone" AssociatedControlID="textTelefone" SkinID="labelItem"></asp:Label>
                            <asp:TextBox ID="textTelefone" runat="server" SkinID="textInput"></asp:TextBox>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="labelEmail" runat="server" Text="E-mail" AssociatedControlID="textEmail" SkinID="labelItem"></asp:Label>
                            <asp:TextBox ID="textEmail" runat="server" SkinID="textInput"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <hr />
                        </div>

                         <div class="col-md-4">
                            <asp:Label ID="label1" runat="server" Text="Login (Usuário)" AssociatedControlID="textLogin" SkinID="labelItem"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="textLogin"
                                ValidationGroup="Clientes" ErrorMessage="Usuário é obrigatório." CssClass="text-danger" Display="Dynamic">
                                <i class="bi bi-exclamation-circle"></i>
                            </asp:RequiredFieldValidator>
                            <asp:TextBox ID="textLogin" runat="server" SkinID="textInput"></asp:TextBox>
                        </div>

                        <div class="col-4">
                            <asp:Label runat="server" AssociatedControlID="txtSenha" class="form-label">Senha</asp:Label>
                            <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha"
                                ValidationGroup="Clientes" ErrorMessage="Password é obrigatório."
                                CssClass="text-danger" Display="Dynamic">
                                <i class="bi bi-exclamation-circle"></i>
                            </asp:RequiredFieldValidator>
                            <asp:TextBox runat="server" type="password" SkinID="textInput" ID="txtSenha"></asp:TextBox>

                        </div>
                        <div class="col-4">
                            <asp:Label runat="server" AssociatedControlID="txtSenha2" class="form-label">Confirmar Senha</asp:Label>
                            <asp:RequiredFieldValidator ID="rfvSenha1" runat="server" ControlToValidate="txtSenha2"
                                ValidationGroup="Clientes" ErrorMessage="Password é obrigatório."
                                CssClass="text-danger" Display="Dynamic"><i class="bi bi-exclamation-circle"></i>
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator runat="server" ID="cmpSenha" ControlToCompare="txtSenha" ControlToValidate="txtSenha2"
                                Operator="Equal" ErrorMessage="As senhas devem ser iguais" Display="Dynamic" ValidationGroup="Clientes"  CssClass="text-danger">
                                 <i class="bi bi-exclamation-circle"></i>
                            </asp:CompareValidator>
                            <asp:TextBox runat="server" type="password" SkinID="textInput" ID="txtSenha2"></asp:TextBox>
                        </div>

                        <div class="col-md-12">
                            <hr />
                        </div>
                        <div class="col-8">
                            <asp:Label ID="labelLogradouro" runat="server" Text="Logradouro" AssociatedControlID="textLogradouro" SkinID="labelItem"></asp:Label>
                            <asp:TextBox ID="textLogradouro" runat="server" SkinID="textInput"></asp:TextBox>
                        </div>
                        <div class="col-4">
                            <asp:Label ID="labelNumero" runat="server" Text="Número" AssociatedControlID="textNumnero" SkinID="labelItem"></asp:Label>
                            <asp:TextBox ID="textNumnero" runat="server" SkinID="textInput"></asp:TextBox>
                        </div>
                        <div class="col-6">
                            <asp:Label ID="labelComplemento" runat="server" Text="Complemento" AssociatedControlID="textComplemento" SkinID="labelItem"></asp:Label>
                            <asp:TextBox ID="textComplemento" runat="server" SkinID="textInput"></asp:TextBox>
                        </div>
                        <div class="col-6">
                            <asp:Label ID="labelBairro" runat="server" Text="Bairro" AssociatedControlID="textBairro" SkinID="labelItem"></asp:Label>
                            <asp:TextBox ID="textBairro" runat="server" SkinID="textInput"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="labelCidade" runat="server" Text="Cidade" AssociatedControlID="textCidade" SkinID="labelItem"></asp:Label>
                            <asp:TextBox ID="textCidade" runat="server" SkinID="textInput"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label for="labeltEstado" runat="server" Text="Estado" AssociatedControlID="dropUF" SkinID="labelItem"></asp:Label>
                            <asp:DropDownList runat="server" ID="dropUF" AutoPostBack="false" SkinID="dropDown" placeholder="<< Selecione a UF >>"
                                DataTextField="sigla" AppendDataBoundItems="true" DataValueField="sigla" DataMember="Uf">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="labelCep" runat="server" Text="Cep" AssociatedControlID="textCep" SkinID="labelItem"></asp:Label>
                            <asp:TextBox ID="textCep" runat="server" SkinID="textInput"></asp:TextBox>
                        </div>
                        <!-- End Multi Columns Form -->
                    </div>
                </div>

            </div>
        </div>
        <div class="modal-footer">
            <div class="input-group row">
                <div class="col-md-8">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Clientes" Width="100%" />
                </div>
                <div class="col-md-4 d-flex align-items-end">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style="margin-left:auto; margin-right:10px" >Fechar</button>
                    <asp:Button runat="server" ID="btnSave" SkinID="btPrimary" Text="Gravar"
                        OnClick="btnSave_Click1" CausesValidation="true" ValidationGroup="Clientes" />
                </div>
            </div>
        </div>
    </div>
</div>

