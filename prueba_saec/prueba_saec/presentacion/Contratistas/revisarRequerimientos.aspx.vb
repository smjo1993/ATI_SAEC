Public Class revisarRequerimientos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinDocEmpresa.Visible = False
        sinDocTrabajador.Visible = False
        sinDocVehiculo.Visible = False
        validarContratista()
        cargarMenu()
        If IsPostBack Then
            Return
        End If
        cargarGrid()
        cargarNotificacionesComentarios()
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
        Response.Redirect("../login.aspx")
    End Sub
    Protected Sub validarContratista()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        If (contratista Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(contratista.getRut, "60,1", "C")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
        End If
        LblNombreUsuario.Text = contratista.getNombre().ToString()
    End Sub

    Protected Sub cargarMenu()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuContratistaCarpeta(rutContratista, 0, "")
        lblMenu.Text = stringMenu
        lblMenu.Visible = True

    End Sub

    Protected Sub cargarGrid()
        Dim chk As HtmlInputCheckBox
        Dim rutContratista As String = Session("contratistaEntrante").getRut()
        Session("rutUsuario") = Session("contratistaEntrante").getRut()
        Dim TablaDocumentosEsperaEmpresa As DataTable = crearDocumentos().obtenerDocumentoEstadoEsperaEmpresa(rutContratista)

        If (TablaDocumentosEsperaEmpresa Is Nothing) Then
            sinDocEmpresa.Visible = True
        Else
            If (TablaDocumentosEsperaEmpresa.Rows.Count > 0) Then
                documentosEmpresa.DataSource = TablaDocumentosEsperaEmpresa
                documentosEmpresa.DataBind()
                For Each documentoEmpresa As GridViewRow In documentosEmpresa.Rows
                    chk = documentoEmpresa.FindControl("chk")
                    If documentoEmpresa.Cells(8).Text = "aplica" Then
                        chk.Checked = True
                    End If
                Next
            Else
                sinDocEmpresa.Visible = True
            End If
        End If

        Dim TablaDocumentosEsperaTrabajador As DataTable = crearDocumentos().obtenerDocumentoEstadoEsperaTrabajador(rutContratista)

        If (TablaDocumentosEsperaTrabajador Is Nothing) Then
            sinDocEmpresa.Visible = True
        Else
            If (TablaDocumentosEsperaTrabajador.Rows.Count > 0) Then
                documentosTrabajador.DataSource = TablaDocumentosEsperaTrabajador
                documentosTrabajador.DataBind()
                For Each documentoTrabajador As GridViewRow In documentosTrabajador.Rows
                    chk = documentoTrabajador.FindControl("chk")
                    If documentoTrabajador.Cells(8).Text = "aplica" Then
                        chk.Checked = True
                    End If
                Next
            Else
                sinDocTrabajador.Visible = True
            End If
        End If

        Dim TablaDocumentosEsperaVehiculo As DataTable = crearDocumentos().obtenerDocumentoEstadoEsperaVehiculo(rutContratista)

        If (TablaDocumentosEsperaVehiculo Is Nothing) Then
            sinDocEmpresa.Visible = True
        Else
            If (TablaDocumentosEsperaVehiculo.Rows.Count > 0) Then
                documentosVehiculo.DataSource = TablaDocumentosEsperaVehiculo
                documentosVehiculo.DataBind()
                For Each documentoVehiculo As GridViewRow In documentosVehiculo.Rows
                    chk = documentoVehiculo.FindControl("chk")
                    If documentoVehiculo.Cells(8).Text = "aplica" Then
                        chk.Checked = True
                    End If
                Next
            Else
                sinDocVehiculo.Visible = True
            End If
        End If



    End Sub


    Public Function crearDocumentos() As Object

        Dim documentosEspera = New clsDocumento()

        Return documentosEspera

    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles confirmarPinponeo.Click

        Dim dt As DataTable = New DataTable("CambioEstado")

        'Se recorre cada checkbox generado 
        For Each fila As GridViewRow In documentosEmpresa.Rows

            Dim idCarpeta As Integer = fila.Cells(2).Text
            Dim idDocumento As Integer = fila.Cells(3).Text
            Dim idArea As Integer = fila.Cells(4).Text
            Dim check As HtmlInputCheckBox
            Dim actualizarEstado = New clsDocumento()

            check = fila.FindControl("chk")

            'Si está check
            If check.Checked Then
                'Cambia el estado del documento a "aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "aplica", Nothing)
            Else
                'Cambia el estado del documento a "no aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no aplica", Nothing)
            End If

        Next

        For Each fila As GridViewRow In documentosTrabajador.Rows

            Dim idCarpeta As Integer = fila.Cells(2).Text
            Dim idDocumento As Integer = fila.Cells(3).Text
            Dim idArea As Integer = fila.Cells(4).Text
            Dim check As HtmlInputCheckBox
            Dim actualizarEstado = New clsDocumento()

            check = fila.FindControl("chk")

            'Si está check
            If check.Checked Then
                'Cambia el estado del documento a "aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "aplica", Nothing)
            Else
                'Cambia el estado del documento a "no aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no aplica", Nothing)
            End If
        Next

        For Each fila As GridViewRow In documentosVehiculo.Rows

            Dim idCarpeta As Integer = fila.Cells(2).Text
            Dim idDocumento As Integer = fila.Cells(3).Text
            Dim idArea As Integer = fila.Cells(4).Text
            Dim check As HtmlInputCheckBox
            Dim actualizarEstado = New clsDocumento()

            check = fila.FindControl("chk")

            'Si está check
            If check.Checked Then
                'Cambia el estado del documento a "aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "aplica", Nothing)
            Else
                'Cambia el estado del documento a "no aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no aplica", Nothing)
            End If

        Next
        Response.Redirect(HttpContext.Current.Request.Url.ToString)
    End Sub

    Protected Sub documentosEmpresa_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles documentosEmpresa.RowCommand

        If (e.CommandName = "verComentarios") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim areaId As String = documentosEmpresa.Rows(pos).Cells(4).Text
            Dim documentoId As String = documentosEmpresa.Rows(pos).Cells(3).Text
            Dim carpetaId As String = documentosEmpresa.Rows(pos).Cells(2).Text
            Session("areaId") = areaId
            Session("documentoId") = documentoId
            Session("carpetaId") = carpetaId
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("verComentarios.aspx")

        End If


    End Sub

End Class