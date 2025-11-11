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
    public partial class frm_Distrito : System.Web.UI.Page
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

                clsDistrito xDis = new clsDistrito();

                xDis.Ubigeo = txt_Ubigeo.Text.Trim();
                xDis.Nombre = txt_Nombre.Text.Trim();

                xdb.InsDistrito(xDis);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Distrito agregado correctamente.');", true);
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

                clsDistrito xDis = new clsDistrito();

                xDis.Id = Convert.ToInt32(this.txt_IdDistrito.Text.Trim());
                xDis.Ubigeo = txt_Ubigeo.Text.Trim();
                xDis.Nombre = txt_Nombre.Text.Trim();

                xdb.ModDistrito(xDis);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Distrito modificado correctamente.');", true);
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

                clsDistrito xDis = new clsDistrito
                {
                    Id = Convert.ToInt32(this.txt_IdDistrito.Text.Trim())
                };

                xdb.DelDistrito(xDis.Id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Distrito eliminado correctamente.');", true);
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
                List<clsDistrito> lista = xdb.GetDistrito().ToList();

                gvDistritos.DataSource = lista;
                gvDistritos.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al cargar la lista de distritos: {ex.Message}');", true);
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            // Crear instancia del cliente WCF
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            // Obtener la lista desde el servicio
            var lista = xdb.GetDistrito().ToList();

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
            gvDistritos.DataSource = lista;
            gvDistritos.DataBind();
        }

        protected void gvDistritos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    // Obtener todos los usuarios desde el servicio
                    var lista = xdb.GetDistrito(); // 

                    // Buscar el usuario correspondiente al ID
                    var Dist = lista.FirstOrDefault(u => u.Id == id);

                    if (Dist != null)
                    {
                        // Control de botones

                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdDistrito.Text = Dist.Id.ToString();
                        txt_Ubigeo.Text = Dist.Ubigeo;
                        txt_Nombre.Text = Dist.Nombre;

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
                    xdb.DelDistrito(id); // usa tu método WCF
                    CargarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Distrito eliminado correctamente.');", true);
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar: {ex.Message}');", true);
                }
            }
        }

        protected void gvDistritos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDistritos.PageIndex = e.NewPageIndex;
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