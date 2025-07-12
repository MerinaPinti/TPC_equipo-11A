<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerTurnoMedico.aspx.cs" Inherits="TPC_Clinica.VerTurnoMedico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .table td, .table th { vertical-align: middle; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-primary text-center mt-4">Turnos del día</h2>

    
    <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-warning" Visible="false" />
   <asp:GridView ID="gvTurnos" runat="server" CssClass="table table-striped"
    AutoGenerateColumns="False"
    OnRowCommand="gvTurnos_RowCommand"
    OnRowDataBound="gvTurnos_RowDataBound"
    DataKeyNames="NroTurno">
    <Columns>
        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
        <asp:BoundField DataField="Hora" HeaderText="Hora" />
        <asp:BoundField HeaderText="Paciente" DataField="NombrePaciente" />
        <asp:BoundField HeaderText="Especialidad" DataField="Especialidad" />

<%-- Futuro campo: Motivo de consulta
<asp:BoundField HeaderText="Motivo de Consulta" DataField="MotivoConsulta" />
--%>
        <asp:ButtonField ButtonType="Button" CommandName="Atender" Text="Atender" ControlStyle-CssClass="btn btn-success btn-sm" />
        <asp:ButtonField ButtonType="Button" CommandName="NoAsistio" Text="No Asistió" ControlStyle-CssClass="btn btn-danger btn-sm" />
    </Columns>
</asp:GridView>

    <div aria-live="polite" aria-atomic="true" style="position: relative;">
    <div id="toastNoAsistio" class="toast position-absolute top-0 end-0 m-3" role="alert" aria-live="assertive" aria-atomic="true">
        <div class="toast-header bg-danger text-white">
            <strong class="me-auto">Turno cancelado</strong>
            <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Cerrar"></button>
</div>
    <script>
    function mostrarToastNoAsistio() {
        var toastEl = document.getElementById('toastNoAsistio');
        var toast = new bootstrap.Toast(toastEl);
        toast.show();
    }
    </script>

</asp:Content>

