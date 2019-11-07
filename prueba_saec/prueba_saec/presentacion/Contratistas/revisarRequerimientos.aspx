<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="revisarRequerimientos.aspx.vb" Inherits="prueba_saec.revisarRequerimientos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
    <link href="../../css/sb-admin-2.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">

        <div class="container-fluid">


            
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Poner algo aqui</h6>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                            <thead>
                                <tr>
                                    <th>DOCUMENTOS</th>
                                    <th>ÁREA</th>
                                    <th>ACEPTAR</th>
                                    <th>AGREGAR COMENTARIO (opcional)</th>
                                </tr>

                            </thead>
                            <tbody>
                                 <asp:Label ID="LblDocumentos" runat="server" Text=""></asp:Label>
                            </tbody>
                        </table>
                    </div>
                </div>
                
            </div>

        </div>
    </form>
</body>
</html>
