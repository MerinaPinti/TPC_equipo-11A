<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerTurnoMedico.aspx.cs" Inherits="TPC_Clinica.VerTurnoMedico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .table td, .table th { vertical-align: middle; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-primary text-center mt-4">Turnos de la Semana</h2>

    <div class="mb-3">
        <label for="ddlEspecialidades" class="form-label fw-bold">Filtrar por Especialidad:</label>
        <asp:DropDownList ID="ddlEspecialidades" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidades_SelectedIndexChanged" />
    </div>

    <asp:GridView ID="gvTurnos" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="gvTurnos_RowCommand">
        <Columns>
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
            <asp:BoundField DataField="Hora" HeaderText="Hora" />
            <asp:BoundField DataField="NombrePaciente" HeaderText="Paciente" />
            <asp:ButtonField ButtonType="Button" CommandName="Atender" Text="Atender" ControlStyle-CssClass="btn btn-success btn-sm" />
            <asp:ButtonField ButtonType="Button" CommandName="NoAsistio" Text="No Asistió" ControlStyle-CssClass="btn btn-danger btn-sm" />
        </Columns>
    </asp:GridView>
</asp:Content>

