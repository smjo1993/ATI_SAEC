Imports System.Data
Imports System.Data.SqlClient
Public Class clsRol

    Private id As Integer
    Private descripcion As String

    Public Sub New()

    End Sub
    Public Sub New(ByVal id As Integer, ByVal descripcion As String)
        Me.id = id
        Me.descripcion = descripcion
    End Sub

    Public Property getId() As Integer
        Get
            Return Me.id
        End Get
        Set
            Me.id = Value
        End Set
    End Property

    Public Property getDescripcion() As String
        Get
            Return Me.descripcion
        End Get
        Set
            Me.descripcion = Value
        End Set
    End Property
    Public Function listarRoles() As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try
            Dim ds As New DataSet()

            Dim sql As String = "SP_SAEC_ListarRoles "

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
