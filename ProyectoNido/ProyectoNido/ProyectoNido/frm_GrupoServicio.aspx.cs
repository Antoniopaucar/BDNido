using ProyectoNido.wcfNido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_GrupoServicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                CargarGrid();
                this.btn_Modificar.Enabled = false;
                this.btn_Eliminar.Enabled = false;
            }
        }

        private void CargarCombos()
        {
            try
            {
                Service1Client xdb = new Service1Client();

                // Salones
                var listaSalon = xdb.GetSalon().ToList();
                ddl_Salon.DataSource = listaSalon;
                ddl_Salon.DataTextField = "Nombre";
                ddl_Salon.DataValueField = "Id";
                ddl_Salon.DataBind();
                ddl_Salon.Items.Insert(0, new ListItem("--Seleccione--", "0"));

                // Profesores
                var listaProfesor = xdb.GetProfesor().ToList();
                ddl_Profesor.DataSource = listaProfesor;
                ddl_Profesor.DataTextField = "NombreCompleto"; // si luego quieres nombre, puedes cambiar a otra propiedad
                ddl_Profesor.DataValueField = "Id_Profesor";
                ddl_Profesor.DataBind();
                ddl_Profesor.Items.Insert(0, new ListItem("--Seleccione--", "0"));

                // Servicios adicionales
                var listaServ = xdb.GetServicioAdicional().ToList();
                ddl_ServicioAdicional.DataSource = listaServ;
                ddl_ServicioAdicional.DataTextField = "Nombre";
                ddl_ServicioAdicional.DataValueField = "Id_ServicioAdicional";
                ddl_ServicioAdicional.DataBind();
                ddl_ServicioAdicional.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al cargar combos: {ex.Message}');", true);
            }
        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                Service1Client xdb = new Service1Client();
                List<clsGrupoServicio> lista = xdb.GetGrupoServicio().ToList();

                if (!string.IsNullOrEmpty(filtro))
                {
                    if (byte.TryParse(filtro, out byte periodo))
                    {
                        lista = lista.Where(g => g.Periodo == periodo).ToList();
                    }
                }

                lblMensaje.Text = lista.Count == 0
                    ? "No se encontraron resultados."
                    : "";

                gvGrupoServicio.DataSource = lista;
                gvGrupoServicio.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al cargar Grupo Servicio: {ex.Message}');", true);
            }
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Salon.SelectedValue == "0" ||
                    ddl_Profesor.SelectedValue == "0" ||
                    ddl_ServicioAdicional.SelectedValue == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Debe seleccionar Salón, Profesor y Servicio Adicional.');", true);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Periodo.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Debe ingresar el Periodo.');", true);
                    return;
                }

                Service1Client xdb = new Service1Client();

                clsGrupoServicio grupo = new clsGrupoServicio();

                grupo.Id_Salon = int.Parse(ddl_Salon.SelectedValue);
                grupo.Id_Profesor = int.Parse(ddl_Profesor.SelectedValue);
                grupo.Id_ServicioAdicional = int.Parse(ddl_ServicioAdicional.SelectedValue);
                grupo.Periodo = byte.Parse(txt_Periodo.Text.Trim());

                xdb.InsGrupoServicio(grupo);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Grupo de Servicio agregado correctamente.');", true);
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
                if (string.IsNullOrWhiteSpace(txt_IdGrupoServicio.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Debe seleccionar un registro para modificar.');", true);
                    return;
                }

                Service1Client xdb = new Service1Client();

                clsGrupoServicio grupo = new clsGrupoServicio();

                grupo.Id_GrupoServicio = int.Parse(txt_IdGrupoServicio.Text.Trim());
                grupo.Id_Salon = int.Parse(ddl_Salon.SelectedValue);
                grupo.Id_Profesor = int.Parse(ddl_Profesor.SelectedValue);
                grupo.Id_ServicioAdicional = int.Parse(ddl_ServicioAdicional.SelectedValue);
                grupo.Periodo = byte.Parse(txt_Periodo.Text.Trim());

                xdb.ModGrupoServicio(grupo);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Grupo de Servicio modificado correctamente.');", true);
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

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_IdGrupoServicio.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Debe seleccionar un registro para eliminar.');", true);
                    return;
                }

                Service1Client xdb = new Service1Client();

                int id = int.Parse(txt_IdGrupoServicio.Text.Trim());
                xdb.DelGrupoServicio(id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Grupo de Servicio eliminado correctamente.');", true);
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

        private void LimpiarCampos()
        {
            this.btn_Agregar.Enabled = true;
            this.btn_Modificar.Enabled = false;
            this.btn_Eliminar.Enabled = false;

            txt_IdGrupoServicio.Text = string.Empty;
            ddl_Salon.SelectedIndex = 0;
            ddl_Profesor.SelectedIndex = 0;
            ddl_ServicioAdicional.SelectedIndex = 0;
            txt_Periodo.Text = string.Empty;
            txtBuscar.Text = string.Empty;
            lblMensaje.Text = string.Empty;
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }

        protected void gvGrupoServicio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Service1Client xdb = new Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    var lista = xdb.GetGrupoServicio();
                    var grupo = lista.FirstOrDefault(g => g.Id_GrupoServicio == id);

                    if (grupo != null)
                    {
                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdGrupoServicio.Text = grupo.Id_GrupoServicio.ToString();

                        ddl_Salon.SelectedValue = grupo.Id_Salon.ToString();
                        ddl_Profesor.SelectedValue = grupo.Id_Profesor.ToString();
                        ddl_ServicioAdicional.SelectedValue = grupo.Id_ServicioAdicional.ToString();

                        txt_Periodo.Text = grupo.Periodo.ToString();
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
                    xdb.DelGrupoServicio(id);
                    CargarGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Grupo de Servicio eliminado correctamente.');", true);
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar: {ex.Message}');", true);
                }
            }
        }

        protected void gvGrupoServicio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGrupoServicio.PageIndex = e.NewPageIndex;
            string filtro = txtBuscar.Text.Trim();
            CargarGrid(filtro);
        }
    }
}