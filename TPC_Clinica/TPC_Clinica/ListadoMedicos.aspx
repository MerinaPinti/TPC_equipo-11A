<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListadoMedicos.aspx.cs" Inherits="TPC_Clinica.ListadoMedicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-turno">
        <div class="row">
            <div class="col">
                <h2>Listado de Medicos</h2>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:Button ID="btnAgregar" CssClass="btn btn-success" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
