<%@ Page Title="Error" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TPC_Clinica.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-card {
            max-width: 400px;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }

        .btn-volver {
            border-radius: 25px;
            font-weight: bold;
        }

        .error-msg {
            font-weight: 500;
            color: #d50000;
            margin-bottom: 20px;
        }

        .error-subtext {
            color: #9e9e9e;
            font-size: 0.9rem;
            margin-bottom: 25px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-card text-center">
        <h2 class="error-msg">¡Acceso denegado!</h2>
        <asp:Label ID="lblMensaje" runat="server" CssClass="error-subtext d-block" />
        <asp:Button ID="btnVolver" runat="server" Text="Volver al inicio" CssClass="btn btn-outline-primary btn-volver w-100" OnClick="btnVolver_Click" />
    </div>
</asp:Content>


