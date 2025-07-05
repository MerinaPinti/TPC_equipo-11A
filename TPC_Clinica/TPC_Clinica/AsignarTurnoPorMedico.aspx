<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AsignarTurnoPorMedico.aspx.cs" Inherits="TPC_Clinica.AsignarTurnoPorMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    
    <!-- FullCalendar v5 compatible -->
<link href='https://cdn.jsdelivr.net/npm/fullcalendar@5.11.3/main.min.css' rel='stylesheet' />
<script src='https://cdn.jsdelivr.net/npm/fullcalendar@5.11.3/main.min.js'></script>

    <script type="text/javascript">
        $(function () {
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
        });

            function cargarCalendario(idMedico, idEspecialidad) {
                console.log("Ejecutando cargarCalendario con:", idMedico, idEspecialidad); 

            $.ajax({
                type: "POST",
            url: "AsignarTurnoPorMedico.aspx/ObtenerTurnosDisponibles",
            data: JSON.stringify({idMedico: idMedico, idEspecialidad: idEspecialidad }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var eventos = response.d;
            console.log("Eventos recibidos:", eventos);  

            var calendarEl = document.getElementById('calendar');
            var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
            locale: 'es',
            height: 600,
            events: eventos
                });

            calendar.render();
            },
            error: function (err) {
                console.error("Error en AJAX:", err); 
            }
        });
    }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="mb-3">
        <label for="txtMedico" class="form-label fw-bold">Buscar médico:</label>
        <asp:TextBox ID="txtMedico" runat="server" CssClass="form-control" placeholder="Escriba el nombre del médico..." />
        <asp:HiddenField ID="hfIdMedico" runat="server" />
        <asp:Button ID="btnCargarEspecialidades" runat="server" Text="Cargar Especialidades" CssClass="btn btn-secondary mt-2" OnClick="btnCargarEspecialidades_Click" Style="display:none;" />
    </div>

    <div class="mb-3">
        <label for="ddlEspecialidades" class="form-label fw-bold">Seleccione la especialidad:</label>
        <asp:DropDownList ID="ddlEspecialidades" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidades_SelectedIndexChanged" />
    </div>

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
</asp:Content>
