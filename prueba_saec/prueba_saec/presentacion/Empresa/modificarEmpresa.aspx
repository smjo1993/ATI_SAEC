<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="modificarEmpresa.aspx.vb" Inherits="prueba_saec.modificarEmpresa" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>--%>

<!DOCTYPE html>
<html lang="en">

<head runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Modificación de Empresas - SAEC</title>

    <!-- Custom fonts for this template -->
    <link href="../../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="../../css/sb-admin-2.min.css" rel="stylesheet">

    <!-- Custom styles for this page -->
    <link href="../../vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">
</head>

<body id="page-top">

    <!-- Page Wrapper -->
    <div id="wrapper">

        <!-- Sidebar  BARRA LATERAL DEL DASHBOARD-->
        <ul class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" id="accordionSidebar">

            <!-- Sidebar - Brand -->
            <a class="sidebar-brand d-flex align-items-center justify-content-center" href="">
                <div class="sidebar-brand-icon">

                    <%--<i class="fas fa-laugh-wink"></i>--%>

                    <img src="../../img/LOGO_BLANCO.png" alt="ATI LOGO" style="height:60px; width:60px"; >

                </div>
                <div class="sidebar-brand-text mx-3">SAEC</div>
            </a>
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
 <%--                           <a class="nav-link dropdown-toggle" href="#" id="searchDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
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

                <!-- Begin Page Content -->
                <div>
                    <div class="container-fluid">

                        <!-- Page Heading -->
                        <%--<h1 class="h3 mb-4 text-gray-800">Modificar Empresa Contratista</h1>--%>

                        <form runat="server">

                            <div>
                                <%--<div class="card shadow mb-4">
                                    <div class="card-body">

                                        <div class="row">
                                            <div class="col-sm-4">
                                                <h5 class="font-weight-bold text-primary">Elija una empresa:</h5>
                                            </div>
                                            <div class="col-6">
                                                <asp:DropDownList ID="dropEmpresas" runat="server" class="col-12" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="card shadow mb-4">
                                    <div class="card-header py-3">
                                        <h4 class="m-0 font-weight text-primary">Empresa Contratista</h4>
                                    </div>
                                    <div class="card-body">
                                        <div style="margin-bottom: 8px" class="row">
                                            <div class="col-sm-4">
                                                <label id="lblRazonSocial" class="col-12">Razón Social:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="TxtRazonSocial" runat="server" required Class="form-control bg-light small col-12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 8px" class="row">
                                            <div class="col-sm-4">
                                                <label id="lblRut" class="col-12">Rut:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="TxtRut" runat="server" required Class="form-control bg-light small col-12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 8px" class="row">
                                            <div class="col-sm-4">
                                                <label id="lblGiro" class="col-12">Giro:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="TxtGiro" runat="server" required Class="form-control bg-light small col-12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 8px" class="row">
                                            <div class="col-sm-4">
                                                <label id="lblDireccion" class="col-12">Dirección:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="TxtDireccion" runat="server" required Class="form-control bg-light small col-12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 8px" class="row">
                                            <div class="col-sm-4">
                                                <label id="lblCiudad" class="col-12">Ciudad:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="TxtCiudad" runat="server" required Class="form-control bg-light small col-12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 8px" class="row">
                                            <div class="col-sm-4">
                                                <label id="lblFono" class="col-12">Fono:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="TxtFono" runat="server" required Class="form-control bg-light small col-12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 8px" class="row">
                                            <div class="col-sm-4">
                                                <label id="lblCelular" class="col-12">Celular:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="TxtCelular" runat="server" required Class="form-control bg-light small col-12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 8px" class="row">
                                            <div class="col-sm-4">
                                                <label id="lblCorreo" class="col-12">Correo:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="TxtCorreo" runat="server" required Class="form-control bg-light small col-12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 8px" class="row">
                                            <div class="col-sm-4">
                                                <label id="lblEncargado" class="col-12">Encargado:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:DropDownList ID="DropEncargados" runat="server" required class="btn btn-light bg-light dropdown-toggle col-12"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div>
                                                <p>
                                                    <asp:Label ID="LblAdvertencia" runat="server" Text=""></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer">
                                        <div class="row" style="float: right;">
                                            <a ID="btnVolver" class="btn shadow-sm btn-success" style="float: right;" href="verEmpresas.aspx">Volver</a>
                                            &nbsp;
                                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" class="btn shadow-sm btn-success" style="float: right;" />
                                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" class="btn shadow-sm btn-success" style="float: right;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                    <!-- /.container-fluid -->
                </div>
            </div>
            <!-- End of Main Content -->

        </div>
        <!-- End of Content Wrapper -->

    </div>
    <!-- End of Page Wrapper -->

    <!-- Scroll to Top Button-->
    <a class="scroll-to-top rounded" href="#page-top">
        <i class="fas fa-angle-up"></i>
    </a>

    <!-- Logout Modal-->
    <div class="modal fade" id="logoutModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Ready to Leave?</h5>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">Select "Logout" below if you are ready to end your current session.</div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancel</button>
                    <a class="btn btn-primary" href="login.html">Logout</a>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap core JavaScript-->
    <script src="../../vendor/jquery/jquery.min.js"></script>
    <script src="../../vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="../../vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom scripts for all pages-->
    <script src="../../js/sb-admin-2.min.js"></script>

    <!-- Page level plugins -->
    <script src="../../vendor/datatables/jquery.dataTables.min.js"></script>
    <script src="../../vendor/datatables/dataTables.bootstrap4.min.js"></script>

    <!-- Page level custom scripts -->
    <script src="../../js/demo/datatables-demo.js"></script>

</body>

</html>
