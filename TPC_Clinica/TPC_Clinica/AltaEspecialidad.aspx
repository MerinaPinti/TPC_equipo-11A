<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AltaEspecialidad.aspx.cs" Inherits="TPC_Clinica.AMEspecialidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .especialidad-alta {
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
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <%if (Session["IdModificar"] == null)
        { %>
    <div class="especialidad-alta">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-3">
                        <label for="exampleFormControlInput1" class="form-label mt-3">Especialidad</label>
                        <asp:TextBox ID="txtEspecialidad" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:Button ID="btnAgregarEspecialiad" CssClass="btn btn-outline-primary btn-sm mt-2" runat="server" Text="Agregar" OnClick="btnAgregarEspecialiad_Click" />
                    </div>
                    <div class="col">
                        <table class="table table-striped">
                        </table>
                        <asp:GridView ID="dgvEspecialidades" OnRowDeleting="dgvEspecialidades_RowDeleting" runat="server" AutoGenerateColumns="false" CssClass="table">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Especialidad" DataField="descripcion" />

                                <%--<asp:CommandField ShowDeleteButton="true" />--%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <itemstyle horizontalalign="Right" />
                                        <asp:ImageButton ID="btnEliminar" runat="server"
                                            ImageUrl="https://cdn3.iconfinder.com/data/icons/font-awesome-solid/512/trash-can-256.png"
                                            CommandName="Delete"
                                            ToolTip="Eliminar"
                                            Style="width: 20px; height: 20px;" />
                                        <%--OnClientClick="return confirm('¿Estás seguro que querés eliminar este ítem?');"--%>
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
    <%}
        else
        { %>
    <div class="especialidad-modificar">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col">
                        <label for="exampleFormControlInput1" class="form-label mt-3">ID</label>
                        <asp:TextBox ID="txtBoxIdMod" CssClass="form-control" runat="server" disabled></asp:TextBox>
                        <label for="exampleFormControlInput1" class="form-label mt-3">Especialidad</label>
                        <asp:TextBox ID="txtBoxDescrMod" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:Button ID="btnModificar" CssClass="btn btn-outline-primary btn-sm mt-2" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%} %>
</asp:Content>
