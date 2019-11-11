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
        Dim cantidadDocumento As Integer = 0


        Dim documentosEmpresa As DataTable = documento.buscarDocumentoPorArea(usuario.areaUsuario, "empresa", idCarpeta)
        If (documentosEmpresa Is Nothing) Then
            seccionEmpresa.Visible = False
        Else
            If (documentosEmpresa.Rows.Count > 0) Then
                Me.gridDocumentosEmpresa.DataSource = documentosEmpresa
                Me.gridDocumentosEmpresa.DataBind()
                For Each documentoEmpresa As GridViewRow In gridDocumentosEmpresa.Rows
                    Dim chk As HtmlInputCheckBox
                    chk = documentoEmpresa.FindControl("chkDocEmpresa")
                    If documentoEmpresa.Cells(2).Text = "espera" Then
                        chk.Checked = True
                    End If
                Next
                gridDocumentosEmpresa.Columns(0).Visible = False
                gridDocumentosEmpresa.Columns(2).Visible = False
            End If
        End If

        Dim documentosTrabajador As DataTable = documento.buscarDocumentoPorArea(usuario.areaUsuario, "trabajador", idCarpeta)
        If (documentosTrabajador Is Nothing) Then
            seccionTrabajador.Visible = False
        Else
            If (documentosTrabajador.Rows.Count > 0) Then
                Me.gridDocumentosTrabajador.DataSource = documentosTrabajador
                Me.gridDocumentosTrabajador.DataBind()
                For Each documentoTrabajador As GridViewRow In gridDocumentosTrabajador.Rows
                    Dim chk As HtmlInputCheckBox
                    chk = documentoTrabajador.FindControl("chkDocTrabajador")
                    If documentoTrabajador.Cells(2).Text = "espera" Then
                        chk.Checked = True
                    End If
                Next
                gridDocumentosTrabajador.Columns(0).Visible = False
                gridDocumentosTrabajador.Columns(2).Visible = False
            End If
        End If

        Dim documentosVehiculo As DataTable = documento.buscarDocumentoPorArea(usuario.areaUsuario, "vehiculo", idCarpeta)
        If (documentosVehiculo Is Nothing) Then
            seccionVehiculo.Visible = False
        Else
            If (documentosVehiculo.Rows.Count > 0) Then
                Me.gridDocumentosVehiculo.DataSource = documentosEmpresa
                Me.gridDocumentosVehiculo.DataBind()
                For Each documentoVehiculo As GridViewRow In gridDocumentosVehiculo.Rows
                    Dim chk As HtmlInputCheckBox
                    chk = documentoVehiculo.FindControl("chkDocVehiculo")
                    If documentoVehiculo.Cells(2).Text = "espera" Then
                        chk.Checked = True
                    End If
                Next
                gridDocumentosVehiculo.Columns(0).Visible = False
                gridDocumentosVehiculo.Columns(2).Visible = False
            End If
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim h As New HtmlElement

        'Dim VALUE As String
        'VALUE = Request.Form("gridDocumentosEmpresa_Checkbox1_0")

        'Dim c As New HtmlInputCheckBox

        'If (i1.Checked) Then
        '    Label1.Text = "chequeado"
        'Else
        '    Label1.Text = "no chequeado"
        'End If

        'Dim l As New Checkbox
        'For documento As Integer = 0 To cantidadDocumentosEmpresa - 1
        '    'c = Page.FindControl(documento.ToString())
        '    'If (c.Checked) Then
        '    '    vehiculos.Visible = True
        '    'Else
        '    '    vehiculos.Visible = False
        '    'End If
        '    l = Page.FindControl("lb" + documento.ToString())
        'Next
    End Sub
End Class