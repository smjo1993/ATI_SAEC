Public Class verEmpresas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim tarjeta As String = ""
        Dim color As String
        Dim listaEmpresas As DataTable = crearEmpresas().obtenerEmpresas()
        Dim Empresas As Object = crearEmpresas()

        For Each fila As DataRow In listaEmpresas.Rows

            Dim porcentaje As String = Empresas.calcularPorcentaje(fila("rut"))
            Dim estado As Boolean = Empresas.ObtenerEstado(3, fila("rut"))
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

    End Sub

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
            Else
                Return "warning"
            End If

        End If

    End Function

End Class