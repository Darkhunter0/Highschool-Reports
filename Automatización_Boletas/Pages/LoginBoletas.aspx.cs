using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Automatización_Boletas
{
    public partial class LoginBoletas : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string codigo = Clave.Text.Trim(); 

            bool codigoValido = ValidarCodigo(codigo);

            if (codigoValido)
            {
                Response.Redirect("Verificacion.aspx?codigo=" + codigo); 
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
              "Swal.fire('Error :(','Tuviste un error, vuelve a intentarlo','error')", true);
            }
        }

        private bool ValidarCodigo(string codigo)
        {

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Boleta WHERE Clave = @Codigo", con);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0; 
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
              "Swal.fire('Error :(','Tuviste un error, vuelve a intentarlo','error')", true);
                return false; 

            }
            finally
            {
                con.Close();
            }
        }
    }
}