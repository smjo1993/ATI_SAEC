Imports System.Data.SqlClient

Public Class clsTrabajador

    Private Nombre
    Private Rut
    Private Fono
    Private Correo

    Public Sub New(nombre As Object, rut As Object, fono As Object, correo As Object)
        Me.Nombre = nombre
        Me.Rut = rut
        Me.Fono = fono
        Me.Correo = correo
    End Sub

    Public Sub New()
    End Sub

    Public Function listarTrabajadores(rutContratista As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarTrabajadores '" & rutContratista & "'"
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

    Public Function obtenerRutEmpresa(rutContratista As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarTrabajadores '" & rutContratista & "'"
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

    Public Function listarDocumentosTrabajador(idTrabajador As Integer, rutContratista As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosTrabajador'" & idTrabajador & "','" & rutContratista & "'"
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



    Public Function listarTrabajadoresParaEvaluar(idCarpeta As Integer, idArea As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarTrabajadoresParaEvaluar'" & idCarpeta & "','" & idArea & "'"
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

    Public Function listarDocumentosTrabajadorParaRevisar(idCarpeta As Integer, idArea As Integer, idTrabajador As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosTrabajadorParaRevisar'" & idCarpeta & "','" & idArea & "','" & idTrabajador & "'"
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
