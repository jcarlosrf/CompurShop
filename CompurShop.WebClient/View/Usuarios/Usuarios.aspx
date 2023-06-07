<%@ Page Title="Usuários" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="CompurShop.WebClient.View.Usuarios.Usuarios" %>
<%@ Register Src="~/Controls/UcLoader.ascx" TagPrefix="uc1" TagName="UcLoader" %>
<%@ Register Src="~/Controls/UcMensagem.ascx" TagPrefix="uc2" TagName="UcMensagem" %>
<%@ Register Src="~/View/Usuarios/UcUsuario.ascx" TagPrefix="uc1" TagName="UcUsuario" %>


<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="updUsuarios" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <uc1:UcLoader runat="server" ID="UcLoader" AssociatedUpdatePanelID="updUsuarios" />
            <asp:Label ID="labelMessage" runat="server" EnableViewState="False"></asp:Label>
            <div class="card">
                <div class="card-body">
                    <div class="card-title">
                        <div class="row">
                            <div class="col-sm-12 col-md-3">
                                <asp:Label ID="labelNome" runat="server" Text="Nome" AssociatedControlID="txtNome"
                                    SkinID="labelItem"></asp:Label>
                                <asp:TextBox ID="txtNome" runat="server" aria-describedby="basic-addon1"
                                    SkinID="textInput" ReadOnly="false"></asp:TextBox>
                            </div>
                            <div class="col-sm-12 col-md-3">
                            </div>
                            <div class="col-sm-12 col-md-4 d-flex align-items-end">
                                <asp:LinkButton runat="server" ID="btnSearch" Text="Pesquisar" OnClick=btnSearch_Click SkinID="btPesquisar" Style="margin-left:auto;"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnNew" Text="Novo" OnClick=btnNew_Click SkinID="btNovo"></asp:LinkButton>
                            </div>                         
                        </div>
                    </div>

                    <div>
                        
                        <asp:GridView ID="gridUsuarios" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false"  Visible="true"
                            ShowHeaderWhenEmpty="True" ShowHeader="true"
                            ItemType="CompurShop.Domain.Entities.Usuario" DataKeyNames="Id"
                            SelectMethod=gridUsuarios_GetData
                            OnRowDataBound=gridUsuarios_RowDataBound
                            OnRowCommand=gridUsuarios_RowCommand
                            AllowSorting="false"
                            EmptyDataText=".">
                            <Columns>
                                <asp:TemplateField HeaderText="" SortExpression="id" ShowHeader="true" HeaderStyle-CssClass="icon-column">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="cmdEdit" Text="" 
                                            CommandArgument='<%# Eval("Id") %>'  CssClass="icon-link bi-pencil"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" SortExpression="id" ShowHeader="true" HeaderStyle-CssClass="icon-column">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="true" CommandName="cmdDelete" Text="" 
                                            CommandArgument='<%# Eval("Id") %>' CssClass="icon-link bi-trash"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                                <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome" />
                                <asp:BoundField DataField="idcliente" HeaderText="Cliente" SortExpression="idcliente" />
                                <asp:BoundField DataField="nomecliente" HeaderText="Nome do Cliente" SortExpression="nomecliente" />
                                <asp:BoundField DataField="idnivel" HeaderText="Nivel" SortExpression="idnivel" />
                            </Columns>
                            <PagerSettings Position="Bottom" Mode="NumericFirstLast" PageButtonCount=5 
                                FirstPageText="<i class='bi bi-chevron-double-left'></i>"  
                                LastPageText="<i class='bi bi-chevron-double-right'></i>"  
                                />                             
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="gridUsuarios" />
            <asp:PostBackTrigger ControlID="btnNew" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="modalDiv" class="modal fade">
        <uc1:UcUsuario runat="server" id="UcUsuario" />
    </div>

     <div id="modalDivMensagem" class="modal fade">
         <uc2:UcMensagem runat="server" id="UcMensagem1" />
    </div>
    
</asp:Content>