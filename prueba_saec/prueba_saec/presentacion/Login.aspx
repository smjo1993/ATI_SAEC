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
                <asp:Label ID="lbUsuario" runat="server" Text="Usuario:"></asp:Label>
                <asp:TextBox ID="tbUsuario" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lbContrasenia" runat="server" Text="Contraseña:"></asp:Label>
                <asp:TextBox ID="tbContrasenia" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btLogin" runat="server" Text="Inicia Sesion" />
            </div>
            <div>
                <asp:LinkButton ID="lbRecuperarContrasenia" runat="server">¿Olvido su contraseña? Click aqui para recuperarla</asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
