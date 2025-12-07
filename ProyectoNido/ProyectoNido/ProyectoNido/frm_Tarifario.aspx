<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Tarifario.aspx.cs" Inherits="ProyectoNido.frm_Tarifario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<table id="tablaTarifario" class="tablaForm">
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="Label2" runat="server" Text="Tarifario"></asp:Label>
            </td>
        </tr>

        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="Id:"></asp:Label></td>
            <td><asp:TextBox ID="txt_IdTarifario" runat="server" Enabled="false"></asp:TextBox></td>
            <td>&nbsp;</td><td>&nbsp;</td>
        </tr>

        <tr>
            <td><asp:Label ID="Label4" runat="server" Text="Tipo:"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddl_Tipo" runat="server">
                    <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                    <asp:ListItem Text="Matrícula" Value="M"></asp:ListItem>
                    <asp:ListItem Text="Pensión" Value="P"></asp:ListItem>
                    <asp:ListItem Text="Servicio" Value="S"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td><td>&nbsp;</td>
        </tr>

        <tr>
            <td><asp:Label ID="Label5" runat="server" Text="Nombre:"></asp:Label></td>
            <td colspan="3">
                <asp:TextBox ID="txt_Nombre" runat="server" CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td><asp:Label ID="Label6" runat="server" Text="Descripción:"></asp:Label></td>
            <td colspan="3">
                <asp:TextBox ID="txt_Descripcion" runat="server" CssClass="full-width-textbox"
                    TextMode="MultiLine" Rows="3"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td><asp:Label ID="Label7" runat="server" Text="Periodo (mes):"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddl_Periodo" runat="server">
                    <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td><td>&nbsp;</td>
        </tr>

        <tr>
            <td><asp:Label ID="Label8" runat="server" Text="Valor (S/):"></asp:Label></td>
            <td>
                <asp:TextBox ID="txt_Valor" runat="server" CssClass="full-width-textbox"
                    onkeypress="return SoloNumeros(event);"></asp:TextBox>
            </td>
            <td>&nbsp;</td><td>&nbsp;</td>
        </tr>

        <tr>
            <td>
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR" 
                    OnClientClick="return confirm('¿Deseas agregar este Tarifario?') && validarCamposTabla('tablaTarifario','txt_Nombre,txt_Valor');" 
                    OnClick="btn_Agregar_Click" class="btn btn-exito" />
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR" 
                    OnClientClick="return confirm('¿Deseas modificar este Tarifario?') && validarCamposTabla('tablaTarifario','txt_Nombre,txt_Valor');" 
                    OnClick="btn_Modificar_Click" class="btn btn-primario" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR" 
                    OnClick="btn_Limpiar_Click" class="btn btn-advertencia" />
            </td>
            <td>
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR" 
                    OnClientClick="return confirm('¿Deseas eliminar este Tarifario?');"
                    OnClick="btn_Eliminar_Click" class="btn btn-peligro" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<div style="width: 40%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox" 
                placeholder="Buscar por nombre..." />
            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR" CssClass="btn btn-info" 
                OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>

    <asp:GridView ID="gvTarifario" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="6"
        OnPageIndexChanging="gvTarifario_PageIndexChanging"
        OnRowCommand="gvTarifario_RowCommand"
        CssClass="gridview-style sticky-header">
        <Columns>
            <asp:BoundField DataField="Id_Tarifario" HeaderText="ID" />
            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
            <asp:BoundField DataField="Valor" HeaderText="Valor (S/)" DataFormatString="{0:N2}" />
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CommandName="Consultar"
                        CommandArgument='<%# Eval("Id_Tarifario") %>' CssClass="btn btn-info btn-sm" />
                    <asp:Button ID="btnEliminarRow" runat="server" Text="Eliminar" CommandName="Eliminar"
                        CommandArgument='<%# Eval("Id_Tarifario") %>' CssClass="btn btn-peligro btn-sm"
                        OnClientClick="return confirm('¿Deseas eliminar este Tarifario?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
