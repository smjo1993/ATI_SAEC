﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="historicoCarpetaArranque.aspx.vb" Inherits="prueba_saec.historicoCarpetaArranque" %>

<!DOCTYPE html>
<html lang="en">

<head runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Historico Carpeta de Arranque - SAEC</title>

    <!-- Custom fonts for this template -->
    <link href="../../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="../../css/sb-admin-2.min.css" rel="stylesheet">
    <link href="../../css/checkbox.css" rel="stylesheet">
    <style type="text/css">
        .auto-style1 {
            width: 984px;
        }
    </style>
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

                        
                        <%--<li class="nav-item dropdown no-arrow d-sm-none">

                            <a class="nav-link dropdown-toggle" href="#" id="searchDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="fas fa-search fa-fw"></i>
                            </a>
                            
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
                        </li>--%>

                        
                        <%--NOTIFICACIONES ALARMAS--%>
                        <li class="nav-item dropdown no-arrow mx-1">
                            <a class="nav-link dropdown-toggle" href="#" id="alertsDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="fas fa-bell fa-fw"></i>
                                
                                <span class="badge badge-danger badge-counter">2+</span>
                            </a>
                            
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


                       <%-- LISTA DE COMENTARIOS--%>
                        <li class="nav-item dropdown no-arrow mx-1">

                            <a class="nav-link dropdown-toggle" href="#" id="messagesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                <i class="fas fa-envelope fa-fw"></i>

                                <asp:Label ID="LblNotificacionComentarios" class="badge badge-danger badge-counter" runat="server" Text=""></asp:Label>
                            </a>
                            
                            <div class="dropdown-list dropdown-menu dropdown-menu-right shadow animated--grow-in" aria-labelledby="messagesDropdown">
                                <h6 class="dropdown-header">Comentarios
                                </h6>

                                <asp:Label ID="LblNotificacion" runat="server" Text=""></asp:Label>

                                
                                <%--<a class="dropdown-item text-center small text-gray-500" href="#">Read More Messages</a>--%>
                            </div>
                        </li>



                        <div class="topbar-divider d-none d-sm-block"></div>
                        
                        <li class="nav-item dropdown no-arrow">
                            <a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                <asp:Label ID="LblNombreUsuario" class="mr-2 d-none d-lg-inline text-gray-600" runat="server" Text=""></asp:Label>

                                <img class="img-profile rounded-circle" src="https://c7.uihere.com/files/25/400/945/computer-icons-industry-business-laborer-industrail-workers-and-engineers-thumb.jpg" style="height:40px;width:40px;">
                            </a>
                            
                            <div class="dropdown-menu dropdown-menu-right shadow animated--grow-in" aria-labelledby="userDropdown">

                                <%--<a class="dropdown-item" href="#">
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
                                <div class="dropdown-divider"></div>--%>
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
                <div class="container-fluid">

                    <!-- DataTales Example -->




                    <form id="documentos" runat="server">
                        <div>
                            <div class="card shadow mb-4">

                                <div class="card-header py-3">

                               <div class="row">
                                    <div class="col-4">
                                <h4 class="m-0 font-weight text-primary">Documentos:
                                <asp:Label ID="lblNombreEmpresa" runat="server" Text=""></asp:Label></h4>
                                    </div>
                                    <div class="col-4"></div>
                                    <div class="col-4">
                                        <a href="verCarpetas.aspx" class="btn shadow-sm btn-success" style="float: right;">Pagina Principal                                
                                        </a>
                                    </div>
                                </div>
                                </div>


                                <div class="card-body">

                                    <div runat="server" id="seccionEmpresa">
                                        <div class="card shadow mb-4">
                                            <div class="card-header py-3">
                                                <h4 class="font-weight text-primary">Documentos Empresa</h4>

                                            </div>
                                            <div class="card-body">
                                                <div class="table-responsive">

                                <asp:GridView ID="gridDocumentosEmpresa" runat="server" AutoGenerateColumns="False" class="table table-bordered dataTable" Width="100%" CellSpacing="0" role="grid" aria-describedby="dataTable_info" Style="width: 100%;">
                                    <Columns>

                                        <asp:BoundField DataField="nombreDoc" HeaderText="Documentos" />
                                        <asp:BoundField DataField="nombreArea" HeaderText="Área" />
                                        <asp:BoundField DataField="estadoDocumento" HeaderText="Estado" />
                                        <asp:BoundField DataField="idCarpeta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="idDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="idArea" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="tipoDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
<%--                                        <asp:BoundField DataField="rutEmp" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />--%>

                                        <asp:TemplateField HeaderText="Opciones">
                                            <ItemTemplate>
                                                        <asp:ImageButton
                                                            ID="btnDescargar"
                                                            ImageUrl="../../../img/download.png"
                                                            ToolTip="Ver"
                                                            CommandName="Ver"
                                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            runat="server" />
                                                &nbsp
                                                <asp:ImageButton
                                                    ID="btnVerComentarios"
                                                    ImageUrl="../../../img/chat.png"
                                                    ToolTip="Ver Comentarios"
                                                    CommandName="verComentarios"
                                                    OnClientClick=""
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    runat="server" />

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                                    <div runat="server" id="sinDocEmpresa">
                                                        <h1 class="h3 mb-4 text-gray-800 text-center">Sin Documentos en el Sistema</h1>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="seccionTrabajador">
                                        <div class="card shadow mb-4">
                                            <div class="card-header py-3">
                                                <h4 class="font-weight text-primary">Trabajadores</h4>
                                            </div>
                                            <div class="card-body">
                                                <div class="table-responsive">
                                <asp:GridView ID="gridListarTrabajadores" runat="server" AutoGenerateColumns="False" class="table table-bordered dataTable" Width="100%" CellSpacing="0" role="grid" aria-describedby="dataTable_info" Style="width: 100%;">

                                    <Columns>
                                        <asp:BoundField DataField="rut" HeaderText="RUT" />
                                        <asp:BoundField DataField="nombre" HeaderText="NOMBRE" />
                                        <asp:BoundField DataField="idCarpeta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="idTrabajador" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:TemplateField HeaderText="OPCIONES">

                                            <ItemTemplate>
                                                <asp:ImageButton
                                                    ID="btnIrTrabajador"
                                                    ImageUrl="../../../img/user.png"
                                                    CommandName="ir"
                                                    ToolTip="Ver Trabajador"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    runat="server" />

                        
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>
                                                    <div runat="server" id="sinTrabajadores">
                                                        <h1 class="h3 mb-4 text-gray-800 text-center">Sin Trabajadores ingresados en este periodo</h1>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="seccionVehiculo">
                                        <div class="card shadow mb-4">
                                            <div class="card-header py-3">
                                                <h4 class="font-weight text-primary">Documentos Vehiculo</h4>
                                            </div>
                                            <div class="card-body">
                                                <div class="table-responsive">
                                                <asp:GridView ID="gridListarVehiculos" runat="server" AutoGenerateColumns="False" class="table table-bordered dataTable" Width="100%" CellSpacing="0" role="grid" aria-describedby="dataTable_info" Style="width: 100%;">

                                                    <Columns>
                                                        <asp:BoundField DataField="patente" HeaderText="Patente" />
                                                        <asp:BoundField DataField="marca" HeaderText="Marca" />
                                                        <asp:BoundField DataField="idCarpeta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                        <asp:BoundField DataField="idVehiculo" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                        <asp:TemplateField HeaderText="Opciones">

                                                            <ItemTemplate>
                                                                <asp:ImageButton
                                                                    ID="btnIrVehiculo"
                                                                    ImageUrl="../../../img/delivery-truck.png"
                                                                    CommandName="ir"
                                                                    Tooltip="Ver Vehículo"
                                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>    
                                                                                            <div runat="server" id="sinVehiculos">
                                            <h1 class="h3 mb-4 text-gray-800 text-center">Sin Vehiculos ingresados en el sistema</h1>
                                        </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="card-footer">

                                    <div class="row" style="float: right;">

<%--                                        <input id="btnModalConfirmacion" type="button" class="btn shadow-sm btn-success btn-user" value="Pedir Documentos" data-toggle="modal"
                                            data-target="#modalConfirmacion" />--%>

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
                                            <div class="modal-body">¿Desea confirmar la lista de Documento?</div>

                                            <div class="modal-footer">
                                                <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancelar</button>

                                                <asp:Button ID="btnPedirDocumento" class="btn btn-success btn-user" runat="server" Text="Aceptar" />

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
                                        <button class="btn btn-secondary shadow-sm" type="button" data-dismiss="modal">Cancelar</button>
                                        <asp:Button
                                            ID="btnLogOut"
                                            runat="server"
                                            class="btn shadow-sm btn-success btn-user"
                                            Text="Aceptar" />

                                    </div>
                                </div>
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