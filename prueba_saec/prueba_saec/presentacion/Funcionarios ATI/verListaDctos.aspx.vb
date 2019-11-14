Public Class verListaDctos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Return
        End If
        'If (Not clsUsuario.ValidaAccesoForm(Session("Usuario"), Request.Url.Segments(Request.Url.Segments.Length - 1))) Then
        '    Response.Redirect("AccesoDenegado.aspx")
        'End If
        cargarGrid()
    End Sub

    Private Sub cargarGrid()
        Dim documento As New clsDocumento
        Try
            Dim dt As Data.DataTable = documento.listarRequisitosDocumentales()

            If dt.Rows.Count > 0 Then
                Me.gridRequisitos.DataSource = dt
                Me.gridRequisitos.DataBind()

                For Each documentoMaestro As GridViewRow In gridRequisitos.Rows

                    Dim chk As HtmlInputCheckBox
                    chk = documentoMaestro.FindControl("chkDocumento")

                    If documentoMaestro.Cells(4).Text = "A" Then
                        chk.Checked = True
                    End If

                Next
                gridRequisitos.Columns(3).Visible = False

            Else
                Return
            End If
        Catch ex As Exception

            Return
        End Try
    End Sub

    Protected Sub gridEmpresas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridRequisitos.RowCommand

        If (e.CommandName = "editar") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim nombreDocumento As String = gridRequisitos.Rows(pos).Cells(1).Text
            Dim areaDocumento As String = gridRequisitos.Rows(pos).Cells(3).Text

            'Response.Redirect("modificarEmpresa.aspx")

            Session("nombreDocumento") = nombreDocumento
            Session("areaDocumento") = areaDocumento
            Session("idDocumento") = Convert.ToInt32(gridRequisitos.Rows(pos).Cells(0).Text)
            Response.Redirect("modificarDcto.aspx")
        End If


    End Sub

End Class