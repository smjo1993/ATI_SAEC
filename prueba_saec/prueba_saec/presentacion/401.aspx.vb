Public Class WebForm2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cargarMenu()
        If Not Page.IsPostBack Then
            cargarMenu()
        End If
    End Sub
    Protected Sub cargarMenu()
        Dim menu As New clsMenu
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If usuario Is Nothing Then
            Dim contratista As clsContratista = Session("contratistaEntrante")
            Dim rutContratista As String = contratista.getRut()
            'Dim idCarpeta As Integer = decodificarId()
            Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutContratista)
            lblMenu.Text = stringMenu
            lblMenu.Visible = True
        Else
            Dim rutUsuario As String = usuario.getRut()
            'Dim idCarpeta As Integer = decodificarId()

            Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
            lblMenu.Text = stringMenu
            lblMenu.Visible = True
            LblNombreUsuario.Text = usuario.getNombre().ToString()
        End If

    End Sub
    Protected Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Session.Contents.RemoveAll()
        Response.Redirect("login.aspx")
    End Sub

End Class