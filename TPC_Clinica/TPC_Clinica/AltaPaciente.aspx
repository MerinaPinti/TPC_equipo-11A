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
            font-weight: bold;
            border-radius: 25px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="card-alta">
        <h2>Datos del Paciente</h2>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnActivarPaciente" runat="server" Value="false" />
                <div class="mb-3">
                    <label for="txtDNI" class="form-label">DNI</label>
                    <asp:TextBox ID="txtDNI" runat="server" AutoPostBack="true" OnTextChanged="txtDNI_DataBinding" CssClass="form-control" placeholder="40123456" />
                    <asp:Label ID="lblDNI" runat="server" CssClass="form-text" />
                </div>

                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Juan" />
                    <asp:Label ID="lblNombre" runat="server" CssClass="form-text" />
                </div>

                <div class="mb-3">
                    <label for="txtApellido" class="form-label">Apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Pérez" />
                    <asp:Label ID="lblApellido" runat="server" CssClass="form-text" />
                </div>

                <div class="mb-3">
                    <label for="txtFechaNac" class="form-label">Fecha de Nacimiento</label>
                    <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control" TextMode="Date" />
                    <asp:Label ID="lblFechaNac" runat="server" CssClass="form-text" />
                </div>

                <div class="mb-3">
                    <label for="txtTelefono" class="form-label">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" TextMode="Phone" placeholder="+54 9 11 1234-5678" />
                    <asp:Label ID="lblTelefono" runat="server" CssClass="form-text" />
                </div>

                <div class="mb-3">
                    <label for="txtDireccion" class="form-label">Dirección</label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Calle 123" />
                    <asp:Label ID="lblDireccion" runat="server" CssClass="form-text" />
                </div>

                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="correo@mail.com" />
                    <asp:Label ID="lblEmail" runat="server" CssClass="form-text" />
                </div>

                <asp:Button ID="btnGuardarPaciente" runat="server" Text="Guardar" CssClass="btn btn-success w-100" OnClick="btnGuardarPaciente_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-danger w-100 mt-3" OnClick="btnCancelar_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
