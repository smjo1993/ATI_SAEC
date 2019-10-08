Public Class menu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim contratista As clsContratista = Session("contratistaEntrante")
        lbMensaje.Text = contratista.nombreContratista

    End Sub

End Class