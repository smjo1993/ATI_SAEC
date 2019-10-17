Imports System.Data
Imports System.Data.SqlClient
Public Class clsCarpetaArranque
    Private fechaExpiracion
    Private rutEmpresa
    Private fechaCreacion

    Public Sub New()

    End Sub

    Public Sub New(ByVal fechaExpiracion As Date, ByVal rutEmpresa As String, ByVal fechaCreacion As Date)
        Me.fechaExpiracion = fechaExpiracion
        Me.rutEmpresa = rutEmpresa
        Me.fechaCreacion = fechaCreacion
    End Sub

    Public Function insertarEmpresa(fechaExpiracion As Date,
                               rutEmpresa As String,
                               fechaCreacion As Date,
                               descripcion As String,
                               rutUsuario As String
                               ) As Boolean
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_NuevaCarpetaArranque '" & fechaExpiracion & "' , '" & rutEmpresa & "'  , '" & fechaCreacion & "' , '" & descripcion & "', '" & rutUsuario & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Return True
        Catch ex As Exception
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

End Class
