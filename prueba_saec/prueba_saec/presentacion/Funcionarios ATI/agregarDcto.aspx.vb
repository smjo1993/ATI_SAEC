Public Class agregarDcto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        cargarAreas()
        cargarDocumentos()

    End Sub

    Public Sub cargarAreas()

        Dim areas As DataTable = obtenerTablaAreas()

        chkListaAreas.Items.Clear()

        For Each celda As DataRow In areas.Rows

            Dim item As New ListItem()

            item.Text = celda("descripcion").ToString()

            If celda("descripcion").ToString().Equals("RRHH") Then
                item.Text = ("Recursos Humanos")
            ElseIf celda("descripcion").ToString().Equals("Prevencion") Then
                item.Text = ("Prevención")
            ElseIf celda("descripcion").ToString().Equals("ControlGestion") Then
                item.Text = ("Control de Gestión")
            ElseIf celda("descripcion").ToString().Equals("MedioAmbiente") Then
                item.Text = ("Medio Ambiente")

            End If

            item.Value = celda("id").ToString()
            'item.Selected = Convert.ToBoolean(celda("IsSelected"))

            chkListaAreas.Items.Add(item)
        Next

    End Sub

    Public Sub cargarDocumentos()

        Dim documentos As DataTable = obtenerDocumentos()

        dropTipoDocumento.Items.Clear()
        dropTipoDocumento.Items.Add("")

        For Each celda As DataRow In documentos.Rows

            Dim itemDrop As New ListItem

            itemDrop.Text = celda("tipo").ToString()

            itemDrop.Value = celda("tipo").ToString()

            dropTipoDocumento.Items.Add(itemDrop)

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

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("../login.aspx")
    End Sub

    Protected Sub btnCrearDocumento_Click(sender As Object, e As EventArgs) Handles btnCrearDocumento.Click

        Dim nuevoDocumento = New clsDocumento()

        Dim insercion As New Boolean

        If (txtNombreDocumento.Text.Trim() = "") Then
            lblAdvertencia.Text = "Uno de los campos necesarios se encuentra en blanco"
        Else
            insercion = nuevoDocumento.insertarDocumento(txtNombreDocumento.Text.Trim(), txtIdDocumento.Text.Trim(), dropTipoDocumento.SelectedItem.Text.Trim(), chkListaAreas.SelectedItem.Value.ToString.Trim())
        End If

    End Sub
End Class