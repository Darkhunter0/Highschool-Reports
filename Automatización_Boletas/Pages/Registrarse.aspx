<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="Automatización_Boletas.Pages.Registrarse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Registrarse
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="container box">
         <div class="columns is-centered">
      <div class="column is-half">
        <h1 class="title">Registro de Usuarios</h1>

        <div class="field">
            <label class="label">Correo Electrónico</label>
            <div class="control">
                <asp:TextBox runat="server" type="email" ID="correo_electronico" CssClass="input" MaxLength="50"></asp:TextBox>
            </div>
        </div>

         <div class="field">
            <label class="label">Nombre Completo</label>
            <div class="control">
                <asp:TextBox runat="server" type="text" ID="nombre" CssClass="input" MaxLength="60"></asp:TextBox>
            </div>
        </div>

         <div class="field">
            <label class="label">Contraseña</label>
            <div class="control">
                 <asp:TextBox runat="server" type="password" ID="contrasena" CssClass="input" MaxLength="20"></asp:TextBox>
            </div>
        </div>

         <div class="field">
            <label class="label">Carnet</label>
            <div class="control">
                <asp:TextBox runat="server" ID="carnet" CssClass="input" MaxLength="7"></asp:TextBox>
            </div>
        </div>

         <div class="field">
            <label class="label">Correo electrónico encargado</label>
            <div class="control">
                <asp:TextBox runat="server" ID="correo_encargado" CssClass="input" MaxLength="50" ></asp:TextBox>
            </div>
        </div>

         <div class="field">
            <label class="label">Sección</label>
            <div class="control">
                <div class="select">
                <select runat="server" ID="seccion" CssClass="input" >
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

         <div class="field is-grouped">
            <div class="control">
                <asp:Button runat="server" ID="Registrar" CssClass="btn btn-primary" OnClick="Registrar_Click" Text="Enviar"></asp:Button>
            </div>
              <div class="control">
                  <a class="btn btn-secondary " href="Index.aspx">Cancelar</a>
            </div>
        </div>
    </div>
               </div>
               </div>

     <script src="
https://cdn.jsdelivr.net/npm/sweetalert2@11.7.18/dist/sweetalert2.all.min.js
"></script>
<link href="
https://cdn.jsdelivr.net/npm/sweetalert2@11.7.18/dist/sweetalert2.min.css
" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>
