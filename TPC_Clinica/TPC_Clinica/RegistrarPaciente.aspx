<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="RegistrarPaciente.aspx.cs" Inherits="TPC_Clinica.RegistrarPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-card {
            max-width: 700px;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-card">
        <div class="row mb-3">
            <div class="col">
                <label for="exampleFormControlInput1" class="form-label">Nombre/s</label>
                <input type="text" class="form-control" placeholder="Juan" />
            </div>
            <div class="col">
                <label for="exampleFormControlInput1" class="form-label">Apellido/s</label>
                <input type="text" class="form-control" placeholder="Perez">
            </div>
        </div>
        <div class="row mb-3">
            <div class="col">
                <label for="exampleFormControlInput1" class="form-label">Email</label>
                <input type="email" class="form-control" placeholder="Juanperez@hotmail.com" />
            </div>
        </div>
        <div class="row mb-3">
            <div class="col">
                <label for="exampleFormControlInput1" class="form-label">DNI</label>
                <input type="number" class="form-control" placeholder="Juan" />
            </div>
            <div class="col">
                <label for="exampleFormControlInput1" class="form-label">Fecha de Nacimiento</label>
                <input type="date" class="form-control" placeholder="Perez">
            </div>
        </div>
        <div class="row mb-3">
            <div class="col">
                <label for="exampleFormControlInput1" class="form-label">Provincia</label>
                <asp:DropDownList ID="DropDownList2" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="col">
                <label for="exampleFormControlInput1" class="form-label">Localidad</label>
                <asp:DropDownList ID="DropDownList1" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col">
                <label for="exampleFormControlInput1" class="form-label">Dirección</label>
                <input type="text" class="form-control" placeholder="Av. Don Bosco 1111">
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-2">
                <label for="exampleFormControlInput1" class="form-label">Prefijo</label>
                <input type="number" class="form-control" placeholder="11" />
            </div>
            <div class="col">
                <label for="exampleFormControlInput1" class="form-label">Número de teléfono</label>
                <input type="number" class="form-control" placeholder="58587896">
            </div>
        </div>
    </div>

</asp:Content>
