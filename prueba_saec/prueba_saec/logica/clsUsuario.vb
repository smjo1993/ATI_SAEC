Imports System.Data
Imports System.Data.SqlClient
Public Class clsUsuario
    Public Function buscarUsuarioAti(usuario As String) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ValidarUsuarioATI '" & usuario & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ValidarUsuarioATI")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function obtenerRevisores() As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerRevisores"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ObtenerRevisores")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
End Class
