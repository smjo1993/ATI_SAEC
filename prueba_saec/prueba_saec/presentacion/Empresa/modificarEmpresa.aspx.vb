Public Class modificarEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            btnAceptar.Visible = False
            Dim rutEmpresa As String = Session("rutEmpresa")
            Dim empresa As New clsEmpresa
            Dim dt As New DataTable
            dt = empresa.obtenerEmpresa(rutEmpresa)
            Dim row As DataRow
            row = dt.Rows(0)
            TxtRazonSocial.Text = row("razonSocial").ToString
            TxtRut.Text = row("rut").ToString
            TxtGiro.Text = row("giro").ToString
            TxtDireccion.Text = row("direccion").ToString
            TxtCiudad.Text = row("ciudad").ToString
            TxtFono.Text = row("fono").ToString
            TxtCelular.Text = row("celular").ToString
            TxtCorreo.Text = row("correo").ToString
            cargarEncargadoEmpresa(row("rut").ToString)
            'cargarOtrosContratistasDisponibles()
            validarUsuario()
            cargarMenu()
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
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "2,2", "A")

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


    'Public Sub cargarDropEmpresas()
    '    Dim empresa As New clsEmpresa
    '    Dim listaEmpresas As DataTable = empresa.listarEmpresas()

    '    Dim datav As New DataView
    '    datav = listaEmpresas.DefaultView
    '    datav.Sort = "razonSocial"
    '    listaEmpresas = datav.ToTable()

    '    dropEmpresas.Items.Clear()
    '    dropEmpresas.Items.Add("")
    '    For Each row As DataRow In listaEmpresas.Rows
    '        Dim item As New ListItem()
    '        item.Text = row("razonSocial").ToString()
    '        item.Value = row("rut").ToString()
    '        dropEmpresas.Items.Add(item)
    '    Next

    'End Sub

    Public Sub bloquearCampos()
        TxtRazonSocial.ReadOnly = True
        TxtRut.ReadOnly = True
        TxtGiro.ReadOnly = True
        TxtDireccion.ReadOnly = True
        TxtCiudad.ReadOnly = True
        TxtFono.ReadOnly = True
        TxtCelular.ReadOnly = True
        TxtCorreo.ReadOnly = True
        DropEncargados.Enabled = False
        DropEncargados.CssClass = "btn btn-light bg-light dropdown-toggle col-12"
    End Sub

    Public Sub desbloquearCampos()
        TxtRazonSocial.ReadOnly = False
        TxtGiro.ReadOnly = False
        TxtDireccion.ReadOnly = False
        TxtCiudad.ReadOnly = False
        TxtFono.ReadOnly = False
        TxtCelular.ReadOnly = False
        TxtCorreo.ReadOnly = False
        DropEncargados.Enabled = True
    End Sub

    'Public Sub dropEmpresas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropEmpresas.SelectedIndexChanged
    '    If dropEmpresas.SelectedItem.Text.ToString <> "" Then
    '        Dim empresa As New clsEmpresa
    '        Dim dt As New DataTable
    '        dt = empresa.obtenerEmpresa(dropEmpresas.SelectedItem.Value.ToString())
    '        Dim row As DataRow
    '        row = dt.Rows(0)
    '        TxtRazonSocial.Text = row("razonSocial").ToString
    '        TxtRut.Text = row("rut").ToString
    '        TxtGiro.Text = row("giro").ToString
    '        TxtDireccion.Text = row("direccion").ToString
    '        TxtCiudad.Text = row("ciudad").ToString
    '        TxtFono.Text = row("fono").ToString
    '        TxtCelular.Text = row("celular").ToString
    '        TxtCorreo.Text = row("correo").ToString
    '        cargarEncargadoEmpresa(row("rut").ToString)
    '        cargarOtrosContratistasDisponibles()
    '        TxtRazonSocial.ReadOnly = False
    '        TxtGiro.ReadOnly = False
    '        TxtDireccion.ReadOnly = False
    '        TxtCiudad.ReadOnly = False
    '        TxtFono.ReadOnly = False
    '        TxtCelular.ReadOnly = False
    '        TxtCorreo.ReadOnly = False
    '    Else
    '        TxtRazonSocial.Text = ""
    '        TxtRut.Text = ""
    '        TxtGiro.Text = ""
    '        TxtDireccion.Text = ""
    '        TxtCiudad.Text = ""
    '        TxtFono.Text = ""
    '        TxtCelular.Text = ""
    '        TxtCorreo.Text = ""
    '        bloquearCampos()
    '        DropEncargados.Items.Clear()
    '    End If



    'End Sub

    Public Sub cargarEncargadoEmpresa(rut As String)
        DropEncargados.Items.Clear()
        Dim empresa As New clsEmpresa
        Dim dt As New DataTable
        dt = empresa.obtenerContratistaDeEmpresa(rut)
        Dim row As DataRow
        row = dt.Rows(0)
        Dim item As New ListItem()
        item.Text = row("nombre").ToString()
        item.Value = row("rut").ToString()
        DropEncargados.Items.Add(item)
    End Sub

    Public Function obtenerRutEncargadoEmpresa(rut As String) As String
        Dim empresa As New clsEmpresa
        Dim dt As New DataTable
        dt = empresa.obtenerContratistaDeEmpresa(rut)
        Dim row As DataRow
        row = dt.Rows(0)
        Return row("rut").ToString()
    End Function

    Public Sub cargarOtrosContratistasDisponibles()
        Dim empresa As New clsEmpresa
        Dim dt As New DataTable
        dt = empresa.listarContratistasSinempresa()
        'Ordenando la lista de Contratistas a agregar
        Dim datav As New DataView
        datav = dt.DefaultView
        datav.Sort = "nombre"
        dt = datav.ToTable()
        'Agregando los items
        For Each row As DataRow In dt.Rows
            Dim item As New ListItem()
            item.Text = row("nombre").ToString()
            item.Value = row("rut").ToString()
            DropEncargados.Items.Add(item)
        Next
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Dim empresa As New clsEmpresa
        Dim contratista As New clsContratista
        Dim log As New clsLog
        Dim accion As Boolean
        Dim accion2 As Boolean
        Dim accion3 As Boolean
        Dim registro As Boolean
        LblAdvertencia.Text = ""
        If (TxtRazonSocial.Text.Trim() = "" Or TxtRut.Text.Trim() = "" Or TxtGiro.Text.Trim() = "" Or TxtDireccion.Text.Trim() = "" Or TxtCiudad.Text.Trim() = "" Or TxtFono.Text.Trim() = "" Or TxtCelular.Text.Trim() = "" Or TxtCorreo.Text.Trim() = "") Then
            LblAdvertencia.Text = "Ha ocurrido un error. Uno de los campos se encuentra en blanco."
        Else
            accion3 = contratista.desactivarContratista(obtenerRutEncargadoEmpresa(TxtRut.Text.Trim()))
            accion = empresa.actualizarEmpresa(TxtRazonSocial.Text.Trim(), TxtRut.Text.Trim(), TxtGiro.Text.Trim(), TxtDireccion.Text.Trim(), TxtCiudad.Text.Trim(), DropEncargados.SelectedItem.Text.Trim(), TxtCorreo.Text.Trim(), TxtFono.Text.Trim(), TxtCelular.Text.Trim(), DropEncargados.SelectedItem.Value.Trim())
            accion2 = contratista.activarContratista(DropEncargados.SelectedItem.Value.Trim())

            If accion = False Or accion2 = False Or accion3 = False Then
                LblAdvertencia.Text = "Ha ocurrido un error en la conexión. Favor inténtelo nuevamente."
            Else
                LblAdvertencia.Text = "Se ha modificado la empresa con éxito."
                registro = log.insertarRegistro("Se ha modificado a la empresa de rut: " + TxtRut.Text.Trim(), Session("usuario").getRut)
                bloquearCampos()
                btnAceptar.Visible = False
                btnModificar.Visible = True
                DropEncargados.Items.Clear()
                cargarEncargadoEmpresa(TxtRut.Text)
            End If
        End If
    End Sub

    Protected Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        LblAdvertencia.Text = ""
        DropEncargados.Items.Clear()
        cargarEncargadoEmpresa(TxtRut.Text)
        desbloquearCampos()
        cargarOtrosContratistasDisponibles()
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
End Class