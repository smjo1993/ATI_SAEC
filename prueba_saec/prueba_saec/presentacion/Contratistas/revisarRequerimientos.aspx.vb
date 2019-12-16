Public Class revisarRequerimientos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinDocEmpresa.Visible = False
        sinDocTrabajador.Visible = False
        sinDocVehiculo.Visible = False
        validarContratista()
        cargarMenu()
        If IsPostBack Then
            Return
        End If
        cargarGrid()

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

    Protected Sub cargarMenu()

        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuContratistaCarpeta(rutContratista, 0, "")
        lblMenu.Text = stringMenu
        lblMenu.Visible = True

    End Sub

    Protected Sub cargarGrid()

        Dim rutContratista As String = Session("contratistaEntrante").getRut()
        Session("rutUsuario") = Session("contratistaEntrante").getRut()
        Dim TablaDocumentosEsperaEmpresa As DataTable = crearDocumentos().obtenerDocumentoEstadoEsperaEmpresa(rutContratista)

        If (TablaDocumentosEsperaEmpresa Is Nothing) Then
            sinDocEmpresa.Visible = True
        Else
            If (TablaDocumentosEsperaEmpresa.Rows.Count > 0) Then
                documentosEmpresa.DataSource = TablaDocumentosEsperaEmpresa
                documentosEmpresa.DataBind()
            Else
                sinDocEmpresa.Visible = True
            End If
        End If

        Dim TablaDocumentosEsperaTrabajador As DataTable = crearDocumentos().obtenerDocumentoEstadoEsperaTrabajador(rutContratista)

        If (TablaDocumentosEsperaTrabajador Is Nothing) Then
            sinDocEmpresa.Visible = True
        Else
            If (TablaDocumentosEsperaTrabajador.Rows.Count > 0) Then
                documentosTrabajador.DataSource = TablaDocumentosEsperaTrabajador
                documentosTrabajador.DataBind()
            Else
                sinDocTrabajador.Visible = True
            End If
        End If

        Dim TablaDocumentosEsperaVehiculo As DataTable = crearDocumentos().obtenerDocumentoEstadoEsperaVehiculo(rutContratista)

        If (TablaDocumentosEsperaVehiculo Is Nothing) Then
            sinDocEmpresa.Visible = True
        Else
            If (TablaDocumentosEsperaVehiculo.Rows.Count > 0) Then
                documentosVehiculo.DataSource = TablaDocumentosEsperaVehiculo
                documentosVehiculo.DataBind()
            Else
                sinDocVehiculo.Visible = True
            End If
        End If



    End Sub


    Public Function crearDocumentos() As Object

        Dim documentosEspera = New clsDocumento()

        Return documentosEspera

    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles confirmarPinponeo.Click

        Dim dt As DataTable = New DataTable("CambioEstado")

        'Se recorre cada checkbox generado 
        For Each fila As GridViewRow In documentosEmpresa.Rows

            Dim idCarpeta As Integer = fila.Cells(2).Text
            Dim idDocumento As Integer = fila.Cells(3).Text
            Dim idArea As Integer = fila.Cells(4).Text
            Dim check As HtmlInputCheckBox
            Dim actualizarEstado = New clsDocumento()

            check = fila.FindControl("chk")

            'Si está check
            If check.Checked Then
                'Cambia el estado del documento a "aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "aplica", Nothing)
            Else
                'Cambia el estado del documento a "no aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no aplica", Nothing)
            End If

        Next

        For Each fila As GridViewRow In documentosTrabajador.Rows

            Dim idCarpeta As Integer = fila.Cells(2).Text
            Dim idDocumento As Integer = fila.Cells(3).Text
            Dim idArea As Integer = fila.Cells(4).Text
            Dim check As HtmlInputCheckBox
            Dim actualizarEstado = New clsDocumento()

            check = fila.FindControl("chk")

            'Si está check
            If check.Checked Then
                'Cambia el estado del documento a "aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "aplica", Nothing)
            Else
                'Cambia el estado del documento a "no aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no aplica", Nothing)
            End If
        Next

        For Each fila As GridViewRow In documentosVehiculo.Rows

            Dim idCarpeta As Integer = fila.Cells(2).Text
            Dim idDocumento As Integer = fila.Cells(3).Text
            Dim idArea As Integer = fila.Cells(4).Text
            Dim check As HtmlInputCheckBox
            Dim actualizarEstado = New clsDocumento()

            check = fila.FindControl("chk")

            'Si está check
            If check.Checked Then
                'Cambia el estado del documento a "aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "aplica", Nothing)
            Else
                'Cambia el estado del documento a "no aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no aplica", Nothing)
            End If

        Next

    End Sub

    Protected Sub documentosEmpresa_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles documentosEmpresa.RowCommand

        If (e.CommandName = "Ver") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim areaId As String = documentosEmpresa.Rows(pos).Cells(4).Text
            Dim docuemntoId As String = documentosEmpresa.Rows(pos).Cells(3).Text
            Dim carpetaId As String = documentosEmpresa.Rows(pos).Cells(2).Text
            Session("areaId") = areaId
            Session("docuemntoId") = docuemntoId
            Session("carpetaId") = carpetaId
            Session("origen") = HttpContext.Current.Request.Url.ToString
            Response.Redirect("verComentarios.aspx")

        End If


    End Sub

End Class