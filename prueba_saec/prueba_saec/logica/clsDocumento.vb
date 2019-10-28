Imports System.Data
Imports System.Data.SqlClient
Public Class clsDocumento
    Public Function insertarDocumento(nombre As String,
                                      tipo As String,
                                      area As Integer)

        Dim con As New SqlConnection(Conexion.strSQLSERVER)

        Console.WriteLine(con.ToString())

        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_InsertarDocumento '" & nombre & "' , '" & tipo & "' , '" & area & "'"
            con.Open()

            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)

            dbDataAdapter.Fill(ds, "InsertarDocumento")

            'Return ds.Tables(0)
            Return True

        Catch ex As Exception

            Return False

        Finally

            con.Close()
            con.Dispose()

        End Try

    End Function

    Public Function obtenerDocumento()

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Dim ds As New DataSet()

        Dim sql As String = "SP_SAEC_ListarDocumentos"

        con.Open()

        Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
        dbDataAdapter.Fill(ds)

        Return ds.Tables(0)

    End Function

End Class
