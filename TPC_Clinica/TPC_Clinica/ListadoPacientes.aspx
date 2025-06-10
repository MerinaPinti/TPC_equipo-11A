<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListadoPacientes.aspx.cs" Inherits="TPC_Clinica.ListadoPacientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-turno">
        <div class="row">
            <div class="col">
                <h2>Listado de Pacientes</h2>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:Button ID="btnAgregar" CssClass="btn btn-success" runat="server" Text="Agregar" />
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
