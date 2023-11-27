<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Automatización_Boletas.Pages.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Inicio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
      
      <section class="hero">
        <div class="container">
            <div class="row">
                <div class="col-md-6 mensajes2">
                    <div class="copy" data-aos="fade-up" data-aos-duration="3000">
                        <div class="text-label">
                            Ahora hacer boletas y monitorearlas es más fácil!
                        </div>
                        <div class="text-hero-bold">
                            Sistema de reportes de conducta.
                        </div>
                        <div class="text-hero-regular">
                            Monitorea tus procesos de conducta de manera más sencilla aquí!
                        </div>
                        <div class="cta">
                            <a href="BoletaEstudiante.aspx" runat="server" id="btnBoleta" class="btn btn-primary shadow-none">Mis Boletas</a>
                            <a href="BoletaAdministrar.aspx" runat="server"  id="btnBoletaAdministrar" class="btn btn-secondary shadow-none ms-3">Administrar Boletas</a>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 mensajes" data-aos="fade-up" data-aos-duration="3000">
                    <img src="../assets/img/High School-rafiki.svg" class="w-100" alt="img">
                </div>
            </div>
        </div>
      </section>

        <section class="testimoni-brand">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 mt-5 mb-5 text-center">
                        <img class="imgTestimonial" src="../assets/img/TICS.jpg" alt="testimoni-brand">
                    </div>
                </div>
            </div>
        </section>

</asp:Content>
