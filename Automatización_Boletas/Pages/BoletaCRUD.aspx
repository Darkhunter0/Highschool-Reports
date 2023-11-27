<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="BoletaCRUD.aspx.cs" Inherits="Automatización_Boletas.Pages.BoletaCRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    CRUD Boletas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

     <div class="container box">
         <asp:Label runat="server" CssClass="title" ID="lbltitulo"></asp:Label>

        <div class="field">
            <label class="label">Nombre Completo del Profesor</label>
            <div class="control">
                <asp:TextBox runat="server" type="text" ID="profesorText" CssClass="input" MaxLength="70"></asp:TextBox>
            </div>
        </div>

         <div class="field">
            <label class="label">Fecha de realización de la boleta</label>
            <div class="control">
                <asp:TextBox ID="dtFecha" runat="server" CssClass="input" placeholder="Fecha y Hora"></asp:TextBox>
            </div>
        </div>

         <div class="field">
            <label class="label">Descripción de la falta</label>
            <div class="control">
                <asp:textBox runat="server"  ID="descripcionBoleta" CssClass="textarea" MaxLength="500" ></asp:textBox>

            </div>
        </div>

            <div class="field">
            <label class="label">Estado</label>
            <div class="control">
                <div class="select">
                <select runat="server" ID="seccion" CssClass="input" >
    <option value="">--Escoge un estado de la boleta--</option>
    <option value="Enviada">Enviada</option>
    <option value="Revision">En revisión</option>
    <option value="Finalizada">Finalizada</option>

</select>
                    </div>
            </div>
        </div>

          <div class="field">
            <label class="label">Sección del Estudiante</label>
            <div class="control">
                <div class="select">
                <select runat="server" ID="grupoGuia" CssClass="input" >
    <option value="">--Escoge una sección--</option>
    <option value="12-F">12-F</option>
    <option value="12-E">12-E</option>
    <option value="12-D">12-D</option>
    <option value="12-C">12-C</option>
    <option value="12-B">12-B</option>
    <option value="12-A">12-A</option>

    <option value="11-F">11-F</option>
    <option value="11-E">11-E</option>
    <option value="11-D">11-D</option>
    <option value="11-C">11-C</option>
    <option value="11-B">11-B</option>
    <option value="11-A">11-A</option>

    <option value="10-F">10-F</option>
    <option value="10-E">10-E</option>
    <option value="10-D">10-D</option>
    <option value="10-C">10-C</option>
    <option value="10-B">10-B</option>
    <option value="10-A">10-A</option>

    <option value="9-F">9-F</option>
    <option value="9-E">9-E</option>
    <option value="9-D">9-D</option>
    <option value="9-C">9-C</option>
    <option value="9-B">9-B</option>
    <option value="9-A">9-A</option>
                    
    <option value="8-F">8-F</option>
    <option value="8-E">8-E</option>
    <option value="8-D">8-D</option>
    <option value="8-C">8-C</option>
    <option value="8-B">8-B</option>
    <option value="8-A">8-A</option>

    <option value="7-F">7-F</option>
    <option value="7-E">7-E</option>
    <option value="7-D">7-D</option>
    <option value="7-C">7-C</option>
    <option value="7-B">7-B</option>
    <option value="7-A">7-A</option>
</select>
                    </div>
            </div>
        </div>

         <div class="field">
            <label class="label">Estudiante Responsable</label>
            <div class="control">
                 <asp:Label runat="server" CssClass="input" ID="usuarioasocidado" disabled></asp:Label>
            </div>
        </div>

          <asp:Button runat="server" ID="BtnUpdate" CssClass="button is-link" Onclick="BtnUpdate_Click" Text="Actualizar" Visible="false"></asp:Button>
                <asp:Button runat="server" ID="BtnDelete" CssClass="button is-link" Onclick="BtnDelete_Click" Text="Borrar" Visible="false"></asp:Button>
                <asp:Button runat="server" ID="BtnVolver" CssClass="button is-black" Onclick="BtnVolver_Click" Text="Volver" Visible="True"></asp:Button>
    </div>
</asp:Content>
