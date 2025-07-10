<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="HistoriaClinica.aspx.cs" Inherits="TPC_Clinica.HistoriaClinica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .clinica-list {
            max-width: 70vw;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clinica-list">
        <div class="row">
            <div class="col">
                <div class="container mb-1">
                    <h3>Historial Clinica</h3>

                    <div class="row g-3" runat="server" id="divHeader">
                        <div class="col-md-4">
                            <label for="txtDNI">DNI del paciente:</label>
                            <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-4">
                            <label for="ddlMedico">Médico:</label>
                            <asp:DropDownList ID="ddlMedico" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-4">
                            <label for="ddlEspecialidad">Especialidad:</label>
                            <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-control" />
                        </div>
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary mt-3" OnClick="btnBuscar_Click" />
                    </div>

                    <asp:Label ID="lblSinTurnos" runat="server" CssClass="text-danger d-block mt-2" Text="🔍 No se encontraron turnos asignados al paciente."></asp:Label>
                    <asp:Repeater ID="repeaterCard" runat="server">
                        <ItemTemplate>
                            <div class="card mt-3 mb-3">
                                <div class="card-header" style="background-color: #CFE2FF;">
                                </div>
                                <div class="card-body" style="background-color: #F8F9FA;">
                                    <figure>
                                        <blockquote class="blockquote">
                                            <h3><strong><%# ((Dominio.Turno)Container.DataItem).Paciente.Nombre + " " + ((Dominio.Turno)Container.DataItem).Paciente.Apellido %></strong></h3>
                                        </blockquote>
                                        <figcaption class="blockquote-footer"><%# ((Dominio.Turno)Container.DataItem).Especialidad.Descripcion %></figcaption>
                                    </figure>
                                    <p>Fecha: <%# String.Format("{0:dd/MM/yyyy}", Eval("Fecha")) %></p>
                                    <p><small>DNI: <%# (string)Session["DniHistoriaClinica"]%></small></p>
                                    <hr />

                                    <div class="accordion accordion-flush" id='<%# "accordionFlush_" + Eval("NroTurno") %>'>
                                        <div class="accordion-item border rounded">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed"
                                                    type="button"
                                                    data-bs-toggle="collapse"
                                                    data-bs-target='<%# "#flush-collapse_" + Eval("NroTurno") %>'
                                                    aria-expanded="false"
                                                    aria-controls='<%# "flush-collapse_" + Eval("NroTurno") %>'>
                                                    Ver observaciones y diagnóstico
                                                </button>
                                            </h2>
                                            <div id='<%# "flush-collapse_" + Eval("NroTurno") %>'
                                                class="accordion-collapse collapse"
                                                data-bs-parent='<%# "#accordionFlush_" + Eval("NroTurno") %>'>

                                                <div class="accordion-body">
                                                    <h5>Observaciones</h5>
                                                    <p><%#Eval ("Observaciones")%></p>
                                                    <hr />
                                                    <h5>Diagnóstico</h5>
                                                    <p><%#Eval ("Diagnostico")%></p>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
