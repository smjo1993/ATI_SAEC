<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="prueba_saec.Login" %>

<!DOCTYPE html>
<html lang="en">

<head runat="server">

   <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">

  <title>Login - SAEC</title>

  <!-- Custom fonts for this template-->
  <link href="../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
  <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

  <!-- Custom styles for this template-->
  <link href="../../css/sb-admin-2.min.css" rel="stylesheet">

</head>

<body class="bg-gradient-primary">

  <div class="container">

    <!-- Outer Row -->
    <div class="row justify-content-center">

      <div class="col-xl-10 col-lg-12 col-md-9">

        <div class="card o-hidden border-0 shadow-lg my-5">
          <div class="card-body p-0">
            <!-- Nested Row within Card Body -->
            <div class="row">
              <div class="col-lg-6 d-none d-lg-block bg-login-image"></div>
              <div class="col-lg-6">
                <div class="p-5">
                  <div class="text-center">
                    <h1 class="h4 text-gray-900 mb-4">Bienvenido  a SAEC</h1>
                  </div>
                  <form  runat="server" class="user">
                    <div class="form-group">
                        <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
                        <asp:TextBox ID="txtUsuario" runat="server" class="form-control bg-light border-0 small" required></asp:TextBox>
                    </div>
                    <div class="form-group">
                      <asp:Label ID="lblContrasenia" runat="server" Text="Contraseña:"></asp:Label>
                      <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password"  class="form-control bg-light border-0 small" required></asp:TextBox>
                    </div>
                    <div class="form-group">
                    <asp:Button ID="btnLogin" runat="server" class="btn btn-primary btn-user btn-block" Text="Inicia Sesion" />
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                    </div>
<%--                    <div class="text-center">
                    <asp:LinkButton ID="lblRecuperarContrasenia" runat="server">¿Olvido su contraseña? Click aqui para recuperarla</asp:LinkButton>
                  </div>--%>
                  </form>
                </div>
              </div>
            </div>
          </div>
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