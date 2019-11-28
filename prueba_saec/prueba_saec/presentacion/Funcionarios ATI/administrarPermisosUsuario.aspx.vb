﻿Public Class administrarUsuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        validarUsuario()
        lblMenu.Visible = False
        sinPermisos.Visible = False
        btnModalConfirmacion.Visible = False
        cargarMenu()
        If Page.IsPostBack Then
            Return
        End If
        cargarPermisos()
    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        End If
        Dim nombreUsuario As String = Session("nombreUsuario")
        lblNombreUsuario.Text = nombreUsuario
    End Sub

    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        'Dim idCarpeta As Integer = decodificarId()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub
    Protected Sub cargarPermisos()
        Dim menu As New clsUsuarioSAEC
        Dim rutUsuario As String = Session("rutUsuario")
        Dim permisos As DataTable = menu.obtenerPermisos(rutUsuario)

        If (permisos Is Nothing) Then
            sinPermisos.Visible = True
        Else
            If (permisos.Rows.Count > 0) Then
                btnModalConfirmacion.Visible = True
                Me.gridPermisos.DataSource = permisos
                Me.gridPermisos.DataBind()
                Dim chk As HtmlInputCheckBox
                For Each permiso As GridViewRow In gridPermisos.Rows
                    chk = permiso.FindControl("chkEstado")
                    If permiso.Cells(2).Text = "A" Then
                        chk.Checked = True
                    End If
                Next
            Else
                sinPermisos.Visible = True
            End If
        End If

    End Sub

    Protected Sub btnPermisos_Click(sender As Object, e As EventArgs) Handles btnPermisos.Click
        Dim menu As New clsMenu
        Dim rutUsuario As String = Session("rutUsuario")
        Dim chk As HtmlInputCheckBox
        For Each rowUsuario As GridViewRow In gridPermisos.Rows
            chk = rowUsuario.FindControl("chkEstado")
            If chk.Checked = True Then
                menu.actualizarEstadoOpcion(rowUsuario.Cells(0).Text, rutUsuario, rowUsuario.Cells(4).Text, "A")
            Else
                menu.actualizarEstadoOpcion(rowUsuario.Cells(0).Text, rutUsuario, rowUsuario.Cells(4).Text, "I")
            End If
        Next
        'Response.Redirect("administrarPermisosUsuario.aspx")
    End Sub
End Class