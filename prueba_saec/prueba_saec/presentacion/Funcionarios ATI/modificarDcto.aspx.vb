Public Class modificarDcto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblMenu.Visible = False

        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
        End If
        'cargarAreas()
        'bloquearCampos()
        lblHeadEdicion.Text = "Edición: " & Session("nombreDocumento")
    End Sub

    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
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
        Dim nombreDocumento As String = Session("nombreDocumento")
        Dim idDocumento As Integer = Session("idDocumento")
        Dim task As Boolean
        lblAdvertencia.Text = ""

        'If TxtNombreDocumentoEdicion.Text.ToString = "" Or dropTipoNuevoDocumento.SelectedItem.Text.ToString = "" Or dropDocumentos.SelectedItem.Text.ToString = "" Or dropAreas.SelectedItem.Text.ToString = "" Then

        '    lblAdvertencia.Text = "Campos invalidos"

        If TxtNombreDocumentoEdicion.Text.Trim().ToString = "" Or dropTipoNuevoDocumento.SelectedItem.Text.Trim().ToString = "" Then

            lblAdvertencia.Text = "Campos invalidos"

        Else

            'task = documento.actualizarDocumento(dropDocumentos.SelectedItem.Text, TxtNombreDocumentoEdicion.Text.Trim(), dropTipoNuevoDocumento.SelectedItem.Text.Trim())
            task = documento.actualizarDocumento(nombreDocumento, TxtNombreDocumentoEdicion.Text.Trim(), dropTipoNuevoDocumento.SelectedItem.Text.Trim(), idDocumento)

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
End Class