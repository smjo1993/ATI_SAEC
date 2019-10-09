Imports System.Data.SqlClient

Public Class clsEmpresa

    Private nombre
    Private giro
    Private rut
    Private codigo
    Private fono
    Private correo

    Public Sub New(nombre As Object, giro As Object, rut As Object, codigo As Object, fono As Object, correo As Object)
        Me.nombre = nombre
        Me.giro = giro
        Me.rut = rut
        Me.codigo = codigo
        Me.fono = fono
        Me.correo = correo
    End Sub

    Public Sub New()
    End Sub

    Public Function obtenerEmpresas() As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Dim ds As New DataSet()
        Dim sql As String = "SP_SAEC_ListarEmpresas"
        con.Open()
        Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
        dbDataAdapter.Fill(ds)

        Return ds.Tables(0)

    End Function
    Public Function calcularPorcentaje(rutEmpersa As String) As String

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Dim ds As New DataSet()
        Dim sql As String = "SP_SAEC_CalcularProgresoCarpeta '" & rutEmpersa & "'"
        con.Open()
        Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
        dbDataAdapter.Fill(ds)

        Dim aprobados As Integer = ds.Tables(0).Rows(0).Item(0)
        Dim total As Integer = ds.Tables(1).Rows(0).Item(0)
        Dim resultado As String = Int((aprobados * 100) / total)

        Return resultado

    End Function


End Class
