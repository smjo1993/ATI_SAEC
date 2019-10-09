<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="crearEmpresa.aspx.vb" Inherits="prueba_saec.crearEmpresa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        

     
        <div style="margin-top: 0px">
              <label id="lblRazonSocial">Razón Social:</label>
              <asp:TextBox ID="TxtRazonSocial" runat="server"></asp:TextBox>
        </div>
        <div>
              <label id="lblRut">Rut:</label>
              <asp:TextBox ID="TxtRut" runat="server"></asp:TextBox>
        </div>
        <div>
              <label id="lblGiro">Giro:</label>
              <asp:TextBox ID="TxtGiro" runat="server"></asp:TextBox>
        </div>
        <div>
              <label id="lblDireccion">Dirección:</label>
              <asp:TextBox ID="TxtDireccion" runat="server"></asp:TextBox>
        </div>
        <div>
              <label id="lblCiudad">Ciudad:</label>
              <asp:TextBox ID="TxtCiudad" runat="server"></asp:TextBox>
        </div>
        <div>
              <label id="lblFono">Fono:</label>
              <asp:TextBox ID="TxtFono" runat="server"></asp:TextBox>
        </div>
        <div>
              <label id="lblCelular">Celular:</label>
              <asp:TextBox ID="TxtCelular" runat="server"></asp:TextBox>
        </div>
        <div>
              <label id="lblCorreo">Correo:</label>
              <asp:TextBox ID="TxtCorreo" runat="server"></asp:TextBox>
        </div>
        <div>
            <label>Encargado:</label>
            <asp:DropDownList ID="dropContratistas" runat="server"></asp:DropDownList>


        </div>

            <asp:Button ID="Button1" runat="server" Text="Agregar" />

        <p>
            <asp:Label ID="lblAdvertencia" runat="server" Text=""></asp:Label>
        </p>

    </form>
</body>
</html>
