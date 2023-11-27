using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Automatización_Boletas.Pages
{
    public partial class Registrarse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        string Patron = "InfoToolsSV";

        void Limpiar()
        {

            correo_electronico.Text = string.Empty;
            nombre.Text = string.Empty;
            contrasena.Text = string.Empty;
            carnet.Text = string.Empty;
            correo_encargado.Text = string.Empty;


        }

        protected void Registrar_Click(object sender, EventArgs e)
        {
            string correo = correo_electronico.Text;
            try
            {

                string[] allowedDomains = { "@est.cedesdonbosco.ed.cr", "@cedesdonbosco.ed.cr" };
                SqlCommand cmd = new SqlCommand("sp_registrar", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                
                if (IsEmailValid(correo_electronico.Text, allowedDomains))
                {
                    cmd.Parameters.Add("@correo_electronico", System.Data.SqlDbType.VarChar).Value = correo_electronico.Text;
                   
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
                   "Swal.fire('Error :(','El dominio de este correo no es de Cedes Don Bosco','error')", true);
                    con.Close();
                    Limpiar();
                }

                cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = nombre.Text;
                cmd.Parameters.Add("@Contrasena", System.Data.SqlDbType.VarChar).Value = contrasena.Text;
                cmd.Parameters.Add("@Carnet", System.Data.SqlDbType.VarChar).Value = carnet.Text;
                cmd.Parameters.Add("@Correo_Encargado", System.Data.SqlDbType.VarChar).Value = correo_encargado.Text;
                cmd.Parameters.Add("@Grupo_Guia", System.Data.SqlDbType.VarChar).Value = seccion.Value;

                int rol = ObtenerRolPorDominio(correo_electronico.Text);
                cmd.Parameters.Add("@Id_Rol", System.Data.SqlDbType.VarChar).Value = rol;


                cmd.Parameters.Add("@Patron", System.Data.SqlDbType.VarChar).Value = Patron;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Limpiar();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
              "Swal.fire('Buen trabajo!','Inicio de sesión correcto','success')", true);
                Response.Redirect("Login.aspx");



            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
                   "Swal.fire('Error :(','Es probable que este correo ya ha sido usado','error')", true);
                con.Close();
                Limpiar();
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
                return 1; // Otro rol (puedes ajustar el valor según tus necesidades)
            }
        }

        static bool IsEmailValid(string email, string[] allowedDomains)
        {
            // Expresión regular para verificar si el correo electrónico tiene un dominio válido
            string pattern = @"^[A-Za-z0-9._%+-]+(" + string.Join("|", allowedDomains.Select(d => Regex.Escape(d))) + ")$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(email);
        }
    }
}