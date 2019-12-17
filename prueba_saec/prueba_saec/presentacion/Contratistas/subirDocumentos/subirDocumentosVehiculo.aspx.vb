Imports System.Drawing

Public Class subirDocumentosVehiculo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinDocumentos.Visible = False
        validarContratista()
        cargarBotones()
        If IsPostBack Then
            Return
        End If

        cargarMenu()
        cargarGrid()

    End Sub
    Protected Sub cargarBotones()
        Dim boton As String
        Dim texto As String = "Documentos Empresa"
        boton = boton & "<a href=""https://localhost:44310/presentacion/Contratistas/subirDocumentos/subirDocumentosEmpresa.aspx "" Class=""btn shadow-sm btn-success"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosEmpresa.Text = boton
        texto = "Documentos Trabajador"
        boton = ""
        boton = boton & "<a href=""https://localhost:44310/presentacion/Contratistas/subirDocumentos/listarTrabajadores.aspx"" Class=""btn shadow-sm btn-success"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosTrabajador.Text = boton
    End Sub
    Protected Sub validarContratista()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        If (contratista Is Nothing) Then
            Response.Redirect("../../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(contratista.getRut, "61,3", "C")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../../401.aspx")
            End If
        End If

    End Sub
    Function cargarGrid()
        'Se pobla la grilla con los datos obtenidos anteriormente
        Dim vehiculo = New clsVehiculo()
        Dim listaDocumentosVehiculo As DataTable = vehiculo.listarDocumentosVehiculo(Session("idVehiculo"), Session("rutContratista"))

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
    End Function
    Protected Sub cargarMenu()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuContratistaCarpeta(rutContratista, 0, "")
        lblMenu.Text = stringMenu
        lblMenu.Visible = True

    End Sub

    Protected Sub documentosVehiculos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarDocumentosVehiculo.RowCommand

        If (e.CommandName = "subir") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idCarpeta As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(4).Text
            Dim idDocumento As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(5).Text
            Dim idArea As Integer = gridListarDocumentosVehiculo.Rows(pos).Cells(6).Text
            Dim nombreArchivo As String = gridListarDocumentosVehiculo.Rows(pos).Cells(1).Text
            Dim rutEmpresa As String = gridListarDocumentosVehiculo.Rows(pos).Cells(8).Text
            Dim patente As String = gridListarDocumentosVehiculo.Rows(pos).Cells(0).Text
            Dim idVehiculo As String = gridListarDocumentosVehiculo.Rows(pos).Cells(9).Text



            Dim archivo As HtmlInputFile
            archivo = gridListarDocumentosVehiculo.Rows(pos).FindControl("fileArchivo")

            'Si encuentra el archivo subido lo guarda en la ruta especifica
            If archivo.PostedFile.FileName <> "" Then


                If gridListarDocumentosVehiculo.Rows(pos).Cells(10).Text = "" Or gridListarDocumentosVehiculo.Rows(pos).Cells(10).Text = "&nbsp;" Then

                    'Si el contratista no ha subido un archivo anteriormente 
                    My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Vehiculo/" + patente))
                    Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Vehiculo/" + patente + "/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                    archivo.PostedFile.SaveAs(ruta)
                    Dim documento = New clsDocumento()
                    documento.cambiarEstadoDocumentoVehiculo(idCarpeta, idArea, idDocumento, idVehiculo, "enviado", ruta)
                    Response.Redirect(HttpContext.Current.Request.Url.ToString)

                Else

                    'Si el contratista subio un documento previamente, se elimina y se sube el nuevo archivo.
                    My.Computer.FileSystem.DeleteFile(gridListarDocumentosVehiculo.Rows(pos).Cells(10).Text)
                    My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Vehiculo/" + patente))
                    Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Vehiculo/" + patente + "/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                    archivo.PostedFile.SaveAs(ruta)
                    Dim documento = New clsDocumento()
                    documento.cambiarEstadoDocumentoVehiculo(idCarpeta, idArea, idDocumento, idVehiculo, "enviado", ruta)
                    Response.Redirect(HttpContext.Current.Request.Url.ToString)

                End If




            End If

        End If

    End Sub

    Protected Sub gridListarDocumentosVehiculo_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridListarDocumentosVehiculo.RowDataBound

        If e.Row.Cells(3).Text = "aprobado" Then

            e.Row.BackColor = Color.FromArgb(222, 249, 241)

        End If


        If e.Row.Cells(3).Text = "pendiente" Then

            e.Row.BackColor = Color.FromArgb(255, 240, 240)

        End If

        If e.Row.Cells(3).Text = "enviado" Then

            e.Row.BackColor = Color.FromArgb(255, 252, 231)

        End If
    End Sub
End Class