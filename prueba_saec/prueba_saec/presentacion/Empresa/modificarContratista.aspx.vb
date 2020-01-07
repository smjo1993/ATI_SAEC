Public Class modificarContratista
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            BtnAceptar.Visible = False
            Dim rutContratista As String = Session("rutEmpresa")
            Dim contratista As New clsContratista
            Dim dt As New DataTable
            dt = contratista.obtenerContratista(rutContratista)
            Dim row As DataRow
            row = dt.Rows(0)
            TxtNombre.Text = row("nombre").ToString
            'TxtLogin.Text = row("login").ToString
            'TxtClave.Text = row("clave").ToString
            TxtRut.Text = row("rut").ToString
            TxtFono.Text = row("fono").ToString
            TxtCorreo.Text = row("correo").ToString
            'TxtClave.Attributes("type") = "password"
            'cargarEstado(row("estado").ToString)
            bloquearCampos()
            cargarNotificacionesComentarios()
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
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "3,2", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
        End If
        LblNombreUsuario.Text = usuario.getNombre().ToString()
    End Sub

    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        'Dim idCarpeta As Integer = decodificarId()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub

    Public Sub bloquearCampos()
        TxtNombre.ReadOnly = True
        TxtRut.ReadOnly = True
        'TxtLogin.ReadOnly = True
        'TxtClave.ReadOnly = True
        TxtFono.ReadOnly = True
        TxtCorreo.ReadOnly = True
        'DropEstado.Enabled = False
        'DropEstado.CssClass = "btn btn-light bg-light dropdown-toggle col-12"
    End Sub

    Public Sub desbloquearCampos()
        TxtNombre.ReadOnly = False
        TxtFono.ReadOnly = False
        TxtCorreo.ReadOnly = False
        'DropEstado.Enabled = True
    End Sub

    'Public Sub cargarEstado(estado As String)
    '    DropEstado.Items.Clear()
    '    If estado = "A" Then
    '        Dim item As New ListItem()
    '        item.Text = "Activo"
    '        item.Value = "A"
    '        DropEstado.Items.Add(item)
    '        Dim item2 As New ListItem()
    '        item2.Text = "Inactivo"
    '        item2.Value = "I"
    '        DropEstado.Items.Add(item2)
    '    End If
    '    If estado = "I" Then
    '        Dim item As New ListItem()
    '        item.Text = "Inactivo"
    '        item.Value = "I"
    '        DropEstado.Items.Add(item)
    '        Dim item2 As New ListItem()
    '        item2.Text = "Activo"
    '        item2.Value = "A"
    '        DropEstado.Items.Add(item2)
    '    End If

    'End Sub

    'Public Sub cargarOtrosEstados()
    '    Dim item As New ListItem()
    '    If DropEstado.Text = "Activo" Then
    '        item.Text = "Inactivo"
    '        DropEstado.Items.Add(item)
    '    End If
    '    If DropEstado.Text = "Inactivo" Then
    '        item.Text = "Activo"
    '        DropEstado.Items.Add(item)
    '    End If

    'End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Dim contratista As New clsContratista
        Dim log As New clsLog
        Dim accion As Boolean
        Dim registro As Boolean
        LblAdvertencia.Text = ""
        If (TxtNombre.Text.Trim() = "" Or TxtRut.Text.Trim() = "" Or TxtFono.Text.Trim() = "" Or TxtCorreo.Text.Trim() = "") Then
            LblAdvertencia.Text = "Ha ocurrido un error. Uno de los campos se encuentra en blanco."
        Else
            accion = contratista.actualizarContratista(TxtNombre.Text.Trim(), TxtRut.Text.Trim(), TxtFono.Text.Trim(), TxtCorreo.Text.Trim())
            If accion = False Then
                LblAdvertencia.Text = "Ha ocurrido un error en la conexión. Favor inténtelo nuevamente."
            Else
                LblAdvertencia.Text = "Se ha modificado al Contratista con éxito."
                registro = log.insertarRegistro("Se ha modificado al contratista de rut: " + TxtRut.Text.Trim(), Session("usuario").getRut)
                bloquearCampos()
                btnAceptar.Visible = False
                btnModificar.Visible = True
                'DropEncargados.Items.Clear()
                'cargarEncargadoEmpresa(TxtRut.Text)
            End If
        End If
    End Sub

    Protected Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        LblAdvertencia.Text = ""
        'DropEstado.Items.Clear()
        'cargarEncargadoEmpresa(TxtRut.Text)
        desbloquearCampos()
        'cargarOtrosContratistasDisponibles()
        btnModificar.Visible = False
        BtnAceptar.Visible = True
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