<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="HorarioMedico.aspx.cs" Inherits="TPC_Clinica.HorarioMedico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
        .titulo {
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
            color: black;
        }

        .panel-turno {
            background-color: #f5f5f5;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }

        .tabla-turno {
            width: 100%;
            border-collapse: collapse;
        }

        .tabla-turno th,
        .tabla-turno td {
            padding: 10px;
            text-align: center;
            border-bottom: 1px solid #ddd;
        }

        .tabla-turno th {
            background-color: #2c3e50;
            color: white;
        }

        .boton-accion {
            margin-right: 10px;
        }

        .cabecera {
    background-color: #f1f1f1; 
    padding: 10px 20px;
    color: #333;
    border-radius: 10px 10px 0 0;
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-weight: bold;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cabecera">
        <span class="titulo">Turno de trabajo</span>
    </div>

    <div class="panel-turno">

        <!-- DropDownList de especialidades-->
        <div class="mb-3">
            <label for="ddlEspecialidades" class="form-label fw-bold">Seleccione la especialidad:</label>
            <asp:DropDownList ID="ddlEspecialidades" runat="server" CssClass="form-select" AutoPostBack="false">
            </asp:DropDownList>
        </div>

        <!-- GridView de horarios -->
        <asp:GridView 
            ID="gvHorarioMedico" 
            runat="server" 
            AutoGenerateColumns="False" 
            CssClass="tabla-turno" 
            OnRowDataBound="gvHorarioMedico_RowDataBound">
            
            <Columns>
                <asp:BoundField DataField="Dia" HeaderText="Día" ReadOnly="True" />
                
                <asp:TemplateField HeaderText="Horario de ingreso">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlHoraInicio" runat="server" CssClass="form-select"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Horario de salida">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlHoraFin" runat="server" CssClass="form-select"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="No trabaja">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDiaLibre" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div style="margin-top: 20px; text-align: right;">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success boton-accion" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger boton-accion" OnClick="btnCancelar_Click" />
        </div>
    </div>
</asp:Content>
