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


    Public Function listarVehiculos(rutContratista As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarVehiculos '" & rutContratista & "'"
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

    Public Function obtenerRutEmpresa(rutContratista As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarVehiculos '" & rutContratista & "'"
            con.Open()

            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)

            dbDataAdapter.Fill(ds)

            Return ds.Tables(1)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function



    Public Function listarDocumentosVehiculo(idVehiculo As Integer, rutContratista As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosVehiculo'" & idVehiculo & "','" & rutContratista & "'"
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


    Public Function listarVehiculosParaEvaluar(idCarpeta As Integer, idArea As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarVehiculosParaEvaluar'" & idCarpeta & "','" & idArea & "'"
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

    Public Function listarDocumentosVehiculoParaRevisar(idCarpeta As Integer, idArea As Integer, idVehiculo As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosVehiculoParaRevisar'" & idCarpeta & "','" & idArea & "','" & idVehiculo & "'"
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
