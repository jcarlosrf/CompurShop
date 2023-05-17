<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Listas.aspx.cs" Inherits="CompurShop.WebClient.View.Listas.Listas" %>

<%@ Register Src="~/Controls/UcLoader.ascx" TagPrefix="uc1" TagName="UcLoader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">
        <div class="card-body">
            
                    
                    <asp:Label runat="server" ID="lblMensagem" Text="" Visible="false" CssClass="alert alert-warning alert-dismissible fade show"></asp:Label>

                    <div class="card-title">
                        <div class="row">
                            <div class="col-sm-12 col-md-4">
                                <asp:Label ID="labelNome" runat="server" Text="Nome" AssociatedControlID="txtNome"
                                    SkinID="labelItem"></asp:Label>
                                <asp:TextBox ID="txtNome" runat="server" aria-describedby="basic-addon1"
                                    SkinID="textInput" ReadOnly="false"></asp:TextBox>
                            </div>
                            <div class="col-sm-12 col-md-4 d-flex align-items-end">
                                <asp:FileUpload ID="fileUpload" runat="server" CssClass="fileload" />
                                <asp:Button runat="server" ID="btnSearch1" Text="Carregar" OnClick="btnSearch_Click" SkinID="btPrimary"></asp:Button>
                            </div>
                            <div class="col-sm-12 col-md-4 d-flex align-items-end">
                                <asp:LinkButton runat="server" ID="btnSalvar" Text="Salvar" OnClick="btnSalvar_Click" SkinID="btPrimary"></asp:LinkButton>

                            </div>
                        </div>
                    </div>
              
       

            <div>
                <asp:GridView ID="gridListas" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="10" Visible="true"
                    ShowHeaderWhenEmpty="True" ShowHeader="true"
                    DataKeyNames="id"
                    OnRowDataBound="gridListas_RowDataBound"
                    OnRowCommand="gridListas_RowCommand"
                    AllowSorting="false"
                    EmptyDataText=".">
                    <Columns>

                        <%-- <asp:TemplateField HeaderText="" SortExpression="id" ShowHeader="true" HeaderStyle-CssClass="icon-column">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="cmdDelete" Text="" CssClass="icon-link bi-trash"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                        <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                        <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome" />
                        <asp:BoundField DataField="qtdecpfs" HeaderText="CPF" SortExpression="cpfcnpj" />
                        <asp:BoundField DataField="datahora" HeaderText="Data / Hora" SortExpression="cpfcnpj" />
                        <asp:TemplateField HeaderText="" SortExpression="id" ShowHeader="true" HeaderStyle-CssClass="icon-column">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnFile" runat="server" CausesValidation="False"
                                    CommandName="btnFile" Text="" CssClass="icon-link bi-file-earmark"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
