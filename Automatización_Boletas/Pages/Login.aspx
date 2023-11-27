<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Automatización_Boletas.Pages.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Inicio de Sesión
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="container box mb-5 mt-5">
         <div class="mb-5  columns is-centered">
      <div class=" mt-5 column is-half">
        <h1 class="title">Iniciar Sesión</h1>

        <div class="field">
            <label class="label">Correo Electrónico</label>
            <div class="control">
                <asp:TextBox runat="server" type="email" ID="Usuario" class="input" MaxLength="50" ></asp:TextBox>
            </div>
        </div>

    <div class="field">
            <label class="label">Contraseña</label>
            <div class="control">
                <asp:TextBox runat="server" type="password" ID="contrasena" class="input" MaxLength="20" ></asp:TextBox>
            </div>
        </div>

        <asp:Button  runat="server" id="ingresar" OnClick="ingresar_Click" class="btn btn-primary" text="Ingresar"/>
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
