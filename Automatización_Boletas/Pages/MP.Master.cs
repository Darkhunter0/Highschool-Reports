using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Automatización_Boletas.Pages
{
    public partial class MP : System.Web.UI.MasterPage
    {

        string correo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");

            if (Session["correo_electronico"]!=null)
            {
                divuser.Visible = true;
                divbuttons.Visible = false;
                lblusuario.Text = Session["correo_electronico"].ToString();
                correo = Session["correo_electronico"].ToString();
                int idrol = Convert.ToInt32(Session["Id_Rol"]);
                int rol = ObtenerRolPorDominio(correo);



                if (rol == 1)
                {
                    navBarOpciones.Visible = true;
                    Boleta.Visible = true;
                    BoletasEstudiantes.Visible = false;
                    inicio.Visible = true;
                    administrarUsuariosdenav.Visible = false;
                    administrarboletas.Visible = false;

                }
                else
                {
                    navBarOpciones.Visible = false;
                    Boleta.Visible = false;
                    BoletasEstudiantes.Visible = false;

                }
                if(rol == 2)
                {
                    BoletasEstudiantes.Visible = true;
                    inicio.Visible = true;
                }
                else
                {
                    if (idrol == 3)
                    {

                        administrarUsuariosdenav.Visible = true;
                        administrarboletas.Visible = true;
                    }
                }
               

            }
            else
            {
                divuser.Visible = false;
                divbuttons.Visible = true;
                lblusuario.Text = string.Empty;
                    navBarOpciones.Visible = false;
                Boleta.Visible = false;
                BoletasEstudiantes.Visible = false;
                inicio.Visible = false;
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



        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registrarse.aspx");
        }

        protected void Unnamed_Click1(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");

        }

        protected void Unnamed_Click2(object sender, EventArgs e)
        {
            Session["correo_electronico"] = null;
            Session["Id_Rol"] = null;
            Response.Redirect("Login.aspx");
            HttpContext.Current.Session.Abandon();
        }
    }
}