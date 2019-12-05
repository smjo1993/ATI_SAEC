Imports System.Data.SqlClient
Public Class clsNotificacion


    Public Function insertarNotificacion(descripcion As String,
                                      rutAutor As String,
                                      tipo As String,
                                      idItem As Integer,
                                      idItem1 As Integer)

        Dim con As New SqlConnection(Conexion.strSQLSERVER)

        Console.WriteLine(con.ToString())

        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_InsertarNotificacion '" & descripcion & "' , '" & rutAutor & "' , '" & tipo & "' , '" & idItem & "' , '" & idItem1 & "'"
            con.Open()

            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)

            dbDataAdapter.Fill(ds, "InsertarNotificacion")

            'Return ds.Tables(0)|
            Return True

        Catch ex As Exception

            Return False

        Finally

            con.Close()
            con.Dispose()

        End Try

    End Function

    Public Function obtenerNotificaciones(rutConsultante As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerNotificaciones '" & rutConsultante & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ObtenerNotificaciones")
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

End Class
