<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="LoginBoletas.aspx.cs" Inherits="Automatización_Boletas.LoginBoletas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <div class="container box mb-5 mt-5">
         <div class="mb-5  columns is-centered">
      <div class=" mt-5 column is-half">
        <h1 class="title">Verificación de Boletas</h1>

        <div class="field">
            <label class="label">Código de Boleta</label>
            <div class="control">
                <asp:TextBox runat="server" type="text" ID="Clave" class="input" MaxLength="50" ></asp:TextBox>
            </div>
        </div>


        <asp:Button  runat="server" id="ingresar" OnClick="btnIngresar_Click" class="btn btn-primary" text="Ingresar"/>
    </div>
              </div>
         </div>
</asp:Content>
