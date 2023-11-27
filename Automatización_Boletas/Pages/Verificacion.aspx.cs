using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Automatización_Boletas
{
    public partial class Verificacion : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");

            datos.Visible = true;
            
                string codigoVerificacion = Request.QueryString["codigo"];
                if (!string.IsNullOrEmpty(codigoVerificacion))
                {
                    DataTable dtBoleta = ObtenerDatosBoleta(codigoVerificacion);
                    if (dtBoleta != null && dtBoleta.Rows.Count > 0)
                    {
                        divDatosBoleta.Visible = true;
                        datos.DataSource = dtBoleta;
                        datos.DataBind();
                    }
                    else
                    {
                     
                    }
                }
                else
                {
                   
                }
            
        }

        // Método para obtener los datos de la boleta desde la base de datos
        private DataTable ObtenerDatosBoleta(string codigoVerificacion)
        {
            DataTable dtBoleta = new DataTable();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Boleta WHERE Clave = @Clave", con);
                cmd.Parameters.AddWithValue("@Clave", codigoVerificacion);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtBoleta);
            }
            catch (Exception)
            {
            
            }
            finally
            {
                con.Close();
            }

            return dtBoleta;
        }

        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {

            string codigoVerificacion = Request.QueryString["codigo"];
            string codigoBoleta = codigoVerificacion;

            if (!string.IsNullOrEmpty(codigoBoleta))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Estado FROM Boleta WHERE Clave = @codigoBoleta", con);
                    cmd.Parameters.AddWithValue("@codigoBoleta", codigoBoleta);
                    string estadoBoleta = cmd.ExecuteScalar()?.ToString();

                    if (estadoBoleta == "Enviada")
                    {
                        // Actualizar el estado de la boleta a "Verificado"
                        SqlCommand updateCmd = new SqlCommand("UPDATE Boleta SET Estado = 'Verificado' WHERE Clave = @codigoBoleta", con);
                        updateCmd.Parameters.AddWithValue("@codigoBoleta", codigoBoleta);
                        updateCmd.ExecuteNonQuery();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
             "Swal.fire('Buen trabajo!','Verificación Correcta','success')", true);
                        Response.Redirect(Request.RawUrl);

                        // Mostrar mensaje de éxito
                       
                    }
                    else
                    {
                        // Mostrar mensaje de error
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
              "Swal.fire('Error :(','Tuviste un error, vuelve a intentarlo, es posible que esta boleta ya haya sido verificada','error')", true);
                    }
                }
                catch (Exception)
                {
                
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}