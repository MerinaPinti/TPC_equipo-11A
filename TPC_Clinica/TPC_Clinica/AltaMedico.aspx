<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AltaMedico.aspx.cs" Inherits="TPC_Clinica.AltaMedico" %>

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
                color: #198754;
                font-weight: bold;
                text-align: center;
                margin-bottom: 25px;
            }

        .btn {
            font-weight: bold;
            border-radius: 25px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="card-alta">
                <h2>Datos del Médico</h2>

                <div class="mb-3">
                    <label for="txtMatricula" class="form-label">N° Matricula</label>
                    <asp:TextBox ID="txtMatricula" AutoPostBack="true" OnTextChanged="txtMatricula_TextChanged" runat="server" CssClass="form-control" placeholder="123456" />
                    <asp:Label ID="lblMatricula" runat="server" CssClass="form-text" />
                </div>

                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Juan Carlos" />
                    <asp:Label ID="lblNombre" runat="server" CssClass="form-text" />
                </div>

                <div class="mb-3">
                    <label for="txtApellido" class="form-label">Apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Perez" />
                    <asp:Label ID="lblApellido" runat="server" CssClass="form-text" />
                </div>

                <div class="mb-3">
                    <label for="ddlEspecialidad" class="form-label">Especialidad</label>
                    <asp:CheckBoxList ID="chkEspecialidades" runat="server" RepeatLayout="Table" RepeatColumns="2"></asp:CheckBoxList>
                </div>

                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="nombre@mail.com" />
                    <asp:Label ID="lblEmail" runat="server" CssClass="form-text" />
                </div>

                <div class="mb-3">
                    <label for="txtTelefono" class="form-label">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" TextMode="Phone" placeholder="+54 9 11 1234-5678" />
                    <asp:Label ID="lblTelefono" runat="server" CssClass="form-text" />
                </div>
                <div id="divMensaje" class="alert alert-info" role="alert" runat="server">
                    <strong>¡Aviso!</strong> Al crear un médico, también se generará automáticamente un usuario asociado.
            <br />
                    Para ingresar al sistema:<br />
                    - <strong>Usuario:</strong> número de DNI del médico<br />
                    - <strong>Contraseña inicial:</strong> mismo número de DNI<br />
                    <em>Se recomienda cambiar la contraseña en el primer ingreso.</em>
                </div>


                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success w-100" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-danger w-100 mt-3" OnClick="btnCancelar_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
