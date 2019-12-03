<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="crearCuentaSAEC.aspx.vb" Inherits="prueba_saec.crearCuentaSAEC" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html lang="en">
<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>SB Admin 2 - 404</title>

    <!-- Custom fonts for this template-->
    <link href="../../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
    <link href="../../css/checkbox.css" rel="stylesheet">
    <!-- Custom styles for this template-->
    <link href="../../css/sb-admin-2.min.css" rel="stylesheet">
</head>

<body id="page-top">

    <!-- Page Wrapper -->
    <div id="wrapper">

        <!-- Sidebar -->
        <ul class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" id="accordionSidebar">

            <!-- Sidebar - Brand -->
            <a class="sidebar-brand d-flex align-items-center justify-content-center" href="index.html">
                <div class="sidebar-brand-icon rotate-n-15">
                    <i class="fas fa-laugh-wink"></i>
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

                    <!-- Sidebar Toggle (Topbar) -->
                    <button id="sidebarToggleTop" class="btn btn-link d-md-none rounded-circle mr-3">
                        <i class="fa fa-bars"></i>
                    </button>
                    <ul class="navbar-nav ml-auto">

                        <!-- Topbar Navbar -->


                        <!-- Nav Item - Search Dropdown (Visible Only XS) -->
                        <li class="nav-item dropdown no-arrow d-sm-none">
                            <a class="nav-link dropdown-toggle" href="#" id="searchDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="fas fa-search fa-fw"></i>
                            </a>
                            <!-- Dropdown - Messages -->
                            <div class="dropdown-menu dropdown-menu-right p-3 shadow animated--grow-in" aria-labelledby="searchDropdown">
                                <form class="form-inline mr-auto w-100 navbar-search">
                                    <div class="input-group">
                                        <input type="text" class="form-control bg-light border-0 small" placeholder="Search for..." aria-label="Search" aria-describedby="basic-addon2">
                                        <div class="input-group-append">
                                            <button class="btn btn-primary" type="button">
                                                <i class="fas fa-search fa-sm"></i>
                                            </button>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </li>

                    </ul>

                </nav>
                <!-- End of Topbar -->

                <!-- Begin Page Content -->
                <div class="container-fluid">

                    <form id="usuarios" runat="server">
                        <!-- DataTales Example -->
                        <div class="card shadow mb-4">



                            <div>
                                <div class="card-header py-3">
                                    <h5 class="m-0 font-weight-bold text-primary" style="text-align: center;">
                                        <asp:Label ID="lblModulo" runat="server" Text="Crear usuario"></asp:Label></h5>
                                </div>
                                <div class="card-body">
                                    <div class="row form-group">
                                        <div style="margin-top: 0px" class="col-4">
                                            <label id="lblNombre" class="col-12">Nombre Usuario:</label>
                                        </div>
                                        <div class="col-6">
                                            <asp:TextBox ID="txtNombreUsuario" runat="server" Class="col-12 form-control bg-light border-0 small"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div style="margin-top: 0px" class="col-4">
                                            <label id="lblLogin" class="col-12">Login Usuario:</label>
                                        </div>
                                        <div class="col-6">
                                            <asp:TextBox ID="txtLogin" runat="server" Class="col-12 form-control bg-light border-0 small"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div style="margin-top: 0px" class="col-4">
                                            <label id="lblClave" class="col-12">Clave Usuario:</label>
                                        </div>
                                        <div class="col-6">
                                            <asp:TextBox ID="txtClave" runat="server" Class="col-12 form-control bg-light border-0 small"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div style="margin-top: 0px" class="col-4">
                                            <label id="lblRur" class="col-12">Rut Usuario:</label>
                                        </div>
                                        <div class="col-6">
                                            <asp:TextBox ID="txtRut" runat="server" Class="col-12 form-control bg-light border-0 small"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div style="margin-top: 0px" class="col-4">
                                            <label id="lblEmail" class="col-12">E-mail:</label>
                                        </div>
                                        <div class="col-6">
                                            <asp:TextBox ID="txtEmail" runat="server" Class="col-12 form-control bg-light border-0 small"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div style="margin-top: 0px" class="col-4">
                                            <label id="lblFono" class="col-12">Telefono:</label>
                                        </div>
                                        <div class="col-6">
                                            <asp:TextBox ID="txtFono" runat="server" Class="col-12 form-control bg-light border-0 small"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-4"></div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <input id="btnModalConfirmacion" type="button" class="btn btn-primary btn-user btn-block" value="Crear Cuenta" data-toggle="modal"
                                                data-target="#modalConfirmacion" />
                                        </div>
                                    </div>
                                    <div class="col-4"></div>
                                </div>
                            </div>
                        </div>

                        <!--Modal-->
                        <div class="modal fade" id="modalConfirmacion" tabindex="-1" role="dialog" aria-labelledby="lblModalConfirmacion" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="lblModalConfirmacion">Confirmación</h5>
                                        <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">×</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">¿Desea crear cuenta a este Usuario?</div>

                                    <div class="modal-footer">
                                        <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancelar</button>

                                        <asp:Button ID="btnCrearCuenta" class="btn btn-success btn-user" runat="server" Text="Aceptar" />

                                    </div>
                                </div>
                            </div>
                        </div>

                    </form>

                </div>
                <!-- /.container-fluid -->

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

</body>

</html>
