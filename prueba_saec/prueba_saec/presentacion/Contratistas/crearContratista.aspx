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
          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
    <div>
          <label>Username:</label>
          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    </div>
    <div>
          <label>Password:</label>
          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
    </div>
    <div>
          <label>Rut:</label>
          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
    </div>
    <div>
          <label>Fono:</label>
          <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
    </div>
    

    <div>
          <label>Correo:</label>
          <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
    </div>

        <asp:Button ID="Button1" runat="server" Text="Agregar" />

    </form>
</body>
</html>
