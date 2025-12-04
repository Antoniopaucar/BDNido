using ProyectoNido.wcfNido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_Tarifario : System.Web.UI.Page
    {
        wcfNido.Service1Client client = new wcfNido.Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTarifario();
            }
        }
        private void CargarTarifario(string filtro = null)
        {
            try
            {
                var lista = client.GetTarifario();

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    lista = lista
                        .Where(t => t.Nombre != null &&
                                    t.Nombre.ToUpper().Contains(filtro.ToUpper()))
                        .ToArray();
                }

                gvTarifario.DataSource = lista;
                gvTarifario.DataBind();

                ViewState["TarifarioLista"] = lista;
                lblMensaje.Text = lista.Length == 0 ? "No se encontraron registros." : "";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar el tarifario: " + ex.Message;
            }
        }

        // 🔴 ESTE ES UNO DE LOS MÉTODOS QUE FALTABA
        protected void gvTarifario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTarifario.PageIndex = e.NewPageIndex;

            var lista = ViewState["TarifarioLista"] as clsTarifario[];
            if (lista == null)
            {
                CargarTarifario();
            }
            else
            {
                gvTarifario.DataSource = lista;
                gvTarifario.DataBind();
            }
        }

        // ESTE ES EL OTRO METODO QUE FALTABA
        protected void gvTarifario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Consultar" || e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                var lista = ViewState["TarifarioLista"] as clsTarifario[];

                if (lista == null) return;

                var t = lista.FirstOrDefault(x => x.Id_Tarifario == id);
                if (t == null) return;

                if (e.CommandName == "Consultar")
                {
                    // Cargar datos al formulario
                    txt_IdTarifario.Text = t.Id_Tarifario.ToString();
                    ddl_Tipo.SelectedValue = t.Tipo ?? "";
                    txt_Nombre.Text = t.Nombre;
                    txt_Descripcion.Text = t.Descripcion;
                    ddl_Periodo.SelectedValue = t.Periodo.ToString();
                    txt_Valor.Text = t.Valor.ToString("N2");
                }
                else if (e.CommandName == "Eliminar")
                {
                    try
                    {
                        client.DelTarifario(id);
                        lblMensaje.Text = "Tarifario eliminado correctamente.";
                        CargarTarifario();
                    }
                    catch (System.ServiceModel.FaultException exf)
                    {
                        lblMensaje.Text = exf.Message;
                    }
                    catch (Exception exx)
                    {
                        lblMensaje.Text = "Error al eliminar: " + exx.Message;
                    }
                }
            }
        }
        private void Limpiar()
        {
            txt_IdTarifario.Text = "";
            ddl_Tipo.SelectedIndex = 0;
            txt_Nombre.Text = "";
            txt_Descripcion.Text = "";
            ddl_Periodo.SelectedIndex = 0;
            txt_Valor.Text = "";
        }

        private clsTarifario ObtenerDesdeFormulario()
        {
            var t = new clsTarifario();

            if (!string.IsNullOrWhiteSpace(txt_IdTarifario.Text))
                t.Id_Tarifario = int.Parse(txt_IdTarifario.Text);

            t.Tipo = ddl_Tipo.SelectedValue;
            t.Nombre = txt_Nombre.Text.Trim();
            t.Descripcion = txt_Descripcion.Text.Trim();
            t.Periodo = byte.Parse(ddl_Periodo.SelectedValue == "0" ? "0" : ddl_Periodo.SelectedValue);
            t.Valor = string.IsNullOrWhiteSpace(txt_Valor.Text)
                ? 0
                : decimal.Parse(txt_Valor.Text);

            return t;
        }

        private void CargarEnFormulario(clsTarifario t)
        {
            txt_IdTarifario.Text = t.Id_Tarifario.ToString();
            ddl_Tipo.SelectedValue = t.Tipo ?? "";
            txt_Nombre.Text = t.Nombre;
            txt_Descripcion.Text = t.Descripcion;
            ddl_Periodo.SelectedValue = t.Periodo.ToString();
            txt_Valor.Text = t.Valor.ToString("N2");
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Valor.Text) || decimal.Parse(txt_Valor.Text) <= 0)
            {
                lblMensaje.Text = "El valor del tarifario debe ser mayor a 0.";
                return;
            }
            try
            {
                var t = ObtenerDesdeFormulario();   // arma el clsTarifario con los TextBox

                client.InsTarifario(t);            // llama al WCF

                lblMensaje.Text = "Tarifario agregado correctamente.";
                CargarTarifario();
                Limpiar();
            }
            catch (System.ServiceModel.FaultException ex)
            {
                // ⚠️ Aquí capturas el mensaje que viene desde el servicio
                lblMensaje.Text = ex.Message;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al agregar: " + ex.Message;
            }
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_IdTarifario.Text))
            {
                lblMensaje.Text = "Seleccione un registro para modificar.";
                return;
            }

            try
            {
                var t = ObtenerDesdeFormulario();

                client.ModTarifario(t);

                lblMensaje.Text = "Tarifario modificado correctamente.";
                CargarTarifario();
                Limpiar();
            }
            catch (System.ServiceModel.FaultException ex)
            {
                lblMensaje.Text = ex.Message;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al modificar: " + ex.Message;
            }
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            lblMensaje.Text = "";
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_IdTarifario.Text))
            {
                lblMensaje.Text = "Seleccione un registro para eliminar.";
                return;
            }

            try
            {
                int id = int.Parse(txt_IdTarifario.Text);
                client.DelTarifario(id);

                lblMensaje.Text = "Tarifario eliminado correctamente.";
                CargarTarifario();
                Limpiar();
            }
            catch (System.ServiceModel.FaultException ex)
            {
                lblMensaje.Text = ex.Message;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar: " + ex.Message;
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = txtBuscar.Text.Trim();
                var lista = client.FilTarifario(texto);

                gvTarifario.DataSource = lista;
                gvTarifario.DataBind();

                lblMensaje.Text = lista.Length == 0 ? "No se encontraron resultados." : "";
            }
            catch (FaultException ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        protected void gvTarifario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}