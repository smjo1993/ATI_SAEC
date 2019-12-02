Public Class verCarpetas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        validarContratista()
        lblMenu.Visible = False
        cargarMenu()
        If Page.IsPostBack Then
        End If
        cargarTarjeta()

    End Sub
    Protected Sub validarContratista()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        If (contratista Is Nothing) Then
            Response.Redirect("../login.aspx")
        End If

    End Sub

    Protected Sub cargarMenu()
        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut
        'Dim idCarpeta As Integer = decodificarId()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuInicioContratista(rutContratista)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub
    Protected Sub cargarTarjeta()

        Dim tarjeta As String = ""
        Dim color As String
        Dim rutContratista As String = Session("contratistaEntrante").getRut()
        Dim carpetaContratista As Object = New clsContratista()

        For Each fila As DataRow In carpetaContratista.obtenerCarpetas(rutContratista).Rows

            Dim Empresas As Object = New clsEmpresa()
            Dim porcentaje As String = Empresas.calcularPorcentaje(fila("TB_SAEC_Empresarut"))
            Dim estado As Boolean = carpetaContratista.ObtenerEstado(fila("TB_SAEC_Empresarut"))
            color = obtenerColor(estado, porcentaje)
            Dim idCarpeta As String = fila("id")
            Dim idCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(fila("id"))
            Dim idCodificada As String = System.Convert.ToBase64String(idCodificadaBase64)


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

            tarjeta = tarjeta & "              <div Class=""col-auto""> "
            tarjeta = tarjeta & "              <a href=""https://localhost:44310/presentacion/Contratistas/revisarRequerimientos.aspx"" class=""fas fa-comments fa-2x text-" + color + """></a>"
            tarjeta = tarjeta & "              </div> "
            tarjeta = tarjeta & "              <div Class=""col-1""> "
            tarjeta = tarjeta & "              </div> "

            tarjeta = tarjeta & "              <div Class=""col-auto""> "
            tarjeta = tarjeta & "              <a href=""https://localhost:44310/presentacion/Contratistas/subirDocumentos.aspx"" class=""fas fa-fw fa-folder fa-2x text-" + color + """></a>"
            tarjeta = tarjeta & "              </div> "

            tarjeta = tarjeta & "            </div> "
            tarjeta = tarjeta & "          </div> "
            tarjeta = tarjeta & "        </div> "
            tarjeta = tarjeta & "      </div> "
            tarjeta = tarjeta & "            </div>"

            lblTarjetaCarpeta.Text = tarjeta
            lblNombreEmpresa.Text = fila("razonSocial")

        Next
    End Sub

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