Public Class iniciarCuentasUsuarioATI
    Inherits System.Web.UI.Page
    Dim usuariosATI As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        sinUsuarios.Visible = False
        lblMensaje.Visible = False
        sinCoincidencia.Visible = False
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarUsuarios()
        End If
    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "1,1", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
        End If
        Dim mensaje As String = Session("mensaje")
        If mensaje <> Nothing Then
            lblMensaje.Visible = True
            lblMensaje.Text = mensaje
        End If
    End Sub

    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub

    Protected Sub cargarUsuarios()
        Dim usuario As New clsUsuario
        Dim rutUsuario As String = Session("rutUsuario")
        usuariosATI = usuario.listarUsuarios()

        If (usuariosATI Is Nothing) Then
            sinUsuarios.Visible = True
        Else
            If (usuariosATI.Rows.Count > 0) Then
                Me.gridUsuariosATI.DataSource = usuariosATI
                Me.gridUsuariosATI.DataBind()
            Else
                sinUsuarios.Visible = True
            End If
        End If

    End Sub

    Protected Sub gridUsuariosATI_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridUsuariosATI.RowCommand

        If (e.CommandName = "crearCuenta") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim fono As String = gridUsuariosATI.Rows(pos).Cells(5).Text
            If fono = "" Or fono = "-" Or fono = "&nbsp;" Then
                fono = 0
            Else
                fono = Convert.ToInt32(gridUsuariosATI.Rows(pos).Cells(5).Text)
            End If
            Dim correo As String = gridUsuariosATI.Rows(pos).Cells(4).Text
            If correo = "" Or correo = "-" Or correo = "&nbsp;" Then
                correo = "sin correo"
            Else
                correo = gridUsuariosATI.Rows(pos).Cells(4).Text
            End If
            Dim usuario As New clsUsuarioSAEC(gridUsuariosATI.Rows(pos).Cells(0).Text,
                                                                      gridUsuariosATI.Rows(pos).Cells(1).Text,
                                                                      gridUsuariosATI.Rows(pos).Cells(2).Text,
                                                                      gridUsuariosATI.Rows(pos).Cells(3).Text,
                                                                      "A",
                                                                      fono,
                                                                      correo,
                                                                      Convert.ToInt32(gridUsuariosATI.Rows(pos).Cells(6).Text))
            Session("usuarioNuevoSAEC") = usuario
            Response.Redirect("crearCuentaSAEC.aspx")
        End If
    End Sub
    Private Sub lnkBuscarNombre_Click(sender As Object, e As EventArgs) Handles lnkBuscarNombre.Click
        Dim nombreFiltro As String = txtBuscarNombre.Text.Trim
        If nombreFiltro = "" Then
            cargarMenu()
            cargarUsuarios()
        Else
            Dim usuario As New clsUsuario
            usuariosATI = usuario.usuariosFiltrados(nombreFiltro, "")
            If usuariosATI.Rows.Count > 0 Then
                Me.gridUsuariosATI.DataSource = usuariosATI
                Me.gridUsuariosATI.DataBind()
                txtBuscarNombre.Text = ""
                cargarMenu()
            Else
                Dim d As New DataTable
                Me.gridUsuariosATI.DataSource = d
                Me.gridUsuariosATI.DataBind()
                sinCoincidencia.Visible = True
                txtBuscarNombre.Text = ""
                cargarMenu()
            End If
        End If
    End Sub
    Private Sub lnkBuscarUsuario_Click(sender As Object, e As EventArgs) Handles lnkBuscarUsuario.Click
        Dim usuarioFiltro As String = txtBuscarUsuario.Text.Trim
        If usuarioFiltro = "" Then
            cargarMenu()
            cargarUsuarios()
        Else
            Dim usuario As New clsUsuario
            usuariosATI = usuario.usuariosFiltrados("", usuarioFiltro)
            If usuariosATI.Rows.Count > 0 Then
                Me.gridUsuariosATI.DataSource = usuariosATI
                Me.gridUsuariosATI.DataBind()
                txtBuscarUsuario.Text = ""
                cargarMenu()
            Else
                Dim d As New DataTable
                Me.gridUsuariosATI.DataSource = d
                Me.gridUsuariosATI.DataBind()
                sinCoincidencia.Visible = True
                txtBuscarUsuario.Text = ""
                cargarMenu()
            End If
        End If
    End Sub
End Class