Public Class administrarUsuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        sinPermisos.Visible = False
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
        Dim nombreUsuario As String = Session("nombreUsuario")
        lblNombreUsuario.Text = nombreUsuario
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
        Dim menu As New clsUsuarioSAEC
        Dim rutUsuario As String = Session("rutUsuario")
        Dim permisos As DataTable = menu.obtenerPermisos(rutUsuario)

        If (permisos Is Nothing) Then
            sinPermisos.Visible = True
        Else
            If (permisos.Rows.Count > 0) Then
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
End Class