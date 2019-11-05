Imports System.Windows.Controls

Public Class CrearListaDocumentacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        seccionEmpresa.Visible = False
        seccionVehiculo.Visible = False
        seccionTrabajador.Visible = False
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarDatos()
        End If
    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        End If
    End Sub
    Private Sub cargarDatos()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim documento As New clsDocumento

        Dim documentosEmpresa As DataTable = documento.buscarDocumentosArea(usuario.areaUsuario, "empresa")
        If (documentosEmpresa.Rows.Count > 0) Then
            seccionEmpresa.Visible = True
        End If
        Dim documentosTrabajador As DataTable = documento.buscarDocumentosArea(usuario.areaUsuario, "trabajador")
        If (documentosTrabajador.Rows.Count > 0) Then
            seccionVehiculo.Visible = True
        End If
        Dim documentosVehiculo As DataTable = documento.buscarDocumentosArea(usuario.areaUsuario, "vehiculo")
        If (documentosVehiculo.Rows.Count > 0) Then
            seccionTrabajador.Visible = True
        End If
    End Sub

End Class