Public Class evaluarDocumentosTrabajador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then
            Return
        End If

        Dim trabajador = New clsTrabajador()
        Dim idCarpeta As Integer = 113
        Dim idArea As Integer = 2
        Dim idTrabajador As Integer = Session("idTrabajador")
        Dim tablaDocumentosTrabajador = trabajador.listarDocumentosTrabajadorParaRevisar(idCarpeta, idArea, idTrabajador)
        gridListarDocumentosTrabajador.DataSource = tablaDocumentosTrabajador
        gridListarDocumentosTrabajador.DataBind()
        lblTrabajador.Text = Session("rutTrabajador")

    End Sub


    Protected Sub btnVerDocumento_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarDocumentosTrabajador.RowCommand

        Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
        Dim ruta As String = gridListarDocumentosTrabajador.Rows(pos).Cells(10).Text
        Dim nombreArchivo As String = gridListarDocumentosTrabajador.Rows(pos).Cells(2).Text
        Dim extension As String = ExtraerExtension(ruta, ".")

        If (e.CommandName = "ver") Then

            If extension = "pdf" Then

                'Se codifica la ruta del archivo para pasarlo por URl
                Dim rutaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(ruta)
                Dim rutaCodificada As String = System.Convert.ToBase64String(rutaBase64)
                'Response.Clear()
                'Response.ContentType = "application/pdf"
                Response.Write("<script type='text/javascript'>detailedresults=window.open('verDocumento.aspx?r=" + rutaCodificada + "');</script>")
                'Response.WriteFile(ruta)

            Else

                Response.Clear()
                Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", ruta))
                Response.WriteFile(ruta)
                Response.End()

            End If





        End If

    End Sub

    'funcion que obtiene la extension del archivo
    Function ExtraerExtension(Path As String, Caracter As String) As String

        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        ExtraerExtension = ret

    End Function

End Class