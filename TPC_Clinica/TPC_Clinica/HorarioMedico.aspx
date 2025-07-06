<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="HorarioMedico.aspx.cs" Inherits="TPC_Clinica.HorarioMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .titulo {
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
            color: black;
        }

        .panel-turno {
            background-color: #f5f5f5;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }

        .tabla-turno {
            width: 100%;
            border-collapse: collapse;
        }

            .tabla-turno th,
            .tabla-turno td {
                padding: 10px;
                text-align: center;
                border-bottom: 1px solid #ddd;
            }

            .tabla-turno th {
                background-color: #2c3e50;
                color: white;
            }

        .boton-accion {
            margin-right: 10px;
        }

        .cabecera {
            background-color: #f1f1f1;
            padding: 10px 20px;
            color: #333;
            border-radius: 10px 10px 0 0;
            display: flex;
            align-items: center;
            justify-content: space-between;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cabecera">
        <span class="titulo">Turno de trabajo</span>
    </div>

    <div class="panel-turno">

        <div class="accordion" id="accordionExample">
            <div class="accordion-item">
                <h2 class="accordion-header">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                        Agregar Horario
                    </button>
                </h2>
                <div id="collapseOne" class="accordion-collapse collapse">
                    <div class="accordion-body">
                        <div class="row">
                            <div class="col">
                                <label>Dia</label>
                                <asp:DropDownList ID="ddlDia" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col">
                                <label>Horario ingreso</label>
                                <asp:DropDownList ID="ddlHoraInicio" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col">
                                <label>Horario egreso</label>
                                <asp:DropDownList ID="ddlHoraFin" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col">
                                <label>Especialidad</label>
                                <asp:DropDownList ID="ddlEspecialidad" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-1" style="margin-top: 1.5em;">
                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary btn-sm" OnClick="btnGuardar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- GridView de horarios -->
    <asp:GridView
        ID="gvHorarioMedico"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="tabla-turno"
        DataKeyNames="Id"
        OnRowCommand="gvHorarioMedico_RowCommand">

        <Columns>
            <asp:BoundField HeaderText="Id" DataField="Id" />
            <asp:TemplateField HeaderText="Día">
                <ItemTemplate>
                    <%# ObtenerNombreDia(Eval("DiaSemana")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Horario de Ingreso" DataField="HorarioInicio" />
            <asp:BoundField HeaderText="Horario de Egreso" DataField="HorarioFin" />
            <asp:TemplateField HeaderText="Especialidad">
                <ItemTemplate>
                    <%# Eval("Especialidad.Descripcion") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnModificar" runat="server"
                        ImageUrl="https://cdn4.iconfinder.com/data/icons/glyphs/24/icons_edit-256.png"
                        CommandName="Editar"
                        ToolTip="Editar"
                        CommandArgument='<%# Container.DataItemIndex %>'
                        Style="width: 17px; height: 17px; margin: 0 4px" />
                    <asp:ImageButton ID="btnEliminar" runat="server"
                        ImageUrl="https://cdn3.iconfinder.com/data/icons/font-awesome-solid/512/trash-can-256.png"
                        CommandName="Eliminar"
                        ToolTip="Eliminar"
                        CommandArgument='<%# Container.DataItemIndex %>'
                        Style="width: 17px; height: 17px; margin: 0 4px;"
                        OnClientClick="return confirm('¿Estás seguro que quieres eliminar este paciente?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
