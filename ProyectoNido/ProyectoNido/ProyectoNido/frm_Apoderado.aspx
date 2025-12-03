<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Apoderado.aspx.cs" Inherits="ProyectoNido.frm_Apoderado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaApoderado" class="tablaForm" >
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="Label2" runat="server" Text="Apoderado"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Id:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_IdApoderado" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Copia Dni:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:FileUpload ID="fup_Copia_Dni" runat="server" CssClass="file-upload-custom" />
            </td>
        </tr> 
        <tr>
            <td>

            </td>
            <td>

            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR" OnClick="btn_Limpiar_Click" class="btn btn-advertencia" />
            </td>
            <td>

            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
