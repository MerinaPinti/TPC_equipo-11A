<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AsignarTurnoPorMedico.aspx.cs" Inherits="TPC_Clinica.AsignarTurnoPorMedico" %>

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
            $(function () {
                // Autocompletado de médico
                $("#<%= txtMedico.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "AsignarTurnoPorMedico.aspx/BuscarMedico",
                    method: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ prefix: request.term }),
                    success: function (data) {
                        response(data.d);
                    }
                });
            },
            select: function (event, ui) {
                $("#<%= txtMedico.ClientID %>").val(ui.item.label);
                $("#<%= hfIdMedico.ClientID %>").val(ui.item.value);
                __doPostBack('<%= btnCargarEspecialidades.UniqueID %>', '');
                return false;
            },
            minLength: 2
        });

        // Botón para confirmar cancelación del turno 
        $("#btnConfirmarCancelacion").on("click", function () {
            const idTurno = $("#<%= hfIdTurnoACancelar.ClientID %>").val();

            $.ajax({
                url: 'AsignarTurnoPorMedico.aspx/CancelarTurno',
                method: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ idTurno: parseInt(idTurno) }),
                success: function () {
                    var modalCancelar = bootstrap.Modal.getInstance(document.getElementById('modalCancelarTurno'));
                    modalCancelar.hide();

                    // Refrescar el calendario con los turnos actualizados
                    document.getElementById("calendar").innerHTML = "";
                    const idMedico = $("#<%= hfIdMedico.ClientID %>").val();
                    const idEspecialidad = $("#<%= ddlEspecialidades.ClientID %>").val();
                    cargarCalendario(idMedico, idEspecialidad);
                },
                error: function () {
                    alert("Error al cancelar el turno.");
                }
            });
        });

        //  Lógica calendario
        window.cargarCalendario = function (idMedico, idEspecialidad) {
            console.log("Ejecutando cargarCalendario con:", idMedico, idEspecialidad);

            $.ajax({
                type: "POST",
                url: "AsignarTurnoPorMedico.aspx/ObtenerTurnosDisponibles",
                data: JSON.stringify({ idMedico: idMedico, idEspecialidad: idEspecialidad }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventos = response.d;
                    console.log("Eventos recibidos:", eventos);

                    var calendarEl = document.getElementById('calendar');
                    calendarEl.innerHTML = "";

                    var calendar = new FullCalendar.Calendar(calendarEl, {
                        initialView: 'dayGridMonth',
                        locale: 'es',
                        height: 600,
                        eventDisplay: 'block',
                        events: eventos,
                        eventClick: function (info) {
                            const fecha = info.event.start;
                            const hora = info.event.extendedProps.hora;
                            const estado = info.event.extendedProps.estado;
                            const idTurno = info.event.extendedProps.idTurno;
                            const nombrePaciente = info.event.extendedProps.nombrePaciente;
                            const nombreMedico = info.event.extendedProps.nombreMedico;

                            if (estado === "Disponible") {
                                document.getElementById('<%= hfFechaTurno.ClientID %>').value = fecha.toISOString().substring(0, 10);
                                document.getElementById('<%= hfHoraTurno.ClientID %>').value = hora;
                                document.getElementById('<%= hfIdMedico.ClientID %>').value = idMedico;

                                var modal = new bootstrap.Modal(document.getElementById('modalTurno'));
                                modal.show();
                            } else if (estado === "Asignado") {
                                $("#lblPacienteTurno").text(nombrePaciente);
                                $("#lblFechaTurno").text(fecha.toLocaleDateString());
                                $("#lblHoraTurno").text(fecha.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }));
                                $("#lblMedicoTurno").text(nombreMedico);

                                $("#<%= hfIdTurnoACancelar.ClientID %>").val(idTurno);

                                var modalCancelar = new bootstrap.Modal(document.getElementById('modalCancelarTurno'));
                                modalCancelar.show();
                            } else {
                                alert("Este turno no está disponible para modificación.");
                            }
                        }
                    });

                    calendar.render();
                },
                error: function (err) {
                    console.error("Error en AJAX:", err);
                }
            });
        }
    });
        </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <div class="mb-3">
        <label for="txtMedico" class="form-label fw-bold">Buscar médico:</label>
        <asp:TextBox ID="txtMedico" runat="server" CssClass="form-control" placeholder="Escriba el nombre del médico..." />
        <asp:HiddenField ID="hfIdMedico" runat="server" />
        <asp:HiddenField ID="hfFechaTurno" runat="server" />
<asp:HiddenField ID="hfHoraTurno" runat="server" />
        <asp:Button ID="btnCargarEspecialidades" runat="server" Text="Cargar Especialidades" CssClass="btn btn-secondary mt-2" OnClick="btnCargarEspecialidades_Click" Style="display:none;" />
    </div>

    <div class="mb-3">
        <label for="ddlEspecialidades" class="form-label fw-bold">Seleccione la especialidad:</label>
        <asp:DropDownList ID="ddlEspecialidades" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidades_SelectedIndexChanged" />
    </div>

    <!-- Funciona como session pero en realidad sólo guardamos el valor que pertenece a la misma ventana -->
<asp:HiddenField ID="hfIdEspecialidadSeleccionada" runat="server" />
    <!-- Guardamos el ID del turno a cancelar para la segunda ventana modal-->
    <asp:HiddenField ID="hfIdTurnoACancelar" runat="server" />
    
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlCalendario" runat="server" CssClass="mt-4" Visible="true">
                <h4>📅 Turnos disponibles</h4>
                <div id="calendar"></div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlEspecialidades" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

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


    <!-- Modal para cancelar un turno ya asignado -->
<div class="modal fade" id="modalCancelarTurno" tabindex="-1" aria-labelledby="modalCancelarTurnoLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header bg-danger text-white">
        <h5 class="modal-title" id="modalCancelarTurnoLabel">Cancelar Turno</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
      </div>
      <div class="modal-body">
        <p><strong>Paciente:</strong> <span id="lblPacienteTurno"></span></p>
        <p><strong>Fecha:</strong> <span id="lblFechaTurno"></span></p>
        <p><strong>Hora:</strong> <span id="lblHoraTurno"></span></p>
        <p><strong>Profesional:</strong> <span id="lblMedicoTurno"></span></p>
        <p class="text-danger">¿Deseás cancelar este turno? El horario quedará disponible.</p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
        <button type="button" class="btn btn-danger" id="btnConfirmarCancelacion">Cancelar Turno</button>
      </div>
    </div>
  </div>
</div>

</asp:Content>
