Public Class verEmpresas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Session("roles")

        ' If Not (Session("roles") = vbNull) Then
        '     LoginStatus.Text = "logged in user " & System.Web.HttpContext.Current.Session("Username").ToString
        ' Else
        'Response.Redirect("Login.aspx", False)
        'End If


        For Each rol In Session("roles")

            'si tiene rol revisor que muestre la vista correspondiente
            If rol.getIdRol() = 3 Then

                Dim tarjeta As String = ""
                Dim color As String
        Dim listaEmpresas As DataTable = crearEmpresas().obtenerEmpresas()
        Dim Empresas As Object = crearEmpresas()

                ' Ciclo for que recorre la lista de empresas con carpetas de arranque del sistema
                For Each fila As DataRow In listaEmpresas.Rows


                    Dim porcentaje As String = Empresas.calcularPorcentaje(fila("rut"))
                    Dim estado As Boolean = Empresas.ObtenerEstado(Session("usuario").areaUsuario(), fila("rut"))
                    color = obtenerColor(estado, porcentaje)

                    tarjeta = tarjeta & "   <div Class=""col-xl-3 col-md-6 mb-4""> "
                    tarjeta = tarjeta & "         <div Class=""card border-left-" + color + " shadow h-100 py-2""> "
                    tarjeta = tarjeta & "           <div Class=""card-body""> "
                    tarjeta = tarjeta & "             <div Class=""row no-gutters align-items-center""> "
                    tarjeta = tarjeta & "               <div Class=""col mr-2""> "
                    tarjeta = tarjeta & "                 <div Class=""text-xs font-weight-bold text-" + color + " text-uppercase mb-1"">" + fila("razonSocial") + "</div> "
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
                    tarjeta = tarjeta & "              <div Class=""col-auto""> "
                    tarjeta = tarjeta & "              <a href=""#"" class=""btn btn-" + color + """>Ver</a>"
                    tarjeta = tarjeta & "              </div> "
                    tarjeta = tarjeta & "            </div> "
                    tarjeta = tarjeta & "          </div> "
                    tarjeta = tarjeta & "        </div> "
                    tarjeta = tarjeta & "      </div> "

                    lblTarjetaEmpresa.Text = tarjeta

                Next

            End If

        Next



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