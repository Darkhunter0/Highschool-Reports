<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuarios.aspx.cs" Inherits="Automatización_Boletas.Pages.AdministrarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Administrador de Usuarios
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <br />
    <div class="container mt-5">
        <div class="table-responsive">
            <div class="form-group">
                <label for="txtBusqueda">Buscar:</label>
                <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control" OnTextChanged="txtBusqueda_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary mt-5 mb-5" Text="Buscar" OnClick="btnBuscar_Click" />

            <asp:GridView class="table center mx-auto" runat="server" ID="datos">

                <Columns>
                    <asp:TemplateField HeaderText="Opciones">
                        <ItemTemplate>
                            <div class="btn-group" role="group">
                                <asp:Button runat="server" CssClass="button is-primary" ID="btnread" OnClick="btnread_Click" Text="Mostrar" />
                                <asp:Button runat="server" CssClass="button is-warning mx-2" ID="btnupdate" OnClick="btnupdate_Click" Text="Actualizar" />
                                <asp:Button runat="server" CssClass="button is-danger" ID="btndelete" OnClick="btndelete_Click" Text="Borrar" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
