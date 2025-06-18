<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListadoPaciente.aspx.cs" Inherits="TPC_Clinica.ListadoPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-lista {
            max-width: 90%;
            margin: 60px auto;
            padding: 30px;
            border-radius: 15px;
            background-color: #ffffff;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            overflow-x: auto;
        }

            .card-lista h2 {
                font-size: 1.8rem;
                color: #0d6efd;
                font-weight: bold;
                text-align: center;
                margin-bottom: 25px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-lista">
        <h2>Lista de Pacientes</h2>
        <asp:GridView ID="dgvPacientes" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" >
            <Columns>
                <asp:BoundField DataField="DNI" HeaderText="DNI" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="FechaNac" HeaderText="Fecha de Nacimiento" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />

                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnModificar" runat="server"
                            ImageUrl="https://cdn4.iconfinder.com/data/icons/glyphs/24/icons_edit-256.png"
                            CommandName="Editar" CommandArgument='<%# Eval("DNI") %>'
                            ToolTip="Editar" Width="18px" />

                        <asp:ImageButton ID="btnEliminar" runat="server"
                            ImageUrl="https://cdn3.iconfinder.com/data/icons/font-awesome-solid/512/trash-can-256.png"
                            CommandName="Eliminar" CommandArgument='<%# Eval("DNI") %>'
                            ToolTip="Eliminar" Width="18px"
                            OnClientClick="return confirm('¿Seguro que querés eliminar este paciente?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>