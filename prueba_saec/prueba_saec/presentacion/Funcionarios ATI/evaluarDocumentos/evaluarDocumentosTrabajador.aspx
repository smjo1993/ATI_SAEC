﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="evaluarDocumentosTrabajador.aspx.vb" Inherits="prueba_saec.evaluarDocumentosTrabajador" %>

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

<body id="page-top">

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


                    <div class="container-fluid">
                                        <form id="form15" runat="server" class="md-form">

                        <%-- TRABAJADOR --%>
                        <div class="card shadow mb-4">
                            <div class="card-header py-3">
                                <div class="row">
                                    <div class="col-4">
                                        <h4 class="m-0 font-weight text-primary">Trabajador:                                        <asp:Label ID="lblTrabajador" runat="server" Text="Label" class="m-0 font-weight text-primary"></asp:Label></h4>

                                    </div>
                                    <div class="col-4"></div>
                                    <div class="col-4">
<asp:Label ID="lblVolver" runat="server" Text="Label"  style="float: right;"></asp:Label>
                                    </div>
                                </div>

                            </div>
                            <div class="card-body">
                                <div class="table-responsive-xl">
                                <div class="row">
                                    <div class="col-lg-4"></div>
                                    <div class="col-lg-4">
                                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-4"></div>
                                </div>

                                <asp:GridView ID="gridListarDocumentosTrabajador" runat="server" AutoGenerateColumns="False" class="table table-bordered dataTable" Width="100%" CellSpacing="0" role="grid" aria-describedby="dataTable_info" Style="width: 100%;" >

                                    <Columns>

                                        <asp:BoundField DataField="rutTrabajador" HeaderText="RUT" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="nombreTrabajador" HeaderText="NOMBRE" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="nombreDoc" HeaderText="Nombre del documento" />
                                        <asp:BoundField DataField="nombreArea" HeaderText="Área" />
                                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                                        <asp:BoundField DataField="idCarpeta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="idDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="idArea" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="tipoDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="idTrabajador" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="ruta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />

                                        <%--<asp:TemplateField HeaderText="VER">
                                            <ItemTemplate>

                                                <asp:ImageButton
                                                    ID="btnVerDocumento"
                                                    ImageUrl="../../../img/file.png"
                                                    CommandName="ver"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    runat="server" />

                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="COMENTARIOS">

                                            <ItemTemplate>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="APROBAR">
                                            <ItemTemplate>
                                                <asp:ImageButton
                                                    ID="btnAprobar"
                                                    ImageUrl=""
                                                    CommandName="aprobar"
                                                    OnClientClick="return confirm('¿Esta seguro de aprobar este documento?');"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="DESAPROBAR">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton
                                                    ID="btnReprobar"
                                                    ImageUrl=""
                                                    CommandName="reprobar"
                                                    OnClientClick="return confirm('¿Esta seguro de desaprobar este documento?');"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Opciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton
                                                            ID="btnVerDocumento"
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

                                        <asp:TemplateField HeaderText="Fecha de Expiración">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
<%--                                                <asp:TextBox ID="txtFecha" class=" form-control form-control-user" runat="server" TextMode="Date"></asp:TextBox>--%>
                                                <asp:TextBox ID="txtFecha" class=" form-control form-control-user" runat="server" placeholder="YYYY-MM-DD" pattern="(?:19|20)[0-9]{2}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-9])|(?:(?!02)(?:0[1-9]|1[0-2])-(?:30))|(?:(?:0[13578]|1[02])-31))" MaxLength="10"></asp:TextBox>
<%--                                            <asp:TextBox runat="server" ID="txtFecha" AutoPostBack="True" Width="65%"></asp:TextBox>--%>
<%--                                            <button class="button"><span class="mif-calendar"></span></button>
                                        </div>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="fechaDeExpiracion" HeaderText="FECHA DE EXPIRACIÓN"  />
                                         
                                    </Columns>

                                </asp:GridView>
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
                                        <h4 class="m-0 font-weight text-primary">                                       <asp:Label ID="Label1" runat="server" text="Documentos pendientes del Contratista" class="m-0 font-weight text-primary"></asp:Label></h4>

                                    </div>
                                    <div class="col-4"></div>
                                    <div class="col-4">
                                        
                                        <asp:Label ID="Label3" runat="server" ></asp:Label>
                                    </div>
                                </div>

                            </div>
                            <div class="card-body">
                                <div class="table-responsive-xl">
                                <div class="row">
                                    <div class="col-lg-4"></div>
                                    <div class="col-lg-4">
                                        
                                    </div>
                                    <div class="col-lg-4"></div>
                                </div>

                                <asp:GridView ID="gridDocumentosPendiente" runat="server" AutoGenerateColumns="False" class="table table-bordered dataTable" Width="100%" CellSpacing="0" role="grid" aria-describedby="dataTable_info" Style="width: 100%;">

                                    <Columns>

                                        <asp:BoundField DataField="rutTrabajador" HeaderText="RUT" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="nombreTrabajador" HeaderText="NOMBRE" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="nombreDoc" HeaderText="Nombre del documento" />
                                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                                        <asp:BoundField DataField="idCarpeta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="idDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="idArea" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="tipoDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="idTrabajador" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />
                                        <asp:BoundField DataField="ruta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />

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

                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>
                                                                        <div runat="server" id="sinDocPendientes">
                                            <h1 class="h3 mb-4 text-gray-800 text-center">Sin Documentos pendientes</h1>
                                        </div>
                                </div>
                            </div>
                                 <div class="card-footer"></div>    
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
            <footer class="sticky-footer bg-white">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span></span>
                    </div>
                </div>
            </footer>
        </div>
        </div>
    
                  <!-- Bootstrap core JavaScript-->
    <script src="../../../vendor/jquery/jquery.min.js"></script>
    <script src="../../../vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="../../../vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom scripts for all pages-->
    <script src="../../../js/sb-admin-2.min.js"></script>

    <!-- Page level plugins -->
    <script src="../../../vendor/datatables/jquery.dataTables.min.js"></script>
    <script src="../../../vendor/datatables/dataTables.bootstrap4.min.js"></script>

</body>


</html>
