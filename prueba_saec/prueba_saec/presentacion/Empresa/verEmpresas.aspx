<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="verEmpresas.aspx.vb" Inherits="prueba_saec.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>SB Admin 2 - Tables</title>

    <!-- Custom script y para tablas -->
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.10.20/datatables.min.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.20/datatables.min.js"></script>


    <!-- Custom fonts for this template -->
    <link href="../../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="../../css/sb-admin-2.min.css" rel="stylesheet">

    <!-- Custom styles for this page -->
    <link href="../../vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">
    <style type="text/css">
        .auto-style1 {
            width: 984px;
        }
    </style>
</head>
<body>
    <div class="container-fluid col-12">
        <div class="card shadow mb-4">
            <div class="card-header py-3">
                <h6 class="m-0 font-weight-bold text-primary">Empresas</h6>
            </div>
            <div class="card-body">
                <%--<div class="table-responsive">
                    <div class="dataTables_wrapper" class="dt-bootstrap4">
                        <div class="row"></div>
                        <div class="row col-12">
                            <div class="col-sm-12">--%>
                <form id="form1" runat="server">
                    <div>
                        <table class="table table-bordered dataTable" id="tablaEmpresa" width="100%" cellspacing="0" role="grid" aria-describedby="dataTable_info" style="width: 100%;">
                            <%--<caption class="auto-style2">
                                                <strong>Empresas</strong></caption>--%>
                            <%--<tr>
                                <td class="auto-style1">&nbsp;</td>
                            </tr>--%>
                            <tr class="align-left">
                                <td class="auto-style1">
                                    <%--<asp:GridView ID="gridEmpresas" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Horizontal" Width="100%" Visible="true" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Style="text-align: Center">--%>
                                    <asp:GridView ID="gridEmpresas" runat="server" AutoGenerateColumns="False" class="table table-bordered dataTable" Width="100%" CellSpacing="0" role="grid" aria-describedby="dataTable_info" Style="width: 100%;">
                                        <Columns>
                                            <asp:BoundField DataField="razonSocial" HeaderText="Razón Social" />
                                            <asp:BoundField DataField="rut" HeaderText="Rut" />
                                            <asp:BoundField DataField="giro" HeaderText="Giro" />
                                            <asp:BoundField DataField="personaContacto" HeaderText="Contacto" />
                                            <asp:BoundField DataField="correo" HeaderText="Correo" />
                                            <asp:BoundField DataField="celular" HeaderText="Celular" />


                                            <asp:TemplateField HeaderText="Ver">
                                                <ItemTemplate>
                                                    <asp:Button ID="btVer" CssClass="button primary" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" Text="Ver" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <%--<FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />--%>
                                    </asp:GridView>


                                    <%--<br />
                    <br />
                    <br />


                    <asp:Button ID="btCrearMenu" runat="server" Text="Crear Nuevo Menú" CssClass="button primary" />--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </form>
                <%--</div>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
</body>
</html>
