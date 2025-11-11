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
                this.btn_Modificar.Enabled = false;
                this.btn_Eliminar.Enabled = false;

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

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario ROl agregado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {fex.Message}');", true);
            }

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error inesperado: {ex.Message}');", true);
            }
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsUsuarioRol xUsr = new clsUsuarioRol();

                xUsr.Usuario = new clsUsuario();
                xUsr.Rol = new clsRol();

                xUsr.Usuario.Id = int.Parse(Ddl_Usuario.SelectedValue);
                xUsr.Rol.Id = int.Parse(Ddl_Rol.SelectedValue);

                xdb.ModUsuarioRol(xUsr);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario Rol modificado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {fex.Message}');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error inesperado: {ex.Message}');", true);
            }
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsUsuarioRol xUsr = new clsUsuarioRol();
                
                xUsr.Usuario.Id = int.Parse(Ddl_Usuario.SelectedValue);
                xUsr.Rol.Id = int.Parse(Ddl_Rol.SelectedValue);

                xdb.DelUsuarioRol(xUsr.Usuario.Id,xUsr.Rol.Id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario Rol eliminado correctamente.');", true);
            }
            catch (System.ServiceModel.FaultException fex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {fex.Message}');", true);
            }

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error inesperado: {ex.Message}');", true);
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {

        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsUsuarioRol> lista = xdb.GetUsuarioRol().ToList();

                gvUsuarioRol.DataSource = lista;
                gvUsuarioRol.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al cargar la lista de Usuario Rol: {ex.Message}');", true);
            }
        }

        protected void gvUsuarioRol_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // El CommandArgument contendrá ambos Ids separados por un guion, por ejemplo: "3-2"
                string[] argumentos = e.CommandArgument.ToString().Split('-');
                int idUsuario = Convert.ToInt32(argumentos[0]);
                int idRol = Convert.ToInt32(argumentos[1]);

                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                if (e.CommandName == "Consultar")
                {
                    var lista = xdb.GetUsuarioRol().ToList();

                    // Buscar la relación exacta usuario-rol
                    var userRol = lista.FirstOrDefault(u => u.Usuario.Id == idUsuario && u.Rol.Id == idRol);

                    if (userRol != null)
                    {
                        // Control de botones
                        btn_Agregar.Enabled = false;
                        btn_Modificar.Enabled = true;
                        btn_Eliminar.Enabled = true;

                        // Cargar valores en los dropdown
                        Ddl_Usuario.SelectedValue = userRol.Usuario.Id.ToString();
                        Ddl_Rol.SelectedValue = userRol.Rol.Id.ToString();
                    }
                }
                else if (e.CommandName == "Eliminar")
                {
                    xdb.DelUsuarioRol(idUsuario, idRol); // Llama al método WCF
                    CargarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario-Rol eliminado correctamente.');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }

        protected void gvUsuarioRol_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuarioRol.PageIndex = e.NewPageIndex;
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        private void LimpiarCampos()
        {
            this.btn_Agregar.Enabled = true;
            this.btn_Modificar.Enabled = false;
            this.btn_Eliminar.Enabled = false;

            clsValidacion.LimpiarControles(this);
        }
    }
}