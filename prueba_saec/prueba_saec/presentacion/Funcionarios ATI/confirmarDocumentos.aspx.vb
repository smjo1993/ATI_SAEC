Public Class confirmarDocumentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinDocEmpresa.Visible = False
        sinDocTrabajador.Visible = False
        sinDocVehiculo.Visible = False
        cargarMenu()

        If IsPostBack Then
            Return
        End If
        validarUsuario()
        cargarNotificacionesComentarios()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Session("rutUsuario") = usuario.getRut
        Dim idCarpeta As Integer = decodificarId()
        Dim TablaDocumentosEsperaEmpresa As DataTable = crearDocumentos().obtenerDocumentoEstadoAplicaEmpresa(idCarpeta, usuario.getArea)

        If (TablaDocumentosEsperaEmpresa Is Nothing) Then
            sinDocEmpresa.Visible = True
        Else
            If (TablaDocumentosEsperaEmpresa.Rows.Count > 0) Then
                confirmarEmpresa.DataSource = TablaDocumentosEsperaEmpresa
                confirmarEmpresa.DataBind()
            Else
                sinDocEmpresa.Visible = True
            End If
        End If

        Dim TablaDocumentosEsperaTrabajador As DataTable = crearDocumentos().obtenerDocumentoEstadoAplicaTrabajador(idCarpeta, usuario.getArea)

        If (TablaDocumentosEsperaTrabajador Is Nothing) Then
            sinDocTrabajador.Visible = True
        Else
            If (TablaDocumentosEsperaTrabajador.Rows.Count > 0) Then
                confirmarTrabajador.DataSource = TablaDocumentosEsperaTrabajador
                confirmarTrabajador.DataBind()
            Else
                sinDocTrabajador.Visible = True
            End If
        End If

        Dim TablaDocumentosEsperaVehiculo As DataTable = crearDocumentos().obtenerDocumentoEstadoAplicaVehiculo(idCarpeta, usuario.getArea)

        If (TablaDocumentosEsperaVehiculo Is Nothing) Then
            sinDocVehiculo.Visible = True
        Else
            If (TablaDocumentosEsperaVehiculo.Rows.Count > 0) Then
                confirmarVehiculo.DataSource = TablaDocumentosEsperaVehiculo
                confirmarVehiculo.DataBind()
            Else
                sinDocVehiculo.Visible = True
            End If
        End If



    End Sub
    Protected Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Session.Contents.RemoveAll()
        Response.Redirect("../login.aspx")
    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "5,2", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
        End If
        LblNombreUsuario.Text = usuario.getNombre().ToString()
    End Sub
    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut()
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

    Public Function crearDocumentos() As Object

        Dim documentosEspera = New clsDocumento()

        Return documentosEspera

    End Function

    Protected Sub btnConfirmarDocumentos_Click(sender As Object, e As EventArgs) Handles btnConfirmarDocumentos.Click
        Dim contador As Integer = 0
        Dim dt As DataTable = New DataTable("CambioEstado")

        'Se recorre cada checkbox generado 
        For Each fila As GridViewRow In confirmarEmpresa.Rows

            Dim idCarpeta As Integer = fila.Cells(2).Text
            Dim idDocumento As Integer = fila.Cells(3).Text
            Dim idArea As Integer = fila.Cells(4).Text
            Dim check As HtmlInputCheckBox
            Dim actualizarEstado = New clsDocumento()

            check = fila.FindControl("chk")

            'Si está check
            If check.Checked Then
                'Cambia el estado del documento a "pendiente"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "pendiente", Nothing)
            Else
                'Cambia el estado del documento a "no solicitado"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "espera", Nothing)
            End If

        Next

        For Each fila As GridViewRow In confirmarTrabajador.Rows

            Dim idCarpeta As Integer = fila.Cells(2).Text
            Dim idDocumento As Integer = fila.Cells(3).Text
            Dim idArea As Integer = fila.Cells(4).Text
            Dim check As HtmlInputCheckBox
            Dim actualizarEstado = New clsDocumento()

            check = fila.FindControl("chk")

            'Si está check
            If check.Checked Then
                'Cambia el estado del documento a "pendiente"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "pendiente", Nothing)
            Else
                'Cambia el estado del documento a "no solicitado"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "espera", Nothing)
            End If
        Next

        For Each fila As GridViewRow In confirmarVehiculo.Rows

            Dim idCarpeta As Integer = fila.Cells(2).Text
            Dim idDocumento As Integer = fila.Cells(3).Text
            Dim idArea As Integer = fila.Cells(4).Text
            Dim check As HtmlInputCheckBox
            Dim actualizarEstado = New clsDocumento()

            check = fila.FindControl("chk")

            'Si está check
            If check.Checked Then
                'Cambia el estado del documento a "pendiente"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "pendiente", Nothing)
            Else
                'Cambia el estado del documento a "no solicitado"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "espera", Nothing)
            End If

        Next
        Response.Redirect("verCarpetas.aspx")
    End Sub

    Protected Sub documentosEmpresa_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles confirmarEmpresa.RowCommand

        If (e.CommandName = "eliminar") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idCarpeta As Integer = confirmarEmpresa.Rows(pos).Cells(2).Text
            Dim idDocumento As Integer = confirmarEmpresa.Rows(pos).Cells(3).Text
            Dim idArea As Integer = confirmarEmpresa.Rows(pos).Cells(4).Text
            Dim actualizarEstado = New clsDocumento()

            actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no solicitado", Nothing)
            Response.Redirect(HttpContext.Current.Request.Url.ToString)

        End If

        If (e.CommandName = "Ver") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim areaId As String = confirmarEmpresa.Rows(pos).Cells(4).Text
            Dim documentoId As String = confirmarEmpresa.Rows(pos).Cells(3).Text
            Dim carpetaId As String = confirmarEmpresa.Rows(pos).Cells(2).Text
            Session("areaId") = areaId
            Session("documentoId") = documentoId
            Session("carpetaId") = carpetaId
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("../Contratistas/verComentarios.aspx")

        End If


    End Sub


    Protected Sub confirmarTrabajador_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles confirmarTrabajador.RowCommand

        If (e.CommandName = "eliminar") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idCarpeta As Integer = confirmarTrabajador.Rows(pos).Cells(2).Text
            Dim idDocumento As Integer = confirmarTrabajador.Rows(pos).Cells(3).Text
            Dim idArea As Integer = confirmarTrabajador.Rows(pos).Cells(4).Text
            Dim actualizarEstado = New clsDocumento()

            actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no solicitado", Nothing)
            Response.Redirect(HttpContext.Current.Request.Url.ToString)

        End If

        If (e.CommandName = "Ver") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim areaId As String = confirmarTrabajador.Rows(pos).Cells(4).Text
            Dim documentoId As String = confirmarTrabajador.Rows(pos).Cells(3).Text
            Dim carpetaId As String = confirmarTrabajador.Rows(pos).Cells(2).Text
            Session("areaId") = areaId
            Session("documentoId") = documentoId
            Session("carpetaId") = carpetaId
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("../Contratistas/verComentarios.aspx")
        End If


    End Sub
    Protected Sub confirmarVehiculo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles confirmarVehiculo.RowCommand

        If (e.CommandName = "eliminar") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idCarpeta As Integer = confirmarVehiculo.Rows(pos).Cells(2).Text
            Dim idDocumento As Integer = confirmarVehiculo.Rows(pos).Cells(3).Text
            Dim idArea As Integer = confirmarVehiculo.Rows(pos).Cells(4).Text
            Dim actualizarEstado = New clsDocumento()

            actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no solicitado", Nothing)
            Response.Redirect(HttpContext.Current.Request.Url.ToString)

        End If

        If (e.CommandName = "Ver") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim areaId As String = confirmarVehiculo.Rows(pos).Cells(4).Text
            Dim documentoId As String = confirmarVehiculo.Rows(pos).Cells(3).Text
            Dim carpetaId As String = confirmarVehiculo.Rows(pos).Cells(2).Text
            Session("areaId") = areaId
            Session("documentoId") = documentoId
            Session("carpetaId") = carpetaId
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("../Contratistas/verComentarios.aspx")
        End If


    End Sub
    Private Sub cargarNotificacionesComentarios()

        Dim notificacion As New clsNotificacion
        Dim usuario As clsUsuarioSAEC = Session("usuario")
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
End Class