Public Class subirDocumentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then
            Return
        End If

        Dim rutContratista As String = "8660229"
        Dim TablaDocumentosPendienteEmpresa As DataTable = crearDocumentos().obtenerDocumentosEstadoPendienteEmpresa(rutContratista)
        Dim TablaDocumentosPendienteTrabajador As DataTable = crearDocumentos().obtenerDocumentosEstadoPendienteTrabajador(rutContratista)
        Dim TablaDocumentosPendienteVehiculo As DataTable = crearDocumentos().obtenerDocumentosEstadoPendienteVehiculo(rutContratista)

        subirEmpresa.DataSource = TablaDocumentosPendienteEmpresa
        subirTrabajador.DataSource = TablaDocumentosPendienteTrabajador
        subirVehiculos.DataSource = TablaDocumentosPendienteVehiculo

        subirEmpresa.DataBind()
        subirTrabajador.DataBind()
        subirVehiculos.DataBind()

    End Sub

    Public Function crearDocumentos() As Object

        Dim documentosEspera = New clsDocumento()

        Return documentosEspera

    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        'Se recorre la grilla para subirlos documentos del cliente EMPRESA
        For Each fila As GridViewRow In subirEmpresa.Rows

            'Variables para identificar las propiedades del documento y el archivo
            Dim idCarpeta As Integer = fila.Cells(3).Text
            Dim idDocumento As Integer = fila.Cells(4).Text
            Dim idArea As Integer = fila.Cells(5).Text
            Dim nombreArchivo As String = fila.Cells(0).Text
            Dim rutEmpresa As String = fila.Cells(7).Text
            Dim archivo As HtmlInputFile

            archivo = fila.FindControl("fileArchivo")

            'Si encuentra el archivo subido lo guarda en la ruta especifica
            If archivo.PostedFile.FileName <> "" Then

                My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Empresa/"))
                Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Empresa/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                archivo.PostedFile.SaveAs(ruta)
                Dim documento = New clsDocumento()
                documento.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "enviado", ruta)

            End If


        Next

        For Each fila As GridViewRow In subirTrabajador.Rows

            'Variables para identificar las propiedades del documento y el archivo TRABAJADOR
            Dim idCarpeta As Integer = fila.Cells(5).Text
            Dim idDocumento As Integer = fila.Cells(6).Text
            Dim idArea As Integer = fila.Cells(7).Text
            Dim nombreArchivo As String = fila.Cells(2).Text
            Dim rutEmpresa As String = fila.Cells(9).Text
            Dim rutTrabajador As String = fila.Cells(0).Text
            Dim archivo As HtmlInputFile

            archivo = fila.FindControl("fileArchivo")

            'Si encuentra el archivo subido lo guarda en la ruta especifica
            If archivo.PostedFile.FileName <> "" Then

                My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos trabajadores/" + rutTrabajador))
                Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos trabajadores/" + rutTrabajador + "/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                archivo.PostedFile.SaveAs(ruta)
                Dim documento = New clsDocumento()
                documento.cambiarEstadoDocumentoTrabajador(idCarpeta, idArea, idDocumento, rutTrabajador, "enviado", ruta)

            End If

        Next

        For Each fila As GridViewRow In subirVehiculos.Rows

            'Variables para identificar las propiedades del documento y el archivo TRABAJADOR
            Dim idCarpeta As Integer = fila.Cells(3).Text
            Dim idDocumento As Integer = fila.Cells(4).Text
            Dim idArea As Integer = fila.Cells(5).Text
            Dim nombreArchivo As String = fila.Cells(1).Text
            Dim rutEmpresa As String = fila.Cells(7).Text
            Dim patente As String = fila.Cells(0).Text
            Dim archivo As HtmlInputFile

            archivo = fila.FindControl("fileArchivo")

            'Si encuentra el archivo subido lo guarda en la ruta especifica
            If archivo.PostedFile.FileName <> "" Then

                My.Computer.FileSystem.CreateDirectory(Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Vehiculo/" + patente))
                Dim ruta = Server.MapPath("/Carpetas Arranque/" + rutEmpresa + "/documentos Vehiculo/" + patente + "/" + nombreArchivo + "." + archivo.PostedFile.FileName.Split(".")(1))
                archivo.PostedFile.SaveAs(ruta)
                Dim documento = New clsDocumento()
                documento.cambiarEstadoDocumentoVehiculo(idCarpeta, idArea, idDocumento, patente, "enviado", ruta)



            End If


        Next


    End Sub
End Class