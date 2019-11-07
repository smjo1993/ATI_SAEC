<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="verContratistas.aspx.vb" Inherits="prueba_saec.verContratistas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

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
                <h6 class="m-0 font-weight-bold text-primary">Contratistas</h6>
            </div>
            <div class="card-body">
                <form id="form1" runat="server">
                    <div>
                        <table class="table table-bordered dataTable" id="tablaEmpresa" width="100%" cellspacing="0" role="grid" aria-describedby="dataTable_info" style="width: 100%;">
                            <tr class="align-left">
                                <td class="auto-style1">
                                    <asp:GridView ID="gridContratistas" runat="server" AutoGenerateColumns="False" class="table table-bordered dataTable" Width="100%" CellSpacing="0" role="grid" aria-describedby="dataTable_info" Style="width: 100%;">
                                        <Columns>
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:BoundField DataField="rut" HeaderText="Rut" />
                                            <asp:BoundField DataField="fono" HeaderText="Fono" />
                                            <asp:BoundField DataField="correo" HeaderText="Correo" />
                                            <asp:BoundField DataField="estado" HeaderText="Estado" />


                                            <asp:TemplateField HeaderText="Ver">
                                                <ItemTemplate>
                                                    <asp:Button ID="btVer" CssClass="button primary" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" Text="Ver" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </form>
                
            </div>
        </div>
    </div>
</body>
</html>
