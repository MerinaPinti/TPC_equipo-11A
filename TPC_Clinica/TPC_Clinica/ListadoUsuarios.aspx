<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListadoUsuarios.aspx.cs" Inherits="TPC_Clinica.ListadoUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .usuario-list {
            max-width: 70vw;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }

        .transparente {
            opacity: 0.3;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="usuario-list">
        <div class="row">
            <div class="col">
                <h2>Listado de Usuarios</h2>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:Button ID="btnAgregar" CssClass="btn btn-primary" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                <%--<asp:GridView ID="dgvEspecialidad" runat="server" AutoGenerateColumns="false" CssClass="table mt-2">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="descripcion" HeaderText="Categoria" />
                    </Columns>
                </asp:GridView>--%>

                <asp:GridView ID="dgvUsuarios" DataKeyNames="Id" OnRowCommand="dgvUsuarios_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="table mt-2">
                    <columns>
                        <asp:BoundField HeaderText="ID" DataField="Id" />
                        <asp:TemplateField HeaderText="Tipo de Usuario">
                            <itemtemplate>
                                <%# Eval("TipoUsuario.Descripcion") %>
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Usuario" DataField="UserName" />
                        <asp:BoundField HeaderText="Contraseña" DataField="Password" />

                        <asp:TemplateField>
                            <itemstyle horizontalalign="Right" />
                            <itemtemplate>
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
                                    OnClientClick="return confirm('¿Estás seguro que querés eliminar este usuario?');" />
                            </itemtemplate>
                        </asp:TemplateField>
                    </columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
