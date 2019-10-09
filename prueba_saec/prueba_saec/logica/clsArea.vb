Imports System.Data
Imports System.Data.SqlClient
Public Class clsArea
    'Public Function insertarDocumento(nombre As String, tipo As String)

    '    Dim con As New SqlConnection(Conexion.strSQLSERVER)

    '    Console.WriteLine(con.ToString())

    '    Try
    '        Dim ds As New DataSet()
    '        Dim sql As String = "SP_SAEC_InsertarNuevoDocumento '" & nombre & "' " & "'" & tipo & "' "

    '        con.Open()

    '        Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)

    '        dbDataAdapter.Fill(ds, "InsertarNuevoDocumento")

    '        'Return ds.Tables(0)

    '    Catch ex As Exception
    '        Return Nothing
    '    Finally
    '        con.Close()
    '        con.Dispose()
    '    End Try
    'End Function
    Public Function obtenerNombre()

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Dim ds As New DataSet()

        Dim sql As String = "SP_SAEC_ListarAreas"

        con.Open()

        Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
        dbDataAdapter.Fill(ds)

        Return ds.Tables(0)

    End Function

End Class
