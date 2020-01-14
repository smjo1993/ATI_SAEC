Imports System.Drawing

Public Class subirDocumentosEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinDocumentos.Visible = False
        validarContratista()
        cargarMenu()
        cargarNotificacionesComentarios()

        If IsPostBack Then
            Return
        End If

        Dim rutContratista As String = Session("contratistaEntrante").getRut
        Dim TablaDocumentosPendienteEmpresa As DataTable = crearDocumentos().obtenerDocumentosEstadoPendienteEmpresa(rutContratista)

        If TablaDocumentosPendienteEmpresa Is Nothing Then
            sinDocumentos.Visible = True
        Else
            If TablaDocumentosPendienteEmpresa.Rows.Count > 0 Then

                gridSubirDocumentosEmpresa.DataSource = TablaDocumentosPendienteEmpresa
                gridSubirDocumentosEmpresa.DataBind()
            Else
                sinDocumentos.Visible = True
            End If
        End If

    End Sub


    Protected Sub validarContratista()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        If (contratista Is Nothing) Then
            Response.Redirect("../../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(contratista.getRut, "61,1", "C")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../../401.aspx")
            End If
        End If
        LblNombreUsuario.Text = contratista.getNombre().ToString()
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
    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


    '    'Se recorre la grilla para subirlos documentos del cliente EMPRESA
    '    For Each fila As GridViewRow In gridSubirDocumentosEmpresa.Rows

    '        'Variables para identificar las propiedades del documento y el archivo
    '        Dim idCarpeta As Integer = fila.Cells(3).Text
    '        Dim idDocumento As Integer = fila.Cells(4).Text
    '        Dim idArea As Integer = fila.Cells(5).Text
    '        Dim nombreArchivo As String = fila.Cells(0).Text
    '        Dim rutEmpresa As String = fila.Cells(7).Text
    '        Dim archivo As HtmlInputFile

    '        archivo = fila.FindControl("fileArchivo")

    '        'Si encuentra el archivo subido lo guarda en la ruta especifica
    '        If archivo.PostedFile.FileName <> "" Then

    '            My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Empresa/"))
    '            Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Empresa/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
    '            archivo.PostedFile.SaveAs(ruta)
    '            Dim documento = New clsDocumento()
    '            documento.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "enviado", ruta)

    '        End If


    '    Next




    'End Sub

    Protected Sub documentosEmpresa_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridSubirDocumentosEmpresa.RowCommand

        If (e.CommandName = "subir") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idCarpeta As Integer = gridSubirDocumentosEmpresa.Rows(pos).Cells(3).Text
            Dim idDocumento As Integer = gridSubirDocumentosEmpresa.Rows(pos).Cells(4).Text
            Dim idArea As Integer = gridSubirDocumentosEmpresa.Rows(pos).Cells(5).Text
            Dim nombreArchivo As String = gridSubirDocumentosEmpresa.Rows(pos).Cells(0).Text
            Dim rutEmpresa As String = gridSubirDocumentosEmpresa.Rows(pos).Cells(7).Text
            Dim periodo As String = gridSubirDocumentosEmpresa.Rows(pos).Cells(9).Text

            Dim archivo As HtmlInputFile
            archivo = gridSubirDocumentosEmpresa.Rows(pos).FindControl("fileArchivo")

            'Si encuentra el archivo subido lo guarda en la ruta especifica
            If archivo.PostedFile.FileName <> "" Then

                If gridSubirDocumentosEmpresa.Rows(pos).Cells(8).Text = "" Or gridSubirDocumentosEmpresa.Rows(pos).Cells(8).Text = "&nbsp;" Then

                    'Si el contratista no ha subido un archivo anteriormente 
                    My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/" + periodo + "/documentos Empresa/"))
                    Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/" + periodo + "/documentos Empresa/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                    archivo.PostedFile.SaveAs(ruta)
                    Dim documento = New clsDocumento()

                    documento.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "enviado", ruta)
                    Response.Redirect(HttpContext.Current.Request.Url.ToString)

                Else
                    'Si el contratista subio un documento previamente, se elimina y se sube el nuevo archivo.
                    My.Computer.FileSystem.DeleteFile(gridSubirDocumentosEmpresa.Rows(pos).Cells(8).Text)
                    My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/" + periodo + "/documentos Empresa/"))
                    Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/" + periodo + "/documentos Empresa/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                    archivo.PostedFile.SaveAs(ruta)
                    Dim documento = New clsDocumento()

                    documento.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "enviado", ruta)
                    Response.Redirect(HttpContext.Current.Request.Url.ToString)

                End If



            End If

        End If

        If (e.CommandName = "verComentarios") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idArea As Integer = gridSubirDocumentosEmpresa.Rows(pos).Cells(5).Text
            Dim idDocumento As Integer = gridSubirDocumentosEmpresa.Rows(pos).Cells(4).Text
            Dim idCarpeta As Integer = gridSubirDocumentosEmpresa.Rows(pos).Cells(3).Text

            Session("areaId") = idArea
            Session("documentoId") = idDocumento
            Session("carpetaId") = idCarpeta
            Session("rutUsuario") = Session("contratistaEntrante").getRut
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("../verComentarios.aspx")

        End If

    End Sub

    Protected Sub gridSubirDocumentosEmpresa_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridSubirDocumentosEmpresa.RowDataBound

        If e.Row.Cells(2).Text = "aprobado" Then

            e.Row.BackColor = Color.FromArgb(222, 249, 241)

        End If

        If e.Row.Cells(2).Text = "pendiente" Then

            e.Row.BackColor = Color.FromArgb(255, 240, 240)

        End If

        If e.Row.Cells(2).Text = "enviado" Then

            e.Row.BackColor = Color.FromArgb(255, 252, 231)

        End If
    End Sub
    Private Sub cargarNotificacionesComentarios()

        Dim notificacion As New clsNotificacion
        Dim usuario As clsContratista = Session("contratistaEntrante")
        Dim rutUsuario As String = usuario.getRut
        Dim tarjeta As String = ""

        Dim listaNotificaciones As Data.DataTable = notificacion.obtenerNotificaciones(rutUsuario)

        If listaNotificaciones.Rows.Count > 0 Then

            For Each fila As DataRow In listaNotificaciones.Rows

                Dim resumenComentario As String = fila("texto")
                Dim nombreUsuarioRespuesta As String = fila("nombreAutor")
                Dim nombreDocumento As String = fila("nombreDocumento")
                Dim areaComentario As String = fila("areaComentario")
                Dim idDocumento As Integer = fila("idDocumento")
                Dim idCarpeta As Integer = fila("idCarpeta")
                Dim idNotificacion As Integer = fila("idNotificacion")
                Dim idComentario As Integer = fila("idComentario")
                Dim tipo As String = fila("tipo")
                Dim rutDestinatario As String = fila("rutDestinatario")
                Dim rutAutor As String = fila("rutAutor")
                Dim idItem As Integer = fila("idItem")
                Dim nombreEmpresa As String = fila("nombreEmpresa")

                Session("origen") = HttpContext.Current.Request.Url.ToString

                Dim idDocCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idDocumento)
                Dim idDocCodificado As String = System.Convert.ToBase64String(idDocCodificadoBase64)

                Dim idAreaCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(areaComentario)
                Dim idAreaCodificada As String = System.Convert.ToBase64String(idAreaCodificadaBase64)

                Dim idCarpetaCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idCarpeta)
                Dim idCarpetaCodificada As String = System.Convert.ToBase64String(idCarpetaCodificadaBase64)

                Dim rutAutorCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(rutAutor)
                Dim rutAutorCodificado As String = System.Convert.ToBase64String(rutAutorCodificadoBase64)

                Dim rutDestinatarioCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(rutDestinatario)
                Dim rutDestinatarioCodificado As String = System.Convert.ToBase64String(rutDestinatarioCodificadoBase64)

                Dim idComentarioCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idComentario)
                Dim idComentarioCodificado As String = System.Convert.ToBase64String(idComentarioCodificadoBase64)

                Dim idNotificacionCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idNotificacion)
                Dim idNotificacionCodificada As String = System.Convert.ToBase64String(idNotificacionCodificadaBase64)

                Dim tipoCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(tipo)
                Dim tipoCodificado As String = System.Convert.ToBase64String(tipoCodificadoBase64)

                Dim idItemCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idItem)
                Dim idItemCodificado As String = System.Convert.ToBase64String(idItemCodificadoBase64)

                If rutAutor = "ati" Then
                    tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"" href=""../../Contratistas/verComentarios_.aspx?i=" + idDocCodificado + "&n=" + idAreaCodificada + "&a=" + idItemCodificado + "&o=" + idCarpetaCodificada + "&p=" + rutAutorCodificado + "&q=" + idComentarioCodificado + "&x=" + tipoCodificado + "&y=" + rutDestinatarioCodificado + "&z=" + idNotificacionCodificada + """>"
                    tarjeta = tarjeta & "       <div class=""dropdown-list-image mr-3"">"
                    tarjeta = tarjeta & "           <img class=""img-profile"" src=""https://upload.wikimedia.org/wikipedia/commons/thumb/3/3b/OOjs_UI_icon_alert-warning.svg/1024px-OOjs_UI_icon_alert-warning.svg.png"" style=""height:40px;width:40px;"">"
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "       <div> "
                    tarjeta = tarjeta & "           <span class=""font-weight-bold"">" + resumenComentario + "</span>"
                    tarjeta = tarjeta & "           <div class=""small text-gray-500"">" + nombreEmpresa + " • " + nombreDocumento + "</div> "
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "   </a> "

                Else

                    tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"" href=""../../Contratistas/verComentarios_.aspx?i=" + idDocCodificado + "&n=" + idAreaCodificada + "&a=" + idItemCodificado + "&o=" + idCarpetaCodificada + "&p=" + rutAutorCodificado + "&q=" + idComentarioCodificado + "&x=" + tipoCodificado + "&y=" + rutDestinatarioCodificado + "&z=" + idNotificacionCodificada + """>"
                    tarjeta = tarjeta & "       <div class=""dropdown-list-image mr-3"">"
                    tarjeta = tarjeta & "           <img class=""img-profile rounded-circle"" src=""https://c7.uihere.com/files/25/400/945/computer-icons-industry-business-laborer-industrail-workers-and-engineers-thumb.jpg"" style=""height:40px;width:40px;"">"
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "       <div class=""font-weight-bold""> "
                    tarjeta = tarjeta & "           <div class=""text-truncate"">" + resumenComentario + "</div> "
                    tarjeta = tarjeta & "           <div class=""small text-gray-500"">" + nombreUsuarioRespuesta + "</div> "
                    tarjeta = tarjeta & "           <div class=""small text-gray-500"">" + nombreDocumento + "</div> "
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "   </a> "

                End If

                LblNotificacion.Text = tarjeta

                LblNotificacionComentarios.Text = "!"

                If listaNotificaciones.Rows.Count = 5 Then
                    Exit For
                End If

            Next

        Else

            tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"">"
            tarjeta = tarjeta & "   <div class=""font-weight""> "
            tarjeta = tarjeta & "   <div class=""text""> No tienes notificaciones pendientes </div> "
            tarjeta = tarjeta & "   </div> "
            tarjeta = tarjeta & "   </a> "

            LblNotificacion.Text = tarjeta

        End If

    End Sub
    Protected Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Session.Contents.RemoveAll()
        Response.Redirect("../../login.aspx")
    End Sub
End Class