<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="verComentarios.aspx.vb" Inherits="prueba_saec.verComentarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css"/>
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet"/>
    <link href="../../css/sb-admin-2.min.css" rel="stylesheet"/>
</head>
<body>

    <div class="container-fluid">
        <!-- Card de cada comentario -->
            <asp:Label runat="server" ID="lblTarjetaComentario" Text=""></asp:Label>
            <!-- ---------------------------------------- -->
        <label id="lblPrueba" runat="server" class="col-12" text=""></label>

    </div>

    <!-- Begin Page Content -->
    <div class="container-fluid">

        <!-- DataTales Example -->
        <div class="card shadow mb-4">
            <div class="card-header py-3">
                <h5 class="m-0 font-weight-bold text-primary">Comentarios</h5>
            </div>
            <div class="card-body">



                <form id="documentos" runat="server">
                    <div runat="server" id="seccionIngresarComentario">
                        <div class="card shadow mb-4">
                            <div class="card-body">
                                <div <%--class="col-sm-4"--%>>
                                    <textarea id="TxtAreaNuevoComentario" class="col-12" rows="3" runat="server"></textarea>
                                    <div class="row">
                                            <div class="col-12 d-flex justify-content-end">
                                                <div class="col-11"></div>
                                                <asp:Button ID="BtnVolver" runat="server" Text="Volver" class="btn btn-success btn-user ml-auto" />
                                                <asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" class="btn btn-success btn-user ml-auto" />
                                            </div>

                                        </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <!-- /.container-fluid -->
    <!-- Bootstrap core JavaScript-->
    <script src="../../vendor/jquery/jquery.min.js"></script>
    <script src="../../vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="../../vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom scripts for all pages-->
    <script src="../../js/sb-admin-2.min.js"></script>
</body>
</html>
