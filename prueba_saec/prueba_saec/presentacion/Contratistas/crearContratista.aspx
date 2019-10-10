<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="crearContratista.aspx.vb" Inherits="prueba_saec.crearTrabajador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

     
    <div style="margin-top: 0px">
          <label>Nombre:</label>
          <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    </div>
    <div>
          <label>Nombre de Usuario:</label>
          <asp:TextBox ID="txtNombreUsuario" runat="server"></asp:TextBox>
    </div>
    <div>
          <label>Password:</label>
          <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
    </div>
    <div>
          <label>Rut:</label>
          <asp:TextBox ID="txtRut" runat="server"></asp:TextBox>
    </div>
    <div>
          <label>Fono:</label>
          <asp:TextBox ID="txtFono" runat="server"></asp:TextBox>
    </div>
    <div>
          <label>Correo:</label>
          <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
    </div>

    <asp:Button ID="btnAceptar" runat="server" Text="Agregar" />
    <p>
        <asp:Label ID="lblAdvertencia" runat="server" Text=""></asp:Label>
    </p>
    </form>
</body>
</html>
