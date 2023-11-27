using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Automatización_Boletas.Pages
{
    public partial class AdministrarUsuarios : System.Web.UI.Page
    {
        int id_rol = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

            string correo = "";
            Response.AppendHeader("Cache-Control", "no-store");

            if (Session["correo_electronico"] != null)
            {
                correo = Session["correo_electronico"].ToString();
                int rol = ObtenerRolPorDominio(correo);

                if (rol == 3)
                {

                }
                else
                {
                    if(rol == 2) {
                        Response.Redirect("Index.aspx");
                    }
                    else
                    {

                    }
                }
            }
            else
            {

                Response.Redirect("Index.aspx");

            }

            if (!IsPostBack && Session["correo_electronico"] != null)
            {
                id_rol = Convert.ToInt32(Session["Id_Rol"].ToString());
                Datos();
                Permisos(id_rol);

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
                if(dominio == "cedesdonbosco.ed.cr")
                {
                    return 1; // Otro rol (puedes ajustar el valor según tus necesidades)
                }
                else
                {
                    return 3;
                }
            }
        }

        void Datos()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_Datos", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                datos.DataSource = dt;
                datos.DataBind();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        void Permisos(int id_rol)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_permiso", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = id_rol;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                bool Create, Read, Update, Delete;

                while (reader.Read())
                {
                    foreach (GridViewRow fila in datos.Rows)
                    {

                        switch (reader[0].ToString())
                        {
                            case "Create":
                                Create = Convert.ToBoolean(reader[1].ToString());
                                break;

                            case "Read":
                                Read = Convert.ToBoolean(reader[1].ToString());
                                Button btn1 = fila.FindControl("btnread") as Button;

                                if (Read)
                                {
                                    datos.Visible = true;
                                    btn1.Visible = true;
                                }
                                else
                                {
                                    datos.Visible = false;
                                    btn1.Visible = false;

                                }

                                break;

                            case "Update":
                                Update = Convert.ToBoolean(reader[1].ToString());
                                Button btn = fila.FindControl("btnupdate") as Button;

                                if (Update)
                                {
                                    btn.Visible = true;
                                }
                                else
                                {
                                    btn.Visible = false;
                                }

                                break;

                            case "Delete":
                                Delete = Convert.ToBoolean(reader[1].ToString());
                                Button btn2 = fila.FindControl("btndelete") as Button;

                                if (Delete)
                                {
                                    btn2.Visible = true;
                                }
                                else
                                {
                                    btn2.Visible = false;
                                }
                                break;
                        }
                    }
                }


                con.Close();
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {


            string id;
            Button BtnConsultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)BtnConsultar.NamingContainer;
            id = selectedrow.Cells[1].Text;
            Response.Redirect("~/Pages/CRUD.aspx?id=" + id + "&op=D");
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {

            string id;
            Button BtnConsultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)BtnConsultar.NamingContainer;
            id = selectedrow.Cells[1].Text;
            Response.Redirect("~/Pages/CRUD.aspx?id=" + id + "&op=U");
        }

        protected void btnread_Click(object sender, EventArgs e)
        {
            string id;
            Button BtnConsultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)BtnConsultar.NamingContainer;
            id = selectedrow.Cells[1].Text;
            Response.Redirect("~/Pages/CRUD.aspx?id=" + id + "&op=R");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void FiltrarDatos()
        {
            string searchTerm = txtBusqueda.Text.Trim().ToLower();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario", con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Filtrar los datos en el DataTable
                DataRow[] filteredRows = dt.Select($"Nombre LIKE '%{searchTerm}%' OR correo_electronico LIKE '%{searchTerm}%' OR Grupo_Guia LIKE '%{searchTerm}%'");

                // Crear un nuevo DataTable con los resultados filtrados
                DataTable filteredTable = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredTable.ImportRow(row);
                }

                datos.DataSource = filteredTable;
                datos.DataBind();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}