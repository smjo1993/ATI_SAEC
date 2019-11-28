Imports System.Data
Imports System.Data.SqlClient
Public Class clsMenu

    Public Function menuUsuarioAtiInicio(rut As String) As String
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerOpcionesMenuInicioATI '" & rut & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)

            Dim opcionesMenu As DataTable = ds.Tables(0)

            Dim menu As String = generarMenu(opcionesMenu, 0, "")
            Return menu
        Catch ex As Exception
            Dim menu As String = ""
            Return menu
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
    Public Function menuUsuarioAtiCarpeta(rut As String, idCarpeta As Integer, nombreCarpeta As String) As String
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerOpcionesMenuATI '" & rut & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)

            Dim opcionesMenu As DataTable = ds.Tables(0)

            Dim menu As String = generarMenu(opcionesMenu, idCarpeta, nombreCarpeta)
            Return menu
        Catch ex As Exception
            Dim menu As String = ""
            Return menu
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
    Public Function generarMenu(menu As DataTable, idcarpeta As Integer, nombreCarpeta As String) As String
        Dim stringMenu As String = ""
        Dim contadorOpcion As Integer = 0
        If (menu.Rows.Count > 0) Then
            For Each opcion As DataRow In menu.Rows
                If (opcion("opcionPadre") = 0) Then
                    If (contadorOpcion > 0) Then
                        stringMenu = stringMenu & "                    </div>"
                        stringMenu = stringMenu & "                </div>"
                        stringMenu = stringMenu & "            </li>"
                    End If
                    stringMenu = stringMenu & "            <hr class=""sidebar-divider"">"
                    stringMenu = stringMenu & "            <li class=""nav-item"">"
                    stringMenu = stringMenu & "                <a class=""nav-link collapsed"" href=""#"" data-toggle=""collapse"" data-target=""#collapse" + contadorOpcion.ToString + """ aria-expanded=""True"" aria-controls=""collapseTwo"">"
                    stringMenu = stringMenu & "                    <i class=""""></i>"
                    stringMenu = stringMenu & "                   <span>" + opcion("nombre") + "</span>"
                    stringMenu = stringMenu & "                </a>"
                    stringMenu = stringMenu & "                 <div id=""collapse" + contadorOpcion.ToString + """class=""collapse"" aria-labelledby=""heading" + contadorOpcion.ToString + """ data-parent=""#accordionSidebar"">"
                    stringMenu = stringMenu & "                    <div class=""bg-white py-2 collapse-inner rounded"">"
                    contadorOpcion += 1
                Else
                    Dim tipo As Char = System.Convert.ToChar(opcion("tipo").ToString())
                    If (tipo = "C") Then
                        Dim idCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idcarpeta)
                        Dim idCodificada As String = System.Convert.ToBase64String(idCodificadaBase64)
                        Dim razonCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(nombreCarpeta)
                        Dim razonCodificada As String = System.Convert.ToBase64String(razonCodificadaBase64)
                        stringMenu = stringMenu & "                        <a  style=""text-align: center;""  class=""collapse-item"" href= """ + opcion("link") + "?i=" + idCodificada + "&n=" + razonCodificada + """> " + opcion("nombre") + "</a>"
                    Else
                        stringMenu = stringMenu & "                        <a  style=""text-align: center;"" Class=""collapse-item"" href= """ + opcion("link") + """> " + opcion("nombre") + "</a>"
                    End If
                End If
            Next
            stringMenu = stringMenu & "                    </div>"
            stringMenu = stringMenu & "                </div>"
            stringMenu = stringMenu & "            </li>"
            stringMenu = stringMenu & "            <hr Class=""sidebar-divider d-none d-md-block"">"
            stringMenu = stringMenu & "            <div Class=""row justify-content-center "">"
            stringMenu = stringMenu & "                <button Class=""rounded-circle border-0"" id=""sidebarToggle""></button>"
            stringMenu = stringMenu & "            </div>"
        End If
        Return stringMenu
    End Function

    Public Function menuInicioContratista(rut As String) As String
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerOpcionesMenuInicioContratista '" & rut & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)

            Dim menu As String = generarMenu(ds.Tables(0), 0, "")
            Return menu
        Catch ex As Exception
            Dim menu As String = ""
            Return menu
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function menuContratistaCarpeta(rut As String, idCarpeta As Integer, nombreCarpeta As String) As String
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerOpcionesMenuContratista '" & rut & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)

            Dim opcionesMenu As DataTable = ds.Tables(0)

            Dim menu As String = generarMenu(opcionesMenu, idCarpeta, nombreCarpeta)
            Return menu
        Catch ex As Exception
            Dim menu As String = ""
            Return menu
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
    Public Function actualizarEstadoOpcion(opcion As String, rut As String, opcionPadre As String, estado As String) As Boolean
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ActualizarEstadoOpcionMenu '" & opcion & "','" & rut & "','" & opcionPadre & "','" & estado & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)

            Return True
        Catch ex As Exception
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function opcionesCarpeta(rut As String) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_OpcionesCarpeta '" & rut & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)

            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
End Class
