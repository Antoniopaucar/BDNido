using ProyectoNido.Auxiliar;
using ProyectoNido.wcfNido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_Usuario_Rol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();

                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsUsuario> listaUser = xdb.GetUsuario().ToList();

                Ddl_Usuario.DataSource = listaUser;
                Ddl_Usuario.DataTextField = "NombreUsuario";
                Ddl_Usuario.DataValueField = "Id";
                Ddl_Usuario.DataBind();
                Ddl_Usuario.Items.Insert(0, new ListItem("-- Seleccione un Usuario --", ""));

                List<clsRol> listaRol = xdb.GetRol().ToList();

                Ddl_Rol.DataSource = listaRol;
                Ddl_Rol.DataTextField = "NombreRol";
                Ddl_Rol.DataValueField = "Id";
                Ddl_Rol.DataBind();
                Ddl_Rol.Items.Insert(0, new ListItem("-- Seleccione un Rol --", ""));
            }
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsUsuarioRol xUsr = new clsUsuarioRol();

                xUsr.Usuario = new clsUsuario();
                xUsr.Rol = new clsRol();

                xUsr.Usuario.Id = int.Parse(Ddl_Usuario.SelectedValue);
                xUsr.Rol.Id = int.Parse(Ddl_Rol.SelectedValue);

                xdb.InsUsuarioRol(xUsr);

                switch (xUsr.Rol.Id)
                {
                    case 2:
                        //clsProfesor xPro = new clsProfesor();
                        //xPro.Id = xUsr.Usuario.Id;
                        //xdb.InsProfesor(xPro);
                        break;

                     case 3:
                        clsApoderado xApo = new clsApoderado();
                        xApo.Id = xUsr.Usuario.Id;
                        xdb.InsApoderado(xApo);
                        break;

                    default:
                        // opcional: acción por defecto
                        break;
                }


                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario ROl agregado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                string mensaje = fex.Message
                .Replace("'", "\\'")
                .Replace(Environment.NewLine, " ");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "alertError",
                    $"alert('Error: {mensaje}');",
                    true
                );
            }
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            wcfNido.Service1Client xdb = new wcfNido.Service1Client();
            var lista = xdb.GetUsuarioRol().ToList();

            if (!string.IsNullOrEmpty(filtro))
            {
                lista = lista
                    .Where(x =>
                        (x.Usuario.NombreUsuario ?? "").ToLower().Contains(filtro) ||
                        (x.Rol.NombreRol ?? "").ToLower().Contains(filtro)
                    )
                    .ToList();
            }

            lblMensaje.Text = lista.Count == 0
                ? "No se encontraron resultados para el filtro ingresado."
                : "";

            gvUsuarioRol.DataSource = lista;
            gvUsuarioRol.DataBind();
        }

        protected void gvUsuarioRol_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    string[] argumentos = e.CommandArgument.ToString().Split('-');
                    int idUsuario = Convert.ToInt32(argumentos[0]);
                    int idRol = Convert.ToInt32(argumentos[1]);

                    wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                    xdb.DelUsuarioRol(idUsuario, idRol);

                    CargarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Usuario-Rol eliminado correctamente.');", true);
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                string mensaje = fex.Message
                .Replace("'", "\\'")
                .Replace(Environment.NewLine, " ");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "alertError",
                    $"alert('Error: {mensaje}');",
                    true
                );
            }
        }

        protected void gvUsuarioRol_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuarioRol.PageIndex = e.NewPageIndex;
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsUsuarioRol> lista = xdb.GetUsuarioRol().ToList();

                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro = filtro.ToLower();

                    lista = lista
                        .Where(x =>
                            (x.Usuario.NombreUsuario ?? "").ToLower().Contains(filtro) ||
                            (x.Rol.NombreRol ?? "").ToLower().Contains(filtro)
                        )
                        .ToList();
                }

                gvUsuarioRol.DataSource = lista;
                gvUsuarioRol.DataBind();
            }
            catch (System.ServiceModel.FaultException fex)
            {
                string mensaje = fex.Message
                .Replace("'", "\\'")
                .Replace(Environment.NewLine, " ");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "alertError",
                    $"alert('Error: {mensaje}');",
                    true
                );
            }
        }

        private void LimpiarCampos()
        {
            this.btn_Agregar.Enabled = true;

            clsValidacion.LimpiarControles(this);
        }
    }
}