using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_Alumno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvAlumno_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAlumno.PageIndex = e.NewPageIndex;
            // CargarAlumnos();
        }

        protected void gvAlumno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Aquí luego manejamos Consultar / Eliminar según e.CommandName
        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
        }
    }
}