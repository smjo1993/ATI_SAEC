Imports System.Data
Imports System.Configuration
Imports System.Net.Mail
Imports System.Net
Imports System.Data.SqlClient
Imports System.Windows
Public Class crearEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cargarDatos()
    End Sub
    Public Sub cargarDatos()
        Dim contratista As New clsContratista
        Dim listaContratistas As DataTable = contratista.listarContratistas()

    End Sub
End Class