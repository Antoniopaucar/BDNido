<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_GrupoServicio.aspx.cs" Inherits="ProyectoNido.frm_GrupoServicio" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaGrupoServicio" class="tablaForm">
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="LabelTitulo" runat="server" Text="Grupo Servicio"></asp:Label>
            </td>
        </tr>

        <!-- ID GRUPO SERVICIO -->
        <tr>
            <td><asp:Label ID="LabelIdGrupo" runat="server" Text="Id Grupo Servicio:"></asp:Label></td>
            <td><asp:TextBox ID="txt_IdGrupoServicio" runat="server" Enabled="false"></asp:TextBox></td>
            <td>&nbsp;</td><td>&nbsp;</td>
        </tr>

        <!-- SALÓN -->
        <tr>
            <td><asp:Label ID="LabelSalon" runat="server" Text="Salón:"></asp:Label></td>
            <td colspan="3">
                <asp:DropDownList ID="ddl_Salon" runat="server" CssClass="full-width-textbox">
                </asp:DropDownList>
            </td>
        </tr>

        <!-- PROFESOR -->
        <tr>
            <td><asp:Label ID="LabelProfesor" runat="server" Text="Profesor:"></asp:Label></td>
            <td colspan="3">
                <asp:DropDownList ID="ddl_Profesor" runat="server" CssClass="full-width-textbox">
                </asp:DropDownList>
            </td>
        </tr>

        <!-- SERVICIO ADICIONAL -->
        <tr>
            <td><asp:Label ID="LabelServicio" runat="server" Text="Servicio Adicional:"></asp:Label></td>
            <td colspan="3">
                <asp:DropDownList ID="ddl_ServicioAdicional" runat="server" CssClass="full-width-textbox">
                </asp:DropDownList>
            </td>
        </tr>

        <!-- PERIODO -->
        <tr>
            <td><asp:Label ID="LabelPeriodo" runat="server" Text="Periodo:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txt_Periodo" runat="server" MaxLength="3"></asp:TextBox>
            </td>
            <td colspan="2">
                <asp:Label ID="lblInfoPeriodo" runat="server" Text="(Ej: 1, 2... según el catálogo)" Font-Size="Smaller"></asp:Label>
            </td>
        </tr>

        <!-- BOTONES -->
        <tr>
            <td>
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR"
                    OnClientClick="return confirm('¿Deseas agregar este Grupo de Servicio?');"
                    OnClick="btn_Agregar_Click" CssClass="btn btn-exito" />
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR"
                    OnClientClick="return confirm('¿Deseas modificar este Grupo de Servicio?');"
                    OnClick="btn_Modificar_Click" CssClass="btn btn-primario" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR"
                    OnClick="btn_Limpiar_Click" CssClass="btn btn-advertencia" />
            </td>
            <td>
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR"
                    OnClientClick="return confirm('¿Deseas eliminar este Grupo de Servicio?');"
                    OnClick="btn_Eliminar_Click" CssClass="btn btn-peligro" />
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">

    <div style="width: 40%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox" placeholder="Buscar por periodo..." />
            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR" CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>

    <asp:GridView ID="gvGrupoServicio" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
        OnPageIndexChanging="gvGrupoServicio_PageIndexChanging"
        OnRowCommand="gvGrupoServicio_RowCommand"
        CssClass="gridview-style sticky-header">

        <Columns>
            <asp:BoundField DataField="Id_GrupoServicio" HeaderText="ID" />
            <asp:BoundField DataField="Id_Salon" HeaderText="Id Salón" />
            <asp:BoundField DataField="Id_Profesor" HeaderText="Id Profesor" />
            <asp:BoundField DataField="Id_ServicioAdicional" HeaderText="Id Servicio" />
            <asp:BoundField DataField="Periodo" HeaderText="Periodo" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CommandName="Consultar"
                        CommandArgument='<%# Eval("Id_GrupoServicio") %>' CssClass="btn btn-info btn-sm" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar"
                        CommandArgument='<%# Eval("Id_GrupoServicio") %>' CssClass="btn btn-peligro btn-sm"
                        OnClientClick="return confirm('¿Deseas eliminar este Grupo de Servicio?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
