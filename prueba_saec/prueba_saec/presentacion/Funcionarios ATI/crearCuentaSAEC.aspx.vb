Public Class crearCuentaSAEC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        errorNumero.Visible = False
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarRoles()
            cargarDatosUsuario()
            cargarNotificacionesComentarios()
        End If
    End Sub
    Protected Sub cargarRoles()
        Dim newListItem = New ListItem("seleccione un item", "-1")
        dropRol.Items.Add(newListItem)
        Dim rol As New clsRol
        Dim listaRoles As DataTable = rol.listarRoles()
        If (listaRoles Is Nothing) Then
        Else
            If (listaRoles.Rows.Count > 0) Then
                For Each celda As DataRow In listaRoles.Rows
                    Dim item As New ListItem()
                    item.Text = celda("nombre").ToString()
                    item.Value = celda("id").ToString()
                    dropRol.Items.Add(item)
                Next
            End If
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
    Protected Sub cargarDatosUsuario()
        Dim usuarioSAEC As clsUsuarioSAEC = Session("usuarioNuevoSAEC")
        txtClave.ReadOnly = True
        'txtEmail.ReadOnly = True
        'txtFono.ReadOnly = True
        txtNombreUsuario.ReadOnly = True
        txtLogin.ReadOnly = True
        txtRut.ReadOnly = True

        txtClave.Text = usuarioSAEC.getClave
        txtEmail.Text = usuarioSAEC.getCorreo
        txtFono.Text = usuarioSAEC.getFono.ToString
        txtNombreUsuario.Text = usuarioSAEC.getNombre
        txtLogin.Text = usuarioSAEC.getLogin
        txtRut.Text = usuarioSAEC.getRut
    End Sub
    Protected Sub btnCrearCuenta_Click(sender As Object, e As EventArgs) Handles btnCrearCuenta.Click
        If (dropRol.SelectedValue = "-1") Then
            lblMensaje.Text = "Seleccione un rol de la lista"
            errorNumero.Visible = True
            cargarMenu()
        Else
            Try
                Dim Fono As Integer = Convert.ToInt32(txtFono.Text)
                Dim rol As Integer = Convert.ToInt32(dropRol.SelectedValue)
                Dim usuarioSAEC As clsUsuarioSAEC = Session("usuarioNuevoSAEC")
                Dim nuevoUsuario As New clsUsuarioSAEC
                Dim mensaje As String = nuevoUsuario.insertarUsuario(usuarioSAEC.getNombre, usuarioSAEC.getLogin, usuarioSAEC.getClave, usuarioSAEC.getRut, usuarioSAEC.getEstado, Fono, txtEmail.Text, usuarioSAEC.getArea, rol)
                Session("mensaje") = mensaje
                Response.Redirect("iniciarCuentasUsuarioATI.aspx")
            Catch ex As FormatException
                'MessageBox.Show(ex.Message)
                lblMensaje.Text = "Error en el numero Telefonico"
                errorNumero.Visible = True
                cargarMenu()
            End Try
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

                Dim rutAutor As String = fila("rutAutor")

                Session("origen") = HttpContext.Current.Request.Url.ToString

                Dim idDocCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idDocumento)
                Dim idDocCodificado As String = System.Convert.ToBase64String(idDocCodificadoBase64)

                Dim idAreaCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(areaComentario)
                Dim idAreaCodificada As String = System.Convert.ToBase64String(idAreaCodificadaBase64)

                Dim idCarpetaCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idCarpeta)
                Dim idCarpetaCodificada As String = System.Convert.ToBase64String(idCarpetaCodificadaBase64)

                Dim RutCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(rutAutor)
                Dim RutCodificado As String = System.Convert.ToBase64String(RutCodificadoBase64)

                Dim idComentarioCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idComentario)
                Dim idComentarioCodificado As String = System.Convert.ToBase64String(idComentarioCodificadoBase64)

                Dim tipoCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(tipo)
                Dim tipoCodificado As String = System.Convert.ToBase64String(tipoCodificadoBase64)

                tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"" href=""../Contratistas/verComentarios_.aspx?i=" + idDocCodificado + "&n=" + idAreaCodificada + "&o=" + idCarpetaCodificada + "&p=" + RutCodificado + "&q=" + idComentarioCodificado + "&q=" + tipoCodificado + """>"
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