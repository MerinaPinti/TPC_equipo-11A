<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AltaPaciente.aspx.cs" Inherits="TPC_Clinica.AltaPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-alta {
            max-width: 500px;
            margin: 60px auto;
            padding: 30px;
            border-radius: 15px;
            background-color: #ffffff;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
        }

        .card-alta h2 {
            font-size: 1.8rem;
            color: #0d6efd;
            font-weight: bold;
            text-align: center;
            margin-bottom: 25px;
        }

        .btn-alta {
            background-color: #0d6efd;
            color: white;
            font-weight: bold;
            border-radius: 25px;
            border: none;
        }

        .btn-alta:hover {
            background-color: #0b5ed7;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-alta">
        <h2>Datos del Paciente</h2>

        <div class="mb-3">
            <label for="txtDNI" class="form-label">DNI</label>
            <div class="d-flex gap-2">
                <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" placeholder="40123456" />
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-primary" OnClick="btnBuscar_Click" />
            </div>
        </div>

        <div class="mb-3">
            <label for="txtNombre" class="form-label">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Juan" />
        </div>

        <div class="mb-3">
            <label for="txtApellido" class="form-label">Apellido</label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Pérez" />
        </div>

        <div class="mb-3">
            <label for="txtFechaNac" class="form-label">Fecha de Nacimiento</label>
            <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control" TextMode="Date" />
        </div>

        <div class="mb-3">
            <label for="txtTelefono" class="form-label">Teléfono</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" TextMode="Phone" placeholder="+54 9 11 1234-5678" />
        </div>

        <div class="mb-3">
            <label for="txtDireccion" class="form-label">Dirección</label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Calle 123" />
        </div>

        <div class="mb-3">
            <label for="txtEmail" class="form-label">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="correo@mail.com" />
        </div>

        <asp:Button 
            ID="btnGuardarPaciente" runat="server" Text="Guardar Paciente" CssClass="btn btn-alta w-100" OnClick="btnGuardarPaciente_Click" />
    </div>
</asp:Content>
