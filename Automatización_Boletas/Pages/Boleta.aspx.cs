using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Automatización_Boletas.Pages
{
    public partial class Boleta : System.Web.UI.Page
    {
        string correo = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        private string idEstudianteSeleccionado;

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());


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
            Response.AppendHeader("Cache-Control", "no-store");
            if (!IsPostBack)
            {
                CargarUsuarios();
                CargarEstudiantes();
                gvEstudiantes.DataKeyNames = new string[] { "correo_electronico" };
                // Registra el script de jQuery UI Datepicker
                ScriptManager.RegisterStartupScript(this, GetType(), "DateTimePickerScript", "$('#" + dtFecha.ClientID + "').datepicker();", true);
            }
            profesorText.Text = Session["Nombre"].ToString();


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
        private void CargarUsuarios()
        {
            using (con)
            {
                string query = "SELECT correo_electronico, Nombre FROM Usuario";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    ddlUsuarios.DataSource = reader;
                    ddlUsuarios.DataTextField = "correo_electronico";
                    ddlUsuarios.DataValueField = "correo_electronico";
                    ddlUsuarios.DataBind();
                }
            }
        }
        void Limpiar()
        {

            dtFecha.Text = string.Empty;
            descripcionBoleta.Text = string.Empty;
            seccion.Value = string.Empty;

        }
        protected void Guardar_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

            string profesor = Session["Nombre"].ToString();
            DateTime fecha = DateTime.Parse(dtFecha.Text); // Obtener la fecha del TextBox
            string descripcion = descripcionBoleta.Text;
            string estado = seccion.Value;
            string grupo = grupoGuia.Value;
            string correoElectronicoUsuario = hiddenCorreoElectronico.Value;

            using (con)
            {
                string query = "INSERT INTO Boleta (Profesor, Fecha, Descripcion, Estado, Grupo, correo_electronico_usuario, Clave) " +
                               "VALUES (@Profesor, @Fecha, @Descripcion, @Estado, @Grupo, @correo_electronico_usuario, " +
                               "CONVERT(varchar(50), NEWID()))"; // Utilizamos NEWID() para generar un valor alfanumérico aleatorio

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Profesor", profesor);
                    command.Parameters.AddWithValue("@Fecha", fecha);
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@Estado", estado);
                    command.Parameters.AddWithValue("@Grupo", grupo);
                    command.Parameters.AddWithValue("@correo_electronico_usuario", correoElectronicoUsuario);
                    string correoEncargado = ObtenerCorreoElectronicoEncargado(correoElectronicoUsuario);
                    string correoProfesorGuia = ObtenerCorreoProfesorGuia(grupo);

                    con.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Obtener la Clave de la boleta recién insertada y guardarla en el campo oculto (Label)
                        query = "SELECT TOP 1 Clave FROM Boleta ORDER BY id_Boleta DESC"; // Usamos ORDER BY ID DESC para obtener la boleta más reciente
                        using (SqlCommand commandCodigoBoleta = new SqlCommand(query, con))
                        {
                            object result = commandCodigoBoleta.ExecuteScalar();
                            if (result != null)
                            {
                                string codigoBoleta = result.ToString();
                                hiddenClave.Text = codigoBoleta;

                                // Resto del código...
                            }
                        }

                        EnviarBoletaPorEmail(correoEncargado, profesor, fecha, descripcion, estado, grupo, correoProfesorGuia);

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
                            "Swal.fire('Excelente c>','La boleta fue guardada con éxito','success')", true);
                        Limpiar();
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
                            "Swal.fire('Error :(','Hubo un error al crear la boleta, vuelve a intentarlo','error')", true);
                        Limpiar();
                    }
                    con.Close();
                }
            }
        }

        private string ObtenerCorreoElectronicoEncargado(string correoEstudiante)
        {
            string correoEncargado = "";

            using (con)
            {
                string query = "SELECT Correo_Encargado FROM Usuario WHERE correo_electronico = @CorreoEstudiante";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@CorreoEstudiante", correoEstudiante);
                    con.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        correoEncargado = result.ToString();
                    }
                }
            }

            return correoEncargado;
        }
        private string ObtenerCorreoProfesorGuia(string grupo)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

            string correoProfesorGuia = "";

            using (con)
            {
                string query = "SELECT correo_electronico FROM Usuario WHERE Grupo_Guia = @Grupo_Guia AND correo_electronico LIKE '%@gmail.com'";//Cambiar por el dominio del cole, está como gmail para muestra
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Grupo_Guia", grupo);
                    con.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        correoProfesorGuia = result.ToString();
                    }
                }
            }

            return correoProfesorGuia;
        }

        private void EnviarBoletaPorEmail(string destinatario, string profesor, DateTime fecha, string descripcion, string estado, string grupo, string correoProfesorGuia)
        {
            // Configura las credenciales del remitente
            string remitente = "2020198@est.cedesdonbosco.ed.cr";
            string password = "DARKhunter1!23";
            string codigoBoleta = hiddenClave.Text;

            // Configura el servidor SMTP de Gmail
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(remitente, password);

            string cssStyles = @"
        <style>
            body {
                font-family: 'Nunito', Arial, sans-serif;
                background-color: #f2f2f2;
                margin: 0;
                padding: 0;
            }

            .container {
                max-width: 600px;
                margin: 0 auto;
                background-color: #ffffff;
                padding: 20px;
                border-radius: 5px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            }

            .header {
                background-color: #007bff;
                color: #ffffff;
                text-align: center;
                padding: 10px;
                border-radius: 5px 5px 0 0;
            }

            .content {
                padding: 20px;
            }

            .button {
                border-color: #007bff;
                border-style: solid;
                background-color: #ffffff;
                color: #ffffff;
                padding: 10px 20px;
                text-decoration: none;
                border-radius: 5px;
                display: inline-block;
                margin-top: 20px;
            }
                a{
                text-decoration: none;
                }
        </style>
    ";

            // Crea el cuerpo del mensaje con los datos de la boleta en formato HTML con estilos CSS
            string cuerpoMensaje = $@"
        <html>
        <head>
            {cssStyles}
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Boleta de Conducta</h1>
                </div>
                <div class='content'>
                    <p>Estimado(a) madre o padre de familia, buenos días, buenas tardes o buenas noches.</p>
                    <br>
                    <p>Aquí se encuentran adjntos los datos de la la boleta que fue hecha hacia su hijo(a) el día de hoy:</p>
                    <ul>
                        <li><strong>Profesor:</strong> {profesor}</li>
                        <li><strong>Fecha:</strong> {fecha.ToString("dd/MM/yyyy")}</li>
                        <li><strong>Descripción:</strong> {descripcion}</li>
                        <li><strong>Estado:</strong> {estado}</li>
                        <li><strong>Grupo:</strong> {grupo}</li>
                    </ul>
                    <p>Por favor, verifica la información y haz clic en el siguiente enlace para continuar con la verificación de la boleta:</p>
                    <a class='button' href='{ResolveClientUrl("https://localhost:44392/Pages/LoginBoletas.aspx")}'>Verificar</a>
                    <br>
                    <p>Utilice este código que le brindaremos a continuación para la verificación de la boleta: <strong>{hiddenClave.Text}</strong></p>
                </div>
            </div>
        </body>
        </html>
    ";
            // Crea el cuerpo del mensaje con los datos de la boleta en formato HTML con estilos CSS
            string cuerpoMensaje2 = $@"
        <html>
        <head>
            {cssStyles}
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Boleta de Conducta</h1>
                </div>
                <div class='content'>
                    <p>Estimado profesor, se le informa que se ha sido enviada una boleta a un estudiante de su grupo guía.</p>
                    <br>
                    <p>Aquí se encuentran adjntos los datos de la la boleta que fue hecha hacia el estudiante:</p>
                    <ul>
                        <li><strong>Profesor:</strong> {profesor}</li>
                        <li><strong>Fecha:</strong> {fecha.ToString("dd/MM/yyyy")}</li>
                        <li><strong>Descripción:</strong> {descripcion}</li>
                        <li><strong>Estado:</strong> {estado}</li>
                        <li><strong>Grupo:</strong> {grupo}</li>
                    </ul>
                    <p>Por favor, esté pendiente de la verificación de la boleta.</p>
                </div>
            </div>
        </body>
        </html>
    ";

            // Crea el mensaje de correo electrónico
            MailMessage mailMessage = new MailMessage(remitente, destinatario, "Boleta de Conducta", cuerpoMensaje);

            mailMessage.IsBodyHtml = true;

            MailMessage mailMessageProfesor = new MailMessage(remitente, correoProfesorGuia, "Boleta de Conducta - Profesor Guía", cuerpoMensaje2);

            mailMessageProfesor.IsBodyHtml = true;



            try
            {
                // Envía el correo electrónico
                smtpClient.Send(mailMessage);
                smtpClient.Send(mailMessageProfesor);


            }
            catch (Exception ex)
            {

                string script = $@"
                <script>
                Swal.fire({{
                    icon: 'error',
                    title: 'Oops...',
                    text: '{ex.Message}',
                }});
                </script>
            ";

                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", script);
               ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "Swal.fire('Error','Hubo un error al enviar el correo electrónico.','error')", true);
            }
        }

        protected void btnBuscarEstudiante_Click(object sender, EventArgs e)
        {
            string terminoBusqueda = txtBusquedaEstudiante.Text.Trim();

            // Realizar la búsqueda de estudiantes en la tabla Estudiante
            string query = "SELECT correo_electronico, Nombre, Grupo_Guia FROM Usuario WHERE (Nombre LIKE '%' + @TerminoBusqueda + '%' OR Grupo_Guia LIKE '%' + @TerminoBusqueda + '%') AND correo_electronico LIKE '%@est.cedesdonbosco.ed.cr'";

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TerminoBusqueda", terminoBusqueda);

                    con.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dtEstudiantes = new DataTable();
                        da.Fill(dtEstudiantes);

                        gvEstudiantes.DataSource = dtEstudiantes;
                        gvEstudiantes.DataBind();
                    }

                    con.Close();
                }
            }
        }

        private void CargarEstudiantes()
        {
            // Consulta SQL para cargar todos los estudiantes (sin filtro)
            string query = "SELECT correo_electronico, Nombre, Grupo_Guia FROM Usuario WHERE correo_electronico LIKE '%@est.cedesdonbosco.ed.cr'";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());


            using (con)
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dtEstudiantes = new DataTable();
                        da.Fill(dtEstudiantes);

                        gvEstudiantes.DataSource = dtEstudiantes;
                        gvEstudiantes.DataBind();
                    }

                    con.Close();
                }
            }

        }

        protected void gvEstudiantes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "SeleccionarEstudiante")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                if (rowIndex >= 0 && rowIndex < gvEstudiantes.Rows.Count)
                {
                    // Obtener el ID del estudiante seleccionado usando el índice de fila
                    idEstudianteSeleccionado = gvEstudiantes.DataKeys[rowIndex].Values["correo_electronico"].ToString();
                    ddlUsuarios.Text = idEstudianteSeleccionado;
                    hiddenCorreoElectronico.Value = idEstudianteSeleccionado;
                }
            }
        }
    }
}