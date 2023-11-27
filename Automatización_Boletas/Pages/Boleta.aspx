<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Boleta.aspx.cs" Inherits="Automatización_Boletas.Pages.Boleta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Boleta
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

     <div class="container box">
        <asp:HiddenField ID="hiddenCorreoElectronico" runat="server" />
         <asp:Label ID="hiddenClave" runat="server" Visible="false"></asp:Label>
        <h1 class="title">Creación de Boletas</h1>

        <div class="field">
            <label class="label">Nombre Completo del Profesor</label>
            <div class="control">
                <asp:TextBox runat="server" type="text" ID="profesorText" CssClass="input" MaxLength="70" disabled></asp:TextBox>
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
                <asp:textBox runat="server"  ID="descripcionBoleta" CssClass="textarea" MaxLength="500"></asp:textBox>

            </div>
        </div>

            <div class="field">
            <label class="label">Estado</label>
            <div class="control">
                <div class="select">
                <select runat="server" ID="seccion" CssClass="input" >
    <option value="">--Escoge un estado de la boleta--</option>
    <option value="Enviada">Enviada</option>
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
               <asp:DropDownList ID="ddlUsuarios" runat="server" DataTextField="Nombre" CssClass="input" DataValueField="correo_electronico" disabled></asp:DropDownList>
            </div>
        </div>



         <div class="container">

    <div class="field">
        <label class="label">Buscar Estudiante:</label>
        <div class="control">
         <asp:TextBox CssClass="input" ID="txtBusquedaEstudiante"  placeholder="Ingrese el nombre o grupo guía" runat="server"></asp:TextBox>

        </div>
    </div>

    <div class="field">
        <div class="control">
            <asp:Button ID="btnBuscarEstudiante" CssClass="button is-link" runat="server" Text="Buscar" OnClick="btnBuscarEstudiante_Click" />
        </div>
    </div>

    <div class="table-container">
    <asp:GridView ID="gvEstudiantes" runat="server" DataKeyNames="correo_electronico"  CssClass="table is-fullwidth is-hoverable" OnRowCommand="gvEstudiantes_RowCommand">
    <Columns>
        <asp:BoundField />
        <asp:BoundField />
        <asp:BoundField/>

        <asp:TemplateField HeaderText="Seleccionar">
            <ItemTemplate>
                <asp:Button ID="Button1" CssClass="button is-link" runat="server" CommandName="SeleccionarEstudiante" CommandArgument='<%# Container.DataItemIndex %>' Text="Seleccionar" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</div>



         <div class="field is-grouped">
            <div class="control">
                <asp:Button runat="server" ID="Guardar" OnClick="Guardar_Click" CssClass="btn btn-primary"  Text="Enviar"></asp:Button>
            </div>
              <div class="control">
                  <a class="btn btn-secondary" href="Boleta.aspx">Cancelar</a>
            </div>
        </div>
    </div>
         </div>

</asp:Content>
