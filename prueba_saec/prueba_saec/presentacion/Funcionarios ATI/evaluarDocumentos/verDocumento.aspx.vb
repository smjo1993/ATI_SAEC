Public Class verDocumentoPDF
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rutaCodificada As String = Request.QueryString("r").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(rutaCodificada)
        Dim rutaDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)

        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.WriteFile(rutaDecodificado)
    End Sub

End Class