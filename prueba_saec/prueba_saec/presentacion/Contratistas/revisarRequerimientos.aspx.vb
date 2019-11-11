Public Class revisarRequerimientos
    Inherits System.Web.UI.Page

    Private listaDocumentosEspera As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim rutContratista As String = "8660229"
        Dim listaDocumentosEspera As DataTable = crearDocumentos().obtenerDocumentoEstadoEspera(rutContratista)
        Me.listaDocumentosEspera = listaDocumentosEspera

        Dim documento As String = ""
        Dim idChk As Integer = 1

        For Each fila As DataRow In listaDocumentosEspera.Rows


            documento = documento & "    <tr> "
            documento = documento & "        <td> " + fila("nombre") + "</td> "
            documento = documento & "        <td> " + fila("nombre") + "</td> "
            documento = documento & "        <td> " + switchDocumentos(idChk) + "</td> "
            documento = documento & "        <td> Edinburgh</td> "
            documento = documento & "    </tr> "

            idChk = idChk + 1

            'prueba
            'Dim item As New ListItem()
            'item.Text = fila("nombre").ToString()
            'item.Value = primaryKeys
            'chkDocumentos.Items.Add(item)
            '----
        Next


        LblDocumentos.Text = documento

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

        Dim idChk As Integer = 1
        Dim chek As checkbox



        For Each fila As DataRow In listaDocumentosEspera.Rows

            chek = Page.FindControl("chk" + idChk.ToString)

            If (chek.Checked) Then
                Dim asd As String
            End If

        Next

    End Sub

End Class