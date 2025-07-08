<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AtenderPaciente.aspx.cs" Inherits="TPC_Clinica.AtenderPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-label {
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-primary text-center mt-4">Atender Paciente</h2>

    <asp:Panel ID="pnlTurno" runat="server" CssClass="container mt-4">
        <div class="mb-3">
            <label class="form-label">Paciente:</label>
            <asp:Label ID="lblPaciente" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label class="form-label">Fecha:</label>
            <asp:Label ID="lblFecha" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label class="form-label">Hora:</label>
            <asp:Label ID="lblHora" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label for="txtObservaciones" class="form-label">Observaciones:</label>
            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
        </div>

        <div class="mb-3">
            <label for="txtDiagnostico" class="form-label">Diagnóstico:</label>
            <asp:TextBox ID="txtDiagnostico" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
        </div>

        <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success" Text="Cerrar Turno" OnClick="btnGuardar_Click" />
        <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary ms-2" Text="Cancelar" PostBackUrl="~/VerTurnoMedico.aspx" />
    </asp:Panel>
</asp:Content>
