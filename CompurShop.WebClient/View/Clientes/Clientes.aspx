<%@ Page Title="Clientes" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Clientes.aspx.cs" Inherits="CompurShop.WebClient.View.Clientes.Clientes" %>

<%@ Register Src="~/View/Clientes/UcClienteView.ascx" TagPrefix="uc1" TagName="UcClienteView" %>
<%@ Register Src="~/Controls/UcLoader.ascx" TagPrefix="uc1" TagName="UcLoader" %>



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function openModalDanger(iddiv) {
            $(iddiv).modal('show');
        }

        function closeModalDanger() {
            $(iddiv).modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="updClientes" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <uc1:UcLoader runat="server" ID="UcLoader" AssociatedUpdatePanelID="updClientes" />
            <asp:Label runat="server" ID="lblMensagem" Text="" Visible=false CssClass="alert alert-warning alert-dismissible fade show" ></asp:Label>
            <div class="card">
                <div class="card-body">
                    <div class="card-title">
                        <div class="row">
                            <div class="col-sm-12 col-md-4">
                                <asp:Label ID="labelNome" runat="server" Text="Nome" AssociatedControlID="txtNome"
                                    SkinID="labelItem"></asp:Label>
                                <asp:TextBox ID="txtNome" runat="server" aria-describedby="basic-addon1"
                                    SkinID="textInput" ReadOnly="false"></asp:TextBox>
                            </div>
                            <div class="col-sm-12 col-md-4">
                                <asp:Label ID="labelCpf" runat="server" Text="CPF/CNPJ" AssociatedControlID="txtCpf"
                                    SkinID="labelItem"></asp:Label>
                                <asp:TextBox ID="txtCpf" runat="server" placeholder="CPF / CNPJ" aria-describedby="basic-addon1"
                                    SkinID="textInput" ReadOnly="false"></asp:TextBox>
                            </div>
                            <div class="col-sm-12 col-md-4 d-flex align-items-end">
                                <asp:LinkButton runat="server" ID="btnSearch" Text="Pesquisar" OnClick="btnPesquisa_Click" SkinID="btPesquisar"></asp:LinkButton>

                            </div>
                        </div>
                    </div>

                    <div>
                        <asp:GridView ID="gridClientes" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="10" Visible="true"
                            ShowHeaderWhenEmpty="True" ShowHeader="true"
                            DataKeyNames="Id"
                            OnRowDataBound="gridClientes_RowDataBound"
                            OnPageIndexChanging="gridClientes_PageIndexChanging"
                            OnRowCommand="gridClientes_RowCommand"
                            AllowSorting="false"
                            EmptyDataText=".">
                            <Columns>
                                <asp:TemplateField HeaderText="" SortExpression="id" ShowHeader="true" HeaderStyle-CssClass="icon-column">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="cmdEdit" Text="" CssClass="icon-link bi-pencil"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" SortExpression="id" ShowHeader="true" HeaderStyle-CssClass="icon-column">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="cmdDelete" Text="" CssClass="icon-link bi-trash"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                                <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome" />
                                <asp:BoundField DataField="cpfcnpj" HeaderText="CPF / CNPJ" SortExpression="cpfcnpj" />
                                <asp:BoundField DataField="tipodocumento" HeaderText="Tipo" SortExpression="tipo" />
                                <asp:BoundField DataField="telefone" HeaderText="Telefone" SortExpression="telefone" />
                                <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="cidade" />
                                <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="uf" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="gridClientes" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="modalDiv" class="modal fade">
        <uc1:UcClienteView runat="server" ID="UcClienteView" />
    </div>

</asp:Content>
