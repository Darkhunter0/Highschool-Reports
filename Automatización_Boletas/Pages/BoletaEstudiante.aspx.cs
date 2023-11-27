using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Automatización_Boletas.Pages
{
    public partial class BoletaEstudiante : System.Web.UI.Page
    {
        string correo = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["correo_electronico"] != null)
            {
             
            }
            else
            {

                Response.Redirect("Index.aspx");

            }

            Response.AppendHeader("Cache-Control", "no-store");
            if (!IsPostBack)
            {

                CargarBoletas();

            }

        }


        public static int ObtenerRolPorDominio(string correoElectronico)
        {
            // Obtener el dominio del correo electrónico
            string dominio = correoElectronico.Substring(correoElectronico.IndexOf('@') + 1);

            // Asignar el rol correspondiente según el dominio
            if (dominio == "est.cedesdonbosco.ed.cr")
            {
                return 2; // Rol 1
            }
            else
            {
                if (dominio == "cedesdonbosco.ed.cr")
                {
                    return 1; // Otro rol (puedes ajustar el valor según tus necesidades)
                }
                else
                {
                    return 3;
                }
            }
        }
        private void CargarBoletas()
        {
            using (con)
            {
                string query = "SELECT id_Boleta, Profesor, Fecha, Descripcion, Estado, Grupo, correo_electronico_usuario FROM Boleta";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    gvBoletas.DataSource = dt;
                    gvBoletas.DataBind();
                    con.Close();
                    cargarBoletas2();
                }
            }
        }

        private void cargarBoletas2()
        {

            using (con)
            {
                string query = "SELECT id_Boleta, Profesor, Fecha, Descripcion, Estado, Grupo, correo_electronico_usuario FROM Boleta WHERE 1=1";

                query += " AND correo_electronico_usuario = @correo_electronico_usuario";

                using (SqlCommand command = new SqlCommand(query, con))
                {

                    command.Parameters.AddWithValue("@correo_electronico_usuario", Session["correo_electronico"]);

                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    gvBoletas.DataSource = dt;
                    gvBoletas.DataBind();
                }
            }
        }
    }
}