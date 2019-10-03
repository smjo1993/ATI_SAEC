<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="agregarDocumento.aspx.vb" Inherits="prueba_saec.agregarDocumento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Nombre</h3>
            <asp:TextBox ID="txtNombreDocumento" runat="server"></asp:TextBox>
        </div>
        <div>
            <h3>Tipos</h3>
            Escoja el tipo de documento a requerir
            <asp:DropDownList ID="dropTipoDocumento" runat="server"></asp:DropDownList>
        </div>
        <div>
            <h3>Áreas</h3>
            Escoja las áreas pertinentes al documento
            <asp:CheckBoxList ID="chkListaAreas"
                runat="server">
            </asp:CheckBoxList>

        </div>
        <div>
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
            <asp:Button ID="btnCrearDocumento" runat="server" Text="Crear Documento" />
        </div>
    </form>
</body>
</html>