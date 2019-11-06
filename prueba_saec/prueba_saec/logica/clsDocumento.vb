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

    Public Function conexionDb(sql As String) As DataSet

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Dim ds As New DataSet()
        con.Open()
        Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
        dbDataAdapter.Fill(ds)
        Return ds

    End Function
    Public Function buscarDocumentosArea(ByVal area As Integer) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        'Try
        Dim ds As New DataSet()
        Dim sql As String = "SP_SAEC_ListarDocumentosPorArea '" & area & "'"
        con.Open()

        Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)

        dbDataAdapter.Fill(ds, "ListarDocumentosArea")

        Return ds.Tables(0)

        'Catch ex As Exception
        '    Return Nothing
        'Finally
        '    con.Close()
        '    con.Dispose()
        'End Try
    End Function

    Public Function buscarDocumentoPorArea(ByVal area As Integer, ByVal descripcion As String, ByVal idCarpeta As Integer) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_BuscarDocumentosArea '" & area & "' , '" & descripcion & "' , '" & idCarpeta & "'"
            con.Open()

            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)

            dbDataAdapter.Fill(ds, "ListarDocumentoArea")

            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function obtenerDocumentoEstadoEspera(rutContratista As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosEnEspera '" & rutContratista & "'"
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
