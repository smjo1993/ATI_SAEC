Public Class administrarUsuarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        sinUsuarios.Visible = False
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarGrid()
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
        Dim rutUsuario As String = usuario.rutUsuario
        'Dim idCarpeta As Integer = decodificarId()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub
    Protected Sub cargarGrid()
        Dim usuario As New clsUsuarioSAEC
        Dim tablaUsuarios As DataTable = usuario.listarUsuarios()

        If (tablaUsuarios Is Nothing) Then
            sinUsuarios.Visible = True
        Else
            If (tablaUsuarios.Rows.Count > 0) Then
                Me.gridUsuarios.DataSource = tablaUsuarios
                Me.gridUsuarios.DataBind()
                Dim chk As HtmlInputCheckBox
                For Each rowUsuario As GridViewRow In gridUsuarios.Rows
                    chk = rowUsuario.FindControl("chkEstado")
                    If rowUsuario.Cells(4).Text = "A" Then
                        chk.Checked = True
                    End If
                Next
            Else
                sinUsuarios.Visible = True
            End If
        End If
    End Sub
    Protected Sub gridEmpresas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridUsuarios.RowCommand

        If (e.CommandName = "administrar") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Session("nombreUsuario") = gridUsuarios.Rows(pos).Cells(1).Text
            Session("rutUsuario") = gridUsuarios.Rows(pos).Cells(0).Text
            Response.Redirect("administrarPermisosUsuario.aspx")
        End If


    End Sub

    Protected Sub btnCambioEstado_Click(sender As Object, e As EventArgs) Handles btnCambioEstado.Click
        Dim usuario As New clsUsuarioSAEC
        Dim chk As HtmlInputCheckBox
        For Each rowUsuario As GridViewRow In gridUsuarios.Rows
            chk = rowUsuario.FindControl("chkEstado")
            If chk.Checked = True Then
                usuario.cambiarEstadoCuentaUsuario(rowUsuario.Cells(0).Text, "A")
            Else
                usuario.cambiarEstadoCuentaUsuario(rowUsuario.Cells(0).Text, "I")
            End If
        Next
        Response.Redirect("administrarUsuarios.aspx")
    End Sub
End Class