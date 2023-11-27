<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Verificacion.aspx.cs" Inherits="Automatización_Boletas.Verificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <div class="container mt-5">
        <div class="table-responsive">
            <asp:Panel ID="divDatosBoleta" runat="server" Visible="false">
                <h2 class="mb-4">Información de la Boleta</h2>
                <asp:GridView ID="datos" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="true"></asp:GridView>
                <columns>
                    <itemtemplate>
                        <div runat="server" class="btn-group" role="group">
                            <asp:Button ID="btnCambiarEstado" runat="server" Text="Verificar" CssClass="btn btn-primary" OnClick="btnCambiarEstado_Click" />
                        </div>
                    </itemtemplate>
                </columns>
            </asp:Panel>
        </div>
    </div>

</asp:Content>
