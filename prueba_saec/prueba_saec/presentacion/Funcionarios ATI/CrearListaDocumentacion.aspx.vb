Imports System.Windows.Controls

Public Class CrearListaDocumentacion
    Inherits System.Web.UI.Page
    Dim cantidadDocumentosEmpresa As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        empresas.Visible = False
        vehiculos.Visible = False
        trabajadores.Visible = False
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarDatos()
        End If
    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        End If
    End Sub
    Private Sub cargarDatos()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim documento As New clsDocumento
        Dim idCarpeta As Integer = Convert.ToInt32(Request.QueryString("idCarpeta").ToString())
        Dim documentos As String = " "

        Dim documentosEmpresa As DataTable = documento.buscarDocumentoPorArea(usuario.areaUsuario, "empresa", idCarpeta)
        If (documentosEmpresa Is Nothing) Then
            seccionEmpresa.Visible = False
        Else
            If (documentosEmpresa.Rows.Count > 0) Then

                gridDocumentosEmpresa.Columns(0).HeaderText = "fdhgf"
                gridDocumentosEmpresa.Columns(1).HeaderText = "adfasdfasdfas"
                gridDocumentosEmpresa.Columns(1).HeaderText = "5876"

                For Each documentoEmpresa As DataRow In documentosEmpresa.Rows
                    'Dim item As New ListItem()

                    'item.Text = documentoEmpresa("nombre").ToString()
                    'item.Value = documentoEmpresa("TB_SAEC_Documentoid")
                    'If (documentoEmpresa("estado") = "espera") Then
                    '    item.Selected = True
                    'End If

                    'chkDocumentosEmpresa.Items.Add(item)

                    '    documentos = documentos & "   <div class=""wrapper""> "
                    '    documentos = documentos & "   <div class=""row""> "
                    '    documentos = documentos & "      <label id="" lb" + cantidadDocumentosEmpresa.ToString + """ hidden> " + documentoEmpresa("TB_SAEC_Documentoid").ToString + " </label>"
                    '    documentos = documentos & "      <li class="""">" + documentoEmpresa("nombre") + ""
                    '    documentos = documentos & "          <label class=""switch"">"
                    '    If (documentoEmpresa("estado") = "inactivo") Then
                    '        documentos = documentos & "              <input runat=""server"" id=""" + cantidadDocumentosEmpresa.ToString + """ type=""checkbox"" class=""default"">"
                    '    Else
                    '        documentos = documentos & "              <input runat=""server"" id=""" + cantidadDocumentosEmpresa.ToString + """ type=""checkbox"" class=""default"" checked>"
                    '    End If
                    '    documentos = documentos & "              <span class=""slider round""></span>"
                    '    documentos = documentos & "          </label>"
                    '    documentos = documentos & "      </li>"
                    '    documentos = documentos & "   </div> "
                    '    documentos = documentos & "   </div> "
                    '    cantidadDocumentosEmpresa = +1
                Next

                'empresas.Text = documentos
                'empresas.Visible = True



            End If
        End If

        Dim documentosTrabajador As DataTable = documento.buscarDocumentoPorArea(usuario.areaUsuario, "trabajador", idCarpeta)
        If (documentosTrabajador Is Nothing) Then
            seccionTrabajador.Visible = False
        Else
            If (documentosTrabajador.Rows.Count > 0) Then
                trabajadores.Visible = True
                documentos = " "
                For Each documentoTrabajador As DataRow In documentosTrabajador.Rows
                    documentos = documentos & "   <div class=""wrapper""> "
                    documentos = documentos & "      <li class=""list-group-item"">" + documentoTrabajador("nombre") + ""
                    documentos = documentos & "          <label class=""switch"">"
                    If (documentoTrabajador("estado") = "inactivo") Then
                        documentos = documentos & "              <input runat=""server"" type=""checkbox"" class=""default"">"
                    Else
                        documentos = documentos & "              <input runat=""server"" type=""checkbox"" class=""default"" checked>"
                    End If
                    documentos = documentos & "              <span class=""slider round""></span>"
                    documentos = documentos & "          </label>"
                    documentos = documentos & "      </li>"
                    documentos = documentos & "   </div> "
                Next
                trabajadores.Text = documentos
            End If
        End If

        Dim documentosVehiculo As DataTable = documento.buscarDocumentoPorArea(usuario.areaUsuario, "vehiculo", idCarpeta)
        If (documentosVehiculo Is Nothing) Then
            seccionVehiculo.Visible = False
        Else
            If (documentosVehiculo.Rows.Count > 0) Then
                vehiculos.Visible = True
                documentos = " "
                For Each documentoVehiculo As DataRow In documentosTrabajador.Rows
                    documentos = documentos & "   <div class=""wrapper""> "
                    documentos = documentos & "      <li class=""list-group-item"">" + documentoVehiculo("nombre") + ""
                    documentos = documentos & "          <label class=""switch"">"
                    If (documentoVehiculo("estado") = "inactivo") Then
                        documentos = documentos & "              <input runat=""server"" type=""checkbox"" class=""default"">"
                    Else
                        documentos = documentos & "              <input runat=""server"" type=""checkbox"" class=""default"" checked>"
                    End If
                    documentos = documentos & "              <span class=""slider round""></span>"
                    documentos = documentos & "          </label>"
                    documentos = documentos & "      </li>"
                    documentos = documentos & "   </div> "
                Next
                vehiculos.Text = documentos
            End If
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim c As New HtmlInputCheckBox
        'Dim l As New Label
        'For documento As Integer = 0 To cantidadDocumentosEmpresa - 1
        '    'c = Page.FindControl(documento.ToString())
        '    'If (c.Checked) Then
        '    '    vehiculos.Visible = True
        '    'Else
        '    '    vehiculos.Visible = False
        '    'End If
        '    l = Page.FindControl("lb" + documento.ToString())
        'Next
    End Sub
End Class