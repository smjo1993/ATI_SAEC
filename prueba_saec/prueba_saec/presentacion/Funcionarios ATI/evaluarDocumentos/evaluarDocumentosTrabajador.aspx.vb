﻿Public Class evaluarDocumentosTrabajador
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
        cargarBotones()
    End Sub

    Protected Sub cargarBotones()
        Dim boton As String
        Dim texto As String = "Documentos Empresa"
        Dim idCodificada As String = Session("idCodificada").ToString
        Dim nombreCodificado As String = Session("nombreCodificado").ToString()
        boton = boton & "<a href=""https://localhost:44310/presentacion/Funcionarios%20ATI/evaluarDocumentos/evaluarDocumentosEmpresa.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn shadow-sm btn-success"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosEmpresa.Text = boton
        texto = "Documentos Vehiculo"
        boton = ""
        boton = boton & "<a href=""https://localhost:44310/presentacion/Funcionarios%20ATI/evaluarDocumentos/listarVehiculos.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn shadow-sm btn-success"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosVehiculo.Text = boton
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
        Dim txtFecha As TextBox = Me.gridListarDocumentosTrabajador.Rows(pos).Cells(15).Controls(1)
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

        If (e.CommandName = "aprobar") Then

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

        If (e.CommandName = "reprobar") Then

            My.Computer.FileSystem.DeleteFile(ruta)
            Dim documento As New clsDocumento
            documento.cambiarEstadoDocumentoTrabajador(idCarpeta, idArea, idDocumento, idTrabajador, "pendiente", Nothing)
            Response.Redirect(HttpContext.Current.Request.Url.ToString)

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