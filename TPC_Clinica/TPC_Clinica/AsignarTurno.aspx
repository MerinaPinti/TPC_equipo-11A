<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AsignarTurno.aspx.cs" Inherits="TPC_Clinica.AsignarTurno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-turno {
            max-width: 400px;
            margin: 60px auto;
            padding: 30px;
            border-radius: 15px;
            background-color: #ffffff;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
        }

        .card-turno h2 {
            font-size: 1.8rem;
            color: #17a2b8;
            font-weight: bold;
            text-align: center;
            margin-bottom: 25px;
        }

        .btn-turno {
            background-color: #17a2b8;
            color: white;
            font-weight: bold;
            border-radius: 25px;
            border: none;
        }

        .btn-turno:hover {
            background-color: #138496;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="card-turno">
    <h2>Asignar Turno</h2>

    <div class="mb-3">
        <label for="txtDni" class="form-label">DNI del paciente</label>
        <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" placeholder="Ej: 40123456" />
    </div>

    <asp:Button 
        ID="btnBuscarPaciente" runat="server" Text="Buscar paciente" CssClass="btn btn-turno w-100 mb-3" OnClick="btnBuscarPaciente_Click" />

    <%-- Formulario que aparece luego de buscar --%>
    <asp:Panel ID="panelFormularioTurno" runat="server" Visible="false">

        <div class="mb-3">
            <label for="ddlEspecialidad" class="form-label">Especialidad médica</label>
            <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select" />
        </div>


    </asp:Panel>
</div>
</asp:Content>
