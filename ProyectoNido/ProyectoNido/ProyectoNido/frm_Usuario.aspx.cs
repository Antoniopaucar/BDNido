using ProyectoNido.Auxiliar;
using ProyectoNido.wcfNido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();
                this.btn_Modificar.Enabled = false;
                this.btn_Eliminar.Enabled = false;

                // Cargar sexos (sin clase ni BD)
                Ddl_Sexo.Items.Clear();
                Ddl_Sexo.Items.Add(new ListItem("-- Seleccione un Sexo --", ""));
                Ddl_Sexo.Items.Add(new ListItem("Masculino", "M"));
                Ddl_Sexo.Items.Add(new ListItem("Femenino", "F"));
            }
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            string usuario = txt_NombreUsuario.Text.Trim();
            string clave = txt_Clave.Text.Trim();
            string clave2 = txt_Clave2.Text.Trim();

            if (clave != clave2)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Las contraseñas no coinciden.');", true);
                return;
            }

            // Validación con tu clase clsValidacion
            string errorUsuario = clsValidacion.ValidarUser(usuario);
            string errorClave = clsValidacion.ValidarContrasenia(clave);

            if (errorUsuario != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{errorUsuario}');", true);
                return;
            }

            if (errorClave != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{errorClave}');", true);
                return;
            }

            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                clsUsuario xusuario = new clsUsuario
                {
                    NombreUsuario = usuario,
                    Clave = clave,
                    Nombres = txt_Nombres.Text.Trim(),
                    ApellidoPaterno = txt_ApellidoPaterno.Text.Trim(),
                    ApellidoMaterno = txt_ApellidoMaterno.Text.Trim(),
                    Dni = txt_Dni.Text.Trim(),
                    FechaNacimiento = DateTime.TryParse(txt_Fecha_Nacimiento.Text.Trim(), out DateTime f) ? f : (DateTime?)null,
                    Sexo = Ddl_Sexo.SelectedValue,
                    Direccion = txt_Direccion.Text.Trim(),
                    Telefono = txt_Telefono.Text.Trim(),
                    Email = txt_Email.Text.Trim()
                };
                

                xdb.InsUsuarios(xusuario);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario agregado correctamente.');", true);
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
            string usuario = txt_NombreUsuario.Text.Trim();
            string clave = txt_Clave.Text.Trim();
            string clave2 = txt_Clave2.Text.Trim();

            if (clave != clave2)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Las contraseñas no coinciden.');", true);
                return;
            }

            // Validación con tu clase clsValidacion
            string errorUsuario = clsValidacion.ValidarUser(usuario);

            if (errorUsuario != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{errorUsuario}');", true);
                return;
            }

            if (!string.IsNullOrEmpty(clave))
            {
                string errorClave = clsValidacion.ValidarContrasenia(clave);

                if (errorClave != null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{errorClave}');", true);
                    return;
                }
            }

            try
            {
                wcfNido.Service1Client xdb = new wcfNido.Service1Client();

                //bool exito;
                //string mensaje = xdb.ValClaveSegura(usuario, clave, out exito);

                //if (!exito)
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{mensaje}');", true);
                //    return;
                //}

                clsUsuario xusuario = new clsUsuario();

                xusuario.Id = Convert.ToInt32(this.txt_IdUsuario.Text.Trim());
                xusuario.NombreUsuario = usuario;

                if (!string.IsNullOrEmpty(clave))
                {
                    xusuario.Clave = clave;
                }
                
                xusuario.Nombres = txt_Nombres.Text.Trim();
                xusuario.ApellidoPaterno = txt_ApellidoPaterno.Text.Trim();
                xusuario.ApellidoMaterno = txt_ApellidoMaterno.Text.Trim();
                xusuario.Dni = txt_Dni.Text.Trim();
                xusuario.FechaNacimiento = DateTime.TryParse(txt_Fecha_Nacimiento.Text.Trim(), out DateTime f) ? f : (DateTime?)null;
                xusuario.Sexo = Ddl_Sexo.SelectedValue;
                xusuario.Direccion = txt_Direccion.Text.Trim();
                xusuario.Telefono = txt_Telefono.Text.Trim();
                xusuario.Email = txt_Email.Text.Trim();
                xusuario.Activo = this.chb_Activo.Checked;
                

                xdb.ModUsuario(xusuario);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario modificado correctamente.');", true);
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

                clsUsuario xusuario = new clsUsuario
                {
                    Id = Convert.ToInt32(this.txt_IdUsuario.Text.Trim())
                };

                xdb.DelUsuarios(xusuario.Id);

                LimpiarCampos();
                CargarGrid();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario eliminado correctamente.');", true);
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
                List<clsUsuario> lista = xdb.GetUsuario().ToList();

                gvUsuarios.DataSource = lista;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al cargar la lista de usuarios: {ex.Message}');", true);
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            // Crear instancia del cliente WCF
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            // Obtener la lista desde el servicio
            var lista = xdb.GetUsuario().ToList();

            // Aplicar filtro si existe texto
            if (!string.IsNullOrEmpty(filtro))
            {
                lista = lista
                    .Where(x => x.NombreUsuario != null && x.NombreUsuario.ToLower().Contains(filtro))
                    .ToList();
            }

            // Mostrar mensaje si no hay resultados
            lblMensaje.Text = lista.Count == 0
                ? "No se encontraron resultados para el filtro ingresado."
                : "";

            // Enlazar la lista (filtrada o completa)
            gvUsuarios.DataSource = lista;
            gvUsuarios.DataBind();
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            wcfNido.Service1Client xdb = new wcfNido.Service1Client();

            if (e.CommandName == "Consultar")
            {
                try
                {
                    // Obtener todos los usuarios desde el servicio
                    var lista = xdb.GetUsuario(); // o GetUsuarios() si ese es el nombre correcto

                    // Buscar el usuario correspondiente al ID
                    var user = lista.FirstOrDefault(u => u.Id == id);

                    if (user != null)
                    {
                        // Control de botones

                        this.btn_Agregar.Enabled = false;
                        this.btn_Modificar.Enabled = true;
                        this.btn_Eliminar.Enabled = true;

                        txt_IdUsuario.Text = user.Id.ToString();
                        txt_NombreUsuario.Text = user.NombreUsuario;
                        txt_Nombres.Text = user.Nombres;
                        txt_ApellidoPaterno.Text = user.ApellidoPaterno;
                        txt_ApellidoMaterno.Text = user.ApellidoMaterno;
                        txt_Clave.Text = user.Clave;
                        txt_Clave2.Text = user.Clave;
                        txt_Dni.Text = user.Dni;
                        txt_Fecha_Nacimiento.Text = user.FechaNacimiento.HasValue ? user.FechaNacimiento.Value.ToString("yyyy-MM-dd") : string.Empty;
                        Ddl_Sexo.SelectedValue = user.Sexo;
                        txt_Direccion.Text = user.Direccion;
                        txt_Telefono.Text = user.Telefono;
                        txt_Email.Text = user.Email;
                        chb_Activo.Checked =  user.Activo;
                        
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
                    xdb.DelUsuarios(id); // usa tu método WCF
                    CargarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario eliminado correctamente.');", true);
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error al eliminar: {ex.Message}');", true);
                }
            }
        }

        protected void gvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuarios.PageIndex = e.NewPageIndex;
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