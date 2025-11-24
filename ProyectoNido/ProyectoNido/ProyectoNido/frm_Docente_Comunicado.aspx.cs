using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace ProyectoNido
{
    public partial class frm_Docente_Comunicado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarComunicados();
            }
        }

        private void CargarComunicados()
        {
            if (Session["IdUsuario"] != null)
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                wcfNido.Service1Client client = new wcfNido.Service1Client();
                var lista = client.GetComunicado(idUsuario);
                rptComunicados.DataSource = lista;
                rptComunicados.DataBind();
            }
        }

        [WebMethod]
        public static void MarcarComoVisto(int idComunicado)
        {
            if (HttpContext.Current.Session["IdUsuario"] != null)
            {
                int idUsuario = Convert.ToInt32(HttpContext.Current.Session["IdUsuario"]);
                wcfNido.Service1Client client = new wcfNido.Service1Client();
                client.MarcarComunicadoVisto(idComunicado, idUsuario);
            }
        }
    }
}