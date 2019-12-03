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

    Public Function listarTrabajadores(idCarpeta As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarTrabajadores '" & idCarpeta & "'"
            con.Open()

            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)

            dbDataAdapter.Fill(ds, "ListarDocumentosArea")

            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function

    Public Function listarDocumentosTrabajador(idCarpeta As Integer, rutTrabajador As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosTrabajador'" & idCarpeta & "','" & rutTrabajador & "'"
            con.Open()

            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)

            dbDataAdapter.Fill(ds, "ListarDocumentosArea")

            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function



End Class
