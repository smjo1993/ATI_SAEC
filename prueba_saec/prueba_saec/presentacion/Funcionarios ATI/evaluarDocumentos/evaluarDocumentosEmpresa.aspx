<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="evaluarDocumentosEmpresa.aspx.vb" Inherits="prueba_saec.verDocumentos" %>

<!DOCTYPE html>

<html lang="en">

<head runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Evaluar Documentos Empresa - SAEC</title>

    <!-- Custom fonts for this template -->
    <link href="../../../../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="../../css/sb-admin-2.min.css" rel="stylesheet">
    <link href="../../../../css/checkbox.css" rel="stylesheet">
    <style type="text/css">
        .auto-style1 {
            width: 984px;
        }
    </style>
    <!-- Custom styles for this page -->
    <link href="../../../../vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">
</head>

<body id="page-top">



    <!-- Page Wrapper -->
    <div id="wrapper">

        <!-- Sidebar  BARRA LATERAL DEL DASHBOARD-->
        <ul class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" id="accordionSidebar">

            <!-- Sidebar - Brand -->
            <a class="sidebar-brand d-flex align-items-center justify-content-center" href="">
                <div class="sidebar-brand-icon">
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
                    <form id="usuarios" runat="server">
                        <!-- DataTales Example -->

                        <div>

                            <div class="card shadow mb-4">

                                <div class="card-header py-3">
                                    <div class="row">
                                        <div class="col-4">
                                            <h4 class="m-0 font-weight text-primary">Revisar Documentos:
                                <asp:Label ID="lblNombreEmpresa" runat="server" Text=""></asp:Label></h4>
                                        </div>
                                        <div class="col-4">
                                        </div>
                                        <div class="col-4">
                                            <asp:Label ID="lblDocumentosVehiculo" runat="server" Text="" Style="float: right;"></asp:Label>
                                            <asp:Label ID="lblDocumentosTrabajdor" runat="server" Text="" Style="float: right;"></asp:Label>

                                        </div>
                                    </div>

                                </div>
                                <div class="card-body">
                                    <div class="table-responsive-xl">

                                        <!-- tabla con los documentos -->

                                        <div class="row">
                                            <div class="col-lg-4"></div>
                                            <div class="col-lg-4">
                                                <asp:Label ID="lblMensaje" runat="server" Text="Label"></asp:Label>
                                            </div>
                                            <div class="col-lg-4"></div>
                                        </div>

                                        <asp:GridView ID="gridDocumentos" runat="server"
                                            AutoGenerateColumns="False"
                                            class="table table-bordered dataTable"
                                            Width="100%"
                                            CellSpacing="0"
                                            role="grid"
                                            aria-describedby="dataTable_info"
                                            Style="width: 100%;">
                                            <Columns>
                                                <asp:BoundField DataField="idCarpeta" HeaderText="Id Carpeta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                <asp:BoundField DataField="idDocumento" HeaderText="Id Documento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                <asp:BoundField DataField="idArea" HeaderText="id Area" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                <asp:BoundField DataField="nombreArea" HeaderText="Área" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                <asp:BoundField DataField="nombreDoc" HeaderText="Nombre del documento" />
                                                <asp:BoundField DataField="estadoDocumento" HeaderText="Estado" />
                                                <asp:BoundField DataField="ruta" HeaderText="Ruta del Documento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />

                                                <asp:TemplateField HeaderText="Opciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton
                                                            ID="btnDescargar"
                                                            ImageUrl="../../../img/download.png"
                                                            ToolTip="Ver"
                                                            CommandName="Ver"
                                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            runat="server" />

                                                        <asp:ImageButton
                                                            ID="btnAprobar"
                                                            ImageUrl="../../../img/check.png"
                                                            ToolTip="Aprobar"
                                                            CommandName="Aprobar"
                                                            OnClientClick="return confirm('¿Esta seguro de aprobar este documento?');"
                                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            runat="server" />

                                                        <asp:ImageButton
                                                            ID="btnReprobar"
                                                            ImageUrl="../../../img/remove.png"
                                                            ToolTip="Rechazar"
                                                            CommandName="Reprobar"
                                                            OnClientClick="return confirm('¿Esta seguro de desaprobar este documento?');"
                                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            runat="server" />

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

                                                <asp:TemplateField HeaderText="Fecha de expiracion">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                     <asp:TextBox ID="txtFecha" class=" form-control form-control-user" runat="server" placeholder="YYYY-MM-DD" pattern="(?:19|20)[0-9]{2}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-9])|(?:(?!02)(?:0[1-9]|1[0-2])-(?:30))|(?:(?:0[13578]|1[02])-31))" MaxLength="10"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="fechaDeExpiracion" HeaderText="Fecha de Expiracion" />
                                            </Columns>
                                        </asp:GridView>

                                        <!-- seccion que muestra si no hay documentos en estado enviado -->
                                        <div runat="server" id="sinDocumentos">
                                            <h1 class="h3 mb-4 text-gray-800 text-center">Sin Documentos para revisar</h1>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer"></div>
                            </div>




                            <div class="card shadow mb-4">

                                <div class="card-header py-3">
                                    <div class="row">
                                        <div class="col-4">
                                            <h4 class="m-0 font-weight text-primary">Documentos Pendientes:
                                <asp:Label ID="lblNombreEmpresa2" runat="server" Text=""></asp:Label></h4>
                                        </div>
                                        <div class="col-4">
                                        </div>
                                        <div class="col-4">
                                        </div>
                                    </div>

                                </div>
                                <div class="card-body">
                                    <div class="table-responsive">

                                        <!-- tabla con los documentos -->



                                        <asp:GridView ID="gridDocumentosPendientes" runat="server"
                                            AutoGenerateColumns="False"
                                            class="table table-bordered dataTable"
                                            Width="100%"
                                            CellSpacing="0"
                                            role="grid"
                                            aria-describedby="dataTable_info"
                                            Style="width: 100%;">
                                            <Columns>
                                                <asp:BoundField DataField="idCarpeta" HeaderText="Id Carpeta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                <asp:BoundField DataField="idDocumento" HeaderText="Id Documento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                <asp:BoundField DataField="idArea" HeaderText="id Area" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                <asp:BoundField DataField="nombreArea" HeaderText="Nombre Area" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                <asp:BoundField DataField="nombreDoc" HeaderText="Nombre Documento" />
                                                <asp:BoundField DataField="estadoDocumento" HeaderText="Estado" />
                                                <asp:BoundField DataField="ruta" HeaderText="Ruta del Documento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                                <asp:TemplateField HeaderText="Comentarios">
                                                    <ItemTemplate>
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

                                        <!-- seccion que muestra si no hay documentos en estado enviado -->
                                        <div runat="server" id="sinDocPendientes">
                                            <h1 class="h3 mb-4 text-gray-800 text-center">Sin Documentos pendientes</h1>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer"></div>
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

            </div>
            <!-- /.container-fluid -->
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

    <!-- End of Page Wrapper -->
    <!-- Scroll to Top Button-->
    <a class="scroll-to-top rounded" href="#page-top">
        <i class="fas fa-angle-up"></i>
    </a>

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

    <script>
        $(document).ready(function () {
            //FORMATO DE MASCARAS
            $('#text1').mask('SSSSS');
            $('#txtFecha').mask('00/00/0000');
            $('#text3').mask('(000) 0000-0000', { placeholder: '(000) 0000-0000' }); //placeholder
            $('#text4').mask('-99999999999999999.00', {
                //opciones
                placeholder: '[-]000[.00]',
                translation: {
                    '-': { pattern: /[-]/, optional: true }
                }
            });
        });
    </script>
</body>

</html>
