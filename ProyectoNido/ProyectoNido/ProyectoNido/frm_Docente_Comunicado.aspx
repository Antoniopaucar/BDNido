<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true"
    CodeBehind="frm_Docente_Comunicado.aspx.cs" Inherits="ProyectoNido.frm_Docente_Comunicado" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
            window.addEventListener('load', function () {
                var menuButtons = document.querySelectorAll('.docente-menu a');
                menuButtons.forEach(function (btn) {
                    btn.addEventListener('click', function () {
                        menuButtons.forEach(function (b) { b.classList.remove('activo'); });
                        btn.classList.add('activo');
                    });
                });
            });
        </script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <div class="docente-left">
            <div class="docente-avatar"></div>
            <div class="docente-nombre">
                <asp:Label ID="lblNombreDocente" runat="server" Text="nombre y apellido"></asp:Label>
            </div>
            <div class="docente-menu">
                <a href="frm_Docente_Datos.aspx">Datos</a>
                <a href="frm_Docente_Comunicado.aspx" class="activo">Ver Comunicado</a>
                <a href="frm_Docente_GrupoAnual.aspx">Grupo Anual</a>
                <a href="frm_Docente_GrupoServicio.aspx">Grupo Servicio</a>
            </div>
        </div>
    </asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
        <link href="CSS/docente_comunicado.css" rel="stylesheet" />
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <div class="comunicado-container">
            <asp:Repeater ID="rptComunicados" runat="server">
                <ItemTemplate>
                    <div class="comunicado-item" onclick="toggleDetalle(this)" data-id='<%# Eval("Id") %>'>
                        <div class="comunicado-titulo">
                            <%# Eval("Nombre") %>
                        </div>
                        <div class="comunicado-remitente">
                            <%# Eval("Usuario.Nombres") %>
                                <%# Eval("Usuario.ApellidoPaterno") %>
                        </div>
                        <div class="comunicado-fecha">
                            <%# Eval("FechaCreacion", "{0:dd/MM/yyyy}" ) %>
                        </div>
                        <div class="comunicado-estado <%# (bool)Eval(" Visto") ? "estado-visto" : "estado-abrir" %>">
                            <%# (bool)Eval("Visto") ? "visto" : "abrir" %>
                        </div>
                    </div>
                    <div class="comunicado-detalle">
                        <p>
                            <%# Eval("Descripcion") %>
                        </p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <script type="text/javascript">
            function toggleDetalle(element) {
                var detalle = element.nextElementSibling;
                var estado = element.querySelector('.comunicado-estado');
                var id = element.getAttribute('data-id');

                if (detalle.style.display === "block") {
                    detalle.style.display = "none";
                    element.classList.remove("expanded");
                    // No revertir el estado a 'abrir', se queda en 'visto'
                } else {
                    detalle.style.display = "block";
                    element.classList.add("expanded");

                    // Cambiar a estado 'visto' si aún no lo está
                    if (estado.textContent.trim() === "abrir") {
                        estado.textContent = "visto";
                        estado.classList.remove("estado-abrir");
                        estado.classList.add("estado-visto");

                        // Llamada AJAX al WebMethod
                        if (typeof PageMethods !== 'undefined') {
                            PageMethods.MarcarComoVisto(id, function () {
                                console.log("Marcado como visto");
                            }, function (err) {
                                console.error("Error: " + err.get_message());
                            });
                        }
                    }
                }
            }
        </script>
    </asp:Content>