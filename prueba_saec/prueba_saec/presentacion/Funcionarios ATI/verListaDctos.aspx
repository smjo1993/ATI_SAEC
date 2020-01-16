<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="verListaDctos.aspx.vb" Inherits="prueba_saec.verListaDctos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">--%>

    <title>Lista De Requisitos Documentales</title>

    <!-- Custom fonts for this template-->
    <link href="../../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

    <!-- Custom styles for this template-->
    <link href="../../css/sb-admin-2.min.css" rel="stylesheet">

    <!-- Custom script y para tablas -->
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.10.20/datatables.min.css" />

    <%--Custom script para Checkbox--%>
    <link href="../../css/checkbox.css" rel="stylesheet" />

</head>

<body id="page-top">

    <!-- Page Wrapper -->
    <div id="wrapper">


        <ul class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" id="accordionSidebar">


            <a class="sidebar-brand d-flex align-items-center justify-content-center" href="">
                <div class="sidebar-brand-icon">

                    <%--<i class="fas fa-laugh-wink"></i>--%>

                    <img src="../../img/LOGO_BLANCO.png" alt="ATI LOGO" style="height:60px; width:60px"; >
                </div>
                <div class="sidebar-brand-text mx-3">SAEC</div>
            </a>

            <asp:Label ID="lblMenu" runat="server" Text=""></asp:Label>
        </ul>



        <div id="content-wrapper" class="d-flex flex-column">


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

                                <img class="img-profile rounded-circle" src="https://c7.uihere.com/files/25/400/945/computer-icons-industry-business-laborer-industrail-workers-and-engineers-thumb.jpg" style="height: 40px; width: 40px;">
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
                <div class="container-fluid">

                    <form runat="server">

                        <div class="card shadow mb-4">

                            <div class="card-header py-3">

                                <div class="row">
                                    <div class="col-6">


                                        <h4 class="m-0 font-weight text-primary">Requerimientos Documentales /  <span id="datetime"></span></h4>

                                        <script>var dt = new Date();
                                            document.getElementById("datetime").innerHTML = dt.toLocaleDateString();
                                        </script>
                                    </div>
                                    <div class="col-6 ">
                                        <a href="agregarDcto.aspx" class="btn shadow-sm btn-success" style="float: right;">

                                            <i class="far fa-plus-square"></i>
                                            Nuevo Documento
                                                        
                                        </a>
                                    </div>
                                </div>
                            </div>

                            <div class="card-body">

                                <div class="table-responsive">

                                    <asp:GridView ID="gridRequisitos"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        class="table table-bordered dataTable"
                                        Width="100%"
                                        CellSpacing="0"
                                        role="grid"
                                        aria-describedby="dataTable_info"
                                        Style="width: 100%;">

                                        <Columns>
                                            <asp:BoundField DataField="TB_SAEC_Documentoid" HeaderText="ID" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                            <asp:BoundField DataField="Area" HeaderText="Área" />
                                            <asp:BoundField DataField="Estado" HeaderText="Estado en Sistema" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                            <asp:TemplateField HeaderText="Estado">
                                                <ItemTemplate>
                                                    <label class="switch ">
                                                        <input runat="server" id="chkDocumento" type="checkbox" class="default" />
                                                        <span class="slider round"></span>
                                                    </label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edición">

                                                <ItemTemplate>
                                                    <asp:ImageButton
                                                        ID="btnVerDocumento"
                                                        ImageUrl="https://cdn4.iconfinder.com/data/icons/simplicio/32x32/file_edit.png"
                                                        CommandName="editar"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        runat="server" />

                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />

                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                    <!-- label de gridview vacia -->
                                    <div runat="server" id="lblsinRegistros">
                                        <h1 class="h3 mb-4 text-gray-800 text-center">No hay registros disponibles</h1>
                                    </div>


                                </div>
                            </div>
                            <%--card body--%>

                            <div class="card-footer">

                                <div class="row" style="float: right;">

                                    <input id="btnModalConfirmacion" type="button" class="btn shadow-sm btn-secondary btn-user" value="Realizar Cambios" data-toggle="modal"
                                        data-target="#modalConfirmacion" />

                                </div>

                            </div>
                        </div>
                        <%--card shadow--%>



                        <!-- Modal-->
                        <div class="modal fade" id="modalConfirmacion" tabindex="-1" role="dialog" aria-labelledby="lblModalConfirmacion" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="lblModalConfirmacion">Confirmación</h5>
                                        <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">×</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">¿Desea confirmar los cambios a la lista de Documentos?</div>

                                    <div class="modal-footer">
                                        <button class="btn btn-success shadow-sm" type="button" data-dismiss="modal">Cancelar</button>
                                        <asp:Button
                                            ID="btnRealizarCambios"
                                            runat="server"
                                            class="btn shadow-sm btn-secondary btn-user"
                                            Text="Aceptar" />
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
                        <%--<span>Copyright &copy; SAEC, CAPSTONE 2019</span>--%>
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

    <!-- Custom script y para tablas -->
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.20/datatables.min.js"></script>



</body>
</html>
