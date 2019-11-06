Public Class verCarpetas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim tarjeta As String = ""
        Dim color As String
        ' definir por session el rut
        Dim rutContratista As String = Session("contratistaEntrante").rutContratista()
        Dim carpetaContratista As Object = crearCarpetasContratista()

        For Each fila As DataRow In carpetaContratista.obtenerCarpetas(rutContratista).Rows

            Dim Empresas As Object = crearEmpresas()
            Dim porcentaje As String = Empresas.calcularPorcentaje(fila("TB_SAEC_Empresarut"))
            Dim estado As Boolean = carpetaContratista.ObtenerEstado(fila("TB_SAEC_Empresarut"))
            color = obtenerColor(estado, porcentaje)
            Dim idCarpeta As String = fila("id")


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
            tarjeta = tarjeta & "              <a href=""https://localhost:44310/presentacion/Contratistas/revisarRequerimientos.aspx?idCarpeta=" + idCarpeta + """ class=""btn btn-" + color + """>Ver</a>"
            tarjeta = tarjeta & "              </div> "
            tarjeta = tarjeta & "            </div> "
            tarjeta = tarjeta & "          </div> "
            tarjeta = tarjeta & "        </div> "
            tarjeta = tarjeta & "      </div> "

            lblTarjetaCarpeta.Text = tarjeta

        Next

    End Sub

    Public Function crearCarpetasContratista() As Object

        Dim Empresas = New clsContratista()

        Return Empresas

    End Function
    Public Function crearEmpresas() As Object

        Dim Empresas = New clsEmpresa()

        Return Empresas

    End Function

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