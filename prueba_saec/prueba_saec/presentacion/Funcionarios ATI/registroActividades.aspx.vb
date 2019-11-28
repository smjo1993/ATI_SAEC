Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class registroActividades
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        validarUsuario()
        If IsPostBack Then
            Return
        End If

    End Sub

    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim listaRoles As List(Of clsRol) = New List(Of clsRol)

        listaRoles = Session("roles")

        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else

            For Each rol As clsRol In listaRoles

                If rol.getDescripcion.ToString <> "super-admin" Then

                    Response.Redirect("../login.aspx")

                Else

                    cargarGrid()

                End If

            Next

        End If
    End Sub

    Private Sub cargarGrid()
        Dim log As New clsLog
        Try
            Dim dt As Data.DataTable = log.obtenerRegistro()

            If dt.Rows.Count > 0 Then
                Me.gridRegistros.DataSource = dt
                Me.gridRegistros.DataBind()

                'For Each registro As GridViewRow In gridRegistros.Rows

                '    Dim chk As HtmlInputCheckBox
                '    chk = registro.FindControl("chkDocumento")

                '    If registro.Cells(4).Text = "A" Then
                '        chk.Checked = True

                '    ElseIf registro.Cells(4).Text = "I" Then
                '        chk.Checked = False
                '    End If

                'Next

            Else
                Return
            End If
        Catch ex As Exception

            Return
        End Try
    End Sub

    Protected Sub gridRegistros_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gridRegistros.PageIndexChanging

        'Me.txtBuscar_TextChanged(sender, e)

        cargarGrid()
        gridRegistros.PageIndex = e.NewPageIndex
        gridRegistros.DataBind()

    End Sub

    'Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs)

    '    Dim log As New clsLog

    '    If Not String.IsNullOrEmpty(txtBuscar.Text.Trim()) Then

    '        Dim dt As Data.DataTable = log.buscarRegistros(txtBuscar.Text.Trim())

    '        If dt.Rows.Count > 0 Then

    '            Me.gridRegistros.DataSource = dt
    '            Me.gridRegistros.DataBind()

    '        Else

    '            'Return
    '            cargarGrid()

    '        End If

    '    End If

    'End Sub
End Class