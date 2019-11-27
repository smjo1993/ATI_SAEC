Public Class confirmarDocumentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Return
        End If
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Session("rutUsuario") = usuario.rutUsuario
        Dim idCarpeta As Integer = decodificarId()
        'Dim areaRevisor = 2
        Dim TablaDocumentosEsperaEmpresa As DataTable = crearDocumentos().obtenerDocumentoEstadoAplicaEmpresa(idCarpeta, usuario.areaUsuario)
        Dim TablaDocumentosEsperaTrabajador As DataTable = crearDocumentos().obtenerDocumentoEstadoAplicaTrabajador(idCarpeta, usuario.areaUsuario)
        Dim TablaDocumentosEsperaVehiculo As DataTable = crearDocumentos().obtenerDocumentoEstadoAplicaVehiculo(idCarpeta, usuario.areaUsuario)

        confirmarEmpresa.DataSource = TablaDocumentosEsperaEmpresa
        confirmarTrabajador.DataSource = TablaDocumentosEsperaTrabajador
        confirmarVehiculo.DataSource = TablaDocumentosEsperaVehiculo

        confirmarEmpresa.DataBind()
        confirmarTrabajador.DataBind()
        confirmarVehiculo.DataBind()
    End Sub
    Protected Function decodificarId() As Integer
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idCodificada)
        Dim idDecodificada As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim idCarpeta As Integer = Convert.ToInt32(idDecodificada)
        Return idCarpeta
    End Function

    Public Function crearDocumentos() As Object

        Dim documentosEspera = New clsDocumento()

        Return documentosEspera

    End Function


    Protected Sub btnConfirmarDocumentos_Click(sender As Object, e As EventArgs) Handles btnConfirmarDocumentos.Click
        Dim contador As Integer = 0
        Dim dt As DataTable = New DataTable("CambioEstado")

        'Se recorre cada checkbox generado 
        For Each fila As GridViewRow In confirmarEmpresa.Rows

            Dim idCarpeta As Integer = fila.Cells(2).Text
            Dim idDocumento As Integer = fila.Cells(3).Text
            Dim idArea As Integer = fila.Cells(4).Text
            Dim check As HtmlInputCheckBox
            Dim actualizarEstado = New clsDocumento()

            check = fila.FindControl("chk")

            'Si está check
            If check.Checked Then
                'Cambia el estado del documento a "pendiente"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "pendiente", Nothing)
            Else
                'Cambia el estado del documento a "no solicitado"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no solicitado", Nothing)
            End If

        Next

        For Each fila As GridViewRow In confirmarTrabajador.Rows

            Dim idCarpeta As Integer = fila.Cells(2).Text
            Dim idDocumento As Integer = fila.Cells(3).Text
            Dim idArea As Integer = fila.Cells(4).Text
            Dim check As HtmlInputCheckBox
            Dim actualizarEstado = New clsDocumento()

            check = fila.FindControl("chk")

            'Si está check
            If check.Checked Then
                'Cambia el estado del documento a "pendiente"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "pendiente", Nothing)
            Else
                'Cambia el estado del documento a "no solicitado"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no solicitado", Nothing)
            End If
        Next

        For Each fila As GridViewRow In confirmarVehiculo.Rows

            Dim idCarpeta As Integer = fila.Cells(2).Text
            Dim idDocumento As Integer = fila.Cells(3).Text
            Dim idArea As Integer = fila.Cells(4).Text
            Dim check As HtmlInputCheckBox
            Dim actualizarEstado = New clsDocumento()

            check = fila.FindControl("chk")

            'Si está check
            If check.Checked Then
                'Cambia el estado del documento a "pendiente"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "pendiente", Nothing)
            Else
                'Cambia el estado del documento a "no solicitado"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no solicitado", Nothing)
            End If

        Next
    End Sub

    Protected Sub documentosEmpresa_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles confirmarEmpresa.RowCommand

        If (e.CommandName = "Ver") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim areaId As String = confirmarEmpresa.Rows(pos).Cells(4).Text
            Dim docuemntoId As String = confirmarEmpresa.Rows(pos).Cells(3).Text
            Dim carpetaId As String = confirmarEmpresa.Rows(pos).Cells(2).Text
            Session("areaId") = areaId
            Session("docuemntoId") = docuemntoId
            Session("carpetaId") = carpetaId

            Session("origen") = HttpContext.Current.Request.Url.ToString()
            Response.Redirect("../Contratistas/verComentarios.aspx")
        End If


    End Sub
End Class