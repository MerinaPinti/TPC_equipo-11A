<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Recepcion.aspx.cs" Inherits="TPC_Clinica.Recepcion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .filtro-label {
        font-weight: bold;
        margin-right: 10px;
    }

    .filtro-input {
        margin-bottom: 15px;
    }

    .tabla-turnos {
        margin-top: 20px;
    }

    .filtros-container {
        padding: 15px;
        background-color: #f5f5f5;
        border-radius: 10px;
        margin-bottom: 20px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2 class="text-center mt-4 mb-4 display-5 fw-bold text-uppercase text-primary">
    TURNOS DEL DÍA
</h2>

    <div class="container">
        
        
        <!-- Filtros -->
        <div class="row filtros-container">
            <div class="col-md-4">
                <label class="filtro-label" for="txtDni">Buscar por DNI:</label>
                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control filtro-input" placeholder="DNI del paciente" AutoPostBack="true" OnTextChanged="txtDni_TextChanged" />
            </div>

            <div class="col-md-4">
                <label class="filtro-label" for="ddlEspecialidades">Filtrar por especialidad:</label>
                <asp:DropDownList ID="ddlEspecialidades" runat="server" CssClass="form-control filtro-input" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidades_SelectedIndexChanged" />
            </div>

            <div class="col-md-4">
                <label class="filtro-label" for="txtMedico">Buscar por nombre del médico:</label>
                <asp:TextBox ID="txtMedico" runat="server" CssClass="form-control filtro-input" placeholder="Nombre o apellido" AutoPostBack="true" OnTextChanged="txtMedico_TextChanged" />
            </div>
        </div>
        <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-warning" Visible="false"></asp:Label>
        <!-- GRID -->
        <asp:GridView ID="gvTurnos" runat="server" CssClass="table table-bordered tabla-turnos" AutoGenerateColumns="false" OnRowCommand="gvTurnos_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="Hora" DataField="Hora" />
                <asp:BoundField HeaderText="Paciente" DataField="PacienteNombre" />
                <asp:BoundField HeaderText="DNI" DataField="DniPaciente" />
                <asp:BoundField HeaderText="Especialidad" DataField="Especialidad" />
                <asp:BoundField HeaderText="Médico" DataField="MedicoNombre" />
                <asp:BoundField HeaderText="Estado" DataField="EstadoTurno" />
              
    <asp:TemplateField HeaderText="Acción">
    <ItemTemplate>
        <asp:Button ID="btnAsistio" runat="server" Text="Asistió"
            CssClass="btn btn-success btn-sm me-2"
            CommandName="Asistio"
            CommandArgument='<%# Eval("NroTurno") %>'
            Enabled='<%# Eval("IdEstado").ToString() == "1" %>' />

        <asp:Button ID="btnNoAsistio" runat="server" Text="No Asistió"
            CssClass="btn btn-danger btn-sm"
            CommandName="NoAsistio"
            CommandArgument='<%# Eval("NroTurno") %>'
            Enabled='<%# Eval("IdEstado").ToString() == "1" %>' />
    </ItemTemplate>
</asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
