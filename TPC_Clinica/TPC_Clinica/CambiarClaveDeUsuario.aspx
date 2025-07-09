<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CambiarClaveDeUsuario.aspx.cs" Inherits="TPC_Clinica.CambiarClaveDeUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container mt-5">
    <h3>Cambiar contraseña</h3>
    
    <div class="mb-3">
        <label for="txtActual" class="form-label">Contraseña actual:</label>
        <asp:TextBox ID="txtActual" runat="server" TextMode="Password" CssClass="form-control" />
        <asp:Label ID="lblActual" runat="server" Visible="false" />
    </div>

    <div class="mb-3">
        <label for="txtNueva" class="form-label">Nueva contraseña:</label>
        <asp:TextBox ID="txtNueva" runat="server" TextMode="Password" CssClass="form-control" />
        <asp:Label ID="lblNueva" runat="server" Visible="false" />
    </div>

    <div class="mb-3">
        <label for="txtConfirmar" class="form-label">Confirmar nueva contraseña:</label>
        <asp:TextBox ID="txtConfirmar" runat="server" TextMode="Password" CssClass="form-control" />        
    </div>

    <asp:Button ID="btnCambiar" runat="server" Text="Actualizar" CssClass="btn btn-primary" OnClick="btnCambiar_Click" />
</div>

</asp:Content>
