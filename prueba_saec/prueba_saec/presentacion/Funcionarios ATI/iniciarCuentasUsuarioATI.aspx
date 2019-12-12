<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="iniciarCuentasUsuarioATI.aspx.vb" Inherits="prueba_saec.iniciarCuentasUsuarioATI" %>

<!DOCTYPE html>
<html lang="en">
<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Usuario ATI</title>

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

                    <!-- Sidebar Toggle (Topbar) -->
                    <button id="sidebarToggleTop" class="btn btn-link d-md-none rounded-circle mr-3">
                        <i class="fa fa-bars"></i>
                    </button>
                    <ul class="navbar-nav ml-auto">

                        <!-- Topbar Navbar -->


                        <!-- Nav Item - Search Dropdown (Visible Only XS) -->
                        <li class="nav-item dropdown no-arrow d-sm-none">
                            <%--                            <a class="nav-link dropdown-toggle" href="#" id="searchDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="fas fa-search fa-fw"></i>
                            </a>--%>
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
                        <div>
                            <div class="card shadow mb-4">

                                <div class="card-header py-3">
                                    <h5 class="m-0 font-weight text-primary" style="text-align: center;">Usuarios ATI</h5>
                                </div>
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <div class="dataTables_wrapper dt-bootstrap4">
                                            <div>
                                                <div class="row">
                                                    <div class="col-sm-12 col-md-6">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtBuscarNombre" runat="server" Class="col-12  form-control form-control-user" placeholder="Buscar  por Nombre"></asp:TextBox>
                                                            <div class="input-group-append">
                                                                <asp:LinkButton ID="lnkBuscarNombre" runat="server" class="btn btn-primary">
                                                                    <i class="fas fa-search fa-sm"></i>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 col-md-6">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtBuscarUsuario" runat="server" Class="col-12  form-control form-control-user" placeholder="Buscar por Usuario"></asp:TextBox>
                                                            <div class="input-group-append">
                                                                <asp:LinkButton ID="lnkBuscarUsuario" runat="server" class="btn btn-primary">
                                                                    <i class="fas fa-search fa-sm"></i>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div runat="server" id="Div1">
                                                    <h1 class="h3 mb-4 text-gray-800 text-center">
                                                        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></h1>
                                                </div>
                                                <asp:GridView ID="gridUsuariosATI" runat="server"
                                                    AutoGenerateColumns="False"
                                                    class="table table-bordered dataTable"
                                                    Width="100%"
                                                    CellSpacing="0"
                                                    role="grid"
                                                    aria-describedby="dataTable_info"
                                                    Style="width: 100%;">
                                                    <Columns>
                                                        <asp:BoundField DataField="NOMBRE_USUARIO" HeaderText="Nombre" />
                                                        <asp:BoundField DataField="LOGIN_USUARIO" HeaderText="Usuario" />
                                                        <asp:BoundField DataField="CLAVE_USUARIO" HeaderText="Estado" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                        <asp:BoundField DataField="RUT_USUARIO" HeaderText="Rut" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                        <asp:BoundField DataField="MAIL" HeaderText="E-mail" />
                                                        <asp:BoundField DataField="FONO" HeaderText="Fono de Contacto" />
                                                        <asp:BoundField DataField="AREA" HeaderText="Area" />
                                                        <asp:TemplateField HeaderText="CrearCuenta">
                                                            <ItemTemplate>
                                                                <asp:ImageButton
                                                                    ID="btnCrearCuenta"
                                                                    ImageUrl="https://cdn4.iconfinder.com/data/icons/simplicio/32x32/file_edit.png"
                                                                    CommandName="crearCuenta"
                                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                    runat="server" />

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <div runat="server" id="sinCoincidencia">
                                                    <h1 class="h3 mb-4 text-gray-800 text-center">No se encontraron coincidencias</h1>
                                                </div>
                                                <div runat="server" id="sinUsuarios">
                                                    <h1 class="h3 mb-4 text-gray-800 text-center">No hay usuarios en el sistema</h1>
                                                </div>
                                            </div>
                                        </div>
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
                                    <div class="modal-body">¿Desea confirmar el acceso a los permisos seleccionados?</div>

                                    <div class="modal-footer">
                                        <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancelar</button>

                                        <asp:Button ID="btnPermisos" class="btn btn-success btn-user" runat="server" Text="Aceptar" />

                                    </div>
                                </div>
                            </div>
                        </div>

                    </form>

                </div>
                <!-- /.container-fluid -->

            </div>
            <!-- End of Main Content -->
            <footer class="sticky-footer bg-white">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span></span>
                    </div>
                </div>
            </footer>
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
