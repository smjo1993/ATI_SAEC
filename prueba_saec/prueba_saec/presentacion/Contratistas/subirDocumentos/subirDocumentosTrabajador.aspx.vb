Public Class SubirDocumentosTrabajador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        validarContratista()
        cargarMenu()

        If IsPostBack Then
            Return
        End If

        'Se pobla la grilla con los datos obtenidos en listarTrabajadores
        Dim trabajador = New clsTrabajador()
        Dim listaDocumentosTrabajador As DataTable = trabajador.listarDocumentosTrabajador(Session("idTrabajador"), Session("rutContratista"))
        gridListarDocumentosTrabajador.DataSource = listaDocumentosTrabajador
        gridListarDocumentosTrabajador.DataBind()
        lblTrabajador.Text = Session("rutTrabajador")

    End Sub
    Protected Sub validarContratista()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        If (contratista Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(contratista.getRut, "61,3", "C")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
        End If

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

            'Se recuperan los datos del datagrid 
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


                If gridListarDocumentosTrabajador.Rows(pos).Cells(11).Text = "" Or gridListarDocumentosTrabajador.Rows(pos).Cells(11).Text = "&nbsp;" Then

                    'Si el contratista no ha subido un archivo anteriormente 
                    My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos trabajadores/" + rutTrabajador))
                    Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos trabajadores/" + rutTrabajador + "/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                    archivo.PostedFile.SaveAs(ruta)
                    Dim documento = New clsDocumento()
                    documento.cambiarEstadoDocumentoTrabajador(idCarpeta, idArea, idDocumento, idTrabajador, "enviado", ruta)
                    Response.Redirect(HttpContext.Current.Request.Url.ToString)

                Else
                    'Si el contratista subio un documento previamente, se elimina y se sube el nuevo archivo.
                    My.Computer.FileSystem.DeleteFile(gridListarDocumentosTrabajador.Rows(pos).Cells(11).Text)
                    My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos trabajadores/" + rutTrabajador))
                    Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos trabajadores/" + rutTrabajador + "/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                    archivo.PostedFile.SaveAs(ruta)
                    Dim documento = New clsDocumento()
                    documento.cambiarEstadoDocumentoTrabajador(idCarpeta, idArea, idDocumento, idTrabajador, "enviado", ruta)
                    Response.Redirect(HttpContext.Current.Request.Url.ToString)

                End If



            End If

        End If

    End Sub

End Class