Public Class modificarDcto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            cargarAreas()
            bloquearCampos()

        End If
    End Sub

    Public Sub bloquearCampos()
        dropDocumentos.Enabled = False
        dropDocumentos.CssClass = "btn btn-light bg-light dropdown-toggle col-12"
        dropTipoNuevoDocumento.Enabled = False
        dropTipoNuevoDocumento.CssClass = "btn btn-light bg-light dropdown-toggle col-12"
        TxtNombreDocumentoEdicion.ReadOnly = True
        chkListaAreasEdicion.Enabled = False
    End Sub

    Public Sub cargarAreas()

        Dim areas As DataTable = obtenerTablaAreas()

        dropAreas.Items.Clear()
        chkListaAreasEdicion.Items.Clear()

        dropAreas.Items.Add("")

        For Each celda As DataRow In areas.Rows

            Dim item As New ListItem()

            item.Text = celda("nombre").ToString()
            item.Value = celda("id")

            dropAreas.Items.Add(item)
            chkListaAreasEdicion.Items.Add(item)

        Next

    End Sub

    Public Sub cargarDocumentos()

        Dim documentos As DataTable = obtenerDocumentos()

        dropDocumentos.Items.Clear()
        dropDocumentos.Items.Add("")

        For Each celda As DataRow In documentos.Rows

            Dim itemDrop As New ListItem

            itemDrop.Text = celda("nombre").ToString()

            itemDrop.Value = celda("id")

            dropDocumentos.Items.Add(itemDrop)

        Next

    End Sub

    Public Function obtenerTablaAreas() As DataTable
        Dim Areas = New clsArea()
        Return Areas.obtenerNombre()
    End Function

    Public Function obtenerDocumentos() As DataTable
        Dim Documentos = New clsDocumento()
        Return Documentos.obtenerDocumento()
    End Function

    Protected Sub dropAreas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropAreas.SelectedIndexChanged

        If dropAreas.SelectedItem.Text.ToString <> "" Then
            Dim documento As New clsDocumento

            dropDocumentos.Items.Clear()
            dropDocumentos.Items.Add("")
            lblHeadEdicion.Text = "Edición"
            TxtNombreDocumentoEdicion.Text = ""
            dropTipoNuevoDocumento.ClearSelection()
            chkListaAreasEdicion.ClearSelection()
            bloquearCampos()


            Dim dt As New DataTable
            dt = documento.buscarDocumentosArea(dropAreas.SelectedValue)

            For Each celda As DataRow In dt.Rows

                Dim itemDrop As New ListItem

                itemDrop.Text = celda("nombre").ToString()

                itemDrop.Value = celda("id")

                dropDocumentos.Items.Add(itemDrop)

            Next

            dropDocumentos.Enabled = True

        Else
            dropDocumentos.Items.Clear()
            dropDocumentos.Items.Add("")
            lblHeadEdicion.Text = "Edición"
            TxtNombreDocumentoEdicion.Text = ""
            dropTipoNuevoDocumento.ClearSelection()
            chkListaAreasEdicion.ClearSelection()
            bloquearCampos()
        End If

    End Sub

    Protected Sub dropDocumentos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropDocumentos.SelectedIndexChanged

        If dropDocumentos.SelectedItem.Text.ToString <> "" Then

            TxtNombreDocumentoEdicion.Text = ""
            dropTipoNuevoDocumento.ClearSelection()
            chkListaAreasEdicion.ClearSelection()

            dropTipoNuevoDocumento.Enabled = True
            TxtNombreDocumentoEdicion.ReadOnly = False
            chkListaAreasEdicion.Enabled = True
            lblHeadEdicion.Text = "Edición / " & dropDocumentos.SelectedItem.Text.ToString

        Else
            TxtNombreDocumentoEdicion.Text = ""
            dropTipoNuevoDocumento.ClearSelection()
            chkListaAreasEdicion.ClearSelection()
            lblHeadEdicion.Text = "Edición"
        End If

    End Sub

    Protected Sub btnRealizarCambios_Click(sender As Object, e As EventArgs) Handles btnRealizarCambios.Click
        Dim documento As New clsDocumento
        Dim task As Boolean
        lblAdvertencia.Text = ""

        If TxtNombreDocumentoEdicion.Text.ToString = "" Or dropTipoNuevoDocumento.SelectedItem.Text.ToString = "" Or chkListaAreasEdicion.SelectedIndex = -1 Or dropDocumentos.SelectedItem.Text.ToString = "" Or dropAreas.SelectedItem.Text.ToString = "" Then

            lblAdvertencia.Text = "Campos invalidos"

        Else
            task = documento.actualizarDocumento(dropDocumentos.SelectedItem.Text, TxtNombreDocumentoEdicion.Text.Trim(), dropTipoNuevoDocumento.SelectedItem.Text, chkListaAreasEdicion.SelectedItem.Value)
            If task = False Then
                lblAdvertencia.Text = "Error de procedimiento almc."
            Else
                lblAdvertencia.Text = "Operación exitosa"
                Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
            End If
        End If


    End Sub
End Class