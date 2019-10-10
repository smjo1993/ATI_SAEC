Imports System.Data
Imports System.Data.SqlClient
Public Class clsEmpresa
    Private razonSocial
    Private rut
    Private giro
    Private direccion
    Private ciudad
    Private personaContacto
    Private correo
    Private fono
    Private celular
    Private rutContratista

    Public Sub New()

    End Sub

    Public Sub New(razonSocial As Object, rut As Object, giro As Object, direccion As Object, ciudad As Object, personacontacto As Object, fono As Object, correo As Object, rutContratista As Object)
        Me.razonSocial = razonSocial
        Me.rut = rut
        Me.giro = giro
        Me.direccion = direccion
        Me.ciudad = ciudad
        Me.personaContacto = personacontacto
        Me.correo = correo
        Me.fono = fono
        Me.celular = celular
        Me.rutContratista = rutContratista

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

    Public Function insertarEmpresa(nombre As String,
                                   rut As String,
                                   giro As String,
                                   direccion As String,
                                   ciudad As String,
                                   personacontacto As String,
                                   fono As String,
                                   celular As String,
                                   correo As String,
                                   rutContratista As String) As Boolean
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_InsertarEmpresa '" & nombre & "' , '" & rut & "' , '" & giro & "' , '" & direccion & "' , '" & ciudad & "' , '" & personacontacto & "' , '" & correo & "' , '" & fono & "' , '" & celular & "' , '" & rutContratista & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "InsertarEmpresa")

            Return True



        Catch ex As Exception
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
End Class
