﻿Public Class WebForm1
    Inherits System.Web.UI.Page

    Dim empresa As New clsEmpresa

    Dim dt As DataTable = empresa.listarEmpresas()

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
        Dim empresa As New clsEmpresa
        Try
            Dim dt As Data.DataTable = empresa.listarEmpresas()

            If dt.Rows.Count > 0 Then
                Me.gridEmpresas.DataSource = dt
                Me.gridEmpresas.DataBind()


            Else
                'METRO.UI.MsgBox.Show(Page, "Alerta", "", METRO.UI.MsgBox.Modalidad.alert, METRO.UI.MsgBox.TipoWin8.Si, METRO.UI.MsgBox.OpcionColor.black)
                Return
            End If
        Catch ex As Exception
            'METRO.UI.MsgBox.Show(Page, "Alerta", "Problemas desde BD al cargar menús", METRO.UI.MsgBox.Modalidad.alert, METRO.UI.MsgBox.TipoWin8.Si, METRO.UI.MsgBox.OpcionColor.black)
            Return
        End Try
    End Sub

    Protected Sub gridEmpresas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridEmpresas.RowCommand

        If (e.CommandName = "Ver") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim rutEmpresa As String = gridEmpresas.Rows(pos).Cells(1).Text

            'Response.Redirect("modificarEmpresa.aspx")
            Session("rutEmpresa") = rutEmpresa
            Response.Redirect("modificarEmpresa.aspx")
        End If


    End Sub

End Class