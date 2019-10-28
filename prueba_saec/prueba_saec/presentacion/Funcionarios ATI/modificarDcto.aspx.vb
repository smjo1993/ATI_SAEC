Public Class modificarDcto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            cargarAreas()
            cargarDocumentos()

        End If
    End Sub

    Public Sub cargarAreas()

        Dim areas As DataTable = obtenerTablaAreas()

        dropAreas.Items.Clear()
        dropAreas.Items.Add("")

        For Each celda As DataRow In areas.Rows

            Dim item As New ListItem()

            item.Text = celda("descripcion").ToString()
            item.Value = celda("id")

            dropAreas.Items.Add(item)

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

End Class