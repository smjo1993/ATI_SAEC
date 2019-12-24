Public Class listarTrabajadores1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sinTrabajadores.Visible = False
        If IsPostBack Then
            Return
        End If
        validarUsuario()
        cargarMenu()
        cargarBotones()
        Dim trabajadores = New clsTrabajador()
        Dim idCarpeta As Integer = decodificarId()
        Dim idArea As Integer = Session("usuario").getArea()
        Dim TablaTrabajadores As DataTable = trabajadores.listarTrabajadoresParaEvaluar(idCarpeta, idArea)


        If TablaTrabajadores Is Nothing Then
            sinTrabajadores.Visible = True
        Else
            If TablaTrabajadores.Rows.Count > 0 Then
                gridListarTrabajadoresParaEvaluar.DataSource = TablaTrabajadores
                gridListarTrabajadoresParaEvaluar.DataBind()
            Else
                sinTrabajadores.Visible = True
            End If
        End If

    End Sub
    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../../login.aspx")
        Else
            Dim menu As New clsMenu
            Dim acceso As String = menu.validarAcceso(usuario.getRut, "5,4", "A")

            If acceso = "I" Or acceso Is Nothing Then
                Response.Redirect("../../401.aspx")
            End If
        End If
        Dim nombreUsuario As String = Session("nombreUsuario")
        'lblNombreUsuario.Text = nombreUsuario
    End Sub
    Protected Sub cargarBotones()
        Dim boton As String
        Dim texto As String = "Documentos Empresa"
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim nombreCodificado As String = Request.QueryString("n").ToString()
        boton = boton & "<a href=""https://localhost:44310/presentacion/Funcionarios%20ATI/evaluarDocumentos/evaluarDocumentosEmpresa.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn shadow-sm btn-success"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosEmpresa.Text = boton
        texto = "Documentos Vehiculo"
        boton = ""
        boton = boton & "<a href=""https://localhost:44310/presentacion/Funcionarios%20ATI/evaluarDocumentos/listarVehiculos.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn shadow-sm btn-success"" style=""float: Right();"">"
        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosVehiculo.Text = boton
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
    Protected Sub btnIrTrabajador_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridListarTrabajadoresParaEvaluar.RowCommand

        If (e.CommandName = "ir") Then
            'Se obtienen los datos de la columna de la grid para mandar el datatable a la otra vista
            Dim pos As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim rutTrabajador As String = gridListarTrabajadoresParaEvaluar.Rows(pos).Cells(0).Text
            Dim idCarpeta As Integer = gridListarTrabajadoresParaEvaluar.Rows(pos).Cells(2).Text
            Dim idTrabajador As Integer = gridListarTrabajadoresParaEvaluar.Rows(pos).Cells(3).Text

            Session("rutTrabajador") = rutTrabajador
            Session("idTrabajador") = idTrabajador
            Session("idCarpeta") = idCarpeta

            'Session("rutContratista") = rutContratista
            'Response.Redirect("evaluarDocumentosTrabajador.aspx")

            'Se codifica idCarpeta para enviarlo por URL

            Dim idCarpetaURL As String = Request.QueryString("i").ToString()
            Dim nombreCarpetaURL As String = Request.QueryString("n").ToString()
            'Session("idCodificada") = idCarpetaURL
            'Session("nombreCodificado") = nombreCarpetaURL
            Response.Redirect("evaluarDocumentosTrabajador.aspx?i=" + idCarpetaURL + "&n=" + nombreCarpetaURL + " ")

        End If

    End Sub
End Class