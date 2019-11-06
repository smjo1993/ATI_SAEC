Imports System.Windows.Controls

Public Class CrearListaDocumentacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        Dim idCarpeta As Integer = Convert.ToInt32(Request.QueryString("idCarpeta").ToString())

        Dim documentosEmpresa As DataTable = documento.buscarDocumentosArea(usuario.areaUsuario, "empresa", idCarpeta)
        If (documentosEmpresa.Rows.Count > 0) Then
            GridView1.DataSource = documentosEmpresa
            GridView1.DataBind()
        End If
        Dim documentosTrabajador As DataTable = documento.buscarDocumentosArea(usuario.areaUsuario, "trabajador", idCarpeta)
        If (documentosTrabajador.Rows.Count > 0) Then
            GridView1.DataSource = documentosTrabajador
            GridView1.DataBind()
        End If
        Dim documentosVehiculo As DataTable = documento.buscarDocumentosArea(usuario.areaUsuario, "vehiculo", idCarpeta)
        If (documentosVehiculo.Rows.Count > 0) Then
            GridView1.DataSource = documentosVehiculo
            GridView1.DataBind()
        End If
    End Sub

End Class