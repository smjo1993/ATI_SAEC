<%@ Page Language="VB" %>

<!DOCTYPE html>
<script runat="server">

    Protected Sub Page_Load(sender As Object, e As EventArgs)

    End Sub
</script>



<html lang="en" xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

<meta charset="utf-8" />
    <h3> Nuevo Documento </h3>    

</head>
<body>
 
    <form id="form2" runat="server">
        
        <h3> Nombre </h3>
    
        <asp:TextBox ID="txtNombreDocumento" 
            runat="server" 
            OnTextChanged="txtNombreDocumento_TextChanged">

        </asp:TextBox>

    </form>

    <form id="form3" runat="server">
        <h3> Tipo </h3>
        Escoja el tipo documento
        
        <asp:DropDownList ID="dropTipoDocumento" 
            runat="server" 
            OnSelectedIndexChanged="dropTipoDocumento_SelectedIndexChanged">

        </asp:DropDownList>

    </form>

    <form id="form4" runat="server">

      <h3> Areas </h3>
      Escoja las áreas pertinentes al documento
      <br /><br />

        <asp:CheckBoxList ID="chkListaAreas" 
            AutoPostBack="True"
            CellPadding="5"
            CellSpacing="5"
            RepeatColumns="2"
            RepeatDirection="Vertical"
            RepeatLayout="Flow"
            TextAlign="Right"
            runat="server">
        </asp:CheckBoxList>

        <%--<asp:Label ID="lblItemCheckbox" runat="server" Text=""></asp:Label>--%>
        
        <br /><br />

    </form>

    <asp:Button ID="btn1" runat="server" Text="Cancelar" />
    <asp:Button ID="btn2" runat="server" Text="Crear Documento" />

</body>
</html>
