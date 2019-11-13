Public Class revisarRequerimientos
    Inherits System.Web.UI.Page

    Private listaDocumentosEspera As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then
            Return
        End If

        Dim rutContratista As String = "8660229"
        Dim TablaDocumentosEspera As DataTable = crearDocumentos().obtenerDocumentoEstadoEspera(rutContratista)

        Me.listaDocumentosEspera = TablaDocumentosEspera

        gridDocumentos.DataSource = TablaDocumentosEspera
        Me.gridDocumentos.Columns(2).Visible = False
        Me.gridDocumentos.Columns(3).Visible = False
        Me.gridDocumentos.Columns(4).Visible = False
        gridDocumentos.DataBind()

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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim idChk As Integer = 0
        Dim dt As DataTable = New DataTable("CambioEstado")

        'Se recorre cada checkbox generado 
        For Each fila As GridViewRow In gridDocumentos.Rows

            Dim check As HtmlInputCheckBox
            check = fila.FindControl("chk")

            If check.Checked Then
                'Cambia el estado del documento a "aplica"
                Dim asd As String = fila.Cells(1).Text
                Dim asds As String = fila.Cells(2).Text
                Dim asdss As String = fila.Cells(3).Text
            Else
                'Cambia el estado del documento a "no aplica"


            End If





        Next




        'idChk = 0
        'Dim chek As GridView
        'Dim idCheckbox As String = "gridDocumentos_chk_" + idChk.ToString
        'chek = Page.FindControl(idcheckbox)

        '    If (chek.Checked) Then
        '        Dim asd As String
        '    End If

        '    idChk = idChk + 1



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
    Public Function cambiarEstado()


    End Function

End Class