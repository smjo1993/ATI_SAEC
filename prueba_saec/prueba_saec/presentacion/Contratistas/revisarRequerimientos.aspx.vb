Public Class revisarRequerimientos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then
            Return
        End If
        validarContratista()
        cargarMenu()
        cargarGrid()
    End Sub

    Protected Sub validarContratista()
        Dim contratista As clsContratista = Session("contratistaEntrante")
        If (contratista Is Nothing) Then
            Response.Redirect("../login.aspx")
        End If
    End Sub

    Protected Sub cargarMenu()
        Dim contratista As clsContratista = Session("contratistaEntrante")
        Dim rutContratista As String = contratista.getRut
        'Dim idCarpeta As Integer = decodificarId()
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuContratistaCarpeta(rutContratista, 0, "")
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub

    Protected Sub cargarGrid()

        Dim rutContratista As String = Session("contratistaEntrante").getRut()
        Session("rutUsuario") = Session("contratistaEntrante").getRut()
        Dim TablaDocumentosEsperaEmpresa As DataTable = crearDocumentos().obtenerDocumentoEstadoEsperaEmpresa(rutContratista)
        Dim TablaDocumentosEsperaTrabajador As DataTable = crearDocumentos().obtenerDocumentoEstadoEsperaTrabajador(rutContratista)
        Dim TablaDocumentosEsperaVehiculo As DataTable = crearDocumentos().obtenerDocumentoEstadoEsperaVehiculo(rutContratista)

        documentosEmpresa.DataSource = TablaDocumentosEsperaEmpresa
        documentosTrabajador.DataSource = TablaDocumentosEsperaTrabajador
        documentosVehiculo.DataSource = TablaDocumentosEsperaVehiculo

        documentosEmpresa.DataBind()
        documentosTrabajador.DataBind()
        documentosVehiculo.DataBind()

        'For Each fila As DataRow In listaDocumentosEspera.Rows


        '    documento = documento & "    <tr> "
        '    documento = documento & "        <td> " + fila("nombre") + "</td> "
        '    documento = documento & "        <td> " + fila("nombre") + "</td> "
        '    documento = documento & "        <td> " + switchDocumentos(idChk) + "</td> "
        '    documento = documento & "        <td> Edinburgh</td> "
        '    documento = documento & "    </tr> "

        '    idChk = idChk + 1

        '    'prueba
        '    'Dim item As New ListItem()
        '    'item.Text = fila("nombre").ToString()
        '    'item.Value = primaryKeys
        '    'chkDocumentos.Items.Add(item)
        '    '----
        'Next
    End Sub
    Public Function switchDocumentos(id As Integer) As String

        Dim seleccion As String = ""
        ' seleccion = seleccion & "    <label class=""switch"">"
        seleccion = seleccion & "       < asp : CheckBox id = chk" + id.ToString + " runat=""server"" />"

        'seleccion = seleccion & "        <input runat=""server"" type=""checkbox"" class=""default"" id =chk" + id.ToString + ">"
        'seleccion = seleccion & "        <span class=""slider round""></span>"
        'seleccion = seleccion & "     </label>"

        Return seleccion

    End Function

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
        'idChk = 0
        'Dim chek As GridView
        'Dim idCheckbox As String = "gridDocumentos_chk_" + idChk.ToString
        'chek = Page.FindControl(idcheckbox)

        '    If (chek.Checked) Then
        '        Dim asd As String
        '    End If





        '    idChk = 0










        'Dim idChk As Integer = 1
        'Dim chek As checkbox



        'For Each fila As DataRow In listaDocumentosEspera.Rows

        '    chek = Page.FindControl("chk" + idChk.ToString)

        '    If (chek.Checked) Then
        '        Dim asd As String
        '    End If

        'Next

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
            Session("origen") = "revisarRequerimientos.aspx"
            Response.Redirect("verComentarios.aspx")
        End If


    End Sub

End Class