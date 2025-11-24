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
    public partial class frm_Comunicado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();
                this.btn_Modificar.Enabled = false;
                this.btn_Eliminar.Enabled = false;

                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                List<clsUsuario> lista = xdb.GetUsuario().ToList();

                Ddl_Usuario.DataSource = lista;
                Ddl_Usuario.DataTextField = "NombreUsuario";
                Ddl_Usuario.DataValueField = "Id";
                Ddl_Usuario.DataBind();
                Ddl_Usuario.Items.Insert(0, new ListItem("-- Seleccione un Usuario --", ""));
            }
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsComunicado xCom = new clsComunicado();

                xCom.Usuario = new clsUsuario();

                xCom.Usuario.Id = int.Parse(Ddl_Usuario.SelectedValue);
                xCom.Nombre = txt_Nombre.Text.Trim();
                xCom.Descripcion = txt_Descripcion.Text.Trim();
                xCom.FechaFinal = DateTime.TryParse(txt_Fecha_Final.Text.Trim(), out DateTime f) ? f : (DateTime?)null;

                xdb.InsComunicado(xCom);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Comunicado agregado correctamente.');", true);
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

                clsComunicado xCom = new clsComunicado();
                xCom.Usuario = new clsUsuario();

                xCom.Id = Convert.ToInt32(this.txt_IdComunicado.Text.Trim());
                xCom.Usuario.Id = int.Parse(Ddl_Usuario.SelectedValue);
                xCom.Nombre = txt_Nombre.Text.Trim();
                xCom.Descripcion = txt_Descripcion.Text.Trim();
                xCom.FechaFinal = DateTime.TryParse(txt_Fecha_Final.Text.Trim(), out DateTime f) ? f : (DateTime?)null;

                xdb.ModComunicado(xCom);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Comunicado modificado correctamente.');", true);
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

                clsComunicado xCom = new clsComunicado
                {
                    Id = Convert.ToInt32(this.txt_IdComunicado.Text.Trim())
                };

                xdb.DelComunicado(xCom.Id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Comunicado eliminado correctamente.');", true);
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

        private void CargarGrid(string filtro = "")
        {
            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                List<clsComunicado> lista = xdb.GetComunicado(idUsuario).ToList();

                gvComunicados.DataSource = lista;
                gvComunicados.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al cargar la lista de comunicados: {ex.Message}');", true);
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            // Crear instancia del cliente WCF
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            // Obtener la lista desde el servicio
            int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
            var lista = xdb.GetComunicado(idUsuario).ToList();

            // Aplicar filtro si existe texto
            if (!string.IsNullOrEmpty(filtro))
            {
                lista = lista
                    .Where(x => x.Nombre != null && x.Nombre.ToLower().Contains(filtro))
                    .ToList();
            }

            // Mostrar mensaje si no hay resultados
            lblMensaje.Text = lista.Count == 0
                ? "No se encontraron resultados para el filtro ingresado."
                : "";

            // Enlazar la lista (filtrada o completa)
            gvComunicados.DataSource = lista;
            gvComunicados.DataBind();
        }

        protected void gvComunicados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    // Obtener todos los usuarios desde el servicio
                    int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                    var lista = xdb.GetComunicado(idUsuario); // 

                    // Buscar el usuario correspondiente al ID
                    var Comu = lista.FirstOrDefault(u => u.Id == id);

                    if (Comu != null)
                    {
                        // Control de botones

                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdComunicado.Text = Comu.Id.ToString();
                        Ddl_Usuario.SelectedValue = Comu.Usuario.Id.ToString();
                        txt_Nombre.Text = Comu.Nombre;
                        txt_Descripcion.Text = Comu.Descripcion;
                        txt_Fecha_Creacion.Text = Comu.FechaCreacion.HasValue ? Comu.FechaCreacion.Value.ToString("yyyy-MM-dd") : string.Empty;
                        txt_Fecha_Final.Text = Comu.FechaFinal.HasValue ? Comu.FechaFinal.Value.ToString("yyyy-MM-dd") : string.Empty;

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
                    xdb.DelComunicado(id); // usa tu método WCF
                    CargarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Comunicado eliminado correctamente.');", true);
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar: {ex.Message}');", true);
                }
            }
        }

        protected void gvComunicados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvComunicados.PageIndex = e.NewPageIndex;
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