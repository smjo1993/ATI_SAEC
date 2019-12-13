﻿Public Class subirDocumentosVehiculo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then
            Return
        End If

        cargarMenu()
        cargarGrid()

    End Sub

    Function cargarGrid()
        'Se pobla la grilla con los datos obtenidos anteriormente
        Dim vehiculo = New clsVehiculo()
        Dim listaDocumentosVehiculo As DataTable = vehiculo.listarDocumentosVehiculo(Session("idVehiculo"), Session("rutContratista"))
        gridListarDocumentosVehiculo.DataSource = listaDocumentosVehiculo
        gridListarDocumentosVehiculo.DataBind()
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
End Class