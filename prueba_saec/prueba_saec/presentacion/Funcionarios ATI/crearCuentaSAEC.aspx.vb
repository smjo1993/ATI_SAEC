Public Class crearCuentaSAEC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        errorNumero.Visible = False
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarRoles()
            cargarDatosUsuario()
        End If
    End Sub
    Protected Sub cargarRoles()
        Dim newListItem = New ListItem("seleccione un item", "-1")
        dropRol.Items.Add(newListItem)
        Dim rol As New clsRol
        Dim listaRoles As DataTable = rol.listarRoles()
        If (listaRoles Is Nothing) Then
        Else
            If (listaRoles.Rows.Count > 0) Then
                For Each celda As DataRow In listaRoles.Rows
                    Dim item As New ListItem()
                    item.Text = celda("nombre").ToString()
                    item.Value = celda("id").ToString()
                    dropRol.Items.Add(item)
                Next
            End If
        End If
    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "3,3", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
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
        'txtEmail.ReadOnly = True
        'txtFono.ReadOnly = True
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
        If (dropRol.SelectedValue = "-1") Then
            lblMensaje.Text = "Seleccione un rol de la lista"
            errorNumero.Visible = True
            cargarMenu()
        Else
            Try
                Dim Fono As Integer = Convert.ToInt32(txtFono.Text)
                Dim rol As Integer = Convert.ToInt32(dropRol.SelectedValue)
                Dim usuarioSAEC As clsUsuarioSAEC = Session("usuarioNuevoSAEC")
                Dim nuevoUsuario As New clsUsuarioSAEC
                Dim mensaje As String = nuevoUsuario.insertarUsuario(usuarioSAEC.getNombre, usuarioSAEC.getLogin, usuarioSAEC.getClave, usuarioSAEC.getRut, usuarioSAEC.getEstado, Fono, txtEmail.Text, usuarioSAEC.getArea, rol)
                Session("mensaje") = mensaje
                Response.Redirect("iniciarCuentasUsuarioATI.aspx")
            Catch ex As FormatException
                'MessageBox.Show(ex.Message)
                lblMensaje.Text = "Error en el numero Telefonico"
                errorNumero.Visible = True
                cargarMenu()
            End Try
        End If
    End Sub
End Class