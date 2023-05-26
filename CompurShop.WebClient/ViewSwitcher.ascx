<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewSwitcher.ascx.cs" Inherits="CompurShop.WebClient.ViewSwitcher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ASP.NET Web Forms App</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.7.3/font/bootstrap-icons.css" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <aside id="sidebar" class="sidebar">
            <asp:Panel runat="server" ID="sidebarNav" CssClass="sidebar-nav" >

                <ul id="sidebar-nav">
                    <li>
                        <a class="nav-link collapsed" data-bs-target="#forms-nav" data-bs-toggle="collapse" href="#">
                            <i class="bi bi-journal-text"></i><span>Cadastros</span><i class="bi bi-chevron-down ms-auto"></i>
                        </a>
                        <ul id="forms-nav" class="nav-content collapse" data-bs-parent="#sidebar-nav">
                            <li>
                                <a href="/view/clientes/Clientes.aspx">
                                    <i class="bi bi-circle"></i><span>Clientes</span>
                                </a>
                            </li>
                            <li>
                                <a href="/View/Listas/Listas.aspx">
                                    <i class="bi bi-circle"></i><span>Importar Arquivos</span>
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <a class="nav-link collapsed" data-bs-target="#config-nav" data-bs-toggle="collapse" href="#">
                            <i class="bi bi-grid"></i><span>Configurações</span><i class="bi bi-chevron-down ms-auto"></i>
                        </a>
                        <ul id="config-nav" class="nav-content collapse" data-bs-parent="#sidebar-nav">
                            <li>
                                <a class="nav-link collapsed" href="users-profile.html">
                                    <i class="bi bi-person"></i>
                                    <span>Usuários</span>
                                </a>
                            </li>
                            <li>
                                <a href="forms-layouts.html">
                                    <i class="bi bi-circle"></i><span>Perfis</span>
                                </a>
                            </li>
                        </ul>
                    </li>
                </ul>
            </asp:Panel>
        </aside>
    </form>
</body>
</html>
