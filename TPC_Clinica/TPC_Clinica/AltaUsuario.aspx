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
                        <asp:Label ID="lblIdMod" runat="server" Visible="false" CssClass="form-label mt-3" Text="ID"></asp:Label>
                        <asp:TextBox ID="txtBoxIdMod" Visible="false" CssClass="form-control" runat="server" disabled></asp:TextBox>

                        <label for="txtUsuario" class="form-label mt-3">Usuario</label>
                        <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server"></asp:TextBox>

                        <label for="txtPassword" class="form-label mt-3">Contraseña</label>
                        <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>

                        <label for="txtTipoUsuario" class="form-label mt-3">Tipo Usuario (ID)</label>
                        <asp:TextBox ID="txtTipoUsuario" CssClass="form-control" runat="server"></asp:TextBox>

                        <asp:Button ID="btnAgregarUsuario" CssClass="btn btn-outline-primary btn-sm mt-2" runat="server" Text="Agregar" OnClick="btnAgregarUsuario_Click" />
                    </div>

                    <div class="col">
                        <asp:GridView ID="dgvUsuarios" OnRowDeleting="dgvUsuarios_RowDeleting" runat="server" AutoGenerateColumns="false" CssClass="table">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Tipo Usuario (ID)" DataField="TipoUsuario" />
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
