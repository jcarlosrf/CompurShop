<%@ Page Title="Listas de arquivos" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Listas.aspx.cs" Inherits="CompurShop.WebClient.View.Listas.Listas" %>

<%@ Register Src="~/Controls/UcLoader.ascx" TagPrefix="uc1" TagName="UcLoader" %>
<%@ Register Src="~/View/Listas/UcLista.ascx" TagPrefix="uc2" TagName="UcLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/jquery-3.6.0.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="labelMessage" runat="server" EnableViewState="False"></asp:Label>
    <div class="card">
        <div class="card-title">
            <div class="row" style="padding: 10px 20px">

                <div class="col-sm-12 col-md-4 ui-widget">
                    <asp:Label ID="label2" runat="server" Text="Cliente" AssociatedControlID="listClientes" Width="100%"
                        SkinID="labelItem"></asp:Label>
                    <asp:ListBox runat="server" ID="listClientes" ClientIDMode="Static" AutoPostBack="false" CssClass="form-control"
                        DataTextField="nome" AppendDataBoundItems="true" DataValueField="id" ItemType="CompurShop.Domain.Entities.Cliente"></asp:ListBox>
                </div>

                <div class="col-sm-12 col-md-4 d-flex align-items-end">
                    <asp:LinkButton runat="server" ID="btnSearch" Text="Pesquisar" OnClick="btnSearch_Click" SkinID="btPesquisar" Style="margin-left: auto;"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnNew" Text="Novo" OnClick="btnNew_Click"
                        SkinID="btNovo" CausesValidation="false">                                
                    </asp:LinkButton>
                </div>

            </div>

        </div>

        <div class="card-body">
            <div id="cadastroDiv">
                <div class="accordion" id="accordionExample">
                    <div class="accordion-item">
                        <div id="collapseOne" class="accordion-collapse collapse" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                            <uc2:UcLista runat="server" ID="UcLista" />
                        </div>
                    </div>
                </div>
            </div>
            
            <asp:UpdatePanel runat="server" ID="UpdListas" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Timer ID="Timer1" runat="server" Interval="30000" Enabled="false" OnTick="Timer1_Tick"></asp:Timer>
                    
                    <uc1:UcLoader runat="server" id="LoaderListas" AssociatedUpdatePanelID = "UpdListas" />
                    <asp:GridView ID="gridListas" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false"
                        ShowHeaderWhenEmpty="True" ShowHeader="true"
                        DataKeyNames="id" ItemType="CompurShop.Domain.Entities.Lista"
                        OnRowCommand="gridListas_RowCommand"
                        SelectMethod="GridListas_GetData"
                        AllowSorting="false"
                        EmptyDataText=".">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                            <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome" />
                            <asp:BoundField DataField="qtdecpfs" HeaderText="CPF" SortExpression="cpfcnpj" />
                            <asp:BoundField DataField="datahora" HeaderText="Data / Hora" SortExpression="datahora" />
                            <asp:BoundField DataField="idcliente" HeaderText="Cliente" SortExpression="idcliente" />
                            <asp:TemplateField HeaderText="Status" SortExpression="status" ShowHeader="true" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnProcessar" runat="server" CausesValidation="False"
                                        CommandName="btnProcessar" Text="" CssClass="icon-link bi-arrow-down-square-fill"
                                        Visible='<%# Eval("btnProcessar") %>'
                                        CommandArgument='<%# Eval("Id") %>'>
                                    <span style="font-size:small; font-weight:normal">Processar</span>
                                    </asp:LinkButton>
                                    <asp:Label ID="Label1" runat="server" Text="Label" Visible='<%# Eval("iconeProcessar") %>'>
                                    <div class="spinner-border text-primary" role="status" style="width: 20px; height: 20px; font-weight: bold" aria-hidden="true"></div>
                                    </asp:Label>
                                    <asp:LinkButton ID="btnFileZipados" runat="server" CausesValidation="False"
                                        CommandName="btnFileDocumentos" Text="" CssClass="icon-link bi-download"
                                        Visible='<%# Eval("btnArquivo") %>'
                                        CommandArgument='<%# Eval("Id") %>'>
                                    <span style="font-size:small; font-weight:normal">Download</span>
                                    </asp:LinkButton>
                                    </ContentTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Críticas (.csv)" SortExpression="id" ShowHeader="true" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnFileCriticas" runat="server" CausesValidation="False"
                                        CommandName="btnCriticas" Text="" CssClass="icon-link-red bi-file-earmark-excel-fill"
                                         Visible='<%# Eval("Critica") %>'
                                        CommandArgument='<%# Eval("Id") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CPF (.csv)" SortExpression="id" ShowHeader="true" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnFile" runat="server" CausesValidation="False"
                                        CommandName="btnFile" Text="" CssClass="icon-link bi-file-earmark"
                                        CommandArgument='<%# Eval("Id") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>                    
                    <asp:PostBackTrigger ControlID="gridListas" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
