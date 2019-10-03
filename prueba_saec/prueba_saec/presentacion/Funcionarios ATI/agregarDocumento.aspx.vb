Imports System.Data.SqlClient

Public Class agregarDocumento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim chkListItem As String = ""
        Dim areas As DataTable = obtenerTablaAreas()

        'For Each celda As DataRow In areas.Rows
        '    chkListItem = chkListItem & "<asp:ListItem>" + celda("descripcion") + "</asp:ListItem>"
        '    lblChkItem.Text = chkListItem

        'Next
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

    'Private Sub obtenerAreas()
    '    Dim chkListaAreas = New CheckBoxList

    '    Using conn As New SqlConnection()
    '        conn.ConnectionString = ConfigurationManager _
    '            .ConnectionStrings("constr").ConnectionString()
    '        Using cmd As New SqlCommand()
    '            'cmd.CommandText = "select * from dbo.TB_SAEC_Area"
    '            cmd.CommandText = "SP_SAEC_ListarAreas"
    '            cmd.Connection = conn

    '            conn.Open()
    '            Using sdr As SqlDataReader = cmd.ExecuteReader()
    '                While sdr.Read()
    '                    Dim item As New ListItem()
    '                    item.Text = sdr("descripcion").ToString()
    '                    item.Selected = Convert.ToBoolean(sdr("IsSelected"))
    '                    chkListaAreas.Items.Add(item)
    '                End While
    '            End Using
    '            conn.Close()
    '        End Using
    '    End Using
    'End Sub

End Class