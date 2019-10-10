Public Class agregarDcto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim areas As DataTable = obtenerTablaAreas()
        Dim documentos As DataTable = obtenerDocumentos()

        Dim dropItem As String = ""

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


        For Each celda As DataRow In documentos.Rows

            'recordCount = recordCount + recordCount

            dropItem = dropItem & "<li role=""presentation"">"

            dropItem = dropItem & "<a class=""dropdown-item"" href=""#"">"

            dropItem = dropItem & celda("tipo").ToString() & " "

            dropItem = dropItem & documentos.Rows.IndexOf(celda).ToString

            'dropItem = dropItem & recordCount.ToString()

            dropItem = dropItem & "</a></li>"

            'lblDropTipoDocumentos.Text = lblDropTipoDocumentos.Text & dropItem

            Literal1.Text = Literal1.Text & dropItem

            'Dim item As New ListItem()

            'item.Text = celda("tipo").ToString()

            'If celda("descripcion").ToString().Equals("RRHH") Then

            '    item.Text = ("Recursos Humanos")

            'ElseIf celda("descripcion").ToString().Equals("Prevencion") Then

            '    item.Text = ("Prevención")

            'ElseIf celda("descripcion").ToString().Equals("ControlGestion") Then

            '    item.Text = ("Control de Gestión")

            'ElseIf celda("descripcion").ToString().Equals("MedioAmbiente") Then

            '    item.Text = ("Medio Ambiente")

            'End If

            'item.Value = celda("id").ToString()

            'chkListaAreas.Items.Add(item)


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
        Response.Redirect("../login.aspx")
    End Sub
End Class