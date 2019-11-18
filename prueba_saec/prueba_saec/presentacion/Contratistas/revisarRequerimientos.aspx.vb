Public Class revisarRequerimientos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then
            Return
        End If

        Dim rutContratista As String = "8660229"
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

        Dim contador As Integer = 0
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
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "aplica")
            Else
                'Cambia el estado del documento a "no aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no aplica")
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
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "aplica")
            Else
                'Cambia el estado del documento a "no aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no aplica")
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
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "aplica")
            Else
                'Cambia el estado del documento a "no aplica"
                actualizarEstado.cambiarEstadoDocumento(idCarpeta, idArea, idDocumento, "no aplica")
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

End Class