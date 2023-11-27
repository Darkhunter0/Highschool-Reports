<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="CRUD.aspx.cs" Inherits="Automatización_Boletas.Pages.CRUD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    CRUD
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <div class="container box">
        <asp:Label runat="server" CssClass="title" ID="lbltitulo"></asp:Label>



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
                <asp:TextBox runat="server" ID="correo_encargado" CssClass="input" MaxLength="50"></asp:TextBox>
            </div>
        </div>

        <div class="field">
            <label class="label">Sección</label>
            <div class="control">
                <select runat="server" id="seccion" cssclass="input">
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

        <div class="field">
            <label class="label">Rol</label>
            <div class="control">
                <select runat="server" id="SelectRol" cssclass="input">
                    <option value="">--Escoge un rol--</option>
                    <option value="1">1 (Profesor)</option>
                    <option value="2">2 (Estudiante)</option>
                    <option value="3">3 (Administrador)</option>


                </select>
            </div>
        </div>

        <asp:Button runat="server" ID="BtnUpdate" CssClass="button is-link" OnClick="BtnUpdate_Click" Text="Actualizar" Visible="false"></asp:Button>
        <asp:Button runat="server" ID="BtnDelete" CssClass="button is-link" OnClick="BtnDelete_Click" Text="Borrar" Visible="false"></asp:Button>
        <asp:Button runat="server" ID="BtnVolver" CssClass="button is-black" OnClick="BtnVolver_Click" Text="Volver" Visible="True"></asp:Button>


    </div>

</asp:Content>
