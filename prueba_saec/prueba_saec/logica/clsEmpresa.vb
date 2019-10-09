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

End Class
