Public Class verContratistas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Return
        End If
        'If (Not clsUsuario.ValidaAccesoForm(Session("Usuario"), Request.Url.Segments(Request.Url.Segments.Length - 1))) Then
        '    Response.Redirect("AccesoDenegado.aspx")
        'End If
        cargarGrid()
        cambiarFormatoEstado()
    End Sub

    Private Sub cargarGrid()
        Dim contratista As New clsContratista
        Try
            Dim dt As Data.DataTable = contratista.listarContratistas()

            If dt.Rows.Count > 0 Then
                Me.gridContratistas.DataSource = dt
                Me.gridContratistas.DataBind()


            Else
                'METRO.UI.MsgBox.Show(Page, "Alerta", "", METRO.UI.MsgBox.Modalidad.alert, METRO.UI.MsgBox.TipoWin8.Si, METRO.UI.MsgBox.OpcionColor.black)
                Return
            End If
        Catch ex As Exception
            'METRO.UI.MsgBox.Show(Page, "Alerta", "Problemas desde BD al cargar menús", METRO.UI.MsgBox.Modalidad.alert, METRO.UI.MsgBox.TipoWin8.Si, METRO.UI.MsgBox.OpcionColor.black)
            Return
        End Try
    End Sub

    Private Sub cambiarFormatoEstado()
        Dim cantContratistas As Integer
        cantContratistas = Me.gridContratistas.Rows.Count()
        For i As Integer = 0 To cantContratistas - 1
            Dim texto As String
            texto = Me.gridContratistas.Rows(i).Cells(4).Text

            If texto = "A" Then
                Me.gridContratistas.Rows(i).Cells(4).Text = "Activo"
            End If

            If texto = "I" Then
                Me.gridContratistas.Rows(i).Cells(4).Text = "Inactivo"
            End If

        Next


    End Sub

    Protected Sub gridContratistas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridContratistas.RowCommand

        If (e.CommandName = "Ver") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim rutContratista As String = gridContratistas.Rows(pos).Cells(1).Text

            'Response.Redirect("modificarEmpresa.aspx")
            Session("rutEmpresa") = rutContratista
            Response.Redirect("modificarContratista.aspx")
        End If


    End Sub

End Class