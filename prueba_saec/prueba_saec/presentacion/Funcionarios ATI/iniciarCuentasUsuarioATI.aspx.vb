Public Class iniciarCuentasUsuarioATI
    Inherits System.Web.UI.Page
    Dim usuariosATI As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        sinUsuarios.Visible = False
        lblMensaje.Visible = False
        sinCoincidencia.Visible = False
        If Not Page.IsPostBack Then
            cargarNotificacionesComentarios()
            validarUsuario()
            cargarMenu()
            cargarUsuarios()
        End If
    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "1,1", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
        End If
        Dim mensaje As String = Session("mensaje")
        If mensaje <> Nothing Then
            lblMensaje.Visible = True
            lblMensaje.Text = mensaje
            Session("mensaje") = Nothing
        End If
        LblNombreUsuario.Text = usuario.getNombre().ToString()
    End Sub

    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub

    Protected Sub cargarUsuarios()
        Dim usuario As New clsUsuario
        Dim rutUsuario As String = Session("rutUsuario")
        usuariosATI = usuario.listarUsuarios()

        If (usuariosATI Is Nothing) Then
            sinUsuarios.Visible = True
        Else
            If (usuariosATI.Rows.Count > 0) Then
                Me.gridUsuariosATI.DataSource = usuariosATI
                Me.gridUsuariosATI.DataBind()
            Else
                sinUsuarios.Visible = True
            End If
        End If

    End Sub

    Protected Sub gridUsuariosATI_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridUsuariosATI.RowCommand

        If (e.CommandName = "crearCuenta") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim fono As String = gridUsuariosATI.Rows(pos).Cells(5).Text
            If fono = "" Or fono = "-" Or fono = "&nbsp;" Then
                fono = "0"
            End If
            Dim correo As String = gridUsuariosATI.Rows(pos).Cells(4).Text
            If correo = "" Or correo = "-" Or correo = "&nbsp;" Then
                correo = "sin correo"
            Else
                correo = gridUsuariosATI.Rows(pos).Cells(4).Text
            End If
            Dim usuario As New clsUsuarioSAEC(gridUsuariosATI.Rows(pos).Cells(0).Text,
                                                                      gridUsuariosATI.Rows(pos).Cells(1).Text,
                                                                      gridUsuariosATI.Rows(pos).Cells(2).Text,
                                                                      gridUsuariosATI.Rows(pos).Cells(3).Text,
                                                                      "A",
                                                                      fono,
                                                                      correo,
                                                                      Convert.ToInt32(gridUsuariosATI.Rows(pos).Cells(6).Text))
            Session("usuarioNuevoSAEC") = usuario
            Response.Redirect("crearCuentaSAEC.aspx")
        End If
    End Sub
    Private Sub lnkBuscarNombre_Click(sender As Object, e As EventArgs) Handles lnkBuscarNombre.Click
        Dim nombreFiltro As String = txtBuscarNombre.Text.Trim
        If nombreFiltro = "" Then
            cargarMenu()
            cargarUsuarios()
        Else
            Dim usuario As New clsUsuario
            usuariosATI = usuario.usuariosFiltrados(nombreFiltro, "")
            If usuariosATI.Rows.Count > 0 Then
                Me.gridUsuariosATI.DataSource = usuariosATI
                Me.gridUsuariosATI.DataBind()
                txtBuscarNombre.Text = ""
                cargarMenu()
            Else
                Dim d As New DataTable
                Me.gridUsuariosATI.DataSource = d
                Me.gridUsuariosATI.DataBind()
                sinCoincidencia.Visible = True
                txtBuscarNombre.Text = ""
                cargarMenu()
            End If
        End If
    End Sub
    Private Sub lnkBuscarUsuario_Click(sender As Object, e As EventArgs) Handles lnkBuscarUsuario.Click
        Dim usuarioFiltro As String = txtBuscarUsuario.Text.Trim
        If usuarioFiltro = "" Then
            cargarMenu()
            cargarUsuarios()
        Else
            Dim usuario As New clsUsuario
            usuariosATI = usuario.usuariosFiltrados("", usuarioFiltro)
            If usuariosATI.Rows.Count > 0 Then
                Me.gridUsuariosATI.DataSource = usuariosATI
                Me.gridUsuariosATI.DataBind()
                txtBuscarUsuario.Text = ""
                cargarMenu()
            Else
                Dim d As New DataTable
                Me.gridUsuariosATI.DataSource = d
                Me.gridUsuariosATI.DataBind()
                sinCoincidencia.Visible = True
                txtBuscarUsuario.Text = ""
                cargarMenu()
            End If
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
    Protected Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Session.Contents.RemoveAll()
        Response.Redirect("../login.aspx")
    End Sub
End Class