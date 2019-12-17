Imports System.Drawing

Public Class subirDocumentosEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinDocumentos.Visible = False
        validarContratista()
        cargarMenu()
        cargarBotones()
        If IsPostBack Then
            Return
        End If

        Dim rutContratista As String = Session("contratistaEntrante").getRut
        Dim TablaDocumentosPendienteEmpresa As DataTable = crearDocumentos().obtenerDocumentosEstadoPendienteEmpresa(rutContratista)

        If TablaDocumentosPendienteEmpresa Is Nothing Then
            sinDocumentos.Visible = True
        Else
            If TablaDocumentosPendienteEmpresa.Rows.Count > 0 Then

                gridSubirDocumentosEmpresa.DataSource = TablaDocumentosPendienteEmpresa
                gridSubirDocumentosEmpresa.DataBind()
            Else
                sinDocumentos.Visible = True
            End If
        End If

    End Sub
    Protected Sub cargarBotones()
        Dim boton As String
        Dim texto As String = "Documentos Trabajador"
        boton = boton & "<a href=""https://localhost:44310/presentacion/Contratistas/subirDocumentos/listarTrabajadores.aspx "" Class=""btn shadow-sm btn-success"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosTrabajador.Text = boton
        texto = "Documentos Vehiculo"
        boton = ""
        boton = boton & "<a href=""https://localhost:44310/presentacion/Contratistas/subirDocumentos/listarVehiculos.aspx"" Class=""btn shadow-sm btn-success"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosVehiculo.Text = boton
    End Sub

    Protected Sub validarContratista()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        If (contratista Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(contratista.getRut, "61,1", "C")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
        End If

    End Sub
    Public Function crearDocumentos() As Object

        Dim documentosEspera = New clsDocumento()

        Return documentosEspera

    End Function

    Protected Sub cargarMenu()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuContratistaCarpeta(rutContratista, 0, "")
        lblMenu.Text = stringMenu
        lblMenu.Visible = True

    End Sub
    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


    '    'Se recorre la grilla para subirlos documentos del cliente EMPRESA
    '    For Each fila As GridViewRow In gridSubirDocumentosEmpresa.Rows

    '        'Variables para identificar las propiedades del documento y el archivo
    '        Dim idCarpeta As Integer = fila.Cells(3).Text
    '        Dim idDocumento As Integer = fila.Cells(4).Text
    '        Dim idArea As Integer = fila.Cells(5).Text
    '        Dim nombreArchivo As String = fila.Cells(0).Text
    '        Dim rutEmpresa As String = fila.Cells(7).Text
    '        Dim archivo As HtmlInputFile

    '        archivo = fila.FindControl("fileArchivo")

    '        'Si encuentra el archivo subido lo guarda en la ruta especifica
    '        If archivo.PostedFile.FileName <> "" Then

    '            My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Empresa/"))
    '            Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Empresa/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
    '            archivo.PostedFile.SaveAs(ruta)
    '            Dim documento = New clsDocumento()
    '            documento.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "enviado", ruta)

    '        End If


    '    Next




    'End Sub

    Protected Sub documentosEmpresa_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridSubirDocumentosEmpresa.RowCommand

        If (e.CommandName = "subir") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim idCarpeta As Integer = gridSubirDocumentosEmpresa.Rows(pos).Cells(3).Text
            Dim idDocumento As Integer = gridSubirDocumentosEmpresa.Rows(pos).Cells(4).Text
            Dim idArea As Integer = gridSubirDocumentosEmpresa.Rows(pos).Cells(5).Text
            Dim nombreArchivo As String = gridSubirDocumentosEmpresa.Rows(pos).Cells(0).Text
            Dim rutEmpresa As String = gridSubirDocumentosEmpresa.Rows(pos).Cells(7).Text

            Dim archivo As HtmlInputFile
            archivo = gridSubirDocumentosEmpresa.Rows(pos).FindControl("fileArchivo")

            'Si encuentra el archivo subido lo guarda en la ruta especifica
            If archivo.PostedFile.FileName <> "" Then

                My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Empresa/"))
                Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Empresa/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                archivo.PostedFile.SaveAs(ruta)
                Dim documento = New clsDocumento()

                documento.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "enviado", ruta)
                Response.Redirect(HttpContext.Current.Request.Url.ToString)


            End If

        End If

    End Sub

    Protected Sub gridSubirDocumentosEmpresa_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridSubirDocumentosEmpresa.RowDataBound

        If e.Row.Cells(2).Text = "aprobado" Then

            e.Row.BackColor = Color.FromArgb(222, 249, 241)

        End If

        If e.Row.Cells(2).Text = "pendiente" Then

            e.Row.BackColor = Color.FromArgb(255, 240, 240)

        End If

        If e.Row.Cells(2).Text = "enviado" Then

            e.Row.BackColor = Color.FromArgb(255, 252, 231)

        End If
    End Sub

End Class