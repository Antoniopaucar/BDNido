using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoNido
{
    public partial class frm_Apoderado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Agregar_Click(object sender, EventArgs e)
        {
            // TODO: Implementar lógica para agregar un apoderado
        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
            // TODO: Implementar lógica para modificar un apoderado
        }

        protected void btn_Limpiar_Click(object sender, EventArgs e)
        {
            // TODO: Implementar lógica para limpiar el formulario
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            // TODO: Implementar lógica para eliminar un apoderado
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            // TODO: Implementar lógica para filtrar apoderados en el grid
        }

        protected void gvApoderado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvApoderado.PageIndex = e.NewPageIndex;
            // TODO: Volver a cargar el grid de apoderados
        }

        protected void gvApoderado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // TODO: Implementar lógica para consultar o eliminar según CommandName
        }
    }
}