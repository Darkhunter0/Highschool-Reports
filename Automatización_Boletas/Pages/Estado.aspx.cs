using System;
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
    public partial class Estado : System.Web.UI.Page

    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {

            Response.AppendHeader("Cache-Control", "no-store");

            if (Session["correo_electronico"] != null)
            {


            }
            else
            {

                Response.Redirect("Index.aspx");

            }

            if (!IsPostBack)
            {

                CargarBoletas();

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
                }
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string estadoFiltro = ddlEstados.SelectedValue;

            using (con)
            {
                string query = "SELECT id_Boleta, Profesor, Fecha, Descripcion, Estado, Grupo, correo_electronico_usuario FROM Boleta WHERE 1=1";
                if (!string.IsNullOrEmpty(estadoFiltro))
                {
                    query += " AND Estado = @estadoFiltro";
                }

                query += " AND Grupo = @grupoGuiaUsuario";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(estadoFiltro))
                    {
                        command.Parameters.AddWithValue("@estadoFiltro", estadoFiltro);
                    }

                    command.Parameters.AddWithValue("@grupoGuiaUsuario", Session["Grupo_Guia"]);

                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    gvBoletas.DataSource = dt;
                    gvBoletas.DataBind();
                }
            }
        }

        protected void btnfiltrar2_Click(object sender, EventArgs e)
        {
            string estadoFiltro = ddlEstados.SelectedValue;

            using (con)
            {
                string query = "SELECT id_Boleta, Profesor, Fecha, Descripcion, Estado, Grupo, correo_electronico_usuario FROM Boleta WHERE 1=1";
                if (!string.IsNullOrEmpty(estadoFiltro))
                {
                    query += " AND Estado = @estadoFiltro";
                };

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(estadoFiltro))
                    {
                        command.Parameters.AddWithValue("@estadoFiltro", estadoFiltro);
                    }


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