<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="HistoriaClinica.aspx.cs" Inherits="TPC_Clinica.HistoriaClinica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container mt-4">
        <h3>Historial Clinica</h3>

        <div class="row g-3">
            <div class="col-md-4">
                <label for="txtDNI">DNI del paciente:</label>
                <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" />
            </div>

            <div class="col-md-4">
                <label for="ddlMedico">Médico:</label>
                <asp:DropDownList ID="ddlMedico" runat="server" CssClass="form-control" />
            </div>

            <div class="col-md-4">
                <label for="ddlEspecialidad">Especialidad:</label>
                <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary mt-3" OnClick="btnBuscar_Click" />
        </div>

        <asp:Label ID="lblSinTurnos" runat="server" CssClass="text-danger d-block mt-2" Text="🔍 No se encontraron turnos asignados al paciente."></asp:Label>


        <asp:GridView ID="gvResultados" runat="server" CssClass="table table-striped mt-4" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="Medico" HeaderText="Médico" />
                <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
                <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                <asp:BoundField DataField="Diagnostico" HeaderText="Diagnostico" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
