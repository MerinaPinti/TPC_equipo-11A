<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListadoTipoUsuario.aspx.cs" Inherits="TPC_Clinica.ListadoTipoUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tipo-usuario-list {
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
    <div class="tipo-usuario-list">
        <div class="row">
            <div class="col">
                <h2>Listado de Tipos de Usuario</h2>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:Button ID="btnAgregar" CssClass="btn btn-primary" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                
                <asp:GridView ID="dgvTiposUsuario" DataKeyNames="Id" OnRowCommand="dgvTiposUsuario_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="table">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="Id" />
                        <asp:BoundField HeaderText="Tipo de Usuario" DataField="Descripcion" />
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnModificar" runat="server"
                                    ImageUrl="https://cdn4.iconfinder.com/data/icons/glyphs/24/icons_edit-256.png"
                                    CommandName="Editar"
                                    ToolTip="Editar"
                                    CommandArgument='<%# Container.DataItemIndex %>'
                                    Style="width: 17px; height: 17px; margin:0 4px"/>
                                <asp:ImageButton ID="btnEliminar" runat="server"
                                    ImageUrl="https://cdn3.iconfinder.com/data/icons/font-awesome-solid/512/trash-can-256.png"
                                    CommandName="Eliminar"
                                    ToolTip="Eliminar"
                                    CommandArgument='<%# Container.DataItemIndex %>'
                                    Style="width: 17px; height: 17px; margin:0 4px;"
                                    OnClientClick="return confirm('¿Estás seguro que quieres eliminar este tipo de usuario?');"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>