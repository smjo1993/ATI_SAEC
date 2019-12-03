Imports System.Data.SqlClient

Public Class clsVehiculo
    Private Patente
    Private marca

    Public Sub New(patente As Object, marca As Object)
        Me.Patente = patente
        Me.marca = marca
    End Sub

    Public Sub New()
    End Sub


    Public Function listarVehiculos(idCarpeta As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarVehiculos '" & idCarpeta & "'"
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

    Public Function listarDocumentosVehiculo(idCarpeta As Integer, patente As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosVehiculo'" & idCarpeta & "','" & patente & "'"
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
