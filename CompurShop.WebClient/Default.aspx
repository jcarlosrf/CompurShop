<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CompurShop.WebClient._Default" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <div class="dashboard">
    <!-- Customers Card -->
            <div class="col-lg-3 col-md-4 col-sm-12">

              <div class="card info-card customers-card">

                <div class="filter">
                  <a class="icon" href="#" data-bs-toggle="dropdown"><i class="bi bi-three-dots"></i></a>
                  <ul class="dropdown-menu dropdown-menu-end dropdown-menu-arrow">
                    <li class="dropdown-header text-start">
                      <h6>Links</h6>
                    </li>
                    <li><a class="dropdown-item" href="\View\Clientes\Clientes.aspx">View</a></li>
                    
                  </ul>
                </div>

                <div class="card-body">
                  <h5 class="card-title">Clientes</h5>

                  <div class="d-flex align-items-center">
                    <div class="col-4">
                      <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                      <i class="bi bi-people"></i>
                    </div>
                        </div>
                      <div class="col-8">
                    <div class="ps-3">
                      <h6><asp:Label runat="server" ID="labelDashClientes" Text="1244" >
                          </asp:Label></h6>
                      <%--<span class="text-danger small pt-1 fw-bold">12%</span> --%>
                      <span class="text-muted small pt-2 ps-1">Cadastros</span>
                        </div>
                    </div>
                  </div>

                </div>
              </div>

            </div><!-- End Customers Card -->
    
    </div>
</asp:Content>