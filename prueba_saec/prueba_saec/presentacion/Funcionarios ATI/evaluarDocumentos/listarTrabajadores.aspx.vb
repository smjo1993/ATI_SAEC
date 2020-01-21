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
        cargarNotificacionesComentarios()
        'se carga la gridview
        Dim trabajadores = New clsTrabajador()
        Dim idCarpeta As Integer = decodificarId()
        Dim idArea As Integer = Session("usuario").getArea()

        Dim listaRoles As List(Of clsRol) = New List(Of clsRol)
        listaRoles = Session("roles")

        If listaRoles(0).getId = 1 Then

            Dim TablaTrabajadores As DataTable = trabajadores.listarTrabajadoresParaEvaluarAdmin(idCarpeta, Nothing)

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

        Else

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


        End If



    End Sub
    Protected Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Session.Contents.RemoveAll()
        Response.Redirect("../../login.aspx")
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
        LblNombreUsuario.Text = usuario.getNombre().ToString()
    End Sub
    Protected Sub cargarBotones()
        Dim boton As String
        Dim texto As String = "Documentos Empresa"
        Dim idCodificada As String = Request.QueryString("i").ToString()
        Dim nombreCodificado As String = Request.QueryString("n").ToString()

        boton = boton & "<a href=""https://localhost:44310/presentacion/Funcionarios%20ATI/evaluarDocumentos/evaluarDocumentosEmpresa.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn shadow-sm btn-success"" style=""float: Right();"">"

        ''boton = boton & "<a href=""https://www.atiport.cl/sandbox/saec/presentacion/Funcionarios%20ATI/evaluarDocumentos/evaluarDocumentosEmpresa.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn shadow-sm btn-success"" style=""float: Right();"">"


        boton = boton & "<i class=""""></i>" + texto + "</a>"
        lblDocumentosEmpresa.Text = boton
        texto = "Documentos Vehiculo"
        boton = ""

        boton = boton & "<a href=""https://localhost:44310/presentacion/Funcionarios%20ATI/evaluarDocumentos/listarVehiculos.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn shadow-sm btn-success"" style=""float: Right();"">"

        ''boton = boton & "<a href=""https://www.atiport.cl/sandbox/saec/presentacion/Funcionarios%20ATI/evaluarDocumentos/listarVehiculos.aspx?i=" + idCodificada + "&n=" + nombreCodificado + """ Class=""btn shadow-sm btn-success"" style=""float: Right();"">"

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
    Private Sub cargarNotificacionesComentarios()

        Dim notificacion As New clsNotificacion
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        Dim rutUsuario As String = usuario.getRut
        Dim tarjeta As String = ""

        Dim listaNotificaciones As Data.DataTable = notificacion.obtenerNotificaciones(rutUsuario)

        If listaNotificaciones.Rows.Count > 0 Then

            For Each fila As DataRow In listaNotificaciones.Rows

                Dim resumenComentario As String = fila("texto")
                Dim nombreUsuarioRespuesta As String = fila("nombreAutor")
                Dim nombreDocumento As String = fila("nombreDocumento")
                Dim areaComentario As String = fila("areaComentario")
                Dim idDocumento As Integer = fila("idDocumento")
                Dim idCarpeta As Integer = fila("idCarpeta")
                Dim idNotificacion As Integer = fila("idNotificacion")
                Dim idComentario As Integer = fila("idComentario")
                Dim tipo As String = fila("tipo")
                Dim rutDestinatario As String = fila("rutDestinatario")
                Dim rutAutor As String = fila("rutAutor")
                Dim idItem As Integer = fila("idItem")
                Dim nombreEmpresa As String = fila("nombreEmpresa")

                Session("origen") = HttpContext.Current.Request.Url.ToString

                Dim idDocCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idDocumento)
                Dim idDocCodificado As String = System.Convert.ToBase64String(idDocCodificadoBase64)

                Dim idAreaCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(areaComentario)
                Dim idAreaCodificada As String = System.Convert.ToBase64String(idAreaCodificadaBase64)

                Dim idCarpetaCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idCarpeta)
                Dim idCarpetaCodificada As String = System.Convert.ToBase64String(idCarpetaCodificadaBase64)

                Dim rutAutorCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(rutAutor)
                Dim rutAutorCodificado As String = System.Convert.ToBase64String(rutAutorCodificadoBase64)

                Dim rutDestinatarioCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(rutDestinatario)
                Dim rutDestinatarioCodificado As String = System.Convert.ToBase64String(rutDestinatarioCodificadoBase64)

                Dim idComentarioCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idComentario)
                Dim idComentarioCodificado As String = System.Convert.ToBase64String(idComentarioCodificadoBase64)

                Dim idNotificacionCodificadaBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idNotificacion)
                Dim idNotificacionCodificada As String = System.Convert.ToBase64String(idNotificacionCodificadaBase64)

                Dim tipoCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(tipo)
                Dim tipoCodificado As String = System.Convert.ToBase64String(tipoCodificadoBase64)

                Dim idItemCodificadoBase64 As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(idItem)
                Dim idItemCodificado As String = System.Convert.ToBase64String(idItemCodificadoBase64)

                If rutAutor = "ati" Then
                    tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"" href=""../../Contratistas/verComentarios_.aspx?i=" + idDocCodificado + "&n=" + idAreaCodificada + "&a=" + idItemCodificado + "&o=" + idCarpetaCodificada + "&p=" + rutAutorCodificado + "&q=" + idComentarioCodificado + "&x=" + tipoCodificado + "&y=" + rutDestinatarioCodificado + "&z=" + idNotificacionCodificada + """>"
                    tarjeta = tarjeta & "       <div class=""dropdown-list-image mr-3"">"
                    tarjeta = tarjeta & "           <img class=""img-profile"" src=""https://upload.wikimedia.org/wikipedia/commons/thumb/3/3b/OOjs_UI_icon_alert-warning.svg/1024px-OOjs_UI_icon_alert-warning.svg.png"" style=""height:40px;width:40px;"">"
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "       <div> "
                    tarjeta = tarjeta & "           <span class=""font-weight-bold"">" + resumenComentario + "</span>"
                    tarjeta = tarjeta & "           <div class=""small text-gray-500"">" + nombreEmpresa + " • " + nombreDocumento + "</div> "
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "   </a> "

                Else

                    tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"" href=""../../Contratistas/verComentarios_.aspx?i=" + idDocCodificado + "&n=" + idAreaCodificada + "&a=" + idItemCodificado + "&o=" + idCarpetaCodificada + "&p=" + rutAutorCodificado + "&q=" + idComentarioCodificado + "&x=" + tipoCodificado + "&y=" + rutDestinatarioCodificado + "&z=" + idNotificacionCodificada + """>"
                    tarjeta = tarjeta & "       <div class=""dropdown-list-image mr-3"">"
                    tarjeta = tarjeta & "           <img class=""img-profile rounded-circle"" src=""https://c7.uihere.com/files/25/400/945/computer-icons-industry-business-laborer-industrail-workers-and-engineers-thumb.jpg"" style=""height:40px;width:40px;"">"
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "       <div class=""font-weight-bold""> "
                    tarjeta = tarjeta & "           <div class=""text-truncate"">" + resumenComentario + "</div> "
                    tarjeta = tarjeta & "           <div class=""small text-gray-500"">" + nombreUsuarioRespuesta + "</div> "
                    tarjeta = tarjeta & "           <div class=""small text-gray-500"">" + nombreDocumento + "</div> "
                    tarjeta = tarjeta & "       </div> "
                    tarjeta = tarjeta & "   </a> "

                End If

                LblNotificacion.Text = tarjeta

                LblNotificacionComentarios.Text = "!"

                If listaNotificaciones.Rows.Count = 5 Then
                    Exit For
                End If

            Next

        Else

            tarjeta = tarjeta & "   <a class=""dropdown-item d-flex align-items-center"">"
            tarjeta = tarjeta & "   <div class=""font-weight""> "
            tarjeta = tarjeta & "   <div class=""text""> No tienes notificaciones pendientes </div> "
            tarjeta = tarjeta & "   </div> "
            tarjeta = tarjeta & "   </a> "

            LblNotificacion.Text = tarjeta

        End If

    End Sub
End Class