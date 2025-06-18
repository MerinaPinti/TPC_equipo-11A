<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AltaTipoUsuario.aspx.cs" Inherits="TPC_Clinica.AltaTipoUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .tipo-alta, .tipo-modificar {
        max-width: 70vw;
        margin: 50px auto;
        padding: 30px;
        border-radius: 15px;
        box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
        background-color: #fff;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" />
 <% if (Session["IdModificarTipoUsuario"] == null) { %>
 <div class="tipo-alta">
     <asp:UpdatePanel runat="server">
         <ContentTemplate>
             <asp:Label runat="server" Text="Tipo de Usuario"></asp:Label>
             <asp:TextBox ID="txtTipoUsuario" runat="server" CssClass="form-control"></asp:TextBox>
             <asp:Button ID="btnAgregarTipoUsuario" runat="server" Text="Agregar" CssClass="btn btn-primary mt-2" OnClick="btnAgregarTipoUsuario_Click" />
         </ContentTemplate>
     </asp:UpdatePanel>
 </div>
 <% } else { %>
 <div class="tipo-modificar">
     <asp:UpdatePanel runat="server">
         <ContentTemplate>
             <asp:Label runat="server" Text="ID" />
             <asp:TextBox ID="txtIdTipoUsuario" runat="server" CssClass="form-control" Enabled="false" />
             <asp:Label runat="server" Text="Descripción" />
             <asp:TextBox ID="txtDescripcionTipoUsuario" runat="server" CssClass="form-control" />
             <asp:Button ID="btnModificarTipoUsuario" runat="server" Text="Modificar" CssClass="btn btn-primary mt-2" OnClick="btnModificarTipoUsuario_Click" />
         </ContentTemplate>
     </asp:UpdatePanel>
 </div>
 <% } %>
</asp:Content>
