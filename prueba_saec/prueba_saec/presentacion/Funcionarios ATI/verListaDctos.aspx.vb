﻿Public Class verListaDctos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            validarUsuario()
            cargarNotificacionesComentarios()
        End If

    End Sub

    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim listaRoles As List(Of clsRol) = New List(Of clsRol)

        listaRoles = Session("roles")

        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else

            LblNombreUsuario.Text = usuario.getNombre().Trim()

            For Each rol As clsRol In listaRoles

                If rol.getDescripcion.ToString <> "super-admin" Then

                    Response.Redirect("../login.aspx")

                Else

                    cargarGrid()

                End If

            Next

        End If
        cargarMenu()
        cargarGrid()

    End Sub

    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        'Dim idCarpeta As Integer = decodificarId()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
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

                    ElseIf documentoMaestro.Cells(4).Text = "I" Then
                        chk.Checked = False
                    End If

                Next

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

            Session("nombreDocumento") = nombreDocumento
            Session("idDocumento") = Convert.ToInt32(gridRequisitos.Rows(pos).Cells(0).Text)

            Response.Redirect("modificarDcto.aspx")
        End If


    End Sub

    Protected Sub btnRealizarCambios_Click(sender As Object, e As EventArgs) Handles btnRealizarCambios.Click

        Dim documento As New clsDocumento
        Dim chk As HtmlInputCheckBox
        Dim task As Boolean
        Dim idDocumento As Integer
        Dim estadoDocumento As String
        Dim log As New clsLog

        For Each documentoMaestro As GridViewRow In gridRequisitos.Rows
            chk = documentoMaestro.FindControl("chkDocumento")
            idDocumento = Convert.ToInt32(documentoMaestro.Cells(0).Text)

            If chk.Checked = True Then
                documentoMaestro.Cells(4).Text = "A"
            ElseIf chk.Checked = False Then
                documentoMaestro.Cells(4).Text = "I"
            End If

            estadoDocumento = documentoMaestro.Cells(4).Text

            task = documento.actualizarEstadoRequerimiento(idDocumento, estadoDocumento)

            'log.insertarRegistro("Actualización ESTADO de Requerimiento Documental: " + documentoMaestro.Cells(1).Text.Trim(), Session("usuario").getRut)

            If task = False Then
                'lblAdvertencia.Text = "Error de procedimiento almc."
            Else
                'lblAdvertencia.Text = "Operación exitosa"
                'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

            End If

        Next

        log.insertarRegistro("Actualización ESTADO de Lista de Requerimientos Documentales", Session("usuario").getRut)

        Response.Redirect("verListaDctos.aspx")

    End Sub

    'Protected Function obtenerResumen(rut As String) As String
    '    Dim comentario As New clsComentario
    '    Dim usuarioSaec As New clsUsuarioSAEC
    '    Dim resumen As String
    '    Dim dt As DataTable
    '    Dim dr As DataRow
    '    dt = contratista.obtenerContratista(rut)

    '    If dt.Rows.Count > 0 Then
    '        dr = dt.Rows.Item(0)
    '        resumen = dr("nombre")
    '        Return resumen

    '    Else
    '        Return "Usuario no encontrado"
    '    End If
    'End Function

    Private Sub cargarNotificacionesComentarios()
        Dim comentario As New clsComentario
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim tarjeta As String = ""

        Dim listaNotificaciones As Data.DataTable = comentario.obtenerComentariosRespuesta(rutUsuario)

        For Each fila As DataRow In listaNotificaciones.Rows
            Dim resumenComentario As String = fila("Comentario")
            Dim nombreUsuarioRespuesta As String = fila("autor")
            Dim carpetaArranque As String = fila("CA")

            tarjeta = tarjeta & "  <a class=""dropdown-item d-flex align-items-center"" href=""../Contratistas/verComentarios.aspx"">"
            tarjeta = tarjeta & "   <div class=""dropdown-list-image mr-3"">"
            tarjeta = tarjeta & "   <img class=""img-profile rounded-circle"" src=""https://c7.uihere.com/files/25/400/945/computer-icons-industry-business-laborer-industrail-workers-and-engineers-thumb.jpg"" style=""height:40px;width:40px;"">"
            tarjeta = tarjeta & "    </div> "
            tarjeta = tarjeta & "    <div class=""font-weight-bold""> "
            tarjeta = tarjeta & "    <div class=""text-truncate"">" + resumenComentario + "</div> "
            tarjeta = tarjeta & "    <div class=""small text-gray-500"">" + nombreUsuarioRespuesta + "· 1d</div> "
            tarjeta = tarjeta & "    <div class=""small text-gray-500"">" + carpetaArranque + "</div> "
            tarjeta = tarjeta & "    </div> "
            tarjeta = tarjeta & "    </a> "


            LblNotificacion.Text = tarjeta

        Next

    End Sub

End Class