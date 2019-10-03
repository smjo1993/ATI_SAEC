Public Class menu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbMensaje.Text = Session("usuario") + Session("contrasenia")
    End Sub

End Class