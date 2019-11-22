Public Class verEmpresas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            CargarTarjetas()
        End If

    End Sub

    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        End If
    End Sub

    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.rutUsuario
        'Dim idCarpeta As Integer = decodificarId()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub

    Protected Sub CargarTarjetas()

        Dim listaRoles As List(Of clsRol) = New List(Of clsRol)

        listaRoles = Session("roles")

        If listaRoles.Item(0).getIdRol() = 1 Or listaRoles.Item(0).getIdRol() = 2 Or listaRoles.Item(0).getIdRol() = 3 Then
            Dim tarjeta As String = ""
            Dim color As String
            Dim listaEmpresas As DataTable = crearEmpresas().obtenerCarpetas()
            Dim Empresas As Object = crearEmpresas()

            ' Ciclo for que recorre la lista de empresas con carpetas de arranque del sistema
            For Each fila As DataRow In listaEmpresas.Rows


                Dim porcentaje As String = Empresas.calcularPorcentaje(fila("rut"))
                Dim estado As Boolean = Empresas.ObtenerEstado(Session("usuario").areaUsuario(), fila("rut"))
                color = obtenerColor(estado, porcentaje)
                Dim idCarpeta As String = fila("id")
                Dim idCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(fila("id"))
                Dim idCodificada As String = System.Convert.ToBase64String(idCodificadaBase64)
                Dim razonCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(fila("razonSocial"))
                Dim razonCodificada As String = System.Convert.ToBase64String(razonCodificadaBase64)

                tarjeta = tarjeta & "            <div Class=""row justify-content-center "">"
                tarjeta = tarjeta & "   <div Class="" col-xl-8 col-md-6 mb-4""> "
                tarjeta = tarjeta & "         <div Class=""card border-left-" + color + " shadow h-100 py-2""> "
                tarjeta = tarjeta & "           <div Class=""card-body""> "
                tarjeta = tarjeta & "             <div Class=""row no-gutters align-items-center""> "
                tarjeta = tarjeta & "               <div Class=""col mr-2""> "
                tarjeta = tarjeta & "                 <div Class=""text-l font-weight-bold text-" + color + " text-uppercase mb-1"">" + fila("razonSocial") + "</div> "
                tarjeta = tarjeta & "                 <div Class=""row no-gutters align-items-center""> "
                tarjeta = tarjeta & "                   <div Class=""col-auto""> "
                tarjeta = tarjeta & "                     <div Class=""h5 mb-0 mr-3 font-weight-bold text-gray-800"">" + porcentaje + "%" + "</div> "
                tarjeta = tarjeta & "                   </div> "
                tarjeta = tarjeta & "                  <div Class=""col""> "
                tarjeta = tarjeta & "                     <div Class=""progress progress-sm mr-2""> "
                tarjeta = tarjeta & "                       <div Class=""progress-bar bg-" + color + """ role=""progressbar"" style=""width: " + porcentaje + "%" + """ aria-valuenow=""50"" aria-valuemin=""0"" aria-valuemax=""100""></div> "
                tarjeta = tarjeta & "                     </div> "
                tarjeta = tarjeta & "                  </div> "
                tarjeta = tarjeta & "                </div> "
                tarjeta = tarjeta & "              </div> "
                tarjeta = tarjeta & "             &nbsp;"
                tarjeta = tarjeta & "              <div Class=""col-auto""> "
                tarjeta = tarjeta & "              <a href=""https://localhost:44310/presentacion/funcionarios%20ATI/CrearListaDocumentacion.aspx?i=" + idCodificada + "&n=" + razonCodificada + """ class=""fas fa-clipboard-list fa-2x text-" + color + """></a>"
                tarjeta = tarjeta & "              </div> "
                tarjeta = tarjeta & "              <div Class=""col-1""> "
                tarjeta = tarjeta & "              </div> "
                tarjeta = tarjeta & "              <div Class=""col-auto""> "
                tarjeta = tarjeta & "              <a href=""https://localhost:44310/presentacion/funcionarios%20ATI/confirmarDocumentos.aspx?i=" + idCodificada + "&n=" + razonCodificada + """ class=""fas fa-comments fa-2x text-" + color + """></a>"
                tarjeta = tarjeta & "              </div> "
                tarjeta = tarjeta & "              <div Class=""col-1""> "
                tarjeta = tarjeta & "              </div> "
                tarjeta = tarjeta & "              <div Class=""col-auto""> "
                tarjeta = tarjeta & "              <a href=""#" + idCodificada + """ class=""fas fa-fw fa-folder fa-2x text-" + color + """></a>"
                tarjeta = tarjeta & "              </div> "
                tarjeta = tarjeta & "            </div> "
                tarjeta = tarjeta & "          </div> "
                tarjeta = tarjeta & "        </div> "
                tarjeta = tarjeta & "      </div> "
                tarjeta = tarjeta & "            </div>"

                lblTarjetaEmpresa.Text = tarjeta

            Next
        End If

        'For Each rol In Session("roles")


        'si tiene rol revisor que muestre la vista correspondiente
        'f rol.getIdRol() = 3 Then



        'End If

        'Next
    End Sub

    'Función que crea el objeto Empresa
    Public Function crearEmpresas() As Object

        Dim Empresas = New clsEmpresa()

        Return Empresas

    End Function

    'Función que devuelve el color dependiendo del estado del documento
    Public Function obtenerColor(EstadoRojo As Boolean, porcentaje As String) As String

        If EstadoRojo = True Then

            Return "danger"

        ElseIf EstadoRojo = False Then

            If porcentaje = "100" Then

                Return "success"

            End If

            Return "warning"

        End If

    End Function

End Class