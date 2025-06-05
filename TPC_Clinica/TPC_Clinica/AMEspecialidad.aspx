<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AMEspecialidad.aspx.cs" Inherits="TPC_Clinica.AMEspecialidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <div class="card-turno">
        <asp:UpdatePanel runat="server">
            <contenttemplate>
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
                            <columns>
                                <asp:TemplateField HeaderText="#">
                                    <itemtemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </itemtemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Especialidad" DataField="nombre" />

                                <%--<asp:CommandField ShowDeleteButton="true" />--%>
                                <asp:TemplateField>
                                    <itemtemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server"
                                            ImageUrl="https://cdn3.iconfinder.com/data/icons/font-awesome-solid/512/trash-can-256.png"
                                            CommandName="Delete"
                                            ToolTip="Eliminar"
                                            Style="width: 20px; height: 20px;" />
                                        <%--OnClientClick="return confirm('¿Estás seguro que querés eliminar este ítem?');"--%>
                                    </itemtemplate>
                                </asp:TemplateField>
                            </columns>
                        </asp:GridView>
                        <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                            <asp:Button ID="btnContinuar" CssClass="btn btn-primary mt-2" runat="server" Visible="false" Text="Continuar" OnClick="btnContinuar_Click" />
                        </div>
                    </div>
                </div>
            </contenttemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
