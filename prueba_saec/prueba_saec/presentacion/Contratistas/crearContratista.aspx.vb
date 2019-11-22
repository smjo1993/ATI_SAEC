Public Class crearContratista
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
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
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim contratista As New clsContratista
        Dim insercion As New Boolean
        If (txtNombre.Text.Trim() = "" Or txtNombreUsuario.Text.Trim() = "" Or txtPassword.Text.Trim() = "" Or txtRut.Text.Trim() = "" Or txtFono.Text.Trim() = "" Or txtCorreo.Text.Trim() = "") Then
            lblAdvertencia.Text = "Uno de los campos necesarios se encuentra en blanco"
        Else
            insercion = contratista.insertarContratista(txtNombre.Text.Trim(), txtNombreUsuario.Text.Trim(), txtPassword.Text.Trim(), txtRut.Text.Trim(), "I", txtFono.Text.Trim(), txtCorreo.Text.Trim())
            If (insercion) Then
                lblAdvertencia.Text = "Contratista ingresado con éxito."
                txtNombre.Text = ""
                txtNombreUsuario.Text = ""
                txtPassword.Text = ""
                txtRut.Text = ""
                txtFono.Text = ""
                txtCorreo.Text = ""
            Else
                lblAdvertencia.Text = "La acción no se pudo realizar. Pof favor inténtelo de nuevo."
            End If
        End If
    End Sub

End Class