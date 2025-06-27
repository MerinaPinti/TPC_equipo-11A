<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SeleccionarEspecialidad.aspx.cs" Inherits="TPC_Clinica.SeleccionarEspecialidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
    <div class="card shadow-sm border-0 rounded-4 p-4 mx-auto" style="max-width: 500px;">
        <h5 class="mb-4">Agendar Turno por Especialidad</h5>

        <!-- Dropdown Especialidad -->
        <div class="mb-4">
            <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select rounded-pill" AppendDataBoundItems="true">
                <asp:ListItem Text="Seleccioná una especialidad" Value="" />
            </asp:DropDownList>
        </div>

        <!-- Boton -->
        <asp:Button ID="btnContinuar" runat="server" Text="Continuar" CssClass="btn btn-primary w-100 rounded-pill" OnClick="btnContinuar_Click" />
    </div>
</div>

</asp:Content>
