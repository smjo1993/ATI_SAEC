Public Class modificarDcto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'validarUsuario()
        'lblMenu.Visible = False
        'cargarMenu()

        'If Not IsPostBack Then
        '    Return
        'End If

        'lblHeadEdicion.Text = "Edición: " & Session("nombreDocumento")



        If Not IsPostBack Then

            validarUsuario()
            lblMenu.Visible = False
            cargarMenu()
            lblHeadEdicion.Text = Session("nombreDocumento")
            TxtNombreDocumentoEdicion.Attributes.Add("placeholder", lblHeadEdicion.Text)
            cargarNotificacionesComentarios()
        End If

    End Sub

    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "1,4", "A")

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

    'Public Sub bloquearCampos()
    '    dropTipoNuevoDocumento.Enabled = False
    '    dropTipoNuevoDocumento.CssClass = "btn btn-light bg-light dropdown-toggle col-12"
    '    TxtNombreDocumentoEdicion.ReadOnly = True
    'End Sub

    'Public Sub cargarAreas()

    '    Dim areas As DataTable = obtenerTablaAreas()

    '    dropAreas.Items.Clear()
    '    'chkListaAreasEdicion.Items.Clear()

    '    dropAreas.Items.Add("")

    '    For Each celda As DataRow In areas.Rows

    '        Dim item As New ListItem()

    '        item.Text = celda("nombre").ToString()
    '        item.Value = celda("id")

    '        dropAreas.Items.Add(item)
    '        'chkListaAreasEdicion.Items.Add(item)

    '    Next

    'End Sub

    'Public Sub cargarDocumentos()

    '    Dim documentos As DataTable = obtenerDocumentos()

    '    dropDocumentos.Items.Clear()
    '    dropDocumentos.Items.Add("")

    '    For Each celda As DataRow In documentos.Rows

    '        Dim itemDrop As New ListItem

    '        itemDrop.Text = celda("nombre").ToString()

    '        itemDrop.Value = celda("id")

    '        dropDocumentos.Items.Add(itemDrop)

    '    Next

    'End Sub

    Public Function obtenerTablaAreas() As DataTable
        Dim Areas = New clsArea()
        Return Areas.obtenerNombre()
    End Function

    Public Function obtenerDocumentos() As DataTable
        Dim Documentos = New clsDocumento()
        Return Documentos.obtenerDocumento()
    End Function

    'Protected Sub dropAreas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropAreas.SelectedIndexChanged

    '    If dropAreas.SelectedItem.Text.ToString <> "" Then
    '        Dim documento As New clsDocumento

    '        dropDocumentos.Items.Clear()
    '        dropDocumentos.Items.Add("")
    '        lblHeadEdicion.Text = "Edición"
    '        TxtNombreDocumentoEdicion.Text = ""
    '        dropTipoNuevoDocumento.ClearSelection()
    '        bloquearCampos()


    '        Dim dt As New DataTable
    '        dt = documento.buscarDocumentosArea(dropAreas.SelectedValue)

    '        For Each celda As DataRow In dt.Rows

    '            Dim itemDrop As New ListItem

    '            itemDrop.Text = celda("nombre").ToString()

    '            itemDrop.Value = celda("id")

    '            dropDocumentos.Items.Add(itemDrop)

    '        Next

    '        dropDocumentos.Enabled = True

    '    Else
    '        dropDocumentos.Items.Clear()
    '        dropDocumentos.Items.Add("")
    '        lblHeadEdicion.Text = "Edición"
    '        TxtNombreDocumentoEdicion.Text = ""
    '        dropTipoNuevoDocumento.ClearSelection()
    '        chkListaAreasEdicion.ClearSelection()
    '        bloquearCampos()
    '    End If

    'End Sub

    'Protected Sub dropDocumentos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropDocumentos.SelectedIndexChanged

    '    If dropDocumentos.SelectedItem.Text.ToString <> "" Then

    '        TxtNombreDocumentoEdicion.Text = ""
    '        dropTipoNuevoDocumento.ClearSelection()

    '        dropTipoNuevoDocumento.Enabled = True
    '        TxtNombreDocumentoEdicion.ReadOnly = False
    '        lblHeadEdicion.Text = "Edición / " & dropDocumentos.SelectedItem.Text.ToString

    '    Else
    '        TxtNombreDocumentoEdicion.Text = ""
    '        dropTipoNuevoDocumento.ClearSelection()
    '        lblHeadEdicion.Text = "Edición"
    '    End If

    'End Sub

    Protected Sub btnRealizarCambios_Click(sender As Object, e As EventArgs) Handles btnRealizarCambios.Click
        Dim documento As New clsDocumento
        Dim log As New clsLog
        Dim nombreDocumento As String = Session("nombreDocumento")
        Dim idDocumento As Integer = Session("idDocumento")
        Dim task As Boolean
        lblAdvertencia.Text = ""
        TxtNombreDocumentoEdicion.Attributes.Add("placeholder", "")

        'If TxtNombreDocumentoEdicion.Text.ToString = "" Or dropTipoNuevoDocumento.SelectedItem.Text.ToString = "" Or dropDocumentos.SelectedItem.Text.ToString = "" Or dropAreas.SelectedItem.Text.ToString = "" Then

        '    lblAdvertencia.Text = "Campos invalidos"

        If TxtNombreDocumentoEdicion.Text.Trim().ToString = "" Or dropTipoNuevoDocumento.SelectedItem.Text.Trim().ToString = "" Then

            lblAdvertencia.Text = "Campos invalidos"

        Else

            'task = documento.actualizarDocumento(dropDocumentos.SelectedItem.Text, TxtNombreDocumentoEdicion.Text.Trim(), dropTipoNuevoDocumento.SelectedItem.Text.Trim())
            task = documento.actualizarDocumento(nombreDocumento, TxtNombreDocumentoEdicion.Text.Trim(), dropTipoNuevoDocumento.SelectedItem.Text.Trim(), idDocumento)
            Log.insertarRegistro("Se ha editado el Requerimiento Documental: " + TxtNombreDocumentoEdicion.Text.Trim(), Session("usuario").getRut)

            If task = False Then
                lblAdvertencia.Text = "Error de procedimiento almc."
            Else
                lblAdvertencia.Text = "Operación exitosa"
                Session("nombreDocumento") = TxtNombreDocumentoEdicion.Text.Trim()
                'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
                Response.Redirect("verListaDctos.aspx")
            End If
        End If


    End Sub
    Protected Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Session.Contents.RemoveAll()
        Response.Redirect("../login.aspx")
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