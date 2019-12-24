Public Class carpetasAntiguas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinCarpeta.Visible = False
        cargarMenu()
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            If Not String.IsNullOrEmpty(Request.QueryString("i")) Then
                Dim empresa As New clsEmpresa
                Dim rutEmpresa As String = empresa.obtenerRutEmpresa(decodificarId())
                cargarHistorico(rutEmpresa)
            Else
                Dim rutEmpresa As String = Session("rutEmpresa")
                cargarHistorico(rutEmpresa)
            End If
        End If
    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")

        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "5,6", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If

        End If
    End Sub
    Protected Sub cargarMenu()
        Dim empresa As New clsEmpresa
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim idCarpeta As Integer
        Dim nombreDecodificado As String

        If Not String.IsNullOrEmpty(Request.QueryString("n")) Then
            Dim nombreCodificado = Request.QueryString("n").ToString()
            Dim data() As Byte = System.Convert.FromBase64String(nombreCodificado)
            nombreDecodificado = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Else
            nombreDecodificado = Session("nombreEmpresa").ToString
        End If

        If Not String.IsNullOrEmpty(Request.QueryString("i")) Then
            idCarpeta = decodificarId()
        Else
            idCarpeta = empresa.obtenerIdCarpetaVigente(Session("rutEmpresa").ToString)
        End If

        Dim menu As New clsMenu
        Dim stringMenu As String

        If idCarpeta = "0" Then
            stringMenu = menu.menuUsuarioAtiInicio(rutUsuario)
        Else
            stringMenu = menu.menuUsuarioAtiCarpeta(rutUsuario, idCarpeta, nombreDecodificado)
        End If
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub
    Protected Function decodificarId() As Integer
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idCodificada)
        Dim idDecodificada As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim idCarpeta As Integer = Convert.ToInt32(idDecodificada)
        Return idCarpeta
    End Function
    Private Sub cargarHistorico(rutEmpresa As String)
        Dim empresa As New clsEmpresa
        Dim carpetasHistorico As DataTable = empresa.obtenerCarpetasHistorico(rutEmpresa)
        Dim tarjeta As String = ""
        If carpetasHistorico Is Nothing Then
            sinCarpeta.Visible = True
        Else
            If carpetasHistorico.Rows.Count > 0 Then
                For Each fila As DataRow In carpetasHistorico.Rows

                    Dim Empresas As Object = New clsEmpresa()
                    Dim porcentaje As String = Empresas.calcularPorcentaje(fila("rut"))
                    Dim estado As Boolean = Empresas.ObtenerEstado(Session("usuario").getArea(), fila("rut"))
                    Dim idCarpeta As String = fila("id")
                    Dim idCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(fila("id"))
                    Dim idAntiguaCodificada As String = System.Convert.ToBase64String(idCodificadaBase64)
                    Dim razonCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(fila("razonSocial"))
                    Dim razonCodificada As String = System.Convert.ToBase64String(razonCodificadaBase64)
                    Dim idCarpetaActual As String
                    If Not String.IsNullOrEmpty(Request.QueryString("i")) Then
                        idCarpetaActual = Request.QueryString("i").ToString()
                    Else
                        Dim idCarpetaVigente As String = empresa.obtenerIdCarpetaVigente(Session("rutEmpresa").ToString)
                        If idCarpetaVigente Is Nothing Then
                            idCodificadaBase64 = System.Text.ASCIIEncoding.ASCII.GetBytes("0")
                            idCarpetaActual = System.Convert.ToBase64String(idCodificadaBase64)
                        Else
                            idCodificadaBase64 = System.Text.ASCIIEncoding.ASCII.GetBytes(idCarpetaVigente)
                            idCarpetaActual = System.Convert.ToBase64String(idCodificadaBase64)
                        End If
                    End If

                    tarjeta = tarjeta & "            <div Class="" row justify-content-center "">"
                    tarjeta = tarjeta & "   <div Class="" col-xl-8 col-md-6 mb-4""> "
                    tarjeta = tarjeta & "         <div Class="" card border-left-dark"" shadow h-100 py-2""> "
                    tarjeta = tarjeta & "           <div Class="" card-body""> "
                    tarjeta = tarjeta & "             <div Class="" row no-gutters align-items-center""> "
                    tarjeta = tarjeta & "               <div Class="" col mr-2""> "
                    tarjeta = tarjeta & "                 <div Class="" text-l font-weight-bold text-dark"" text-uppercase mb-1"">" + fila("razonSocial") + " - " + fila("fechaDeExpiracion").Year.ToString + "</div> "
                    tarjeta = tarjeta & "                 <div Class="" row no-gutters align-items-center""> "
                    tarjeta = tarjeta & "                   <div Class="" col-auto""> "
                    tarjeta = tarjeta & "                     <div Class="" h5 mb-0 mr-3 font-weight-bold text-gray-800"">" + porcentaje + "%" + "</div> "
                    tarjeta = tarjeta & "                   </div> "
                    tarjeta = tarjeta & "                  <div Class="" col""> "
                    tarjeta = tarjeta & "                     <div Class="" progress progress-sm mr-2""> "
                    tarjeta = tarjeta & "                       <div Class=""progress-bar bg-dark "" role=""progressbar"" style=""width: " + porcentaje + "%" + """ aria-valuenow=""50"" aria-valuemin=""0"" aria-valuemax=""100""></div> "
                    tarjeta = tarjeta & "                     </div> "
                    tarjeta = tarjeta & "                  </div> "
                    tarjeta = tarjeta & "                </div> "
                    tarjeta = tarjeta & "              </div> "

                    tarjeta = tarjeta & "              <div Class="" col-auto""> "
                    tarjeta = tarjeta & "              <a href="" https://localhost:44310/presentacion/Funcionarios%20ATI/historicoCarpeta.aspx?i=" + idCarpetaActual + "&n=" + razonCodificada + "&ia=" + idAntiguaCodificada + """ class=""fas fa-fw fa-folder fa-2x text-dark""></a>"
                    tarjeta = tarjeta & "              </div> "

                    tarjeta = tarjeta & "            </div> "
                    tarjeta = tarjeta & "          </div> "
                    tarjeta = tarjeta & "        </div> "
                    tarjeta = tarjeta & "      </div> "
                    tarjeta = tarjeta & "            </div>"
                Next
                lblTarjetaEmpresa.Text = tarjeta
            Else
                sinCarpeta.Visible = True
            End If
        End If
    End Sub
End Class