﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="modificarContratista.aspx.vb" Inherits="prueba_saec.modificarContratista" %>

<!DOCTYPE html>
<html lang="en">

<head runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Modificación de Contratistas - SAEC</title>

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
                     <%--BARRA NAVEGACION--%>

                    <ul class="navbar-nav ml-auto">

                       <%-- LISTA DE COMENTARIOS Y ALARMAS--%>

                        <li class="nav-item dropdown no-arrow mx-1">
                            <a class="nav-link dropdown-toggle" href="#" id="messagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="fas fa-envelope fa-fw"></i>
                                <asp:Label ID="LblNotificacionComentarios" class="badge badge-danger badge-counter" runat="server" Text=""></asp:Label>
                            </a>                            
                            <div class="dropdown-list dropdown-menu dropdown-menu-right shadow animated--grow-in" aria-labelledby="messagesDropdown">
                                <h6 class="dropdown-header">Comentarios</h6>
                                <asp:Label ID="LblNotificacion" runat="server" Text=""></asp:Label>
                            </div>
                        </li>

                        <div class="topbar-divider d-none d-sm-block"></div>
                        
                        <li class="nav-item dropdown no-arrow">
                            <a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                <asp:Label ID="LblNombreUsuario" class="mr-2 d-none d-lg-inline text-gray-600" runat="server" Text=""></asp:Label>

                                <img class="img-profile rounded-circle" src="https://c7.uihere.com/files/25/400/945/computer-icons-industry-business-laborer-industrail-workers-and-engineers-thumb.jpg" style="height:40px;width:40px;">
                            </a>
                            
                            <div class="dropdown-menu dropdown-menu-right shadow animated--grow-in" aria-labelledby="userDropdown">
                                <a class="dropdown-item" href="#" data-toggle="modal" data-target="#modalLogout">
                                    <i class="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-gray-400"></i>
                                    Cerrar Sesión
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
                        <%--<h1 class="h3 mb-4 text-gray-800">Ver Contratista</h1>--%>

                        <form runat="server">

                            <div>
                                <div class="card shadow mb-4">
                                    <div class="card-header py-3">
                                        <h4 class="m-0 font-weight text-primary">Contratista</h4>
                                    </div>
                                    <div class="card-body">

                                        <div style="margin-bottom: 8px" class="row">
                                            <div class="col-sm-4">
                                                <label id="lblNombre" class="col-12">Nombre:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="TxtNombre" runat="server" required Class="form-control bg-light small col-12"></asp:TextBox>
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
                                                <label id="lblFono" class="col-12">Fono:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="TxtFono" runat="server" required Class="form-control bg-light small col-12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 8px" class="row">
                                            <div class="col-sm-4">
                                                <label id="lblCorreo" class="col-12">Correo:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="TxtCorreo" runat="server" required Class="form-control bg-light small col-12" pattern="^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"></asp:TextBox>
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
                                            <a ID="btnVolver" class="btn shadow-sm btn-success" style="float: right;" href="../Empresa/verContratistas.aspx">Volver</a>
                                            &nbsp;
                                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" class="btn shadow-sm btn-secondary" style="float: right;" />
                                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" class="btn shadow-sm btn-secondary" style="float: right;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                                                    <!-- Modal Logout-->
                        <div class="modal fade" id="modalLogout" tabindex="-1" role="dialog" aria-labelledby="lblModalConfirmacion" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="lblModalLogout">Confirmación</h5>
                                        <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">×</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">¿Desea cerrar sesión?</div>

                                    <div class="modal-footer">
                                        <button class="btn btn-success shadow-sm" type="button" data-dismiss="modal">Cancelar</button>
                                        <asp:Button
                                            ID="btnLogOut"
                                            runat="server"
                                            class="btn shadow-sm btn-secondary btn-user"
                                            CausesValidation="false" 
                                            formnovalidate="false"
                                            Text="Aceptar" />

                                    </div>
                                </div>
                            </div>
                        </div>
                        </form>
                    </div>
                    <!-- /.container-fluid -->
                </div>
                                <footer class="sticky-footer bg-white">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span></span>
                    </div>
                </div>
            </footer>
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
