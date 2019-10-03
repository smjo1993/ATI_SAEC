<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="prueba_saec.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
                <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblContrasenia" runat="server" Text="Contraseña:"></asp:Label>
                <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="Inicia Sesion" />
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <asp:LinkButton ID="lblRecuperarContrasenia" runat="server">¿Olvido su contraseña? Click aqui para recuperarla</asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>