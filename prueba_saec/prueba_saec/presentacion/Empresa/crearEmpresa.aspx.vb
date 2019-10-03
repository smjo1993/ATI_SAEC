Public Class crearEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim contratista As clsContratista
        Dim contratistas As DataTable = contratista.ListarContratistas()

        dropUsuarios.DataSource = contratistas
        dropUsuarios.DataBind()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class