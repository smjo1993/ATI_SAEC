Imports System.Windows.Controls

Public Class CrearListaDocumentacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinDocEmpresa.Visible = False
        sinDocTrabajador.Visible = False
        sinDocVehiculo.Visible = False
        lblMenu.Visible = False
        lblNombreEmpresa.Visible = False
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarDatos()
        End If
    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        End If
    End Sub
    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim idCarpeta As Integer = decodificarId()
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(nombreCodificado)
        Dim nombreDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiCarpeta(rutUsuario, idCarpeta, nombreDecodificado)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub
    Protected Function decodificarId() As Integer
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idCodificada)
        Dim idDecodificada As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim idCarpeta As Integer = Convert.ToInt32(idDecodificada)
        Return idCarpeta
    End Function
    Private Sub cargarDatos()
        lblNombreEmpresa.Visible = True
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(nombreCodificado)
        Dim nombreDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        lblNombreEmpresa.Text = nombreDecodificado
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim documento As New clsDocumento
        Dim idCarpeta As Integer = decodificarId()
        Dim chk As HtmlInputCheckBox

        Dim documentosEmpresa As DataTable = documento.buscarDocumentoPorArea(usuario.getArea, "empresa", idCarpeta)
        If (documentosEmpresa Is Nothing) Then
            sinDocEmpresa.Visible = True
        Else
            If (documentosEmpresa.Rows.Count > 0) Then
                Me.gridDocumentosEmpresa.DataSource = documentosEmpresa
                Me.gridDocumentosEmpresa.DataBind()
                For Each documentoEmpresa As GridViewRow In gridDocumentosEmpresa.Rows
                    chk = documentoEmpresa.FindControl("chkDocEmpresa")
                    If documentoEmpresa.Cells(2).Text = "espera" Then
                        chk.Checked = True
                    End If
                Next
            Else
                sinDocEmpresa.Visible = True
            End If
        End If

        Dim documentosTrabajador As DataTable = documento.buscarDocumentoPorArea(usuario.getArea, "trabajador", idCarpeta)
        If (documentosTrabajador Is Nothing) Then
            sinDocTrabajador.Visible = True
        Else
            If (documentosTrabajador.Rows.Count > 0) Then
                Me.gridDocumentosTrabajador.DataSource = documentosTrabajador
                Me.gridDocumentosTrabajador.DataBind()
                For Each documentoTrabajador As GridViewRow In gridDocumentosTrabajador.Rows
                    chk = documentoTrabajador.FindControl("chkDocTrabajador")
                    If documentoTrabajador.Cells(2).Text = "espera" Then
                        chk.Checked = True
                    End If
                Next
            Else
                sinDocTrabajador.Visible = True
            End If
        End If

        Dim documentosVehiculo As DataTable = documento.buscarDocumentoPorArea(usuario.getArea, "vehiculo", idCarpeta)
        If (documentosVehiculo Is Nothing) Then
            sinDocVehiculo.Visible = True
        Else
            If (documentosVehiculo.Rows.Count > 0) Then
                Me.gridDocumentosVehiculo.DataSource = documentosEmpresa
                Me.gridDocumentosVehiculo.DataBind()
                For Each documentoVehiculo As GridViewRow In gridDocumentosVehiculo.Rows
                    chk = documentoVehiculo.FindControl("chkDocVehiculo")
                    If documentoVehiculo.Cells(2).Text = "espera" Then
                        chk.Checked = True
                    End If
                Next
            Else
                sinDocVehiculo.Visible = True
            End If
        End If
    End Sub

    Protected Sub btnPedirDocumento_Click(sender As Object, e As EventArgs) Handles btnPedirDocumento.Click
        Dim documento As New clsDocumento
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim idCarpeta As Integer = decodificarId()
        Dim chk As HtmlInputCheckBox
        For Each documentoEmpresa As GridViewRow In gridDocumentosEmpresa.Rows
            chk = documentoEmpresa.FindControl("chkDocEmpresa")
            If chk.Checked = True Then 'pasan a espera
                documento.cambiarEstadoDocumento(idCarpeta, usuario.getArea, documentoEmpresa.Cells(0).Text, "espera", Nothing)
            Else 'sino quedan no solicitados
                documento.cambiarEstadoDocumento(idCarpeta, usuario.getArea, documentoEmpresa.Cells(0).Text, "no solicitado", Nothing)
            End If
        Next

        For Each documentoTrabajador As GridViewRow In gridDocumentosTrabajador.Rows
            chk = documentoTrabajador.FindControl("chkDocTrabajador")
            If chk.Checked = True Then
                documento.cambiarEstadoDocumento(idCarpeta, usuario.getArea, documentoTrabajador.Cells(0).Text, "espera", Nothing)
            Else
                documento.cambiarEstadoDocumento(idCarpeta, usuario.getArea, documentoTrabajador.Cells(0).Text, "no solicitado", Nothing)
            End If
        Next

        For Each documentoVehiculo As GridViewRow In gridDocumentosVehiculo.Rows
            chk = documentoVehiculo.FindControl("chkDocVehiculo")
            If chk.Checked = True Then
                documento.cambiarEstadoDocumento(idCarpeta, usuario.getArea, documentoVehiculo.Cells(0).Text, "espera", Nothing)
            Else
                documento.cambiarEstadoDocumento(idCarpeta, usuario.getArea, documentoVehiculo.Cells(0).Text, "no solicitado", Nothing)
            End If
        Next
        'Response.Redirect(Page.Request.Url.AbsoluteUri)
        cargarMenu()
    End Sub
End Class