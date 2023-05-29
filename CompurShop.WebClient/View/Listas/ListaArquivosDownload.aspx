<%@ Page Title="Download de documentos" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaArquivosDownload.aspx.cs" Inherits="CompurShop.WebClient.View.Listas.ListaArquivosDownload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/jquery-3.6.0.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="labelMessage" runat="server" EnableViewState="False"></asp:Label>
    <div class="card">
        <div class="card-title">

            <div class="col-sm-12" style="padding: 10px 30px">
                <asp:Label ID="lblTitulo" CssClass="card-title" runat="server" Text="" style="font-size:larger" ></asp:Label>
            </div>


        </div>
        <div class="card-body">

            <%--PageSize="10" Visible="true"--%>
            <asp:GridView ID="gridListas" runat="server" AutoGenerateColumns="False"
                AllowPaging="false"
                ShowHeaderWhenEmpty="True" ShowHeader="true"
                DataKeyNames="id" ItemType="CompurShop.Domain.Entities.ListaArquivo"
                OnRowCommand="gridListas_RowCommand"
                SelectMethod="gridListas_GetData"
                AllowSorting="false"
                EmptyDataText=".">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                    <asp:BoundField DataField="idlista" HeaderText="Lista" ReadOnly="True" SortExpression="id" />
                    <asp:BoundField DataField="nomearquivo" HeaderText="Arquivo" ReadOnly="true" SortExpression="nome" />
                    <asp:BoundField DataField="qtdecpfs" HeaderText="Qtde de Pastas" SortExpression="cpfcnpj" />
                    <asp:TemplateField HeaderText="Downalod" SortExpression="" ShowHeader="true" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnFileZipados" runat="server" CausesValidation="False"
                                CommandName="btnFile" Text="" CssClass="icon-link bi-download"
                                Visible='true'
                                CommandArgument='<%# Eval("Id") %>'>
                                    <span style="font-size:small; font-weight:normal">Download</span>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
