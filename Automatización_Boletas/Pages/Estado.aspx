<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Estado.aspx.cs" Inherits="Automatización_Boletas.Pages.Estado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Boletas por Estado
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <div class="container mt-5">
        <div class="table-responsive">
            <asp:HiddenField ID="hdnGrupoGuiaUsuario" runat="server" />
            <div class="container">
                <h1 class="title">Filtro de Boletas</h1>
                <div class="field is-horizontal">
                    <div class="field-label is-normal">
                        <label class="label">Estado:</label>
                    </div>
                    <div class="field-body">
                        <div class="field">
                            <div class="control">
                                <div class="select">
                                    <asp:DropDownList ID="ddlEstados" runat="server" CssClass="select" AutoPostBack="true">
                                        <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Enviada" Value="Enviada"></asp:ListItem>
                                        <asp:ListItem Text="Verificado" Value="Verificado"></asp:ListItem>
                                        <asp:ListItem Text="Revision" Value="Revision"></asp:ListItem>
                                        <asp:ListItem Text="Finalizada" Value="Finalizada"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="field">
                    <div class="control">
                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar por grupo Guía" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
                        <asp:Button ID="btnfiltrar2" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnfiltrar2_Click" />
                    </div>
                </div>
                <div class="table-container">
                    <asp:GridView ID="gvBoletas" runat="server" CssClass="table is-fullwidth">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
