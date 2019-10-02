<%@ Page Language="VB" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<meta charset="utf-8" />
    <title></title>    

</head>
<body>
    <form id="form1" runat="server">   
    <asp:Label ID="lblHeader" runat="server" Text="Nuevo Documento"></asp:Label>
    </form>

    <form id="form2" runat="server">
        <asp:Label ID="Label2" runat="server" Text="Nombre: "></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </form>

    <form id="form3" runat="server">
        <asp:Label ID="Label3" runat="server" Text="Tipo: "></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
    </form>

    <form id="form4" runat="server">
        <asp:Label ID="Label4" runat="server" Text="Áreas: "></asp:Label>
        <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
    </form>

</body>
</html>
