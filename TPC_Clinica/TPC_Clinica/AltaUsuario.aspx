<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AltaUsuario.aspx.cs" Inherits="TPC_Clinica.AltaUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .usuario-alta {
            max-width: 70vw;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }

        .usuario-modificar {
            max-width: 20vw;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>

    <div class="usuario-alta">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-3">
                        <div class="mb-3">
                            <asp:Label ID="lblIdMod" runat="server" Visible="false" CssClass="form-label mt-3" Text="ID"></asp:Label>
                            <asp:TextBox ID="txtBoxIdMod" Visible="false" CssClass="form-control mt-2" runat="server" disabled></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="txtUsuario" class="form-label mt-3">Usuario</label>
                            <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:Label ID="lblUsuarioError" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="lblContraseña" runat="server" CssClass="form-label" Text="Contraseña"></asp:Label>
                            <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:Label ID="lblContraseñaError" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="lblCambiarContraseña" runat="server" Text="Deseo cambiar la contraseña" CssClass="" Visible="false"></asp:Label>
                            <asp:CheckBox ID="cboxPassword" runat="server" AutoPostBack="true" OnCheckedChanged="cboxPassword_CheckedChanged" Visible="false" />
                        </div>
                        <label for="txtTipoUsuario" class="form-label">Tipo Usuario</label>
                        <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-select"></asp:DropDownList>

                        <div class="mt-3">
                            <asp:Button ID="btnAgregarUsuario" CssClass="btn btn-outline-primary btn-sm mt-2" runat="server" Text="Agregar" OnClick="btnAgregarUsuario_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-danger btn-sm mt-2" OnClick="btnCancelar_Click" />
                        </div>
                    </div>

                    <div class="col">
                        <asp:GridView ID="dgvUsuarios" runat="server" AutoGenerateColumns="false" CssClass="table">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tipo de Usuario">
                                    <ItemTemplate>
                                        <%# Eval("TipoUsuario.Descripcion") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Usuario" DataField="UserName" />
                                <asp:BoundField HeaderText="Contraseña" DataField="Password" />

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <itemstyle horizontalalign="Right" />
                                        <asp:ImageButton ID="btnEliminar" runat="server"
                                            ImageUrl="https://cdn3.iconfinder.com/data/icons/font-awesome-solid/512/trash-can-256.png"
                                            CommandName="Delete"
                                            ToolTip="Eliminar"
                                            Style="width: 20px; height: 20px;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                            <asp:Button ID="btnContinuar" CssClass="btn btn-primary mt-2" runat="server" Visible="false" Text="Continuar" OnClick="btnContinuar_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
