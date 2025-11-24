<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Apoderado.aspx.cs" Inherits="ProyectoNido.frm_Apoderado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaApoderado" class="tablaForm">
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="LabelTitulo" runat="server" Text="Apoderado"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelIdApoderado" runat="server" Text="Id Apoderado:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_IdApoderado" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelUsuario" runat="server" Text="Usuario:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddl_Usuario" runat="server" CssClass="full-width-textbox"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelDistrito" runat="server" Text="Distrito:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddl_Distrito" runat="server" CssClass="full-width-textbox"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelCopiaDni" runat="server" Text="Copia DNI (ruta / archivo):"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txt_CopiaDni" runat="server" CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR" 
                    OnClientClick="return confirm('¿Deseas agregar este Apoderado?') && validarCamposTabla('tablaApoderado','txt_CopiaDni');" 
                    OnClick="btn_Agregar_Click" class="btn btn-exito" />
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR" 
                    OnClientClick="return confirm('¿Deseas modificar este Apoderado?') && validarCamposTabla('tablaApoderado','txt_CopiaDni');" 
                    OnClick="btn_Modificar_Click" class="btn btn-primario" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR" OnClick="btn_Limpiar_Click" class="btn btn-advertencia" />
            </td>
            <td>
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR" 
                    OnClientClick="return confirm('¿Deseas eliminar este Apoderado?');" 
                    OnClick="btn_Eliminar_Click" class="btn btn-peligro" />
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 60%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox" placeholder="Buscar por usuario o distrito..." />
            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR" CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>

    <asp:GridView ID="gvApoderado" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="4"
        OnPageIndexChanging="gvApoderado_PageIndexChanging"
        OnRowCommand="gvApoderado_RowCommand"
        CssClass="gridview-style sticky-header">
        <Columns>
            <asp:BoundField DataField="Id_Apoderado" HeaderText="ID" />
            <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
            <asp:BoundField DataField="NombreDistrito" HeaderText="Distrito" />
            <asp:BoundField DataField="CopiaDni" HeaderText="Copia DNI" />
            <asp:ButtonField CommandName="Consultar" Text="Consultar" ButtonType="Button" ControlStyle-CssClass="btn btn-primario" />
            <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button" ControlStyle-CssClass="btn btn-peligro" />
        </Columns>
    </asp:GridView>
</asp:Content>
