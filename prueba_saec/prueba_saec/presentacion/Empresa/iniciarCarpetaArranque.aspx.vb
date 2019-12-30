Public Class iniciarCarpetaArranque
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        validarUsuario()
        cargarMenu()
        lblMensaje.Text = ""
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarDatos()
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
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "4,2", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
        End If
        LblNombreUsuario.Text = usuario.getNombre().ToString()
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
    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        'Dim idCarpeta As Integer = decodificarId()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub

    Protected Sub cargarDatos()
        Dim newListItem = New ListItem("seleccione un item", "-1")
        dropEmpresas.Items.Add(newListItem)
        Dim empresa As New clsEmpresa
        Dim listaEmpresas As DataTable = empresa.empresasSinCarpeta
        If (listaEmpresas Is Nothing) Then
        Else
            If (listaEmpresas.Rows.Count > 0) Then
                For Each celda As DataRow In listaEmpresas.Rows
                    Dim item As New ListItem()
                    item.Text = celda("razonSocial").ToString()
                    item.Value = celda("rut").ToString()
                    'item.Selected = Convert.ToBoolean(celda("IsSelected"))
                    dropEmpresas.Items.Add(item)
                Next
            End If
        End If
    End Sub
    Private Function calcularFechaExpiracion(ByVal fecha As Date) As Date
        Dim mes As Integer = fecha.Month
        Dim diferencia As Integer = 12 - mes
        fecha = fecha.AddMonths(diferencia)
        Dim dia As Integer = fecha.Day
        diferencia = 31 - dia
        fecha = fecha.AddDays(diferencia)
        Return fecha
    End Function
    Protected Sub btnCrearCarpeta_Click(sender As Object, e As EventArgs) Handles btnCrearCarpeta.Click
        Dim alerta As New clsAlertas
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            If (dropEmpresas.SelectedValue = "-1") Then
                lblMensaje.Text = alerta.alerta("ALERTA", "seleccione un item")
            Else
                Dim em As New clsEmpresa
                Dim rutEmpresa As String = dropEmpresas.SelectedValue
                Dim fechaExpiracion As Date
                Dim fechaCreacion As Date
                Dim carpetaArranque As New clsCarpetaArranque
                If (txtFecha.Text = "") Then
                    Dim descripcion As String = "Creacion de la carpeta arranque de la empresa " + dropEmpresas.SelectedItem.Text
                    fechaCreacion = Today
                    Dim mes As Integer = fechaCreacion.Month
                    If (mes = 11 Or mes = 12) Then
                        fechaExpiracion = calcularFechaExpiracion(fechaCreacion)
                        fechaExpiracion = fechaExpiracion.AddYears(1)
                    Else
                        fechaExpiracion = calcularFechaExpiracion(fechaCreacion)
                    End If

                    'fechaExpiracion = DateAdd("m", 12, Today)
                    If (carpetaArranque.insertarEmpresa(fechaExpiracion, rutEmpresa, fechaCreacion, descripcion, usuario.getRut) = True) Then
                        My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa))
                        Response.Redirect("iniciarCarpetaArranque.aspx")
                    Else
                        Response.Redirect("iniciarCarpetaArranque.aspx")
                        End If
                    Else
                        fechaCreacion = Today
                    fechaExpiracion = Convert.ToDateTime(txtFecha.Text)
                    Dim fechaLimite As Date = calcularFechaExpiracion(fechaCreacion)
                    fechaLimite = fechaLimite.AddYears(1)
                    If (DateTime.Compare(fechaCreacion, fechaExpiracion) = 0 Or DateTime.Compare(fechaCreacion, fechaExpiracion) > 0 Or DateTime.Compare(fechaExpiracion, fechaLimite) > 0) Then
                        lblMensaje.Text = alerta.alerta("ALERTA", "fecha erronea")
                    Else
                        Dim descripcion As String = "Creacion de la carpeta arranque de la empresa " + dropEmpresas.SelectedItem.Text
                        If (carpetaArranque.insertarEmpresa(fechaExpiracion, rutEmpresa, fechaCreacion, descripcion, usuario.getRut) = True) Then
                            My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa))
                            Response.Redirect("iniciarCarpetaArranque.aspx")
                        Else
                            Response.Redirect("iniciarCarpetaArranque.aspx")
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Protected Sub btnCrearEmpresa_Click(sender As Object, e As EventArgs) Handles btnCrearEmpresa.Click
        Response.Redirect("crearEmpresa.aspx")
    End Sub
End Class