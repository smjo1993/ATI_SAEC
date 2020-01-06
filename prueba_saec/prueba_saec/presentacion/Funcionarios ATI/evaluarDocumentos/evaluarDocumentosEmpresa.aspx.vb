﻿Imports System.Drawing
Public Class verDocumentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        sinDocumentos.Visible = False
        sinDocPendientes.Visible = False
        lblMensaje.Text = ""
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarDatos()
            cargarBotones()
            cargarNotificacionesComentarios()
            Dim usuario As clsUsuarioSAEC = Session("usuario")
            Session("rutUsuario") = usuario.getRut
        End If
    End Sub
    Protected Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Session.Contents.RemoveAll()
        Response.Redirect("../../login.aspx")
    End Sub
    Protected Sub cargarBotones()
        Dim boton As String
        Dim texto As String = "Documentos Trabajador"
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        boton = boton & "<a href=""https://localhost:44310/presentacion/Funcionarios%20ATI/evaluarDocumentos/listarTrabajadores.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn shadow-sm btn-success"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosTrabajdor.Text = boton
        texto = "Documentos Vehiculo"
        boton = ""
        boton = boton & "<a href=""https://localhost:44310/presentacion/Funcionarios%20ATI/evaluarDocumentos/listarVehiculos.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn shadow-sm btn-success"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosVehiculo.Text = boton
    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "5,3", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../../401.aspx")
            End If
        End If
        Dim nombreUsuario As String = Session("nombreUsuario")
        'lblNombreUsuario.Text = nombreUsuario
        LblNombreUsuario.Text = usuario.getNombre().ToString()
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
    Protected Sub cargarDatos()
        lblNombreEmpresa.Visible = True
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(nombreCodificado)
        Dim nombreDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        lblNombreEmpresa.Text = nombreDecodificado
        lblNombreEmpresa2.Text = nombreDecodificado
        Dim idCarpeta As Integer = decodificarId()

        Dim documento As New clsDocumento
        Dim documentosEmpresa As DataTable = documento.documentosEmpresaParaRevisar(idCarpeta, Session("usuario").getArea())

        If documentosEmpresa Is Nothing Then
            sinDocumentos.Visible = True
        Else
            If documentosEmpresa.Rows.Count > 0 Then
                gridDocumentos.DataSource = documentosEmpresa
                gridDocumentos.DataBind()
            Else
                sinDocumentos.Visible = True
            End If
        End If

        Dim documentosEmpresaPendientes As DataTable = documento.documentosEmpresaPendientes(idCarpeta, Session("usuario").getArea())

        If documentosEmpresaPendientes Is Nothing Then
            sinDocPendientes.Visible = True
        Else
            If documentosEmpresaPendientes.Rows.Count > 0 Then
                gridDocumentosPendientes.DataSource = documentosEmpresaPendientes
                gridDocumentosPendientes.DataBind()
            Else
                sinDocPendientes.Visible = True
            End If
        End If

    End Sub
    Protected Sub gridDocumentos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridDocumentos.RowCommand

        If (e.CommandName = "Ver") Then
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim ruta As String = gridDocumentos.Rows(pos).Cells(6).Text
            Dim nombreArchivo As String = gridDocumentos.Rows(pos).Cells(4).Text
            Dim extension As String = ExtraerExtension(ruta, ".")
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

        If (e.CommandName = "Aprobar") Then
            Dim carpeta As New clsCarpetaArranque
            Dim fechaExpiracionCarpeta As Date = carpeta.obtenerFechaExpiracion(decodificarId())
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim txtFecha As TextBox = Me.gridDocumentos.Rows(pos).Cells(8).Controls(1)
            'txtFecha = Me.gridDocumentos.Rows(pos).Cells(10).Controls(1)
            If txtFecha.Text = "" Then
                Dim documento As New clsDocumento
                documento.cambiarEstadoDocumento(Convert.ToInt32(gridDocumentos.Rows(pos).Cells(0).Text), Convert.ToInt32(gridDocumentos.Rows(pos).Cells(2).Text), Convert.ToInt32(gridDocumentos.Rows(pos).Cells(1).Text), "aprobado", gridDocumentos.Rows(pos).Cells(6).Text)
                documento.fechaExpiracionDocumento(Convert.ToInt32(gridDocumentos.Rows(pos).Cells(0).Text), Convert.ToInt32(gridDocumentos.Rows(pos).Cells(2).Text), Convert.ToInt32(gridDocumentos.Rows(pos).Cells(1).Text), fechaExpiracionCarpeta)
            Else
                Dim alerta As New clsAlertas
                Dim fechaExpiracion As Date = Convert.ToDateTime(txtFecha.Text)
                Dim documento As New clsDocumento
                Dim hoy As Date = Today
                If (DateTime.Compare(fechaExpiracion, hoy) < 0 Or DateTime.Compare(fechaExpiracion, fechaExpiracionCarpeta) > 0 Or DateTime.Compare(hoy, fechaExpiracion) = 0) Then
                    lblMensaje.Text = alerta.alerta("ALERTA", "error con la fecha")
                Else
                    documento.cambiarEstadoDocumento(Convert.ToInt32(gridDocumentos.Rows(pos).Cells(0).Text), Convert.ToInt32(gridDocumentos.Rows(pos).Cells(2).Text), Convert.ToInt32(gridDocumentos.Rows(pos).Cells(1).Text), "aprobado", "")
                    documento.fechaExpiracionDocumento(Convert.ToInt32(gridDocumentos.Rows(pos).Cells(0).Text), Convert.ToInt32(gridDocumentos.Rows(pos).Cells(2).Text), Convert.ToInt32(gridDocumentos.Rows(pos).Cells(1).Text), fechaExpiracion)
                End If

            End If



        End If

        If (e.CommandName = "Reprobar") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            My.Computer.FileSystem.DeleteFile(gridDocumentos.Rows(pos).Cells(6).Text)
            Dim documento As New clsDocumento
            documento.cambiarEstadoDocumento(Convert.ToInt32(gridDocumentos.Rows(pos).Cells(0).Text), Convert.ToInt32(gridDocumentos.Rows(pos).Cells(2).Text), Convert.ToInt32(gridDocumentos.Rows(pos).Cells(1).Text), "pendiente", "")

        End If

        If (e.CommandName = "verComentarios") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idArea As Integer = gridDocumentos.Rows(pos).Cells(5).Text
            Dim idDocumento As Integer = gridDocumentos.Rows(pos).Cells(4).Text
            Dim idCarpeta As Integer = gridDocumentos.Rows(pos).Cells(3).Text

            Session("areaId") = idArea
            Session("documentoId") = idDocumento
            Session("carpetaId") = idCarpeta
            Session("rutUsuario") = Session("contratistaEntrante").getRut
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("../verComentarios.aspx")
        End If

        Response.Redirect(HttpContext.Current.Request.Url.ToString)

    End Sub

    Function ExtraerExtension(Path As String, Caracter As String) As String

        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        ExtraerExtension = ret

    End Function
    Protected Sub gridDocumentos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridDocumentos.RowDataBound

        If e.Row.Cells(5).Text = "aprobado" Then

            e.Row.BackColor = Color.FromArgb(222, 249, 241)

        End If

    End Sub

    Protected Sub gridDocumentosPendientes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridDocumentosPendientes.RowCommand

        If (e.CommandName = "verComentarios") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim areaId As String = Session("usuario").getArea().ToString
            Dim documentoId As String = gridDocumentosPendientes.Rows(pos).Cells(1).Text
            Dim carpetaId As String = gridDocumentosPendientes.Rows(pos).Cells(0).Text
            Session("areaId") = areaId
            Session("documentoId") = documentoId
            Session("carpetaId") = carpetaId
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("../../Contratistas/verComentarios.aspx")
        End If

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

                Dim rutAutor As String = fila("rutAutor")

                Session("origen") = HttpContext.Current.Request.Url.ToString

                Dim idDocCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idDocumento)
                Dim idDocCodificado As String = System.Convert.ToBase64String(idDocCodificadoBase64)

                Dim idAreaCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(areaComentario)
                Dim idAreaCodificada As String = System.Convert.ToBase64String(idAreaCodificadaBase64)

                Dim idCarpetaCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idCarpeta)
                Dim idCarpetaCodificada As String = System.Convert.ToBase64String(idCarpetaCodificadaBase64)

                Dim RutCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(rutAutor)
                Dim RutCodificado As String = System.Convert.ToBase64String(RutCodificadoBase64)

                Dim idComentarioCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idComentario)
                Dim idComentarioCodificado As String = System.Convert.ToBase64String(idComentarioCodificadoBase64)

                Dim tipoCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(tipo)
                Dim tipoCodificado As String = System.Convert.ToBase64String(tipoCodificadoBase64)

                tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"" href=""../Contratistas/verComentarios_.aspx?i=" + idDocCodificado + "&n=" + idAreaCodificada + "&o=" + idCarpetaCodificada + "&p=" + RutCodificado + "&q=" + idComentarioCodificado + "&q=" + tipoCodificado + """>"
                tarjeta = tarjeta & "       <div class=""dropdown-list-image mr-3"">"
                tarjeta = tarjeta & "           <img class=""img-profile rounded-circle"" src=""https://c7.uihere.com/files/25/400/945/computer-icons-industry-business-laborer-industrail-workers-and-engineers-thumb.jpg"" style=""height:40px;width:40px;"">"
                tarjeta = tarjeta & "       </div> "
                tarjeta = tarjeta & "       <div class=""font-weight-bold""> "
                tarjeta = tarjeta & "           <div class=""text-truncate"">" + resumenComentario + "</div> "
                tarjeta = tarjeta & "           <div class=""small text-gray-500"">" + nombreUsuarioRespuesta + "</div> "
                tarjeta = tarjeta & "           <div class=""small text-gray-500"">" + nombreDocumento + "</div> "
                tarjeta = tarjeta & "       </div> "
                tarjeta = tarjeta & "   </a> "

                'contNoLeidos = contNoLeidos + 1
                'LblNotificacionComentarios.Text = contNoLeidos.ToString()

                LblNotificacion.Text = tarjeta

                LblNotificacionComentarios.Text = " ! "

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