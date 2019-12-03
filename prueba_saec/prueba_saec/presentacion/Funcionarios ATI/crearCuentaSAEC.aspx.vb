Public Class crearCuentaSAEC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarDatosUsuario()
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
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub
    Protected Sub cargarDatosUsuario()
        Dim usuarioSAEC As clsUsuarioSAEC = Session("usuarioNuevoSAEC")
        txtClave.ReadOnly = True
        txtEmail.ReadOnly = True
        txtFono.ReadOnly = True
        txtNombreUsuario.ReadOnly = True
        txtLogin.ReadOnly = True
        txtRut.ReadOnly = True

        txtClave.Text = usuarioSAEC.getClave
        txtEmail.Text = usuarioSAEC.getCorreo
        txtFono.Text = usuarioSAEC.getFono.ToString
        txtNombreUsuario.Text = usuarioSAEC.getNombre
        txtLogin.Text = usuarioSAEC.getLogin
        txtRut.Text = usuarioSAEC.getRut
    End Sub
    Protected Sub btnCrearCuenta_Click(sender As Object, e As EventArgs) Handles btnCrearCuenta.Click
        Dim usuarioSAEC As clsUsuarioSAEC = Session("usuarioNuevoSAEC")
        Dim nuevoUsuario As New clsUsuarioSAEC
        Dim mensaje As String = nuevoUsuario.insertarUsuario(usuarioSAEC.getNombre, usuarioSAEC.getLogin, usuarioSAEC.getClave, usuarioSAEC.getRut, usuarioSAEC.getEstado, usuarioSAEC.getFono, usuarioSAEC.getCorreo, usuarioSAEC.getArea)
        Session("mensaje") = mensaje
        Response.Redirect("iniciarCuentasUsuarioATI.aspx")
    End Sub
End Class