<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListadoTurnos.aspx.cs" Inherits="TPC_Clinica.ListadoTurnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .filtros-container {
            display: flex;
            gap: 20px;
            flex-wrap: wrap;
            margin-bottom: 20px;
        }

        .filtros-container .form-group {
            min-width: 200px;
        }

        select.form-select {
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center text-primary mt-4">Listado de Turnos</h2>

    <div class="filtros-container">
        <div class="form-group">
            <label>Especialidad</label>
            <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select" />
        </div>
        <div class="form-group">
            <label>Médico</label>
            <asp:DropDownList ID="ddlMedico" runat="server" CssClass="form-select" />
        </div>
        <div class="form-group">
            <label>Paciente</label>
            <asp:TextBox ID="txtPaciente" runat="server" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label>Estado</label>
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" />
        </div>
        <div class="form-group align-self-end">
            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" Text="Buscar" OnClick="btnBuscar_Click" />
        </div>
    </div>

    <asp:Label ID="lblMensaje" runat="server" Visible="false" />

    <asp:GridView ID="gvTurnos" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="Fecha" DataField="Fecha" />
            <asp:BoundField HeaderText="Hora" DataField="Hora" />
            <asp:BoundField HeaderText="Paciente" DataField="NombrePaciente" />
            <asp:BoundField HeaderText="DNI" DataField="DniPaciente" />
            <asp:BoundField HeaderText="Médico" DataField="NombreMedico" />
            <asp:BoundField HeaderText="Especialidad" DataField="Especialidad" />
            <asp:BoundField HeaderText="Estado" DataField="Estado" />
        </Columns>
    </asp:GridView>
</asp:Content>
