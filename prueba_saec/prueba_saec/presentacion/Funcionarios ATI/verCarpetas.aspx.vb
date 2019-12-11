Public Class verEmpresas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        validarUsuario()
        lblMenu.Visible = False
        cargarMenu()
        If Page.IsPostBack Then
            Return
        End If
        CargarTarjetas()

    End Sub

    Protected Sub validarUsuario()

        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "5,2", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
        End If

    End Sub

    Protected Sub cargarMenu()

        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True

    End Sub

    Protected Sub CargarTarjetas()

        Dim listaRoles As List(Of clsRol) = New List(Of clsRol)
        listaRoles = Session("roles")

        If listaRoles.Item(0).getId() = 1 Or listaRoles.Item(0).getId() = 2 Or listaRoles.Item(0).getId() = 3 Then

            Dim tarjeta As String = ""
            Dim color As String
            Dim empresas As Object = New clsEmpresa()
            Dim listaEmpresas As DataTable = empresas.obtenerCarpetas()
            Dim usuario As clsUsuarioSAEC = Session("usuario")
            Dim menu As New clsMenu
            Dim opcionesCarpeta As DataTable = menu.opcionesCarpeta(usuario.getRut)

            ' Ciclo for que recorre la lista de empresas con carpetas de arranque del sistema
            For Each fila As DataRow In listaEmpresas.Rows

                Dim porcentaje As String = empresas.calcularPorcentaje(fila("rut"))
                Dim estado As Boolean = empresas.ObtenerEstado(Session("usuario").getArea(), fila("rut"))
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
                tarjeta = tarjeta & "                 <div Class=""text-l font-weight-bold text-" + color + " text-uppercase mb-1"">" + fila("razonSocial") + " - " + fila("fechaDeExpiracion").Year.ToString + "</div> "
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

                If (opcionesCarpeta.Rows(0)("estado") = "A") Then
                    tarjeta = tarjeta & "              <div Class=""col-auto""> "
                    tarjeta = tarjeta & "              <a href=""https://localhost:44310/presentacion/funcionarios%20ATI/CrearListaDocumentacion.aspx?i=" + idCodificada + "&n=" + razonCodificada + """ class=""fas fa-clipboard-list fa-2x text-" + color + """></a>"
                    tarjeta = tarjeta & "              </div> "
                    tarjeta = tarjeta & "              <div Class=""col-1""> "
                    tarjeta = tarjeta & "              </div> "
                End If
                If (opcionesCarpeta.Rows(1)("estado") = "A") Then
                    tarjeta = tarjeta & "              <div Class=""col-auto""> "
                    tarjeta = tarjeta & "              <a href=""https://localhost:44310/presentacion/funcionarios%20ATI/confirmarDocumentos.aspx?i=" + idCodificada + "&n=" + razonCodificada + """ class=""fas fa-comments fa-2x text-" + color + """></a>"
                    tarjeta = tarjeta & "              </div> "
                    tarjeta = tarjeta & "              <div Class=""col-1""> "
                    tarjeta = tarjeta & "              </div> "
                End If
                If (opcionesCarpeta.Rows(2)("estado") = "A") Then
                    tarjeta = tarjeta & "              <div Class=""col-auto""> "
                    tarjeta = tarjeta & "              <a href=""https://localhost:44310/presentacion/funcionarios%20ATI/evaluarDocumentosEmpresa.aspx?i=" + idCodificada + "&n=" + razonCodificada + """ class=""fas fa-fw fa-folder fa-2x text-" + color + """></a>"
                    tarjeta = tarjeta & "              </div> "
                End If
                tarjeta = tarjeta & "            </div> "
                tarjeta = tarjeta & "          </div> "
                tarjeta = tarjeta & "        </div> "
                tarjeta = tarjeta & "      </div> "
                tarjeta = tarjeta & "  </div>"

                lblTarjetaEmpresa.Text = tarjeta

            Next
        End If

    End Sub


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