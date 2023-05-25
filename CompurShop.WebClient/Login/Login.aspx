<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CompurShop.WebClient.Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <!-- Vendor CSS Files -->
    <link href="../App_Themes/Default/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Default/bootstrap-icons/bootstrap-icons.css" rel="stylesheet" type="text/css" />
    <link href="assets/vendor/boxicons/css/boxicons.min.css" rel="stylesheet" type="text/css" />

    <!-- Template Main CSS File -->
    <link rel="stylesheet" type="text/css" href="../App_Themes/Default/Style.css" />
    <link rel="stylesheet" type="text/css" href="../App_Themes/Default/Site.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />

                <%--Site Scripts--%>
                <asp:ScriptReference Path="~/Scripts/bootstrap.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap.bundle.js" />
                <asp:ScriptReference Path="~/Scripts/Includes/Main.js" />
                <asp:ScriptReference Path="~/Scripts/jquery-3.6.0.min.js" />
            </Scripts>
        </asp:ScriptManager>
        <div class="container">

            <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
                <div class="container" style="margin-bottom: auto;">
                    <div class="row justify-content-center">
                        <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">

                            <div class="d-flex justify-content-center py-4">
                                <a href="index.html" class="logo d-flex align-items-center w-auto">
                                    <img src="../App_Themes/Default/img/logo.png" alt="" />
                                    <span class="d-none d-lg-block">Login</span>
                                </a>
                            </div>
                            <!-- End Logo -->

                            <div class="card mb-3">

                                <div class="card-body">

                                    <div class="pt-4 pb-2">
                                        <h5 class="card-title text-center pb-0 fs-4">Acessar conta</h5>
                                        <p class="text-center small">Enter com seu usuário e senha</p>
                                        <asp:Label ID="labelMessage" runat="server" EnableViewState="False"></asp:Label>
                                    </div>


                                    <div class="col-12">
                                        <asp:Label runat="server" AssociatedControlID="yourUsername" class="form-label">Usuário</asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="yourUsername"
                                            ValidationGroup="Login" ErrorMessage="Usuário é obrigatório." 
                                            CssClass="text-danger" Display="Dynamic"><i class="bi bi-exclamation-circle"></i>
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="yourUsername" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-12">
                                        <asp:Label runat="server" AssociatedControlID="yourPassword" class="form-label">Password</asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="yourPassword"
                                            ValidationGroup="Login" ErrorMessage="Password é obrigatório." 
                                            CssClass="text-danger" Display="Dynamic"><i class="bi bi-exclamation-circle"></i>
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" type="password" name="password" class="form-control" ID="yourPassword" ></asp:TextBox>

                                    </div>

                                    <div class="col-12">
                                        <asp:Button runat="server" ID="btnLogin" class="btn btn-primary w-100" type="submit" Text="Login" CausesValidation="true" ValidationGroup="Login"
                                           CommandName="Entrar" OnClick="btnLogin_Click" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </section>
        </div>
    </form>
</body>
