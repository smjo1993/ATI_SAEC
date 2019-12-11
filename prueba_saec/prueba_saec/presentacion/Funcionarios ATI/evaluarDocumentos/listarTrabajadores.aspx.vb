Public Class listarTrabajadores1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Return
        End If
        'cargarMenu()

        Dim trabajadores = New clsTrabajador()
        Dim idCarpeta As Integer = 113
        Dim idArea As Integer = 2
        Dim TablaTrabajadores As DataTable = trabajadores.listarTrabajadoresParaEvaluar(idCarpeta, idArea)
        gridListarTrabajadoresParaEvaluar.DataSource = TablaTrabajadores
        gridListarTrabajadoresParaEvaluar.DataBind()
    End Sub
    Protected Sub cargarMenu()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuContratistaCarpeta(rutContratista, 0, "")
        lblMenu.Text = stringMenu
        lblMenu.Visible = True

    End Sub
    Protected Sub btnIrTrabajador_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarTrabajadoresParaEvaluar.RowCommand

        If (e.CommandName = "ir") Then
            'Se obtienen los datos de la columna de la grid para mandar el datatable a la otra vista
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim rutTrabajador As String = gridListarTrabajadoresParaEvaluar.Rows(pos).Cells(0).Text
            Dim idCarpeta As Integer = gridListarTrabajadoresParaEvaluar.Rows(pos).Cells(2).Text
            Dim idTrabajador As Integer = gridListarTrabajadoresParaEvaluar.Rows(pos).Cells(3).Text
            'Dim rutContratista As String = "8660229"
            Session("rutTrabajador") = rutTrabajador
            Session("idTrabajador") = idTrabajador
            Session("idCarpeta") = idCarpeta
            'Session("rutContratista") = rutContratista
            Response.Redirect("evaluarDocumentosTrabajador.aspx")

        End If

    End Sub
End Class