﻿Imports System.Windows.Controls

Public Class CrearListaDocumentacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinDocEmpresaPendiente.Visible = False
        sinDocEmpresaPedido.Visible = False
        sinDocTrabajadorPendiente.Visible = False
        sinDocTrabajadorPedido.Visible = False
        sinDocVehiculoPendiente.Visible = False
        sinDocVehiculoPedido.Visible = False
        lblMenu.Visible = False
        lblNombreEmpresa.Visible = False

        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarDatos()
            cargarNotificacionesComentarios()
        End If

    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "5,1", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
        End If
        LblNombreUsuario.Text = usuario.getNombre().ToString()
    End Sub

    Protected Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Session.Contents.RemoveAll()
        Response.Redirect("../login.aspx")
    End Sub
    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim idCarpeta As Integer = decodificarId()
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(nombreCodificado)
        Dim nombreDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiCarpeta(rutUsuario, idCarpeta, nombreDecodificado)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub
    Protected Function decodificarId() As Integer
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idCodificada)
        Dim idDecodificada As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim idCarpeta As Integer = Convert.ToInt32(idDecodificada)
        Return idCarpeta
    End Function
    Private Sub cargarDatos()
        lblNombreEmpresa.Visible = True
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(nombreCodificado)
        Dim nombreDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        lblNombreEmpresa.Text = nombreDecodificado
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim documento As New clsDocumento
        Dim idCarpeta As Integer = decodificarId()
        Dim chk As HtmlInputCheckBox

        Dim listaRoles As List(Of clsRol) = New List(Of clsRol)
        listaRoles = Session("roles")
        If listaRoles(0).getId = 1 Then

            ''Documentos Empresa

            Dim documentosEmpresa As DataTable = documento.buscarDocumentoPorAreaAdmin(usuario.getArea, "empresa", idCarpeta)
            If (documentosEmpresa Is Nothing) Then
                sinDocEmpresaPedido.Visible = True
            Else
                If (documentosEmpresa.Rows.Count > 0) Then
                    Me.gridDocumentosEmpresa.DataSource = documentosEmpresa
                    Me.gridDocumentosEmpresa.DataBind()
                    For Each documentoEmpresa As GridViewRow In gridDocumentosEmpresa.Rows
                        chk = documentoEmpresa.FindControl("chkDocEmpresa")
                        If documentoEmpresa.Cells(2).Text = "espera" Then
                            chk.Checked = True
                        End If
                    Next
                    'gridDocumentosEmpresa.Columns(3).Visible = False
                Else
                    sinDocEmpresaPedido.Visible = True
                End If
            End If

            Dim documentosEmpresaPendiente As DataTable = documento.buscarDocumentoPorAreaPendienteAdmin(usuario.getArea, "empresa", idCarpeta)

            If (documentosEmpresaPendiente Is Nothing) Then
                sinDocEmpresaPendiente.Visible = True
            Else
                If (documentosEmpresaPendiente.Rows.Count > 0) Then
                    Me.gridDocumentosEmpresaPendientes.DataSource = documentosEmpresaPendiente
                    Me.gridDocumentosEmpresaPendientes.DataBind()
                    'gridDocumentosEmpresaPendientes.Columns("Area").Visible = False
                Else
                    sinDocEmpresaPendiente.Visible = True
                End If
            End If

            ''Documentos Trabajador

            Dim documentosTrabajador As DataTable = documento.buscarDocumentoPorAreaAdmin(usuario.getArea, "trabajador", idCarpeta)
            If (documentosTrabajador Is Nothing) Then
                sinDocTrabajadorPedido.Visible = True
            Else
                If (documentosTrabajador.Rows.Count > 0) Then
                    Me.gridDocumentosTrabajador.DataSource = documentosTrabajador
                    Me.gridDocumentosTrabajador.DataBind()
                    For Each documentoTrabajador As GridViewRow In gridDocumentosTrabajador.Rows
                        chk = documentoTrabajador.FindControl("chkDocTrabajador")
                        If documentoTrabajador.Cells(2).Text = "espera" Then
                            chk.Checked = True
                        End If
                    Next
                    'gridDocumentosTrabajador.Columns("Area").Visible = False
                Else
                    sinDocTrabajadorPedido.Visible = True
                End If
            End If

            Dim documentosTrabajadorPendiente As DataTable = documento.buscarDocumentoPorAreaPendienteAdmin(usuario.getArea, "trabajador", idCarpeta)
            If (documentosTrabajadorPendiente Is Nothing) Then
                sinDocTrabajadorPendiente.Visible = True
            Else
                If (documentosTrabajadorPendiente.Rows.Count > 0) Then
                    Me.gridDocumentosTrabajadorPendiente.DataSource = documentosTrabajadorPendiente
                    Me.gridDocumentosTrabajadorPendiente.DataBind()
                    'gridDocumentosTrabajadorPendiente.Columns("Area").Visible = False
                Else
                    sinDocTrabajadorPendiente.Visible = True
                End If
            End If

            ''Documentos Vehiculo

            Dim documentosVehiculo As DataTable = documento.buscarDocumentoPorAreaAdmin(usuario.getArea, "vehiculo", idCarpeta)
            If (documentosVehiculo Is Nothing) Then
                sinDocVehiculoPedido.Visible = True
            Else
                If (documentosVehiculo.Rows.Count > 0) Then
                    Me.gridDocumentosVehiculo.DataSource = documentosVehiculo
                    Me.gridDocumentosVehiculo.DataBind()
                    For Each documentoVehiculo As GridViewRow In gridDocumentosVehiculo.Rows
                        chk = documentoVehiculo.FindControl("chkDocVehiculo")
                        If documentoVehiculo.Cells(2).Text = "espera" Then
                            chk.Checked = True
                        End If
                    Next
                    'gridDocumentosVehiculo.Columns("Area").Visible = False
                Else
                    sinDocVehiculoPedido.Visible = True
                End If
            End If

            Dim documentosVehiculoPendiente As DataTable = documento.buscarDocumentoPorAreaPendienteAdmin(usuario.getArea, "vehiculo", idCarpeta)
            If (documentosVehiculoPendiente Is Nothing) Then
                sinDocVehiculoPendiente.Visible = True
            Else
                If (documentosVehiculoPendiente.Rows.Count > 0) Then
                    Me.gridDocumentosVehiculoPendiente.DataSource = documentosVehiculoPendiente
                    Me.gridDocumentosVehiculoPendiente.DataBind()
                    'gridDocumentosVehiculoPendiente.Columns("Area").Visible = False
                Else
                    sinDocVehiculoPendiente.Visible = True
                End If
            End If
            ''------------------------------------------------------------------------------------
        Else

            ''Documentos Empresa

            Dim documentosEmpresa As DataTable = documento.buscarDocumentoPorArea(usuario.getArea, "empresa", idCarpeta)
            If (documentosEmpresa Is Nothing) Then
                sinDocEmpresaPedido.Visible = True
            Else
                If (documentosEmpresa.Rows.Count > 0) Then
                    Me.gridDocumentosEmpresa.DataSource = documentosEmpresa
                    Me.gridDocumentosEmpresa.DataBind()
                    For Each documentoEmpresa As GridViewRow In gridDocumentosEmpresa.Rows
                        chk = documentoEmpresa.FindControl("chkDocEmpresa")
                        If documentoEmpresa.Cells(2).Text = "espera" Then
                            chk.Checked = True
                        End If
                    Next
                    gridDocumentosEmpresa.Columns(3).Visible = False
                Else
                    sinDocEmpresaPedido.Visible = True
                End If
            End If

            Dim documentosEmpresaPendiente As DataTable = documento.buscarDocumentoPorAreaPendiente(usuario.getArea, "empresa", idCarpeta)

            If (documentosEmpresaPendiente Is Nothing) Then
                sinDocEmpresaPendiente.Visible = True
            Else
                If (documentosEmpresaPendiente.Rows.Count > 0) Then
                    Me.gridDocumentosEmpresaPendientes.DataSource = documentosEmpresaPendiente
                    Me.gridDocumentosEmpresaPendientes.DataBind()
                    gridDocumentosEmpresaPendientes.Columns(3).Visible = False
                Else
                    sinDocEmpresaPendiente.Visible = True
                End If
            End If

            ''Documentos Trabajador

            Dim documentosTrabajador As DataTable = documento.buscarDocumentoPorArea(usuario.getArea, "trabajador", idCarpeta)
            If (documentosTrabajador Is Nothing) Then
                sinDocTrabajadorPedido.Visible = True
            Else
                If (documentosTrabajador.Rows.Count > 0) Then
                    Me.gridDocumentosTrabajador.DataSource = documentosTrabajador
                    Me.gridDocumentosTrabajador.DataBind()
                    For Each documentoTrabajador As GridViewRow In gridDocumentosTrabajador.Rows
                        chk = documentoTrabajador.FindControl("chkDocTrabajador")
                        If documentoTrabajador.Cells(2).Text = "espera" Then
                            chk.Checked = True
                        End If
                    Next
                    gridDocumentosTrabajador.Columns(3).Visible = False
                Else
                    sinDocTrabajadorPedido.Visible = True
                End If
            End If

            Dim documentosTrabajadorPendiente As DataTable = documento.buscarDocumentoPorAreaPendiente(usuario.getArea, "trabajador", idCarpeta)
            If (documentosTrabajadorPendiente Is Nothing) Then
                sinDocTrabajadorPendiente.Visible = True
            Else
                If (documentosTrabajadorPendiente.Rows.Count > 0) Then
                    Me.gridDocumentosTrabajadorPendiente.DataSource = documentosTrabajadorPendiente
                    Me.gridDocumentosTrabajadorPendiente.DataBind()
                    gridDocumentosTrabajadorPendiente.Columns(3).Visible = False
                Else
                    sinDocTrabajadorPendiente.Visible = True
                End If
            End If

            ''Documentos Vehiculo

            Dim documentosVehiculo As DataTable = documento.buscarDocumentoPorArea(usuario.getArea, "vehiculo", idCarpeta)
            If (documentosVehiculo Is Nothing) Then
                sinDocVehiculoPedido.Visible = True
            Else
                If (documentosVehiculo.Rows.Count > 0) Then
                    Me.gridDocumentosVehiculo.DataSource = documentosVehiculo
                    Me.gridDocumentosVehiculo.DataBind()
                    For Each documentoVehiculo As GridViewRow In gridDocumentosVehiculo.Rows
                        chk = documentoVehiculo.FindControl("chkDocVehiculo")
                        If documentoVehiculo.Cells(2).Text = "espera" Then
                            chk.Checked = True
                        End If
                    Next
                    gridDocumentosVehiculo.Columns(3).Visible = False
                Else
                    sinDocVehiculoPedido.Visible = True
                End If
            End If

            Dim documentosVehiculoPendiente As DataTable = documento.buscarDocumentoPorAreaPendiente(usuario.getArea, "vehiculo", idCarpeta)
            If (documentosVehiculoPendiente Is Nothing) Then
                sinDocVehiculoPendiente.Visible = True
            Else
                If (documentosVehiculoPendiente.Rows.Count > 0) Then
                    Me.gridDocumentosVehiculoPendiente.DataSource = documentosVehiculoPendiente
                    Me.gridDocumentosVehiculoPendiente.DataBind()
                    gridDocumentosVehiculoPendiente.Columns(3).Visible = False
                Else
                    sinDocVehiculoPendiente.Visible = True
                End If
            End If

        End If


    End Sub

    Protected Sub btnPedirDocumento_Click(sender As Object, e As EventArgs) Handles btnPedirDocumento.Click
        Dim documento As New clsDocumento
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim idCarpeta As Integer = decodificarId()
        Dim chk As HtmlInputCheckBox
        Dim listNombreDocumentos As New List(Of String)
        For Each documentoEmpresa As GridViewRow In gridDocumentosEmpresa.Rows
            chk = documentoEmpresa.FindControl("chkDocEmpresa")
            If chk.Checked = True Then 'pasan a espera
                documento.cambiarEstadoDocumento(idCarpeta, documentoEmpresa.Cells(5).Text, documentoEmpresa.Cells(0).Text, "espera", Nothing)
                documento.fechaExpiracionDocumento(idCarpeta, documentoEmpresa.Cells(5).Text, documentoEmpresa.Cells(0).Text, Nothing)
            Else 'sino quedan no solicitados
                documento.cambiarEstadoDocumento(idCarpeta, documentoEmpresa.Cells(5).Text, documentoEmpresa.Cells(0).Text, "no solicitado", Nothing)
                documento.fechaExpiracionDocumento(idCarpeta, documentoEmpresa.Cells(5).Text, documentoEmpresa.Cells(0).Text, Nothing)
            End If
        Next

        For Each documentoEmpresa As GridViewRow In gridDocumentosEmpresaPendientes.Rows
            chk = documentoEmpresa.FindControl("chkDocEmpresa")
            If chk.Checked = True Then 'pasan a espera
                listNombreDocumentos.Add(documentoEmpresa.Cells(1).Text)
                documento.cambiarEstadoDocumento(idCarpeta, documentoEmpresa.Cells(5).Text, documentoEmpresa.Cells(0).Text, "espera", Nothing)
                documento.fechaExpiracionDocumento(idCarpeta, documentoEmpresa.Cells(5).Text, documentoEmpresa.Cells(0).Text, Nothing)
            Else 'sino quedan no solicitados
                documento.cambiarEstadoDocumento(idCarpeta, documentoEmpresa.Cells(5).Text, documentoEmpresa.Cells(0).Text, "no solicitado", Nothing)
                documento.fechaExpiracionDocumento(idCarpeta, documentoEmpresa.Cells(5).Text, documentoEmpresa.Cells(0).Text, Nothing)
            End If
        Next

        For Each documentoTrabajador As GridViewRow In gridDocumentosTrabajador.Rows
            chk = documentoTrabajador.FindControl("chkDocTrabajador")
            If chk.Checked = True Then
                documento.cambiarEstadoDocumento(idCarpeta, documentoTrabajador.Cells(5).Text, documentoTrabajador.Cells(0).Text, "espera", Nothing)
                ''documento.fechaExpiracionDocumento(idCarpeta, usuario.getArea, documentoTrabajador.Cells(0).Text, Nothing)

            Else
                documento.cambiarEstadoDocumento(idCarpeta, documentoTrabajador.Cells(5).Text, documentoTrabajador.Cells(0).Text, "no solicitado", Nothing)
                '' documento.fechaExpiracionDocumento(idCarpeta, usuario.getArea, documentoTrabajador.Cells(0).Text, Nothing)

            End If
        Next

        For Each documentoTrabajador As GridViewRow In gridDocumentosTrabajadorPendiente.Rows
            chk = documentoTrabajador.FindControl("chkDocTrabajador")
            If chk.Checked = True Then
                listNombreDocumentos.Add(documentoTrabajador.Cells(1).Text)
                documento.cambiarEstadoDocumento(idCarpeta, documentoTrabajador.Cells(5).Text, documentoTrabajador.Cells(0).Text, "espera", Nothing)
                ''documento.fechaExpiracionDocumento(idCarpeta, usuario.getArea, documentoTrabajador.Cells(0).Text, Nothing)

            Else
                documento.cambiarEstadoDocumento(idCarpeta, documentoTrabajador.Cells(5).Text, documentoTrabajador.Cells(0).Text, "no solicitado", Nothing)
                '' documento.fechaExpiracionDocumento(idCarpeta, usuario.getArea, documentoTrabajador.Cells(0).Text, Nothing)

            End If
        Next

        For Each documentoVehiculo As GridViewRow In gridDocumentosVehiculo.Rows
            chk = documentoVehiculo.FindControl("chkDocVehiculo")
            If chk.Checked = True Then
                documento.cambiarEstadoDocumento(idCarpeta, documentoVehiculo.Cells(5).Text, documentoVehiculo.Cells(0).Text, "espera", Nothing)
                ''documento.fechaExpiracionDocumento(idCarpeta, usuario.getArea, documentoVehiculo.Cells(0).Text, Nothing)
            Else
                documento.cambiarEstadoDocumento(idCarpeta, documentoVehiculo.Cells(5).Text, documentoVehiculo.Cells(0).Text, "no solicitado", Nothing)
                ''documento.fechaExpiracionDocumento(idCarpeta, usuario.getArea, documentoVehiculo.Cells(0).Text, Nothing)
            End If
        Next

        For Each documentoVehiculo As GridViewRow In gridDocumentosVehiculoPendiente.Rows
            chk = documentoVehiculo.FindControl("chkDocVehiculo")
            If chk.Checked = True Then
                listNombreDocumentos.Add(documentoVehiculo.Cells(1).Text)
                documento.cambiarEstadoDocumento(idCarpeta, documentoVehiculo.Cells(5).Text, documentoVehiculo.Cells(0).Text, "espera", Nothing)
                ''documento.fechaExpiracionDocumento(idCarpeta, usuario.getArea, documentoVehiculo.Cells(0).Text, Nothing)
            Else
                documento.cambiarEstadoDocumento(idCarpeta, documentoVehiculo.Cells(5).Text, documentoVehiculo.Cells(0).Text, "no solicitado", Nothing)
                ''documento.fechaExpiracionDocumento(idCarpeta, usuario.getArea, documentoVehiculo.Cells(0).Text, Nothing)
            End If
        Next

        Dim mensaje As String
        If listNombreDocumentos.Count > 0 Then
            documento.mailListaDocumento(idCarpeta)
        End If


        Response.Redirect(Page.Request.Url.AbsoluteUri)
        ''cargarMenu()
    End Sub
    Private Sub cargarNotificacionesComentarios()

        Dim notificacion As New clsNotificacion
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim tarjeta As String = ""

        Dim listaNotificaciones As Data.DataTable = notificacion.obtenerNotificaciones(rutUsuario)

        If listaNotificaciones.Rows.Count > 0 Then

            For Each fila As DataRow In listaNotificaciones.Rows

                Dim resumenComentario As String = fila("texto")
                Dim nombreUsuarioRespuesta As String = fila("nombreAutor")
                Dim nombreDocumento As String = fila("nombreDocumento")
                Dim contNoLeidos As Integer
                Dim areaComentario As String = fila("areaComentario")
                Dim idDocumento As Integer = fila("idDocumento")
                Dim idCarpeta As Integer = fila("idCarpeta")
                Dim idNotificacion As Integer = fila("idNotificacion")
                Dim idComentario As Integer = fila("idComentario")
                Dim tipo As String = fila("tipo")
                Dim rutDestinatario As String = fila("rutDestinatario")
                Dim rutAutor As String = fila("rutAutor")
                Dim idItem As Integer = fila("idItem")
                Dim nombreEmpresa As String = fila("nombreEmpresa")

                Session("origen") = HttpContext.Current.Request.Url.ToString

                Dim idDocCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idDocumento)
                Dim idDocCodificado As String = System.Convert.ToBase64String(idDocCodificadoBase64)

                Dim idAreaCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(areaComentario)
                Dim idAreaCodificada As String = System.Convert.ToBase64String(idAreaCodificadaBase64)

                Dim idCarpetaCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idCarpeta)
                Dim idCarpetaCodificada As String = System.Convert.ToBase64String(idCarpetaCodificadaBase64)

                Dim rutAutorCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(rutAutor)
                Dim rutAutorCodificado As String = System.Convert.ToBase64String(rutAutorCodificadoBase64)

                Dim rutDestinatarioCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(rutDestinatario)
                Dim rutDestinatarioCodificado As String = System.Convert.ToBase64String(rutDestinatarioCodificadoBase64)

                Dim idComentarioCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idComentario)
                Dim idComentarioCodificado As String = System.Convert.ToBase64String(idComentarioCodificadoBase64)

                Dim idNotificacionCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idNotificacion)
                Dim idNotificacionCodificada As String = System.Convert.ToBase64String(idNotificacionCodificadaBase64)

                Dim tipoCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(tipo)
                Dim tipoCodificado As String = System.Convert.ToBase64String(tipoCodificadoBase64)

                Dim idItemCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idItem)
                Dim idItemCodificado As String = System.Convert.ToBase64String(idItemCodificadoBase64)

                If rutAutor = "ati" Then
                    tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"" href=""../Contratistas/verComentarios_.aspx?i=" + idDocCodificado + "&n=" + idAreaCodificada + "&a=" + idItemCodificado + "&o=" + idCarpetaCodificada + "&p=" + rutAutorCodificado + "&q=" + idComentarioCodificado + "&x=" + tipoCodificado + "&y=" + rutDestinatarioCodificado + "&z=" + idNotificacionCodificada + """>"
                    tarjeta = tarjeta & "       <div class=""dropdown-list-image mr-3"">"
                    tarjeta = tarjeta & "           <img class=""img-profile"" src=""https://upload.wikimedia.org/wikipedia/commons/thumb/3/3b/OOjs_UI_icon_alert-warning.svg/1024px-OOjs_UI_icon_alert-warning.svg.png"" style=""height:40px;width:40px;"">"
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "       <div> "
                    tarjeta = tarjeta & "           <span class=""font-weight-bold"">" + resumenComentario + "</span>"
                    tarjeta = tarjeta & "           <div class=""small text-gray-500"">" + nombreEmpresa + " • " + nombreDocumento + "</div> "
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "   </a> "

                Else

                    tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"" href=""../Contratistas/verComentarios_.aspx?i=" + idDocCodificado + "&n=" + idAreaCodificada + "&a=" + idItemCodificado + "&o=" + idCarpetaCodificada + "&p=" + rutAutorCodificado + "&q=" + idComentarioCodificado + "&x=" + tipoCodificado + "&y=" + rutDestinatarioCodificado + "&z=" + idNotificacionCodificada + """>"
                    tarjeta = tarjeta & "       <div class=""dropdown-list-image mr-3"">"
                    tarjeta = tarjeta & "           <img class=""img-profile rounded-circle"" src=""https://c7.uihere.com/files/25/400/945/computer-icons-industry-business-laborer-industrail-workers-and-engineers-thumb.jpg"" style=""height:40px;width:40px;"">"
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "       <div class=""font-weight-bold""> "
                    tarjeta = tarjeta & "           <div class=""text-truncate"">" + resumenComentario + "</div> "
                    tarjeta = tarjeta & "           <div class=""small text-gray-500"">" + nombreUsuarioRespuesta + "</div> "
                    tarjeta = tarjeta & "           <div class=""small text-gray-500"">" + nombreDocumento + "</div> "
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "   </a> "

                End If

                LblNotificacion.Text = tarjeta

                LblNotificacionComentarios.Text = "!"

                If listaNotificaciones.Rows.Count = 5 Then
                    Exit For
                End If

            Next

        Else

            tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"">"
            tarjeta = tarjeta & "   <div class=""font-weight""> "
            tarjeta = tarjeta & "   <div class=""text""> No tienes notificaciones pendientes </div> "
            tarjeta = tarjeta & "   </div> "
            tarjeta = tarjeta & "   </a> "

            LblNotificacion.Text = tarjeta

        End If

    End Sub
End Class