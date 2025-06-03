<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Clinica.FormuPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-card {
            max-width: 400px;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            background-color:#fff ;
        }

        .login-card h2 {
            font-weight: 500;
            margin-bottom: 30px;
        }

        .btn-login {
            background-color: #d50000;
            color: #fff;
            font-weight: bold;
            border-radius: 25px;
        }

        .btn-login:hover {
            background-color: #b71c1c;
        }

        .login-footer {
            font-size: 0.9rem;
            text-align: center;
            margin-top: 20px;
        }

        .login-footer a {
            color: #d50000;
            text-decoration: none;
        }

        .form-control:focus {
            box-shadow: none;
            border-color: #d50000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-card">
        <h2 class="text-center">Iniciar sesión</h2>

        <div class="mb-3">
            <label for="usuario" class="form-label">Usuario</label>
            <input type="text" class="form-control" id="usuario" placeholder="Ingresar usuario" />
        </div>

        <div class="mb-3">
            <label for="password" class="form-label">Contraseña</label>
            <div class="input-group">
                <input type="password" class="form-control" id="password" placeholder="Ingresar contraseña" />
                <span class="input-group-text">
                    <i class="bi bi-eye"></i> 
                </span>
            </div>
        </div>

        <div class="mb-3 form-check">
            <input type="checkbox" class="form-check-input" id="recordarUsuario" />
            <label class="form-check-label" for="recordarUsuario">Recordar usuario</label>
        </div>

        <button type="submit" class="btn w-100" style="background-color: #17a2b8; color: white; font-weight: bold; border-radius: 25px;">
        Ingresar
        </button>

      
    </div>
</asp:Content>
