using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Automatización_Boletas.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        string Patron = "InfoToolsSV";

        void LimpiarInicio()
        {

            Usuario.Text = string.Empty;
            contrasena.Text = string.Empty;

            
        }
 
        protected void ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_login", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@correo_electronico", System.Data.SqlDbType.VarChar).Value = Usuario.Text;
                cmd.Parameters.Add("@Contrasena", System.Data.SqlDbType.VarChar).Value = contrasena.Text;
                cmd.Parameters.Add("@Patron", System.Data.SqlDbType.VarChar).Value = Patron;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    Session["Id_Rol"] = rd[6].ToString();
                    Session["Nombre"] = rd[1].ToString();
                    Session["correo_electronico"] = rd[0].ToString();
                    Session["Grupo_Guia"] = rd[5].ToString();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
               "Swal.fire('Buen trabajo!','Inicio de sesión correcto','success')", true);
                    Response.Redirect("Index.aspx");
                    LimpiarInicio();

                }
                con.Close();
                LimpiarInicio();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
                "Swal.fire('Error :(','Tuviste un error, vuelve a intentarlo','error')", true);

            }
            catch (Exception) 
            {
                throw;
            }
        }
    }
}