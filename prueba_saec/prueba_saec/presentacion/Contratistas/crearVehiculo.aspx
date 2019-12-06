<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="crearVehiculo.aspx.vb" Inherits="prueba_saec.crearVehiculo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Registro de Vehículos - SAEC</title>

    <!-- Custom fonts for this template-->
    <link href="../../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

    <!-- Custom styles for this template-->
    <link href="../../css/sb-admin-2.min.css" rel="stylesheet">

    <!-- Bootstrap core JavaScript-->
    <script src="../../vendor/jquery/jquery.min.js"></script>
    <script src="../../vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="../../vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom scripts for all pages-->
    <script src="../../js/sb-admin-2.min.js"></script>

</head>

<body id="page-top">

    <!-- Page Wrapper -->
    <div id="wrapper">


        <ul class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" id="accordionSidebar">


            <a class="sidebar-brand d-flex align-items-center justify-content-center" href="">
                <div class="sidebar-brand-icon rotate-n-15">
                    <i class="fas fa-laugh-wink"></i>
                </div>
                <div class="sidebar-brand-text mx-3">SAEC</div>
            </a>

            <asp:Label ID="lblMenu" runat="server" Text=""></asp:Label>
        </ul>



        <div id="content-wrapper" class="d-flex flex-column">


            <div id="content">

                <nav class="navbar navbar-expand navbar-light bg-white topbar mb-4 static-top shadow">


                    <button id="sidebarToggleTop" class="btn btn-link d-md-none rounded-circle mr-3">
                        <i class="fa fa-bars"></i>
                    </button>
                </nav>
                <!-- End of Topbar -->

                <!-- Begin Page Content -->
                <div class="container-fluid">

                    <!-- Page Heading -->
                     <h1 class="h3 mb-4 text-gray-800">Registro de Vehículos</h1>

                    <form runat="server">
                        <div>
                            <div class="card shadow mb-4">
                                <div class="card-header py-3" runat="server">
                                    <h4 class="m-0 font-weight text-primary">
                                        <asp:Label ID="lblHeadEdicion" runat="server" Text=""></asp:Label>

                                    </h4>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <label id="lblPatente" class="col-12">Patente:</label>
                                        </div>
                                        <div class="col-6">

                                            <asp:TextBox ID="TxtPatente"
                                                runat="server"
                                                Style="height: 30px"
                                                placeholder=""
                                                required
                                                Class="form-control bg-light small col-12">
                                            </asp:TextBox>

                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <label id="lblMarca" class="col-12">Marca:</label>
                                        </div>
                                        <div class="col-6">

                                            <asp:TextBox ID="TxtMarca"
                                                runat="server"
                                                Style="height: 30px"
                                                placeholder=""
                                                required
                                                Class="form-control bg-light small col-12">
                                            </asp:TextBox>

                                        </div>
                                    </div>
                                    <br />
                                    <br />


                                    <div class="row">
                                        <div>
                                            <p>
                                                <asp:Label ID="lblAdvertencia" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-footer">

                                    <div class="row" style="float: right;">

                                        <a class="btn btn-secondary" href="subirDocumentos/listarVehiculos.aspx">Volver</a>

                                        &nbsp;

                                        <input id="btnModalConfirmacion" type="button" class="btn btn-success btn-user" value="Aceptar" data-toggle="modal"
                                            data-target="#modalConfirmacion" />

                                    </div>

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
                                    <div class="modal-body">¿Desea confirmar el registro?</div>

                                    <div class="modal-footer">
                                        <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancelar</button>
                                        <asp:Button
                                            ID="btnRealizarRegistro"
                                            runat="server"
                                            class="btn btn-success btn-user"
                                            Text="Aceptar" />


                                    </div>
                                </div>
                            </div>
                        </div>

                    </form>
                </div>
                <!-- /.container-fluid -->
            </div>
            <!-- End of Main Content -->

            <!-- Footer -->
            <footer class="sticky-footer bg-white">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span>Copyright &copy; SAEC, CAPSTONE 2019</span>
                    </div>
                </div>
            </footer>
            <!-- End of Footer -->
        </div>
        <!-- End of Content Wrapper -->
    </div>
    <!-- End of Page Wrapper -->

    <!-- Scroll to Top Button-->
    <a class="scroll-to-top rounded" href="#page-top">
        <i class="fas fa-angle-up"></i>
    </a>


</body>
</html>
