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
    public partial class CRUD : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        public static string sID = "-1";
        public static string sOpc = "";
        string correo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    if (rol == 2)
                    {
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


            //Obtener Id
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    sID = Request.QueryString["id"].ToString();
                    CargarDatos();
                }
                if (Request.QueryString["op"] != null)
                {
                    sOpc = Request.QueryString["op"].ToString();

                    switch (sOpc)
                    {
                        case "R":
                            this.lbltitulo.Text = "Consulta de Usuario";
                            break;
                        case "U":
                            this.lbltitulo.Text = "Modificar Usuario";
                            this.BtnUpdate.Visible = true;
                            break;
                        case "D":
                            this.lbltitulo.Text = "Eliminar Usuario";
                            this.BtnDelete.Visible = true;
                            break;
                    }
                }
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
        void CargarDatos()
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("sp_datosEspecifico", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = sID;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            nombre.Text = row[1].ToString();
            // contrasena.Text = row[2].ToString();
            carnet.Text = row[3].ToString();
            correo_encargado.Text = row[4].ToString();
            seccion.Value = row[5].ToString();
            //Ejemplo con Datetime para las boletas
            // Datetime d=(DateTime)row[numero de columna de la fecha];
            //tbdate.Text = d.ToString("dd/MM/yyyy")
            con.Close();


        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_update", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = sID;
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = nombre.Text;
                cmd.Parameters.Add("@Carnet", SqlDbType.VarChar).Value = carnet.Text;
                cmd.Parameters.Add("@Correo_Encargado", SqlDbType.VarChar).Value = correo_encargado.Text;
                cmd.Parameters.Add("@Grupo_Guia", SqlDbType.VarChar).Value = seccion.Value;
                cmd.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = SelectRol.Value;

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("AdministrarUsuarios.aspx");
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
               "Swal.fire('Error :(','Hubo un error al actualizar los datos, vuelve a intentarlo','error')", true);
                con.Close();

            }



        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("sp_BorrarUsuario", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@correo_electronico", SqlDbType.VarChar).Value = sID;
            try
            {
                cmd.ExecuteNonQuery();
                Response.Redirect("AdministrarUsuarios.aspx");
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
                  "Swal.fire('Error :(','Hubo un error al eliminar, verifica si no hay boletas asociadas a este estudiante y vuelve a intentarlo','error')", true);
            }

            con.Close();

        }
        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarUsuarios.aspx");

        }
    }
}