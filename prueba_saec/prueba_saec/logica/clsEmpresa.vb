Imports System.Data
Imports System.Data.SqlClient
Public Class clsEmpresa
    Private Nombre
    Private Rut
    Private Giro
    Private Codigo
    Private Fono
    Private Correo
    Private RutContratista

    Public Sub New(nombre As Object, rut As Object, giro As Object, codigo As Object, fono As Object, correo As Object, rutContratista As Object)
        Me.Nombre = nombre
        Me.Rut = rut
        Me.Giro = giro
        Me.Codigo = codigo
        Me.Fono = fono
        Me.Correo = correo
        Me.RutContratista = rutContratista

    End Sub
    Public Sub New()
    End Sub

    Public Function obtenerEmpresas() As DataTable

        Dim sql As String = "SP_SAEC_ListarEmpresas"
        Dim ds As DataSet = conexionDb(sql)
        Return ds.Tables(0)

    End Function
    Public Function calcularPorcentaje(rutEmpersa As String) As String


        Dim sql As String = "SP_SAEC_CalcularProgresoCarpeta '" & rutEmpersa & "'"
        Dim ds As DataSet = conexionDb(sql)
        Dim aprobados As Integer = ds.Tables(0).Rows(0).Item(0)
        Dim total As Integer = ds.Tables(1).Rows(0).Item(0)
        Dim resultado As String = Int((aprobados * 100) / total)

        Return resultado

    End Function

    Public Function obtenerEstado(areaRevisor As Integer, rutEmpersa As String) As Boolean

        Dim sql As String = "SP_SAEC_ListarEstadoDocumento '" & areaRevisor & "','" & rutEmpersa & " '"
        Dim ds As DataSet = conexionDb(sql)
        Dim estadoRojo As Integer = ds.Tables(0).Rows.Count

        If estadoRojo > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function conexionDb(sql As String) As DataSet

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Dim ds As New DataSet()
        con.Open()
        Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
        dbDataAdapter.Fill(ds)
        Return ds

    End Function
End Class
