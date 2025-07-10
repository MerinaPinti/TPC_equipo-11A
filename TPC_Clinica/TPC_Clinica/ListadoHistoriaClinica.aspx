<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListadoHistoriaClinica.aspx.cs" Inherits="TPC_Clinica.ListadoHistoriaClinica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card">
        <div class="card-header">
        </div>
        <div class="card-body">
            <figure>
                <blockquote class="blockquote">
                    <p><strong>Paciente</strong></p>
                </blockquote>
                <figcaption class="blockquote-footer">Especialidad</figcaption>
            </figure>
            <p>Fecha:</p>
            <p><small>DNI:</small></p>
                    <hr />
            <div class="accordion accordion-flush" id="accordionFlushExample" >
                <div class="accordion-item" >
                    <h2 class="accordion-header">
                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapseOne" aria-expanded="false" aria-controls="flush-collapseOne">
                            Diagnóstico y observaciones
                        </button>
                    </h2>
                    <div id="flush-collapseOne" class="accordion-collapse collapse" data-bs-parent="#accordionFlushExample">
                        <div class="accordion-body">Placeholder content for this accordion, which is intended to demonstrate the <code>.accordion-flush</code> class. This is the first item’s accordion body.</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="lblTittle" runat="server" Text="Label"></asp:Label>
</asp:Content>
