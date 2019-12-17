Imports System.Drawing

Public Class evaluarDocumentosVehiculo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinDocumentos.Visible = False
        sinDocPendientes.Visible = False
        If IsPostBack Then
            Return
        End If
        cargarMenu()
        Dim vehiculo = New clsVehiculo()
        Dim idCarpeta As Integer = decodificarId()
        Dim idArea As Integer = Session("usuario").getArea()
        Dim idVehiculo As Integer = Session("idVehiculo")
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Session("rutUsuario") = usuario.getRut
        Dim tablaDocumentosVehiculo = vehiculo.listarDocumentosVehiculoParaRevisar(idCarpeta, idArea, idVehiculo)

        If tablaDocumentosVehiculo Is Nothing Then
            sinDocumentos.Visible = True
        Else
            If tablaDocumentosVehiculo.Rows.Count > 0 Then
                gridListarDocumentosVehiculo.DataSource = tablaDocumentosVehiculo
                gridListarDocumentosVehiculo.DataBind()
            Else
                sinDocumentos.Visible = True
            End If
        End If

        Dim tablaDocumentosPendientesVehiculo = vehiculo.ListarDocumentosPendientesVehiculoRevisor(idCarpeta, idArea, idVehiculo)


        If tablaDocumentosPendientesVehiculo Is Nothing Then
            sinDocPendientes.Visible = True
        Else
            If tablaDocumentosPendientesVehiculo.Rows.Count > 0 Then
                gridDocumentosPendientes.DataSource = tablaDocumentosPendientesVehiculo
                gridDocumentosPendientes.DataBind()

            Else
                sinDocPendientes.Visible = True
            End If
        End If



        lblVehiculo.Text = Session("patente")
        cargarBotones()
    End Sub

    Protected Sub cargarBotones()
        Dim boton As String
        Dim texto As String = "Documentos Trabajador"
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        boton = boton & "<a href=""https://localhost:44310/presentacion/Funcionarios%20ATI/evaluarDocumentos/listarTrabajadores.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn shadow-sm btn-success"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosTrabajdor.Text = boton
        texto = "Documentos Empresa"
        boton = ""
        boton = boton & "<a href=""https://localhost:44310/presentacion/Funcionarios%20ATI/evaluarDocumentos/evaluarDocumentosEmpresa.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn shadow-sm btn-success"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosEmpresa.Text = boton
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
    Protected Sub btnVerDocumento_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarDocumentosVehiculo.RowCommand

        Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
        Dim ruta As String = gridListarDocumentosVehiculo.Rows(pos).Cells(9).Text
        Dim nombreArchivo As String = gridListarDocumentosVehiculo.Rows(pos).Cells(1).Text
        Dim idCarpeta As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(4).Text
        Dim idDocumento As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(5).Text
        Dim idArea As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(6).Text
        Dim idVehiculo As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(8).Text
        Dim txtFecha As TextBox = Me.gridListarDocumentosVehiculo.Rows(pos).Cells(11).Controls(1)
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

        If (e.CommandName = "Aprobar") Then

            Dim vehiculo As New clsVehiculo
            Dim documento As New clsDocumento
            Dim carpeta As New clsCarpetaArranque
            Dim fechaExpiracionCarpeta As Date = carpeta.obtenerFechaExpiracion(decodificarId())

            If txtFecha.Text = "" Then

                documento.cambiarEstadoDocumentoVehiculo(idCarpeta, idArea, idDocumento, idVehiculo, "aprobado", ruta)
                vehiculo.fechaExpiracionDocumentoVehiculo(idCarpeta, idArea, idDocumento, idVehiculo, fechaExpiracionCarpeta)
                Response.Redirect(HttpContext.Current.Request.Url.ToString)

            Else

                Dim alerta As New clsAlertas
                Dim fechaExpiracion As Date = Convert.ToDateTime(txtFecha.Text)
                Dim hoy As Date = Today

                If (DateTime.Compare(fechaExpiracion, hoy) < 0 Or DateTime.Compare(fechaExpiracion, fechaExpiracionCarpeta) > 0 Or DateTime.Compare(hoy, fechaExpiracion) = 0) Then

                    'lblMensaje.Text = alerta.alerta("ALERTA", "error con la fecha")

                Else

                    documento.cambiarEstadoDocumentoVehiculo(idCarpeta, idArea, idDocumento, idVehiculo, "aprobado", ruta)
                    vehiculo.fechaExpiracionDocumentoVehiculo(idCarpeta, idArea, idDocumento, idVehiculo, fechaExpiracion)
                    Response.Redirect(HttpContext.Current.Request.Url.ToString)

                End If

            End If

        End If

        If (e.CommandName = "Reprobar") Then

            Dim documento As New clsDocumento
            Dim vehiculo As New clsVehiculo

            My.Computer.FileSystem.DeleteFile(ruta)
            documento.cambiarEstadoDocumentoVehiculo(idCarpeta, idArea, idDocumento, idVehiculo, "pendiente", Nothing)
            vehiculo.fechaExpiracionDocumentoVehiculo(idCarpeta, idArea, idDocumento, idVehiculo, Nothing)
            Response.Redirect(HttpContext.Current.Request.Url.ToString)

        End If

        If (e.CommandName = "verComentarios") Then

            Session("areaId") = idArea
            Session("documentoId") = idDocumento
            Session("carpetaId") = idCarpeta
            Session("vehiculoId") = idVehiculo
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("../verComentariosVehiculo.aspx")

        End If
    End Sub

    'funcion que obtiene la extension del archivo
    Function ExtraerExtension(Path As String, Caracter As String) As String

        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        ExtraerExtension = ret

    End Function

    Protected Sub gridListarDocumentosVehiculo_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridListarDocumentosVehiculo.RowDataBound

        If e.Row.Cells(3).Text = "aprobado" Then

            e.Row.BackColor = Color.FromArgb(222, 249, 241)

        End If

    End Sub

    Protected Sub gridDocumentosPendientes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridDocumentosPendientes.RowCommand

        Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
        Dim idCarpeta As Integer = gridDocumentosPendientes.Rows(pos).Cells(3).Text
        Dim idDocumento As Integer = gridDocumentosPendientes.Rows(pos).Cells(4).Text
        Dim idArea As Integer = gridDocumentosPendientes.Rows(pos).Cells(5).Text
        Dim idVehiculo As Integer = gridDocumentosPendientes.Rows(pos).Cells(7).Text

        If (e.CommandName = "verComentarios") Then

            Session("areaId") = idArea
            Session("documentoId") = idDocumento
            Session("carpetaId") = idCarpeta
            Session("vehiculoId") = idVehiculo
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("../verComentariosVehiculo.aspx")

        End If

    End Sub
End Class