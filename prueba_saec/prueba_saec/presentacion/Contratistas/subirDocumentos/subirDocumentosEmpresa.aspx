<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="subirDocumentosEmpresa.aspx.vb" Inherits="prueba_saec.subirDocumentosEmpresa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Subir Documentación - SAEC</title>

    <link href="../../../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
    <link href="../../../css/sb-admin-2.min.css" rel="stylesheet">
    <link href="../../../css/checkbox.css" rel="stylesheet">
</head>
<body>
    <div id="wrapper">

        <!-- Sidebar  BARRA LATERAL DEL DASHBOARD-->
        <ul class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" id="accordionSidebar">

            <!-- Sidebar - Brand -->
            <a class="sidebar-brand d-flex align-items-center justify-content-center" href="">
                <div class="sidebar-brand-icon">

                    <%--<i class="fas fa-laugh-wink"></i>--%>

                    <img src="../../../img/LOGO_BLANCO.png" alt="ATI LOGO" style="height:60px; width:60px"; >

                </div>
                <div class="sidebar-brand-text mx-3">SAEC</div>
            </a>

            <!-- Divider -->

            <asp:Label ID="lblMenu" runat="server" Text=""></asp:Label>

        </ul>
        <!-- End of Sidebar -->

        <!-- Content Wrapper -->
        <div id="content-wrapper" class="d-flex flex-column">
            <!-- Main Content -->
            <div id="content">

                <!-- Topbar -->
                <nav class="navbar navbar-expand navbar-light bg-white topbar mb-4 static-top shadow">

                    <!----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
                    <!-- Sidebar Toggle (Topbar) -->
                              <button id="sidebarToggleTop" class="btn btn-link d-md-none rounded-circle mr-3">
            <i class="fa fa-bars"></i>
          </button>
                    <!-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
                    <!-- Topbar Navbar -->
                    <ul class="navbar-nav ml-auto">

                        <!-- Nav Item - Search Dropdown (Visible Only XS) -->
                        <li class="nav-item dropdown no-arrow d-sm-none">
<%--                            <a class="nav-link dropdown-toggle" href="#" id="searchDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="fas fa-search fa-fw"></i>
                            </a>--%>
                            <!-- Dropdown - Messages -->
                            <div class="dropdown-menu dropdown-menu-right p-3 shadow animated--grow-in" aria-labelledby="searchDropdown">
                            </div>
                        </li>

                        <!-- Nav Item - Alerts -->
                        <li class="nav-item dropdown no-arrow mx-1">

                            <!-- Dropdown - Alerts -->
                            <div class="dropdown-list dropdown-menu dropdown-menu-right shadow animated--grow-in" aria-labelledby="alertsDropdown">
                                <h6 class="dropdown-header">Alerts Center
                                </h6>
                                <a class="dropdown-item d-flex align-items-center" href="#">
                                    <div class="mr-3">
                                        <div class="icon-circle bg-primary">
                                            <i class="fas fa-file-alt text-white"></i>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="small text-gray-500">December 12, 2019</div>
                                        <span class="font-weight-bold">A new monthly report is ready to download!</span>
                                    </div>
                                </a>
                                <a class="dropdown-item d-flex align-items-center" href="#">
                                    <div class="mr-3">
                                        <div class="icon-circle bg-success">
                                            <i class="fas fa-donate text-white"></i>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="small text-gray-500">December 7, 2019</div>
                                        $290.29 has been deposited into your account!
                                    </div>
                                </a>
                                <a class="dropdown-item d-flex align-items-center" href="#">
                                    <div class="mr-3">
                                        <div class="icon-circle bg-warning">
                                            <i class="fas fa-exclamation-triangle text-white"></i>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="small text-gray-500">December 2, 2019</div>
                                        Spending Alert: We've noticed unusually high spending for your account.
                                    </div>
                                </a>
                                <a class="dropdown-item text-center small text-gray-500" href="#">Show All Alerts</a>
                            </div>
                        </li>

                        <!-- Nav Item - Messages -->

                        <%--<div class="topbar-divider d-none d-sm-block"></div>--%>

                        <!-- Nav Item - User Information -->
                        <li class="nav-item dropdown no-arrow">
                            <!-- Dropdown - User Information -->
                            <div class="dropdown-menu dropdown-menu-right shadow animated--grow-in" aria-labelledby="userDropdown">
                                <a class="dropdown-item" href="#">
                                    <i class="fas fa-user fa-sm fa-fw mr-2 text-gray-400"></i>
                                    Profile
                                </a>
                                <a class="dropdown-item" href="#">
                                    <i class="fas fa-cogs fa-sm fa-fw mr-2 text-gray-400"></i>
                                    Settings
                                </a>
                                <a class="dropdown-item" href="#">
                                    <i class="fas fa-list fa-sm fa-fw mr-2 text-gray-400"></i>
                                    Activity Log
                                </a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item" href="#" data-toggle="modal" data-target="#logoutModal">
                                    <i class="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-gray-400"></i>
                                    Logout
                                </a>
                            </div>
                        </li>

                    </ul>

                </nav>
                <!-- End of Topbar -->


                    <div class="container-fluid">

                                        <form id="form15" runat="server" class="md-form">
                        <%-- EMPRESA --%>
                        <div class="card shadow mb-4">
                            <div class="card-header py-3">



                                                                                        <div class="row">
                                        <div class="col-4">
                                                                            <h4 class="m-0 font-weight text-primary">EMPRESA</h4>
                                        </div>
                                                                                            <div class="col-4"></div>
                                        <div class="col-4" style="float: right;">
                                            <asp:Label ID="lblDocumentosTrabajador" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lblDocumentosVehiculo" runat="server" Text=""></asp:Label>
                          
                                        </div>
                                    </div>

                            </div>
                            <div class="card-body">
                                <asp:GridView ID="gridSubirDocumentosEmpresa" runat="server" AutoGenerateColumns="False" class="table table-bordered dataTable" Width="100%" CellSpacing="0" role="grid" aria-describedby="dataTable_info" Style="width: 100%;">
                                    <Columns>

                                        <asp:BoundField DataField="nombreDoc" HeaderText="DOCUMENTOS" />
                                        <asp:BoundField DataField="nombreArea" HeaderText="ÁREA" />
                                        <asp:BoundField DataField="estado" HeaderText="ESTADO" />
                                        <asp:BoundField DataField="idCarpeta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="idDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="idArea" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="tipoDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="rutEmp" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />

                                        <asp:TemplateField HeaderText="Buscar">

                                            <ItemTemplate>

                                                <div class="form-group">
                                                    <input type="file" class="form-control-file" id="fileArchivo" runat="server" />
                                                </div>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="COMENTARIOS">
                                            <ItemTemplate>
                                                <asp:Button ID="Button1" runat="server" Text="Button" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CONFIRMAR">
                                            <ItemTemplate>
                                                <asp:Button ID="btnSubir"
                                                    CssClass="button primary" CommandName="subir"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    runat="server"
                                                    Text="Subir" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                                                        <div runat="server" id="sinDocumentos">
                                            <h1 class="h3 mb-4 text-gray-800 text-center">Sin Documentos para subir</h1>
                                        </div>
                            </div>
                              <div class="card-footer"></div>    
                        </div>
                                        </form>
                    </div>

            </div>
                            <footer class="sticky-footer bg-white">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span></span>
                    </div>
                </div>
            </footer>
        </div>

    </div>
   
        <a class="scroll-to-top rounded" href="#page-top">
        <i class="fas fa-angle-up"></i>
    </a>

    <script>
</script>
    <!-- Bootstrap core JavaScript-->
    <script src="../../../../vendor/jquery/jquery.min.js"></script>
    <script src="../../../../vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="../../../../vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom scripts for all pages-->
    <script src="../../../../js/sb-admin-2.min.js"></script>

    <!-- Page level plugins -->
    <script src="../../../../vendor/datatables/jquery.dataTables.min.js"></script>
    <script src="../../../../vendor/datatables/dataTables.bootstrap4.min.js"></script>

    <!-- Page level custom scripts -->
    <script src="../../../../js/demo/datatables-demo.js"></script>
</body>


</html>
