<%@ Page Title="Clientes" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Clientes.aspx.cs" Inherits="CompurShop.WebClient.View.Clientes.Clientes" %>

<%@ Register Src="~/View/Clientes/UcClienteView.ascx" TagPrefix="uc1" TagName="UcClienteView" %>


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
    <div class="card">
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
            <div class="col-sm-12 col-md-4">
                <asp:Button ID="btnPesquisa" Text="Pesquisar" runat="server" SkinID="btPrimary" OnClick="btnPesquisa_Click"
                    ValidationGroup="Viagens" />
            </div>
                </div>
        </div>
        <div class="card-body">
            <div>
                <asp:GridView ID="gridClientes" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="10" AllowCustomPaging="True"
                    ShowHeaderWhenEmpty="True"
                    DataKeyNames="Id"
                    OnRowDataBound="gridClientes_RowDataBound"
                    OnPageIndexChanging="gridClientes_PageIndexChanging"
                    OnRowCommand="gridClientes_RowCommand"                   
                    AllowSorting="False">
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


    <div id="modalDiv" class="modal fade">
        <uc1:ucclienteview  runat="server" id="UcClienteView" />
    </div>

</asp:Content>
