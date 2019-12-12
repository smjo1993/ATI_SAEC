Public Class listarTrabajadores1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Return
        End If
        cargarMenu()

        Dim trabajadores = New clsTrabajador()
        Dim idCarpeta As Integer = decodificarId()
        Dim idArea As Integer = Session("usuario").getArea()
        Dim TablaTrabajadores As DataTable = trabajadores.listarTrabajadoresParaEvaluar(idCarpeta, idArea)
        gridListarTrabajadoresParaEvaluar.DataSource = TablaTrabajadores
        gridListarTrabajadoresParaEvaluar.DataBind()
    End Sub
    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim idCarpeta As Integer = decodificarId()
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(nombreCodificado)
        Dim nombreDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiCarpeta(rutUsuario, idCarpeta, nombreDecodificado)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub

    Protected Function decodificarId() As Integer
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idCodificada)
        Dim idDecodificada As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim idCarpeta As Integer = Convert.ToInt32(idDecodificada)
        Return idCarpeta
    End Function
    Protected Sub btnIrTrabajador_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarTrabajadoresParaEvaluar.RowCommand

        If (e.CommandName = "ir") Then
            'Se obtienen los datos de la columna de la grid para mandar el datatable a la otra vista
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim rutTrabajador As String = gridListarTrabajadoresParaEvaluar.Rows(pos).Cells(0).Text
            Dim idCarpeta As Integer = gridListarTrabajadoresParaEvaluar.Rows(pos).Cells(2).Text
            Dim idTrabajador As Integer = gridListarTrabajadoresParaEvaluar.Rows(pos).Cells(3).Text

            Session("rutTrabajador") = rutTrabajador
            Session("idTrabajador") = idTrabajador
            Session("idCarpeta") = idCarpeta

            'Session("rutContratista") = rutContratista
            'Response.Redirect("evaluarDocumentosTrabajador.aspx")

            'Se codifica idCarpeta para enviarlo por URL

            Dim idCarpetaURL As String = Request.QueryString("i").ToString()
            Dim nombreCarpetaURL As String = Request.QueryString("n").ToString()
            'Session("idCodificada") = idCarpetaURL
            'Session("nombreCodificado") = nombreCarpetaURL
            Response.Redirect("evaluarDocumentosTrabajador.aspx?i=" + idCarpetaURL + "&n=" + nombreCarpetaURL + " ")

        End If

    End Sub
End Class