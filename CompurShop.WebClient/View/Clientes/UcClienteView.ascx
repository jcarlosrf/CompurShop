<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcClienteView.ascx.cs" Inherits="CompurShop.WebClient.View.Clientes.UcClienteView" %>
<div class="modal-dialog modal-dialog-scrollable" style="min-width: 900px">
    <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title">
                <asp:Label runat="server" ID="labelTitulo" Text="Cadastro de Cliente"></asp:Label></h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>

        <div class="modal-body">
            <section class="section">
                <div class="card">
                    <div class="card-body">
                        <div class="row input-group">
                            <!-- Multi Columns Form -->
                            <div class="col-md-6">
                           <%--     <label for="inputName5" class="form-label">CPF/CNPJ</label>
                                <input type="text" class="form-control" id="inputName5">--%>
                                <asp:Label ID="labelCpf" runat="server" Text="CPF/CNPJ" AssociatedControlID="txtCpf"
                                    SkinID="labelItem"></asp:Label>
                                <asp:TextBox ID="txtCpf" runat="server" placeholder="CPF / CNPJ" aria-describedby="basic-addon1"
                                    SkinID="textInput" ReadOnly="false"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="inputEmail5" class="form-label">Nome/Nome Fantasia</label>
                                <input type="nome" class="form-control" id="inputName">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword5" class="form-label">E-mail</label>
                                <input type="email" class="form-control" id="inputEmail5">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword5" class="form-label">Telefone</label>
                                <input type="email" class="form-control" id="inputPassword5">
                            </div>
                            <div class="col-10">
                                <label for="inputAddress5" class="form-label">Endereço (Logradouro)</label>
                                <input type="text" class="form-control" id="inputAddres5s" placeholder="1234 Main St">
                            </div>
                            <div class="col-10">
                                <label for="inputAddress5" class="form-label">Número</label>
                                <input type="text" class="form-control" id="inputAddres5s" placeholder="1234 Main St">
                            </div>
                            <div class="col-6">
                                <label for="inputAddress2" class="form-label">Complemento</label>
                                <input type="text" class="form-control" id="inputAddress2" placeholder="Apartment, studio, or floor">
                            </div>
                            <div class="col-6">
                                <label for="inputAddress2" class="form-label">Bairro</label>
                                <input type="text" class="form-control" id="inputAddress2" placeholder="Apartment, studio, or floor">
                            </div>
                            <div class="col-md-6">
                                <label for="inputCity" class="form-label">Cidade</label>
                                <input type="text" class="form-control" id="inputCity">
                            </div>
                            <div class="col-md-4">
                                <label for="inputState" class="form-label">Estado</label>
                                <select id="inputState" class="form-select">
                                    <option selected>Choose...</option>
                                    <option>...</option>
                                </select>
                            </div>
                            <div class="col-md-2">
                                <label for="inputZip" class="form-label">CEP</label>
                                <input type="text" class="form-control" id="inputZip">
                            </div>
                            <!-- End Multi Columns Form -->
                        </div>
                    </div>
                </div>
            </section>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary">Save changes</button>
        </div>
    </div>
</div>

