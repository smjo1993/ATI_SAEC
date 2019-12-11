Public Class verDocumentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMenu.Visible = False
        sinDocumentos.Visible = False
        'btnModalConfirmacion.Visible = False
        If Not Page.IsPostBack Then
            validarUsuario()
            cargarMenu()
            cargarDatos()
        End If
    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "6,3", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../401.aspx")
            End If
        End If
        Dim nombreUsuario As String = Session("nombreUsuario")
        'lblNombreUsuario.Text = nombreUsuario
    End Sub
    Protected Sub cargarMenu()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim idCarpeta As Integer = decodificarId()
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(nombreCodificado)
        Dim nombreDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim menu As New clsMenu
        Dim stringMenu As String = menu.menuUsuarioAtiCarpeta(rutUsuario, idCarpeta, nombreDecodificado)
        lblMenu.Text = stringMenu
        lblMenu.Visible = True
    End Sub
    Protected Function decodificarId() As Integer
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idCodificada)
        Dim idDecodificada As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim idCarpeta As Integer = Convert.ToInt32(idDecodificada)
        Return idCarpeta
    End Function
    Protected Sub cargarDatos()
        lblNombreEmpresa.Visible = True
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(nombreCodificado)
        Dim nombreDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        lblNombreEmpresa.Text = nombreDecodificado
        Dim idCarpeta As Integer = decodificarId()

        Dim documento As New clsDocumento
        Dim documentosEmpresa As DataTable = documento.documentosEmpresaParaRevisar(idCarpeta, Session("usuario").getArea())

        If documentosEmpresa Is Nothing Then
            sinDocumentos.Visible = True
        Else
            If documentosEmpresa.Rows.Count > 0 Then
                gridDocumentos.DataSource = documentosEmpresa
                gridDocumentos.DataBind()
            Else
                sinDocumentos.Visible = True
            End If
        End If
    End Sub
    Protected Sub gridDocumentos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridDocumentos.RowCommand

        If (e.CommandName = "Desaprobar") Then

            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            'Dim nombreDocumento As String = gridDocumentos.Rows(pos).Cells(1).Text
            'Dim areaDocumento As String = gridDocumentos.Rows(pos).Cells(3).Text

            'Session("nombreDocumento") = nombreDocumento
            'Session("idDocumento") = Convert.ToInt32(gridDocumentos.Rows(pos).Cells(0).Text)

            'Response.Redirect("modificarDcto.aspx")
        End If


    End Sub

End Class