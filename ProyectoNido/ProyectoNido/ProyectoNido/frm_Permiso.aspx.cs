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
    public partial class frm_Permiso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();
                this.btn_Modificar.Enabled = false;
                this.btn_Eliminar.Enabled = false;
            }
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsPermiso xPermiso = new clsPermiso();

                xPermiso.NombrePermiso = txt_Nombre.Text.Trim();
                xPermiso.Descripcion = txt_Descripcion.Text.Trim();

                xdb.InsPermiso(xPermiso);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Permiso agregado correctamente.');", true);
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

                clsPermiso xPermiso = new clsPermiso();

                xPermiso.Id = Convert.ToInt32(this.txt_IdPermiso.Text.Trim());
                xPermiso.NombrePermiso = txt_Nombre.Text.Trim();
                xPermiso.Descripcion = txt_Descripcion.Text.Trim();

                xdb.ModPermiso(xPermiso);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Permiso modificado correctamente.');", true);
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

                clsPermiso xPer = new clsPermiso
                {
                    Id = Convert.ToInt32(this.txt_IdPermiso.Text.Trim())
                };

                xdb.DelPermiso(xPer.Id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Permiso eliminado correctamente.');", true);
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
            string filtro = txtBuscar.Text.Trim().ToLower();

            // Crear instancia del cliente WCF
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            // Obtener la lista desde el servicio
            var lista = xdb.GetPermiso().ToList();

            // Aplicar filtro si existe texto
            if (!string.IsNullOrEmpty(filtro))
            {
                lista = lista
                    .Where(x => x.NombrePermiso != null && x.NombrePermiso.ToLower().Contains(filtro))
                    .ToList();
            }

            // Mostrar mensaje si no hay resultados
            lblMensaje.Text = lista.Count == 0
                ? "No se encontraron resultados para el filtro ingresado."
                : "";

            // Enlazar la lista (filtrada o completa)
            gvPermiso.DataSource = lista;
            gvPermiso.DataBind();
        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsPermiso> lista = xdb.GetPermiso().ToList();

                gvPermiso.DataSource = lista;
                gvPermiso.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al cargar la lista de permisos: {ex.Message}');", true);
            }
        }

        protected void gvPermiso_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    // Obtener todos los usuarios desde el servicio
                    var lista = xdb.GetPermiso(); // 

                    // Buscar el usuario correspondiente al ID
                    var Permiso = lista.FirstOrDefault(u => u.Id == id);

                    if (Permiso != null)
                    {
                        // Control de botones

                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdPermiso.Text =Permiso.Id.ToString();
                        txt_Nombre.Text = Permiso.NombrePermiso;
                        txt_Descripcion.Text = Permiso.Descripcion;

                    }

                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al consultar: {ex.Message}');", true);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    xdb.DelPermiso(id); // usa tu método WCF
                    CargarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Permiso eliminado correctamente.');", true);
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar: {ex.Message}');", true);
                }
            }
        }

        protected void gvPermiso_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPermiso.PageIndex = e.NewPageIndex;
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