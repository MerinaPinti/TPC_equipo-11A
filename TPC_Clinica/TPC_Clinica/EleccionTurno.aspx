<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EleccionTurno.aspx.cs" Inherits="TPC_Clinica.EleccionTurno" %>

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
        <h2>¿Qué desea hacer?</h2>

        <div class="d-grid gap-3">
       


            <asp:Button 
   ID="btnAsignar" runat="server" Text="Asignar un nuevo turno" CssClass="btn btn-turno w-100 mb-3" OnClick="btnAsignar_Click" />

<asp:Button 
    ID="btnModificar" runat="server" Text="Modificar un turno asignado" CssClass="btn btn-turno w-100 mb-3" OnClick="btnModificar_Click" />

<asp:Button 
    ID="btnCancelar" runat="server" Text="Cancelar un turno" CssClass="btn btn-turno w-100" OnClick="btnCancelar_Click" />
   </div>
    </div>
</asp:Content>
