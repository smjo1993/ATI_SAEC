Public Class SubirDocumentosTrabajador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        cargarMenu()

        If IsPostBack Then
            Return
        End If

        'Se pobla la grilla con los datos obtenidos en la vista anterior
        gridListarDocumentosTrabajador.DataSource = Session("documentosTrabajador")
        gridListarDocumentosTrabajador.DataBind()
        lblTrabajador.Text = Session("rutTrabajador")

    End Sub

    Protected Sub cargarMenu()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuContratistaCarpeta(rutContratista, 0, "")
        lblMenu.Text = stringMenu
        lblMenu.Visible = True

    End Sub

    Protected Sub documentosVehiculos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarDocumentosTrabajador.RowCommand

        If (e.CommandName = "subir") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idCarpeta As Integer = gridListarDocumentosTrabajador.Rows(pos).Cells(5).Text
            Dim idDocumento As Integer = gridListarDocumentosTrabajador.Rows(pos).Cells(6).Text
            Dim idArea As Integer = gridListarDocumentosTrabajador.Rows(pos).Cells(7).Text
            Dim nombreArchivo As String = gridListarDocumentosTrabajador.Rows(pos).Cells(2).Text
            Dim rutEmpresa As String = gridListarDocumentosTrabajador.Rows(pos).Cells(9).Text
            Dim rutTrabajador As String = gridListarDocumentosTrabajador.Rows(pos).Cells(0).Text
            Dim idTrabajador As Integer = gridListarDocumentosTrabajador.Rows(pos).Cells(10).Text

            Dim archivo As HtmlInputFile
            archivo = gridListarDocumentosTrabajador.Rows(pos).FindControl("fileArchivo")

            'Si encuentra el archivo subido lo guarda en la ruta especifica
            If archivo.PostedFile.FileName <> "" Then

                My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos trabajadores/" + rutTrabajador))
                Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos trabajadores/" + rutTrabajador + "/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                archivo.PostedFile.SaveAs(ruta)
                Dim documento = New clsDocumento()
                documento.cambiarEstadoDocumentoTrabajador(idCarpeta, idArea, idDocumento, idTrabajador, "enviado", ruta)
                Response.Redirect(HttpContext.Current.Request.Url.ToString)

            End If

        End If

    End Sub

End Class