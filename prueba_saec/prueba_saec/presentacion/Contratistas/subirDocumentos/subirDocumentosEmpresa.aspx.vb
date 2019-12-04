Public Class subirDocumentosEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        cargarMenu()

        If IsPostBack Then
            Return
        End If

        Dim rutContratista As String = "8660229"
        Dim TablaDocumentosPendienteEmpresa As DataTable = crearDocumentos().obtenerDocumentosEstadoPendienteEmpresa(rutContratista)


        gridSubirDocumentosEmpresa.DataSource = TablaDocumentosPendienteEmpresa
        gridSubirDocumentosEmpresa.DataBind()



    End Sub

    Public Function crearDocumentos() As Object

        Dim documentosEspera = New clsDocumento()

        Return documentosEspera

    End Function

    Protected Sub cargarMenu()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuContratistaCarpeta(rutContratista, 0, "")
        lblMenu.Text = stringMenu
        lblMenu.Visible = True

    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        'Se recorre la grilla para subirlos documentos del cliente EMPRESA
        For Each fila As GridViewRow In gridSubirDocumentosEmpresa.Rows

            'Variables para identificar las propiedades del documento y el archivo
            Dim idCarpeta As Integer = fila.Cells(3).Text
            Dim idDocumento As Integer = fila.Cells(4).Text
            Dim idArea As Integer = fila.Cells(5).Text
            Dim nombreArchivo As String = fila.Cells(0).Text
            Dim rutEmpresa As String = fila.Cells(7).Text
            Dim archivo As HtmlInputFile

            archivo = fila.FindControl("fileArchivo")

            'Si encuentra el archivo subido lo guarda en la ruta especifica
            If archivo.PostedFile.FileName <> "" Then

                My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Empresa/"))
                Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Empresa/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                archivo.PostedFile.SaveAs(ruta)
                Dim documento = New clsDocumento()
                documento.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "enviado", ruta)

            End If


        Next




    End Sub

    Protected Sub documentosEmpresa_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridSubirDocumentosEmpresa.RowCommand

        If (e.CommandName = "subir") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idCarpeta As Integer = gridSubirDocumentosEmpresa.Rows(pos).Cells(3).Text
            Dim idDocumento As Integer = gridSubirDocumentosEmpresa.Rows(pos).Cells(4).Text
            Dim idArea As Integer = gridSubirDocumentosEmpresa.Rows(pos).Cells(5).Text
            Dim nombreArchivo As String = gridSubirDocumentosEmpresa.Rows(pos).Cells(0).Text
            Dim rutEmpresa As String = gridSubirDocumentosEmpresa.Rows(pos).Cells(7).Text

            Dim archivo As HtmlInputFile
            archivo = gridSubirDocumentosEmpresa.Rows(pos).FindControl("fileArchivo")

            'Si encuentra el archivo subido lo guarda en la ruta especifica
            If archivo.PostedFile.FileName <> "" Then

                My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Empresa/"))
                Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Empresa/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                archivo.PostedFile.SaveAs(ruta)
                Dim documento = New clsDocumento()

                documento.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "enviado", ruta)
                Response.Redirect(HttpContext.Current.Request.Url.ToString)


            End If

        End If

    End Sub

End Class