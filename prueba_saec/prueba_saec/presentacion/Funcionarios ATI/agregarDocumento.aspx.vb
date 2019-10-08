Imports System.Data.SqlClient

Public Class agregarDocumento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim areas As DataTable = obtenerTablaAreas()

        For Each celda As DataRow In areas.Rows
            Dim item As New ListItem()
            item.Text = celda("descripcion").ToString()
            item.Value = celda("id").ToString()
            'item.Selected = Convert.ToBoolean(celda("IsSelected"))
            chkListaAreas.Items.Add(item)
        Next

    End Sub

    Public Function obtenerTablaAreas() As DataTable
        Dim Areas = New clsArea()
        Return Areas.obtenerNombre()
    End Function

End Class