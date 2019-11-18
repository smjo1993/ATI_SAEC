Imports System.Data
Imports System.Data.SqlClient
Public Class clsMenu
    Public Function menuUsuarioAti(rut As String, idCarpeta As Integer) As String
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerOpcionesMenu '" & rut & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)

            Dim menu As String = generarMenuATI(ds.Tables(0), idCarpeta)
            Return menu
        Catch ex As Exception
            Dim menu As String = ""
            Return menu
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
    Public Function generarMenuATI(menu As DataTable, idcarpeta As Integer) As String
        Dim stringMenu As String = ""
        Dim contadorOpcion As Integer = 0
        If (menu.Rows.Count > 0) Then
            For Each opcion As DataRow In menu.Rows
                If (opcion("opcionPadre") = 0) Then
                    If (contadorOpcion > 0) Then
                        stringMenu = stringMenu & "                </div>"
                        stringMenu = stringMenu & "            </li>"
                    End If
                    stringMenu = stringMenu & "            <hr class=""sidebar-divider"">"
                    stringMenu = stringMenu & "            <li class=""nav-item"">"
                    stringMenu = stringMenu & "                <a class=""nav-link collapsed"" href=""#"" data-toggle=""collapse"" data-target=""#collapse" + contadorOpcion.ToString + """ aria-expanded=""True"" aria-controls=""collapse" + contadorOpcion.ToString + """>"
                    stringMenu = stringMenu & "                    <i class=""fas fa-fw fa-cog""></i>"
                    stringMenu = stringMenu & "                   <span>" + opcion("nombre") + "</span>"
                    stringMenu = stringMenu & "                </a>"
                    stringMenu = stringMenu & "                 <div id=""collapse" + contadorOpcion.ToString + " ""class=""collapse"" aria-labelledby=""heading" + contadorOpcion.ToString + """ data-parent=""#accordionSidebar"">"
                    contadorOpcion += 1
                Else

                    stringMenu = stringMenu & "                    <div class=""bg-white py-2 collapse-inner rounded"">"
                    stringMenu = stringMenu & "                        <a class=""collapse-item"" href=" + opcion("link") + ">" + opcion("nombre") + "</a>"
                    stringMenu = stringMenu & "                    </div>"

                End If
            Next
            stringMenu = stringMenu & "            <hr class=""sidebar-divider d-none d-md-block"">"
            'stringMenu = stringMenu & "            <div class=""text-center d-none d-md-inline"">"
            'stringMenu = stringMenu & "                <button class=""rounded-circle border-0"" id=""sidebarToggle""></button>"
            'stringMenu = stringMenu & "            </div>"
        End If
        Return stringMenu
    End Function
End Class
