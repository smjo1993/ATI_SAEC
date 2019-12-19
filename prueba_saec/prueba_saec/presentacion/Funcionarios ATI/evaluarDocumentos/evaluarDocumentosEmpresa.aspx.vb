Imports System.Drawing
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
            Dim usuario As clsUsuarioSAEC = Session("usuario")
            Session("rutUsuario") = usuario.getRut
        End If
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
            Dim areaId As String = Session("usuario").getArea().ToString
            Dim documentoId As String = gridDocumentos.Rows(pos).Cells(1).Text
            Dim carpetaId As String = gridDocumentos.Rows(pos).Cells(0).Text
            Session("areaId") = areaId
            Session("documentoId") = documentoId
            Session("carpetaId") = carpetaId
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("../../Contratistas/verComentarios.aspx")
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
End Class