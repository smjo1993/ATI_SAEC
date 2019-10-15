Public Class crearTrabajador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Dim contratista As New clsContratista
        Dim insercion As New Boolean
        If (txtNombre.Text.Trim() = "" Or txtNombreUsuario.Text.Trim() = "" Or txtPassword.Text.Trim() = "" Or txtRut.Text.Trim() = "" Or txtFono.Text.Trim() = "" Or txtCorreo.Text.Trim() = "") Then
            lblAdvertencia.Text = "Uno o más de los campos necesarios se encuentran en blanco"
        Else
            insercion = contratista.insertarContratista(txtNombre.Text.Trim, txtNombreUsuario.Text.Trim(), txtPassword.Text.Trim(), txtRut.Text.Trim(), "a", txtFono.Text.Trim(), txtCorreo.Text.Trim())
        End If
    End Sub
End Class