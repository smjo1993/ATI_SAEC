<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ambiente.aspx.vb" Inherits="APP_ATI.ambiente" %>

<html lang="en">
<head>
    <meta charset="utf-8" />
    <link rel="apple-touch-icon" sizes="76x76" href="img/favicon.png">
    <link rel="shortcut icon" href="img/favicon.png" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>ATI Report</title>

    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' name='viewport' />
    <meta name="viewport" content="width=device-width" />

    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/paper-kit.css?v=2.1.0" rel="stylesheet" />
    <link href="assets/css/demo.css" rel="stylesheet" />

    <!--     Fonts and icons     -->
    <link href='https://fonts.googleapis.com/css?family=Montserrat:400,300,700' rel='stylesheet' type='text/css'>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" rel="stylesheet">
    <link href="assets/css/nucleo-icons.css" rel="stylesheet">
    <link rel="shortcut icon" href="https://www.atiport.cl/Saam.Web.Metronic/theme/assets/custom/images/favicon.ico" />

    <script lang="javascript" type="text/javascript">
<%--        var size = 2;
        var id = 0;

        function ProgressBar() {
            if (document.getElementById('<%=fileFoto1.ClientID %>').value != "") {
                document.getElementById("divProgress").style.display = "block";
                document.getElementById("divUpload").style.display = "block";
                id = setInterval("progress()", 20);
                return true;
            }
            else {
                alert("Select a file to upload");
                return false;
            }

        }

        function progress() {
            size = size + 1;
            if (size > 1000) {
                clearTimeout(id);
            }
            document.getElementById("divProgress").style.width = size + "pt";
            document.getElementById("<%=lblPercentage.ClientID %>").firstChild.data = parseInt(size / 3) + "%";
        }--%>

        function Loading() {
            if (document.getElementById("txtNombreEmpresa").value != "" && document.getElementById("txtEmail").value != "" && document.getElementById("txtLugar").value != "" && document.getElementById("txtDescripcion").value != "") {
                var path = "img/loading.gif"; //-->Editar la ruta

                var img = document.createElement('img');
                img.setAttribute("src", path);
                img.setAttribute("width", "71");
                img.setAttribute("height", "71");
                document.getElementById("cargando").innerHTML = "";
                document.getElementById("cargando").appendChild(img);
            }
        }

    </script>
</head>
<body class="full-screen register">
    <div class="wrapper">
        <div class="page-header" style="background-image: url('img/bg.png');">
            <div class="filter">
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-md-6 col-sm-7 col-12 ml-auto">
                        <div class="info info-horizontal">
                            <div class="description ">
                                <img src="img/logo.png" alt="" />
                            </div>
                        </div>
                        <div class="info info-horizontal">
                            <%--<div class="icon">
                                <a class="btn btn-primary " href="ayuda.aspx">Ver guía</a>
                            </div>--%>
                            <div>
                                <h3>¿Que Reportar? </h3>
                                 <p>Te presentamos una pequeña guía para que sepas que reportar.</p>
                                 <a class="btn btn-primary ml-2" href="ayuda.aspx">Ver guía</a>
                            </div>
                        </div>
                        <div class="info info-horizontal">
                            <div class="icon">
                                <i class="fa fa-paper-plane"></i>
                            </div>
                            <div class="description">
                                <h3>Olvídate del papel </h3>
                                <p>Sin necesidad de enviarnos el papel. Haz tu reporte ambiental desde dónde estés.</p>
                            </div>
                        </div>
                        <div class="info info-horizontal">
                            <div class="icon">
                                <i class="fa fa-mobile-phone"></i>
                            </div>
                            <div class="description">
                                <h3>Registro en Terreno</h3>
                                <p>Desde la oficina o en Terreno. Fácil y rápido.</p>
                            </div>
                        </div>
                        <div class="info info-horizontal">
                            <div class="icon">
                                <i class="fa fa-tree"></i>
                            </div>
                            <div class="description">
                                <h3>Cuidado Ambiental</h3>
                                <p>Entre todos podemos hacer de este mundo un mejor lugar para vivir.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-5 col-12 mr-auto">
                        <div class="card card-register">
                            <h3 class="card-title text-center"><b>Registro</b></h3>
                            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                            <form id="form1" runat="server" class="form-signin">
                                <asp:DropDownList runat="server" ID="dropTipo" class="form-control" required>
                                    <asp:ListItem Value="MA">Medio Ambiente</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox runat="server" TextMode="SingleLine" ID="txtNombreEmpresa" class="form-control" placeholder="Nombre Completo" required />
                                <asp:TextBox runat="server" TextMode="SingleLine" ID="txtEmail" class="form-control" placeholder="E-Mail contacto" required />
                                <asp:TextBox runat="server" TextMode="SingleLine" ID="txtLugar" class="form-control" placeholder="Lugar del hallazgo" required />
                                <span class="help-block">
                                    <i class="fa fa-btn fa-user"></i>
                                </span>
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescripcion" class="form-control" placeholder="Describe el hallazgo" required />
                                <span class="help-block">
                                    <i class="fa fa-btn fa-user"></i>
                                </span>
                                <br />
                                <span class="card-title"><b>Dejanos una foto</b></span>
                                <div class="file-field">
                                    <div class="bg-transparent ">
                                        <asp:FileUpload runat="server" ID="fileFoto1" CssClass="form-control-file b " Style="color: transparent" />
                                    </div>
                                </div>
                                <%--                                <span class="card-title"><b>Foto 2</b></span>
                                <div class="file-field" >
                                    <div class="bg-transparent " >
                                        <asp:FileUpload runat="server" ID="fileFoto2" CssClass="form-control-file b " style="color:transparent" />
                                    </div>
                                </div>
                                <span class="card-title"><b>Foto 3</b></span>
                                <div class="file-field" >
                                    <div class="bg-transparent " >
                                        <asp:FileUpload runat="server" ID="fileFoto3" CssClass="form-control-file b " style="color:transparent" />
                                    </div>
                                </div>--%>

                                <asp:Button runat="server" ID="btnIngresar" class="btn btn-lg btn-danger btn-block btn-signin mb-4 " Text="Registrar" OnClientClick="return Loading()"></asp:Button>
                                <div id="cargando" style="text-align: center; width: 72px; height: 72px"></div>
                                <%--                               <div id="divUpload" style="display: none">
                                    <div style="width: 200pt; text-align: center;">
                                        Subiendo...
                                    </div>
                                    <div style="width: 200pt; height: 20px; border: solid 1pt gray">
                                        <div id="divProgress" runat="server" style="width: 1pt; height: 20px; background-color: orange; display: none">
                                        </div>
                                    </div>
                                    <div style="width: 200pt; text-align: center;">
                                        <asp:Label ID="lblPercentage" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <br />
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </div>--%>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <div class="demo-footer text-center">
                <h6>&copy;
                    <script>document.write(new Date().getFullYear())</script>
                    , ATI S.A</h6>
            </div>
        </div>
    </div>

</body>

<!-- Core JS Files -->
<script src="assets/js/jquery-3.2.1.min.js" type="text/javascript"></script>
<script src="assets/js/jquery-ui-1.12.1.custom.min.js" type="text/javascript"></script>
<script src="assets/js/popper.js" type="text/javascript"></script>
<script src="assets/js/bootstrap.min.js" type="text/javascript"></script>

<!--  Plugins for Select -->
<script src="assets/js/bootstrap-select.js"></script>

<!--  Plugins for DateTimePicker -->
<script src="assets/js/moment.min.js"></script>
<script src="assets/js/bootstrap-datetimepicker.min.js"></script>

<!-- Control Center for Paper Kit: parallax effects, scripts for the example pages etc -->
<script src="assets/js/paper-kit.js?v=2.1.0"></script>
</html>
