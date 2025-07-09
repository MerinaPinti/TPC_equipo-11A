<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ModificarTurno.aspx.cs" Inherits="TPC_Clinica.ModificarTurno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="paciente-list">
        <div class="row mb-3">
            <div class="col-md-8">
                <asp:TextBox ID="txtBuscarDNI" runat="server" CssClass="form-control" Placeholder="Buscar por DNI..." />
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary w-100" OnClick="btnBuscar_Click" />
                <asp:Label ID="lblSinTurnos" runat="server" CssClass="text-danger d-block mt-2" Text="🔍 No se encontraron turnos asignados al paciente."
            Visible="false" />
            </div>
        </div>

        <asp:GridView ID="dgvTurnos" runat="server" AutoGenerateColumns="False"
              CssClass="table" OnRowCommand="dgvTurnos_RowCommand" 
              DataKeyNames="NroTurno">
    <Columns>
        <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField HeaderText="Hora" DataField="Hora" DataFormatString="{0:hh\\:mm}" />
        <asp:BoundField HeaderText="Medico" DataField="Medico" />
        <asp:BoundField HeaderText="Especialidad" DataField="Especialidad" />

        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button ID="btnModificar" runat="server"
                    Text="Modificar"
                    CommandName="Modificar"
                    CommandArgument='<%# Eval("NroTurno") %>'
                    CssClass="btn btn-success btn-sm me-2" />
                <asp:Button ID="btnCancelar" runat="server"
                    Text="Cancelar"
                    CommandName="Cancelar"
                    CommandArgument='<%# Eval("NroTurno") %>'
                    CssClass="btn btn-danger btn-sm"
                    OnClientClick="return confirm('¿Seguro que querés cancelar este turno?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
    </div>
</asp:Content>
