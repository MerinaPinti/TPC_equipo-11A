<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AltaMedico.aspx.cs" Inherits="TPC_Clinica.AltaMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-mod {
            max-width: 500px;
            margin: 60px auto;
            padding: 30px;
            border-radius: 15px;
            background-color: #ffffff;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
        }

        .card-mod h2 {
            font-size: 1.8rem;
            color: #198754;
            font-weight: bold;
            text-align: center;
            margin-bottom: 25px;
        }

        .btn-mod {
            background-color: #198754;
            color: white;
            font-weight: bold;
            border-radius: 25px;
            border: none;
        }

        .btn-mod:hover {
            background-color: #157347;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-mod">
        <h2>Modificar Datos de Médico</h2>

        <div class="mb-3">
            <label for="txtMatricula" class="form-label">Matrícula</label>
            <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" placeholder="M.N.: 12345" />
        </div>

        <div class="mb-3">
            <label for="txtNombre" class="form-label">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Juan Carlos"/>
        </div>

        <div class="mb-3">
            <label for="txtApellido" class="form-label">Apellido</label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Perez"/>
        </div>
        
        <div class="mb-3"> <!--ACA TIENEN QUE APARECER LAS ESPECIALIDADES YA ASIGNADAS AL MEDICO-->
            <label for="ddlEspecialidad" class="form-label">Especialidad</label>
            <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select">
            </asp:DropDownList>
        </div>

        <div class="mb-3">
            <label for="txtEmail" class="form-label">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="nombre@mail.com"/>
        </div>

        <div class="mb-3">
            <label for="txtTelefono" class="form-label">Teléfono</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" TextMode="Phone" placeholder="+54 9 11 1234-5678" />
        </div>

        <asp:Button 
            ID="btnGuardarCambios" runat="server" Text="Guardar cambios" CssClass="btn btn-mod w-100" OnClick="btnGuardar_Click" />
    </div>
</asp:Content>
