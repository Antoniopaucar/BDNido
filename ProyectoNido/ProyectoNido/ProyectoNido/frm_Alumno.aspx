<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Alumno.aspx.cs" Inherits="ProyectoNido.frm_Alumno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaAlumno" class="contenedor-form" style="width:60%; margin:auto;">
        <tr>
            <th colspan="2">MANTENIMIENTO DE ALUMNOS</th>
        </tr>

        <tr>
            <td>Apoderado:</td>
            <td>
                <asp:DropDownList ID="ddl_Apoderado" runat="server" CssClass="full-width-dropdown"></asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Nombres:</td>
            <td>
                <asp:TextBox ID="txt_Nombres" runat="server" CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Apellido Paterno:</td>
            <td>
                <asp:TextBox ID="txt_ApPaterno" runat="server" CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Apellido Materno:</td>
            <td>
                <asp:TextBox ID="txt_ApMaterno" runat="server" CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>DNI:</td>
            <td>
                <asp:TextBox ID="txt_Dni" runat="server" MaxLength="8" CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Fecha Nacimiento:</td>
            <td>
                <asp:TextBox ID="txt_FechaNacimiento" runat="server" TextMode="Date" CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Sexo:</td>
            <td>
                <asp:RadioButtonList ID="rbl_Sexo" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>

        <tr>
            <td>Activo:</td>
            <td>
                <asp:CheckBox ID="chk_Activo" runat="server" Checked="true" />
            </td>
        </tr>

        <tr>
            <td>Foto:</td>
            <td>
                <asp:FileUpload ID="fu_Fotos" runat="server" />
            </td>
        </tr>

        <tr>
            <td>Copia DNI:</td>
            <td>
                <asp:FileUpload ID="fu_CopiaDni" runat="server" />
            </td>
        </tr>

        <tr>
            <td>Permiso Publicidad:</td>
            <td>
                <asp:FileUpload ID="fu_PermisosPublicidad" runat="server" />
            </td>
        </tr>

        <tr>
            <td>Carnet Seguro:</td>
            <td>
                <asp:FileUpload ID="fu_CarnetSeguro" runat="server" />
            </td>
        </tr>

        <tr>
            <td colspan="2" style="text-align:center; padding-top:15px;">
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR"
                    OnClientClick="return confirm('¿Deseas agregar este Alumno?');"
                    OnClick="btn_Agregar_Click" class="btn btn-exito" />
                &nbsp;
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR"
                    OnClientClick="return confirm('¿Deseas modificar este Alumno?');"
                    OnClick="btn_Modificar_Click" class="btn btn-primario" />
                &nbsp;
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR"
                    OnClick="btn_Limpiar_Click" class="btn btn-advertencia" />
                &nbsp;
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR"
                    OnClientClick="return confirm('¿Deseas eliminar este Alumno?');"
                    OnClick="btn_Eliminar_Click" class="btn btn-peligro" />
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 60%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox"
                placeholder="Buscar por nombre, DNI o apoderado..." />
            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR"
                CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>

    <asp:GridView ID="gvAlumno" runat="server" AutoGenerateColumns="False"
        AllowPaging="True" PageSize="4"
        OnPageIndexChanging="gvAlumno_PageIndexChanging"
        OnRowCommand="gvAlumno_RowCommand"
        CssClass="gridview-style sticky-header">
        <Columns>
            <asp:BoundField DataField="Id_Alumno" HeaderText="ID" />
            <asp:BoundField DataField="NombreCompleto" HeaderText="Alumno" />
            <asp:BoundField DataField="Dni" HeaderText="DNI" />
            <asp:BoundField DataField="NombreApoderado" HeaderText="Apoderado" />
            <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
            <asp:BoundField DataField="Activo" HeaderText="Activo" />
            <asp:ButtonField CommandName="Consultar" Text="Consultar"
                ButtonType="Button" ControlStyle-CssClass="btn btn-primario" />
            
        </Columns>
    </asp:GridView>
</asp:Content>