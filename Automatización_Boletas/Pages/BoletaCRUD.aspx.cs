using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Automatización_Boletas.Pages
{
    public partial class BoletaCRUD : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        public static string sID = "-1";
        public static string sOpc = "";
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
            //Obtener Id
            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "DateTimePickerScript", "$('#" + dtFecha.ClientID + "').datepicker();", true);
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

        void CargarDatos()
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("sp_datosEspecificoBoleta", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = sID;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            profesorText.Text = row[1].ToString();
            // contrasena.Text = row[2].ToString();
            //  DateTime d=(DateTime)row[2];
            dtFecha.Text = row[2].ToString();
            descripcionBoleta.Text = row[3].ToString();
            seccion.Value = row[4].ToString();
            grupoGuia.Value = row[5].ToString();
            usuarioasocidado.Text = row[6].ToString();
            //Ejemplo con Datetime para las boletas
            // Datetime d=(DateTime)row[numero de columna de la fecha];
            //tbdate.Text = d.ToString("dd/MM/yyyy")


            con.Close();


        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarBoleta", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id_Boleta", SqlDbType.VarChar).Value = sID;
                cmd.Parameters.Add("@Profesor", SqlDbType.VarChar).Value = profesorText.Text;
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = dtFecha.Text;
                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = descripcionBoleta.Text;
                cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = seccion.Value;
                cmd.Parameters.Add("@Grupo", SqlDbType.VarChar).Value = grupoGuia.Value;
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("BoletaAdministrar.aspx");
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
                "Swal.fire('Error :(','Hubo un error al actualizar los datos, vuelve a intentarlo','error')", true);
            }



        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("sp_BorrarBoleta", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id_Boleta", SqlDbType.VarChar).Value = sID;
            try
            {
                cmd.ExecuteNonQuery();
                Response.Redirect("BoletaAdministrar.aspx");
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
                  "Swal.fire('Error :(','Hubo un error al eliminar la boleta, vuelve a intentarlo','error')", true);
            }

            con.Close();

        }
        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("BoletaAdministrar.aspx");

        }
    }
}