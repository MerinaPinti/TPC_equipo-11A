<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AsignarTurnoPorEspecialidad.aspx.cs" Inherits="TPC_Clinica.AsignarTurnoPorEspecialidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>

    <!-- FullCalendar v5 -->
    <link href='https://cdn.jsdelivr.net/npm/fullcalendar@5.11.3/main.min.css' rel='stylesheet' />
    <script src='https://cdn.jsdelivr.net/npm/fullcalendar@5.11.3/main.min.js'></script>

    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

    <script type="text/javascript">
        function cargarCalendario(idMedico, idEspecialidad) {
            $.ajax({
                type: "POST",
                url: "AsignarTurnoPorEspecialidad.aspx/ObtenerTurnosDisponibles",
                data: JSON.stringify({ idMedico: idMedico, idEspecialidad: idEspecialidad }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventos = response.d;

                    var calendarEl = document.getElementById('calendar');
                    calendarEl.innerHTML = ""; // Limpiar antes de renderizar

                    var calendar = new FullCalendar.Calendar(calendarEl, {
                        initialView: 'dayGridMonth',
                        locale: 'es',
                        height: 600,
                        events: eventos,
                        eventDisplay: 'block',
                        eventClick: function (info) {
                            const fecha = info.event.start;
                            const hora = info.event.extendedProps.hora;
                            const estado = info.event.extendedProps.estado;

                            if (estado !== "Disponible") {
                                alert("Ese turno ya fue asignado.");
                                return;
                            }

                            // Guardamos los valores en los campos ocultos
                            document.getElementById('<%= hfFechaTurno.ClientID %>').value = fecha.toISOString().substring(0, 10);
                            document.getElementById('<%= hfHoraTurno.ClientID %>').value = hora;
                            document.getElementById('<%= hfIdMedico.ClientID %>').value = document.getElementById('<%= ddlMedicos.ClientID %>').value;

                            // Mostrar modal
                            var modal = new bootstrap.Modal(document.getElementById('modalTurno'));
                            modal.show();
                        }
                    });

                    calendar.render();
                },
                error: function (err) {
                    console.error("Error al obtener turnos:", err);
                }
            });
        }

        document.addEventListener("DOMContentLoaded", function () {
            const ddlMedicos = document.getElementById('<%= ddlMedicos.ClientID %>');
            ddlMedicos.addEventListener("change", function () {
                const idMedico = ddlMedicos.value;
                const idEspecialidad = document.getElementById('<%= ddlEspecialidades.ClientID %>').value;

                if (idMedico !== "" && idEspecialidad !== "") {
                    cargarCalendario(idMedico, idEspecialidad);
                }
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Campos ocultos para guardar la selección -->
<asp:HiddenField ID="hfFechaTurno" runat="server" />
<asp:HiddenField ID="hfHoraTurno" runat="server" />
<asp:HiddenField ID="hfIdMedico" runat="server" />

    <div class="container mt-4">
        <h2 class="text-primary text-center mb-4">Asignar Turno por Especialidad</h2>

        <div class="mb-3">
            <label for="ddlEspecialidades" class="form-label fw-bold">Especialidad:</label>
            <asp:DropDownList ID="ddlEspecialidades" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidades_SelectedIndexChanged" />
        </div>

        <div class="mb-3">
            <label for="ddlMedicos" class="form-label fw-bold">Médico:</label>
            <asp:DropDownList ID="ddlMedicos" runat="server" CssClass="form-select" AutoPostBack="false" />
        </div>

        <hr />

        <div id="calendar"></div>
    </div>

        <!-- Modal Bootstrap para ingresar DNI -->
    <div class="modal fade" id="modalTurno" tabindex="-1" aria-labelledby="modalTurnoLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTurnoLabel">Asignar turno</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
    <div class="mb-3">
        <label for="txtDniPaciente" class="form-label">DNI del paciente:</label>
        <asp:TextBox ID="txtDniPaciente" runat="server" CssClass="form-control" />
    </div>
</div>
                <div class="modal-footer">
                    <asp:Button ID="btnAsignarTurno" runat="server" CssClass="btn btn-primary" Text="Confirmar Turno" OnClick="btnAsignarTurno_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
