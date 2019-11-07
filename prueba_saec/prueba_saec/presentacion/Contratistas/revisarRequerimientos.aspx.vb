Public Class revisarRequerimientos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim documento As String = ""
        ' Dim idCarpeta As Integer = Convert.ToInt32(Request.QueryString("idCarpeta").ToString())
        Dim listaDocumentosEspera As DataTable = crearDocumentos().obtenerDocumentoEstadoEspera("6178212")

        For Each fila As DataRow In listaDocumentosEspera.Rows

            documento = documento & "    <tr> "
            documento = documento & "        <td> " + fila("nombre") + "</td> "
            documento = documento & "        <td> " + fila("descripcion") + "</td> "
            documento = documento & "        <td> System Architect</td> "
            documento = documento & "        <td> Edinburgh</td> "
            documento = documento & "    </tr> "


        Next




        LblDocumentos.Text = documento

    End Sub



    Public Function crearDocumentos() As Object

        Dim documentosEspera = New clsDocumento()

        Return documentosEspera

    End Function



End Class