<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SeleccionarProfesional.aspx.cs" Inherits="TPC_Clinica.SeleccionarProfesional" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
    <div class="card shadow-sm border-0 rounded-4 p-4 mx-auto" style="max-width: 500px;">
        <h5 class="mb-4">Agendar turno por Profesional</h5>

        <!-- Buscar -->
        <div class="mb-4">
            <label for="txtBuscarProfesional" class="form-label">Profesional</label>
            <div class="input-group">
                <span class="input-group-text bg-white border-end-0 rounded-start-pill">
                    <i class="bi bi-search"></i>
                </span>
                <asp:TextBox ID="txtBuscarProfesional" runat="server" CssClass="form-control rounded-end-pill border-start-0" placeholder="Escribí al menos 3 letras" />
            </div>
        </div>

        <!-- Boton -->
        <asp:Button ID="btnContinuar" runat="server" Text="Continuar" CssClass="btn btn-primary w-100 rounded-pill" OnClick="btnContinuar_Click" />
    </div>
</div>

</asp:Content>
