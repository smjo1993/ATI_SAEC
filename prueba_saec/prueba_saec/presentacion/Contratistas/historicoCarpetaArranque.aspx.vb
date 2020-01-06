Public Class historicoCarpetaArranque
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        sinDocEmpresa.Visible = False
        sinTrabajadores.Visible = False
        sinVehiculos.Visible = False
        If Not Page.IsPostBack Then
            validarContratista()
            'cargarMenu()
            cargarDatos()
            cargarNotificacionesComentarios()
        End If
    End Sub
    Protected Sub validarContratista()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        If (contratista Is Nothing) Then
            Response.Redirect("../login.aspx")
            'Else
            '    Dim menu As New clsMenu
            '    Dim acceso As String = menu.validarAcceso(contratista.getRut, "60,1", "C")

            '    If acceso = "I" Or acceso Is Nothing Then
            '        Response.Redirect("../401.aspx")
            '    End If
        End If
        LblNombreUsuario.Text = contratista.getNombre().ToString()
    End Sub
    Protected Sub cargarMenu()
        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut
        'Dim idCarpeta As Integer = decodificarId()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuInicioContratista(rutContratista)
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
    Protected Sub cargarDatos()
        Dim rutContratista As String = Session("contratistaEntrante").getRut
        Dim documentos As New clsDocumento
        Dim idCarpeta As Integer = decodificarId()
        Dim documentosEmpresa As DataTable = documentos.documentosEmpresaHistorico(rutContratista, idCarpeta)

        If documentosEmpresa Is Nothing Then
            sinDocEmpresa.Visible = True
        Else
            If documentosEmpresa.Rows.Count > 0 Then
                gridDocumentosEmpresa.DataSource = documentosEmpresa
                gridDocumentosEmpresa.DataBind()
            Else
                sinDocEmpresa.Visible = True
            End If
        End If

        Dim trabajadores = New clsTrabajador()
        Dim TablaTrabajadores As DataTable = trabajadores.listarTrabajadoresHistorico(rutContratista, idCarpeta)

        If TablaTrabajadores Is Nothing Then
            sinTrabajadores.Visible = True
        Else
            If TablaTrabajadores.Rows.Count > 0 Then
                gridListarTrabajadores.DataSource = TablaTrabajadores
                gridListarTrabajadores.DataBind()
            Else
                sinTrabajadores.Visible = True
            End If
        End If

        Dim vehiculos = New clsVehiculo()
        Dim TablaVehiculos As DataTable = vehiculos.listarVehiculosHistorico(rutContratista, idCarpeta)

        If TablaVehiculos Is Nothing Then
            sinVehiculos.Visible = True
        Else
            If TablaVehiculos.Rows.Count > 0 Then

                gridListarVehiculos.DataSource = TablaVehiculos
                gridListarVehiculos.DataBind()
            Else
                sinVehiculos.Visible = True
            End If
        End If

    End Sub

    Protected Sub btnIrTrabajador_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarTrabajadores.RowCommand

        If (e.CommandName = "ir") Then
            'Se obtienen los datos de la columna de la grid para mandar el datatable a la otra vista
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim rutTrabajador As String = gridListarTrabajadores.Rows(pos).Cells(0).Text
            Dim idCarpeta As Integer = gridListarTrabajadores.Rows(pos).Cells(2).Text
            Dim idTrabajador As Integer = gridListarTrabajadores.Rows(pos).Cells(3).Text
            Dim trabajador = New clsTrabajador()
            Dim rutContratista As String = Session("contratistaEntrante").getRut
            Session("rutTrabajador") = rutTrabajador
            Session("idTrabajador") = idTrabajador
            Session("rutContratista") = rutContratista
            Session("idCarpetaHistorico") = decodificarId()
            Response.Redirect("TrabajadorHistorico.aspx")

        End If
    End Sub

    Protected Sub btnIrVehiculo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarVehiculos.RowCommand

        If (e.CommandName = "ir") Then

            'Se obtienen los datos de la columna de la grid para mandar el datatable a la otra vista
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim patente As String = gridListarVehiculos.Rows(pos).Cells(0).Text
            Dim idVehiculo As Integer = gridListarVehiculos.Rows(pos).Cells(3).Text
            Dim rutContratista As String = Session("contratistaEntrante").getRut
            Session("patente") = patente
            Session("idVehiculo") = idVehiculo
            Session("rutContratista") = rutContratista
            Session("idCarpetaHistorico") = decodificarId()
            Response.Redirect("VehiculoHistorico.aspx")

        End If
    End Sub

    Protected Sub gridDocumentosEmpresa_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridDocumentosEmpresa.RowCommand

        If (e.CommandName = "Ver") Then
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim ruta As String = gridDocumentosEmpresa.Rows(pos).Cells(6).Text
            Dim nombreArchivo As String = gridDocumentosEmpresa.Rows(pos).Cells(4).Text
            Dim extension As String = ExtraerExtension(ruta, ".")
            If extension = "pdf" Then
                'Se codifica la ruta del archivo para pasarlo por URl
                Dim rutaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(ruta)
                Dim rutaCodificada As String = System.Convert.ToBase64String(rutaBase64)
                'Response.Clear()
                'Response.ContentType = "application/pdf"
                Response.Write("<script type='text/javascript'>detailedresults=window.open('verDocumento.aspx?r=" + rutaCodificada + "');</script>")
                'Response.WriteFile(ruta)
            Else

                Response.Clear()
                Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", ruta))
                Response.WriteFile(ruta)
                Response.End()
            End If
        End If

        If (e.CommandName = "verComentarios") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idArea As Integer = gridDocumentosEmpresa.Rows(pos).Cells(5).Text
            Dim idDocumento As Integer = gridDocumentosEmpresa.Rows(pos).Cells(4).Text
            Dim idCarpeta As Integer = gridDocumentosEmpresa.Rows(pos).Cells(3).Text

            Session("areaId") = idArea
            Session("documentoId") = idDocumento
            Session("carpetaId") = idCarpeta
            Session("rutUsuario") = Session("contratistaEntrante").getRut
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Session("verComentarios") = "I"
            Response.Redirect("verComentarios.aspx")
        End If
    End Sub
    Function ExtraerExtension(Path As String, Caracter As String) As String

        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        ExtraerExtension = ret

    End Function
    Private Sub cargarNotificacionesComentarios()

        Dim notificacion As New clsNotificacion
        Dim usuario As clsContratista = Session("contratistaEntrante")
        Dim rutUsuario As String = usuario.getRut
        Dim tarjeta As String = ""


        Dim listaNotificaciones As Data.DataTable = notificacion.obtenerNotificaciones(rutUsuario)

        If listaNotificaciones.Rows.Count > 0 Then

            'For Each fila As DataRow In listaNotificaciones.Rows
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

                'contNoLeidos = contNoLeidos + 1
                'LblNotificacionComentarios.Text = contNoLeidos.ToString()

                LblNotificacion.Text = tarjeta

                LblNotificacionComentarios.Text = " ! "

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
End Class