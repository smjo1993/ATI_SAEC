Imports System.Data
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

    Public Function insertarTrabajador(rut As String,
                                       nombre As String,
                                       fono As String,
                                       correo As String) As Boolean
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_InsertarTrabajador '" & rut & "' , '" & nombre & "', '" & fono & "', '" & correo & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "InsertarTrabajador")

            Return True

        Catch ex As Exception
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
End Class
