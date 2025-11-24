<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Profesor.aspx.cs" Inherits="ProyectoNido.frm_Profesor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaProfesor" class="contenedor-form" style="width:60%; margin:auto;">
        <tr>
            <th colspan="2">MANTENIMIENTO DE PROFESORES</th>
        </tr>

        <tr>
            <td>Usuario:</td>
            <td>
                <asp:DropDownList ID="ddl_Usuario" runat="server" CssClass="full-width-dropdown"></asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Fecha Ingreso:</td>
            <td>
                <asp:TextBox ID="txt_FechaIngreso" runat="server" TextMode="Date" CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Título Profesional:</td>
            <td>
                <asp:TextBox ID="txt_TituloProfesional" runat="server" CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>CV:</td>
            <td>
                <asp:FileUpload ID="fu_Cv" runat="server" />
            </td>
        </tr>

        <tr>
            <td>Evaluación Psicológica:</td>
            <td>
                <asp:FileUpload ID="fu_EvaluacionPsicologica" runat="server" />
            </td>
        </tr>

        <tr>
            <td>Foto:</td>
            <td>
                <asp:FileUpload ID="fu_Fotos" runat="server" />
            </td>
        </tr>

        <tr>
            <td>Verificación Domiciliaria:</td>
            <td>
                <asp:FileUpload ID="fu_VerificacionDomiciliaria" runat="server" />
            </td>
        </tr>

        <tr>
            <td colspan="2" style="text-align:center; padding-top:15px;">
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR"
                    OnClientClick="return confirm('¿Deseas agregar este Profesor?');"
                    OnClick="btn_Agregar_Click" class="btn btn-exito" />
                &nbsp;
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR"
                    OnClientClick="return confirm('¿Deseas modificar este Profesor?');"
                    OnClick="btn_Modificar_Click" class="btn btn-primario" />
                &nbsp;
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR"
                    OnClick="btn_Limpiar_Click" class="btn btn-advertencia" />
                &nbsp;
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR"
                    OnClientClick="return confirm('¿Deseas eliminar este Profesor?');"
                    OnClick="btn_Eliminar_Click" class="btn btn-peligro" />
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 60%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox"
                placeholder="Buscar por usuario o título..." />
            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR"
                CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>

    <asp:GridView ID="gvProfesor" runat="server" AutoGenerateColumns="False"
        AllowPaging="True" PageSize="4"
        OnPageIndexChanging="gvProfesor_PageIndexChanging"
        OnRowCommand="gvProfesor_RowCommand"
        CssClass="gridview-style sticky-header">
        <Columns>
            <asp:BoundField DataField="Id_Profesor" HeaderText="ID" />
            <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
            <asp:BoundField DataField="TituloProfesional" HeaderText="Título" />
            <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha Ingreso" />
            <asp:ButtonField CommandName="Consultar" Text="Consultar"
                ButtonType="Button" ControlStyle-CssClass="btn btn-primario" />
            <asp:ButtonField CommandName="Eliminar" Text="Eliminar"
                ButtonType="Button" ControlStyle-CssClass="btn btn-peligro" />
        </Columns>
    </asp:GridView>
</asp:Content>