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
    public partial class Index : System.Web.UI.Page
    {

        string correo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");

            if (Session["correo_electronico"] != null)
            {
              
                correo = Session["correo_electronico"].ToString();
                int rol = ObtenerRolPorDominio(correo);

                if (rol == 1)
                {
                    btnBoletaAdministrar.Visible = true;
                    btnBoleta.Visible = false;

                }
                else
                {
                    btnBoleta.Visible = true;
                    btnBoletaAdministrar.Visible = false;

                }
                if (rol == 2)
                {
                   
                }

            }
            else
            {
                btnBoleta.Visible = false;
                btnBoletaAdministrar.Visible = false;
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



    }
}