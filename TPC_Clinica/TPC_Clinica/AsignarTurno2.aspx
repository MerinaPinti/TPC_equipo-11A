<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AsignarTurno2.aspx.cs" Inherits="TPC_Clinica.AsignarTurno2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
    <h3 class="mb-4">Nuevo turno</h3>
    <div class="row g-4">

        <!-- Tarjeta Especialidades Médicas -->
<div class="col-md-6">
    <asp:LinkButton ID="btnEspecialidad" runat="server" CssClass="text-decoration-none" OnClick="btnEspecialidad_Click">
        <div class="card text-center shadow-sm border-0 h-100">
            <div class="card-body">
                <img src="https://img.freepik.com/vector-premium/simbolo-cruz-medica-icono-lineal-atencion-medica-signo-primeros-auxilios_53562-18944.jpg" alt="Especialidad" style="height: 40px;" class="mb-3" />
                <h5 class="card-title text-dark">Especialidades Médicas</h5>
            </div>
        </div>
    </asp:LinkButton>
</div>

<!-- Tarjeta Profesional -->
<div class="col-md-6">
    <asp:LinkButton ID="btnProfesional" runat="server" CssClass="text-decoration-none" OnClick="btnProfesional_Click">
        <div class="card text-center shadow-sm border-0 h-100">
            <div class="card-body">
                <img src="https://cdn-icons-png.flaticon.com/512/16/16746.png" alt="Profesional" style="height: 40px;" class="mb-3" />
                <h5 class="card-title text-dark">Profesional</h5>
            </div>
        </div>
    </asp:LinkButton>
</div>

    </div>
</div>

</asp:Content>
