<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListadoUsuarios.aspx.cs" Inherits="TPC_Clinica.ListadoUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
     .usuario-list {
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

     <div class="usuario-list">
     <div class="row">
         <div class="col">
             <h2>Listado de Usuarios</h2>
         </div>
     </div>
     <div class="row">
         <div class="col">
             <%--<asp:Button ID="btnAgregar" CssClass="btn btn-primary" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
             <%--<asp:GridView ID="dgvEspecialidad" runat="server" AutoGenerateColumns="false" CssClass="table mt-3">
                 <Columns>
                     <asp:BoundField DataField="Id" HeaderText="ID" />
                     <asp:BoundField DataField="descripcion" HeaderText="Categoria" />
                 </Columns>
             </asp:GridView>--%>

             <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
         </div>
     </div>
 </div>

</asp:Content>
