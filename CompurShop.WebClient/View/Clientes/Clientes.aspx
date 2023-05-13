<%@ Page Title = "Clientes" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Clientes.aspx.cs" Inherits="CompurShop.WebClient.View.Clientes.Clientes" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <div class="card">
          <div class="card-body">
        <div>


                        <asp:GridView ID="gridClientes" runat="server" AutoGenerateColumns="False" OnRowCommand=gridClientes_RowCommand
                            AllowPaging="True" PageSize="10" OnPageIndexChanging=gridClientes_PageIndexChanging AllowCustomPaging="True" >
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                                <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome" />
                                <asp:BoundField DataField="cpfcnpj" HeaderText="CPF / CNPJ" SortExpression="cpfcnpj" />
                                <asp:BoundField DataField="tipo" HeaderText="Tipo" SortExpression="tipo" />
                                <asp:BoundField DataField="telefone" HeaderText="Telefone" SortExpression="telefone" />
                               <%-- <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="cidade" />
                                <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="uf" />--%>
                                <asp:ButtonField HeaderText="" CommandName="cmdSelecionar" ButtonType="Image" Text="Selecionar"
                                    ImageUrl="/Includes/Images/select.png" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                <asp:ButtonField HeaderText="" CommandName="cmdExcluir" ButtonType="Image" Text="Excluir"
                                    ImageUrl="/Includes/Images/select.png" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                            </Columns>
                        </asp:GridView>
                    </div>
    </div>

     </div>
    
</asp:Content>