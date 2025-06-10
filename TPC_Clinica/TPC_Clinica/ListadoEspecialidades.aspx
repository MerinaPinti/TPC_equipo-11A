<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListadoEspecialidades.aspx.cs" Inherits="TPC_Clinica.ListadoEspecialidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-turno">
        <div class="row">
            <div class="col">
                <h2>Listado de Especialidades</h2>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:Button ID="btnAgregar" CssClass="btn btn-success" runat="server" Text="Agregar" OnClick="btnAgregar_Click"/>
                <asp:GridView ID="dgvEspecialidad" runat="server" AutoGenerateColumns="false" CssClass="table mt-3">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="descripcion" HeaderText="Categoria" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
