Imports System.Drawing

Public Class subirDocumentosVehiculo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinDocumentos.Visible = False
        validarContratista()
        If IsPostBack Then
            Return
        End If
        cargarNotificacionesComentarios()
        cargarMenu()
        cargarGrid()

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
                Dim contNoLeidos As Integer
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
                    tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"" href=""../Contratistas/verComentarios_.aspx?i=" + idDocCodificado + "&n=" + idAreaCodificada + "&a=" + idItemCodificado + "&o=" + idCarpetaCodificada + "&p=" + rutAutorCodificado + "&q=" + idComentarioCodificado + "&x=" + tipoCodificado + "&y=" + rutDestinatarioCodificado + "&z=" + idNotificacionCodificada + """>"
                    tarjeta = tarjeta & "       <div class=""dropdown-list-image mr-3"">"
                    tarjeta = tarjeta & "           <img class=""img-profile"" src=""https://upload.wikimedia.org/wikipedia/commons/thumb/3/3b/OOjs_UI_icon_alert-warning.svg/1024px-OOjs_UI_icon_alert-warning.svg.png"" style=""height:40px;width:40px;"">"
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "       <div> "
                    tarjeta = tarjeta & "           <span class=""font-weight-bold"">" + resumenComentario + "</span>"
                    tarjeta = tarjeta & "           <div class=""small text-gray-500"">" + nombreEmpresa + " • " + nombreDocumento + "</div> "
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "   </a> "

                Else

                    tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"" href=""../Contratistas/verComentarios_.aspx?i=" + idDocCodificado + "&n=" + idAreaCodificada + "&a=" + idItemCodificado + "&o=" + idCarpetaCodificada + "&p=" + rutAutorCodificado + "&q=" + idComentarioCodificado + "&x=" + tipoCodificado + "&y=" + rutDestinatarioCodificado + "&z=" + idNotificacionCodificada + """>"
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
    Protected Sub validarContratista()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        If (contratista Is Nothing) Then
            Response.Redirect("../../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(contratista.getRut, "61,3", "C")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../../401.aspx")
            End If
        End If
        LblNombreUsuario.Text = contratista.getNombre().ToString()
    End Sub
    Protected Sub cargarGrid()
        'Se pobla la grilla con los datos obtenidos anteriormente
        Dim vehiculo = New clsVehiculo()
        Dim listaDocumentosVehiculo As DataTable = vehiculo.listarDocumentosVehiculo(Session("idVehiculo"), Session("rutContratista"))

        If listaDocumentosVehiculo Is Nothing Then
            sinDocumentos.Visible = True
        Else
            If listaDocumentosVehiculo.Rows.Count > 0 Then
                gridListarDocumentosVehiculo.DataSource = listaDocumentosVehiculo
                gridListarDocumentosVehiculo.DataBind()
            Else
                sinDocumentos.Visible = True
            End If
        End If


        lblVehiculo.Text = Session("patente")
    End Sub
    Protected Sub cargarMenu()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuContratistaCarpeta(rutContratista, 0, "")
        lblMenu.Text = stringMenu
        lblMenu.Visible = True

    End Sub

    Protected Sub documentosVehiculos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarDocumentosVehiculo.RowCommand

        If (e.CommandName = "subir") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idCarpeta As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(4).Text
            Dim idDocumento As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(5).Text
            Dim idArea As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(6).Text
            Dim nombreArchivo As String = gridListarDocumentosVehiculo.Rows(pos).Cells(1).Text
            Dim rutEmpresa As String = gridListarDocumentosVehiculo.Rows(pos).Cells(8).Text
            Dim patente As String = gridListarDocumentosVehiculo.Rows(pos).Cells(0).Text
            Dim idVehiculo As String = gridListarDocumentosVehiculo.Rows(pos).Cells(9).Text
            Dim periodo As String = gridListarDocumentosVehiculo.Rows(pos).Cells(11).Text


            Dim archivo As HtmlInputFile
            archivo = gridListarDocumentosVehiculo.Rows(pos).FindControl("fileArchivo")

            'Si encuentra el archivo subido lo guarda en la ruta especifica
            If archivo.PostedFile.FileName <> "" Then


                If gridListarDocumentosVehiculo.Rows(pos).Cells(10).Text = "" Or gridListarDocumentosVehiculo.Rows(pos).Cells(10).Text = "&nbsp;" Then

                    'Si el contratista no ha subido un archivo anteriormente 
                    My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/" + periodo + "/" + "/documentos Vehiculo/" + patente))
                    Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/" + periodo + "/" + "/documentos Vehiculo/" + patente + "/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                    archivo.PostedFile.SaveAs(ruta)
                    Dim documento = New clsDocumento()
                    documento.cambiarEstadoDocumentoVehiculo(idCarpeta, idArea, idDocumento, idVehiculo, "enviado", ruta)
                    Response.Redirect(HttpContext.Current.Request.Url.ToString)

                Else

                    'Si el contratista subio un documento previamente, se elimina y se sube el nuevo archivo.
                    My.Computer.FileSystem.DeleteFile(gridListarDocumentosVehiculo.Rows(pos).Cells(10).Text)
                    My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/" + periodo + "/" + "/documentos Vehiculo/" + patente))
                    Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/" + periodo + "/" + "/documentos Vehiculo/" + patente + "/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                    archivo.PostedFile.SaveAs(ruta)
                    Dim documento = New clsDocumento()
                    documento.cambiarEstadoDocumentoVehiculo(idCarpeta, idArea, idDocumento, idVehiculo, "enviado", ruta)
                    Response.Redirect(HttpContext.Current.Request.Url.ToString)

                End If




            End If

        End If

        If (e.CommandName = "verComentarios") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idArea As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(6).Text
            Dim idDocumento As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(5).Text
            Dim idCarpeta As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(4).Text
            Dim idVehiculo As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(9).Text

            Session("areaId") = idArea
            Session("documentoId") = idDocumento
            Session("carpetaId") = idCarpeta
            Session("vehiculoId") = idVehiculo
            Session("rutUsuario") = Session("contratistaEntrante").getRut
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("../../Funcionarios ATI/verComentariosVehiculo.aspx")

        End If

    End Sub

    Protected Sub gridListarDocumentosVehiculo_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridListarDocumentosVehiculo.RowDataBound

        If e.Row.Cells(3).Text = "aprobado" Then

            e.Row.BackColor = Color.FromArgb(222, 249, 241)

        End If


        If e.Row.Cells(3).Text = "pendiente" Then

            e.Row.BackColor = Color.FromArgb(255, 240, 240)

        End If

        If e.Row.Cells(3).Text = "enviado" Then

            e.Row.BackColor = Color.FromArgb(255, 252, 231)

        End If
    End Sub
End Class