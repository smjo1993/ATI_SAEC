Imports System.Data
Imports System.Data.SqlClient
Public Class clsUsuario
    Public Function buscarUsuarioAti(usuario As String) As Integer
        Dim con As New SqlConnection(Conexion.strSQLSERVER)

        Return 1
    End Function
End Class
