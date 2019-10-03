Imports System.Data
Imports System.Data.SqlClient
Public Class clsContratista
    Private Nombre
    Private Login
    Private Clave
    Private Rut
    Private Estado
    Private Fono
    Private Correo

    Public Sub New()

    End Sub

    Public Sub New(nombre As Object, login As Object, clave As Object, rut As Object, fono As Object, correo As Object)
        Me.Nombre = nombre
        Me.Login = login
        Me.Clave = clave
        Me.Rut = rut
        Me.Nombre = "A"
        Me.Fono = fono
        Me.Correo = correo
    End Sub

    Public Function listarContratistas() As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarContratistas"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ListarContratistas")
            Return ds.Tables(0)




        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function buscarContratista(contratista As String) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ValidarContratista '" & contratista & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ValidarContratista")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
End Class
