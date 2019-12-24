Public Class VehiculoHistorico1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        sinDocumentos.Visible = False
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarDatos()
            cargarBotonVolver()
        End If
    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")

        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "5,6", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If

        End If
    End Sub
    Protected Sub cargarBotonVolver()
        Dim boton As String
        Dim texto As String = "Volver"
        Dim idCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(Session("idCarpeta"))
        Dim idCodificada As String = System.Convert.ToBase64String(idCodificadaBase64)
        Dim nombreCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(Session("nombreEmpresa"))
        Dim nombreCodificado As String = System.Convert.ToBase64String(nombreCodificadoBase64)
        Dim idAntiguaCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(Session("idCarpetaHistorico"))
        Dim idAntiguaCodificada As String = System.Convert.ToBase64String(idAntiguaCodificadaBase64)
        boton = boton & "<a href=""https://localhost:44310/presentacion/Funcionarios%20ATI/historicoCarpeta.aspx?i=" + idCodificada + "&n=" + nombreCodificado + "&ia=" + idAntiguaCodificada + """ Class=""btn btn-secondary"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblVolver.Text = boton
    End Sub
    Protected Sub cargarMenu()
        Dim empresa As New clsEmpresa
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim idCarpeta As Integer = Session("idCarpeta")
        Dim nombreDecodificado As String = Session("nombreEmpresa")

        Dim menu As New clsMenu
        Dim stringMenu As String

        If idCarpeta = "0" Then
            stringMenu = menu.menuUsuarioAtiInicio(rutUsuario)
        Else
            stringMenu = menu.menuUsuarioAtiCarpeta(rutUsuario, idCarpeta, nombreDecodificado)
        End If
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub
    Protected Sub cargarDatos()
        Dim vehiculo = New clsVehiculo()
        Dim listaDocumentosVehiculo As DataTable = vehiculo.listarDocumentosVehiculoHistorico(Session("idVehiculo"), Session("idCarpetaHistorico"))

        If listaDocumentosVehiculo Is Nothing Then
            sinDocumentos.Visible = True
        Else
            If listaDocumentosVehiculo.Rows.Count > 0 Then
                gridListarDocumentosVehiculo.DataSource = listaDocumentosVehiculo
                gridListarDocumentosVehiculo.DataBind()
            Else
                sinDocumentos.Visible = True
            End If
        End If


        lblVehiculo.Text = Session("patente")
    End Sub
    Protected Sub btnVerDocumento_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarDocumentosVehiculo.RowCommand

        Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
        Dim ruta As String = gridListarDocumentosVehiculo.Rows(pos).Cells(9).Text
        Dim nombreArchivo As String = gridListarDocumentosVehiculo.Rows(pos).Cells(1).Text
        Dim idCarpeta As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(4).Text
        Dim idDocumento As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(5).Text
        Dim idArea As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(6).Text
        Dim idVehiculo As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(8).Text
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
        If (e.CommandName = "verComentarios") Then

            Session("areaId") = idArea
            Session("documentoId") = idDocumento
            Session("carpetaId") = idCarpeta
            Session("vehiculoId") = idVehiculo
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Session("verComentarios") = "I"
            Response.Redirect("../Funcionarios ATI/verComentariosVehiculo.aspx")

        End If
    End Sub
    Function ExtraerExtension(Path As String, Caracter As String) As String

        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        ExtraerExtension = ret

    End Function
End Class