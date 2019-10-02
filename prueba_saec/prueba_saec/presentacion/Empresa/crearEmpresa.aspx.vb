Public Class crearEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim contratista As clsContratista
        Dim contratistas As DataTable = contratista.ListarContratistas()

        dropUsuarios.DataSource = contratistas
        dropUsuarios.DataBind()
    End Sub

End Class