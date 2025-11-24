using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_Profesor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            // Lógica para agregar profesor
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            // Lógica para modificar profesor
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            // Lógica para limpiar controles
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            // Lógica para eliminar profesor
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            // Lógica para filtrar profesores en el GridView
        }

        protected void gvProfesor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Lógica para paginar el GridView
        }

        protected void gvProfesor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Lógica para manejar los comandos Consultar / Eliminar del GridView
        }
    }
}