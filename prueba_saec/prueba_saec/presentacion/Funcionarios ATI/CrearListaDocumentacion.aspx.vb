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

    Protected Function decodificarId() As Integer
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idCodificada)
        Dim idDecodificada As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim idCarpeta As Integer = Convert.ToInt32(idDecodificada)
        Return idCarpeta
    End Function
    Private Sub cargarDatos()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim documento As New clsDocumento
        Dim idCarpeta As Integer = decodificarId()
        Dim chk As HtmlInputCheckBox

        Dim documentosEmpresa As DataTable = documento.buscarDocumentoPorArea(usuario.areaUsuario, "empresa", idCarpeta)
        If (documentosEmpresa Is Nothing) Then
            seccionEmpresa.Visible = False
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
                    chk = documentoTrabajador.FindControl("chkDocTrabajador")
                    If documentoTrabajador.Cells(2).Text = "espera" Then
                        chk.Checked = True
                    End If
                Next
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
                    chk = documentoVehiculo.FindControl("chkDocVehiculo")
                    If documentoVehiculo.Cells(2).Text = "espera" Then
                        chk.Checked = True
                    End If
                Next
            End If
        End If
    End Sub

    Protected Sub btnPedirDocumento_Click(sender As Object, e As EventArgs) Handles btnPedirDocumento.Click
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim idCarpeta As Integer = decodificarId()
        Dim chk As HtmlInputCheckBox
        For Each documentoTrabajador As GridViewRow In gridDocumentosTrabajador.Rows
            chk = documentoTrabajador.FindControl("chkDocTrabajador")
            If chk.Checked = True Then 'pasan a espera
            Else 'sino quedan no solicitados
            End If
        Next

        For Each documentoTrabajador As GridViewRow In gridDocumentosTrabajador.Rows
            chk = documentoTrabajador.FindControl("chkDocTrabajador")
            If chk.Checked = True Then
            Else
            End If
        Next

        For Each documentoVehiculo As GridViewRow In gridDocumentosVehiculo.Rows
            chk = documentoVehiculo.FindControl("chkDocVehiculo")
            If chk.Checked = True Then
            Else
            End If
        Next
    End Sub
End Class