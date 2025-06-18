<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AltaTipoUsuario.aspx.cs" Inherits="TPC_Clinica.AltaTipoUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tipo-usuario-container {
            max-width: 70vw;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }

        .especialidad-modificar { 
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="tipo-usuario-container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-3">
                        <div class="row">
                            <div class="col">
                                <asp:Label ID="lblIdModTipo" runat="server" Visible="false" CssClass="form-label mt-3" Text="ID"></asp:Label>
                                <asp:TextBox ID="txtBoxIdModTipo" Visible="false" CssClass="form-control" runat="server" ></asp:TextBox>
                                
                                <label for="txtTipoUsuario" class="form-label mt-3">Tipo de Usuario</label>
                                <asp:TextBox ID="txtTipoUsuario" CssClass="form-control" runat="server"></asp:TextBox>
                                
                                <asp:Label ID="lblValidacionTipo" runat="server" Text=" " CssClass="form-label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:Button ID="btnAgregarTipoUsuario" CssClass="btn btn-outline-primary btn-sm mt-2" runat="server" Text="Agregar" OnClick="btnAgregarTipoUsuario_Click" />
                            </div>
                        </div>
                    </div>

                    <div class="col">
                        <table class="table table-striped"></table>
                        
                        <asp:GridView ID="dgvTiposUsuario" OnRowDeleting="dgvTiposUsuario_RowDeleting" runat="server" AutoGenerateColumns="false" CssClass="table">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Tipo de Usuario" DataField="Descripcion" />

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <itemstyle horizontalalign="Right" />
                                        <asp:ImageButton ID="btnEliminar" runat="server"
                                            ImageUrl="https://cdn3.iconfinder.com/data/icons/font-awesome-solid/512/trash-can-256.png"
                                            CommandName="Delete"
                                            ToolTip="Eliminar"
                                            OnClientClick="return confirm('¿Estás seguro que quieres eliminar este Tipo de Usuario de la lista?');"
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