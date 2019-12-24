Imports System.Drawing

Public Class evaluarDocumentosTrabajador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinDocumentos.Visible = False
        sinDocPendientes.Visible = False
        validarUsuario()
        cargarMenu()
        cargarBotonVolver()
        If IsPostBack Then
            Return
        End If

        Dim trabajador = New clsTrabajador()
        Dim idCarpeta As Integer = decodificarId()
        Dim idArea As Integer = Session("usuario").getArea()
        Dim idTrabajador As Integer = Session("idTrabajador")
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Session("rutUsuario") = usuario.getRut
        Dim tablaDocumentosTrabajador = trabajador.listarDocumentosTrabajadorParaRevisar(idCarpeta, idArea, idTrabajador)

        If tablaDocumentosTrabajador Is Nothing Then
            sinDocumentos.Visible = True
        Else
            If tablaDocumentosTrabajador.Rows.Count > 0 Then
                gridListarDocumentosTrabajador.DataSource = tablaDocumentosTrabajador
                gridListarDocumentosTrabajador.DataBind()
            Else
                sinDocumentos.Visible = True
            End If
        End If

        Dim tablaDocumentosTrabajadorPentdientes = trabajador.ListarDocumentosPendientesTrabajadorRevisor(idCarpeta, idArea, idTrabajador)

        If tablaDocumentosTrabajadorPentdientes Is Nothing Then
            sinDocPendientes.Visible = True
        Else
            If tablaDocumentosTrabajadorPentdientes.Rows.Count > 0 Then
                gridDocumentosPendiente.DataSource = tablaDocumentosTrabajadorPentdientes
                gridDocumentosPendiente.DataBind()

            Else
                sinDocPendientes.Visible = True
            End If
        End If





        lblTrabajador.Text = Session("rutTrabajador")


    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "5,4", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../../401.aspx")
            End If
        End If
        Dim nombreUsuario As String = Session("nombreUsuario")
        'lblNombreUsuario.Text = nombreUsuario
    End Sub
    Protected Sub cargarBotonVolver()
        Dim boton As String
        Dim texto As String = "Volver"
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        boton = boton & "<a href=""https://localhost:44310/presentacion/Funcionarios%20ATI/evaluarDocumentos/listarTrabajadores.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn btn-secondary"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblVolver.Text = boton
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

    Protected Sub btnVerDocumento_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarDocumentosTrabajador.RowCommand

        Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
        Dim ruta As String = gridListarDocumentosTrabajador.Rows(pos).Cells(10).Text
        Dim idCarpeta As Integer = gridListarDocumentosTrabajador.Rows(pos).Cells(5).Text
        Dim idDocumento As Integer = gridListarDocumentosTrabajador.Rows(pos).Cells(6).Text
        Dim idArea As Integer = gridListarDocumentosTrabajador.Rows(pos).Cells(7).Text
        Dim idTrabajador As Integer = gridListarDocumentosTrabajador.Rows(pos).Cells(9).Text
        Dim nombreArchivo As String = gridListarDocumentosTrabajador.Rows(pos).Cells(2).Text
        Dim txtFecha As TextBox = Me.gridListarDocumentosTrabajador.Rows(pos).Cells(12).Controls(1)
        Dim extension As String = ExtraerExtension(ruta, ".")

        If (e.CommandName = "Ver") Then

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

            Dim Trabajador As New clsTrabajador
            Dim documento As New clsDocumento
            Dim carpeta As New clsCarpetaArranque
            Dim fechaExpiracionCarpeta As Date = carpeta.obtenerFechaExpiracion(decodificarId())

            If txtFecha.Text = "" Then

                documento.cambiarEstadoDocumentoTrabajador(idCarpeta, idArea, idDocumento, idTrabajador, "aprobado", ruta)
                Trabajador.fechaExpiracionDocumentoTrabajador(idCarpeta, idArea, idDocumento, idTrabajador, fechaExpiracionCarpeta)
                Response.Redirect(HttpContext.Current.Request.Url.ToString)

            Else

                Dim alerta As New clsAlertas
                Dim fechaExpiracion As Date = Convert.ToDateTime(txtFecha.Text)
                Dim hoy As Date = Today

                If (DateTime.Compare(fechaExpiracion, hoy) < 0 Or DateTime.Compare(fechaExpiracion, fechaExpiracionCarpeta) > 0 Or DateTime.Compare(hoy, fechaExpiracion) = 0) Then

                    lblMensaje.Text = alerta.alerta("ALERTA", "error con la fecha")

                Else

                    documento.cambiarEstadoDocumentoTrabajador(idCarpeta, idArea, idDocumento, idTrabajador, "aprobado", ruta)
                    Trabajador.fechaExpiracionDocumentoTrabajador(idCarpeta, idArea, idDocumento, idTrabajador, fechaExpiracion)
                    Response.Redirect(HttpContext.Current.Request.Url.ToString)

                End If

            End If

        End If

        If (e.CommandName = "Reprobar") Then

            Dim Trabajador As New clsTrabajador
            My.Computer.FileSystem.DeleteFile(ruta)
            Dim documento As New clsDocumento
            documento.cambiarEstadoDocumentoTrabajador(idCarpeta, idArea, idDocumento, idTrabajador, "pendiente", Nothing)
            Trabajador.fechaExpiracionDocumentoTrabajador(idCarpeta, idArea, idDocumento, idTrabajador, Nothing)
            Response.Redirect(HttpContext.Current.Request.Url.ToString)

        End If

        If (e.CommandName = "verComentarios") Then

            Session("areaId") = idArea
            Session("documentoId") = idDocumento
            Session("carpetaId") = idCarpeta
            Session("trabajadorId") = idTrabajador
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("../verComentariosTrabajador.aspx")

        End If

    End Sub

    'funcion que obtiene la extension del archivo
    Function ExtraerExtension(Path As String, Caracter As String) As String

        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        ExtraerExtension = ret

    End Function

    Protected Sub gridListarDocumentosTrabajador_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridListarDocumentosTrabajador.RowDataBound

        If e.Row.Cells(4).Text = "aprobado" Then

            e.Row.BackColor = Color.FromArgb(222, 249, 241)

        End If

    End Sub

    Protected Sub gridDocumentosPendientes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridDocumentosPendiente.RowCommand

        Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
        Dim idCarpeta As Integer = gridDocumentosPendiente.Rows(pos).Cells(4).Text
        Dim idDocumento As Integer = gridDocumentosPendiente.Rows(pos).Cells(5).Text
        Dim idArea As Integer = gridDocumentosPendiente.Rows(pos).Cells(6).Text
        Dim idTrabajador As Integer = gridDocumentosPendiente.Rows(pos).Cells(8).Text

        If (e.CommandName = "verComentarios") Then

            Session("areaId") = idArea
            Session("documentoId") = idDocumento
            Session("carpetaId") = idCarpeta
            Session("trabajadorId") = idTrabajador
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("../verComentariosTrabajador.aspx")

        End If

    End Sub
End Class