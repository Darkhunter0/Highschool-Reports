<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="BoletaEstudiante.aspx.cs" Inherits="Automatización_Boletas.Pages.BoletaEstudiante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Vista Boletas Estudiante
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

      <div class="container">
    <h1 class="title">Tus boletas</h1>
  
    <div class="table-container">
        <asp:GridView ID="gvBoletas" runat="server" CssClass="table is-fullwidth">
             
        </asp:GridView>
    </div>
</div>
</asp:Content>
