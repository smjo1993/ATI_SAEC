Imports System.Data
Imports System.Data.SqlClient
Public Class verComentarios_
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim notificacion As New clsNotificacion

        If Not Page.IsPostBack Then
            'Session("areaId") = decodificarIdArea()
            'Session("docuemntoId") = decodificarIdDocumento()
            'Session("carpetaId") = decodificarIdCarpeta()
            'Session("rutUsuario") = decodificarRutAutor()

            'notificacion.actualizarEstado(decodificarIdNotificacion())
            notificacion.actualizarEstado(decodificarIdComentario(), decodificarTipo())

            cargarComentarios()
            cargarMenu()
            lblDocumento.Text = cargarNombreDocumento(Session("documentoId"))
        End If
    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles BtnVolver.Click
        Response.Redirect(Session("origen"))
    End Sub



    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        'Aquí va el la inserción a la BD del comentario
        Dim comentario As New clsComentario
        Dim accion As Boolean
        'Dim fecha As Date
        'fecha = DateTime.Now
        accion = comentario.insertarComentario(Session("rutUsuario"), TxtAreaNuevoComentario.Value, Convert.ToInt32(Session("areaId")), Convert.ToInt32(Session("documentoId")), Convert.ToInt32(Session("carpetaId")))
        If accion = True Then
            Response.Redirect("verComentarios_.aspx")
            lblPrueba.InnerText = "Inserción realizada con éxito"
        Else
            lblPrueba.InnerText = "La inserción ha fallado. Por favor inténtelo de nuevo"
        End If
    End Sub

    Public Function listarComentarios(areaId As Integer, documentoId As Integer, carpetaArranqueId As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_listarComentarios '" & areaId & "' , '" & documentoId & "' , '" & carpetaArranqueId & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "listarComentarios")
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Protected Function obtenerNombreAutor(rut As String) As String
        Dim contratista As New clsContratista
        Dim usuarioSaec As New clsUsuarioSAEC
        Dim nombre As String
        Dim dt As DataTable
        Dim dr As DataRow
        dt = contratista.obtenerContratista(rut)
        If dt.Rows.Count > 0 Then
            dr = dt.Rows.Item(0)
            nombre = dr("nombre")
            Return nombre
        Else
            dt = usuarioSaec.validarUsuarioSAEC(rut)
            If dt.Rows.Count > 0 Then
                dr = dt.Rows.Item(0)
                nombre = dr("nombre")
                Return nombre
            Else
                Return "Usuario no encontrado"
            End If
        End If
    End Function

    Protected Function obtenerRolAutor(rut As String) As String
        Dim contratista As New clsContratista
        Dim usuarioSaec As New clsUsuarioSAEC
        Dim rol As String
        Dim dt As DataTable
        Dim dr As DataRow
        dt = contratista.obtenerContratista(rut)
        If dt.Rows.Count > 0 Then
            rol = "Contratista"
            Return rol
        Else
            dt = usuarioSaec.validarUsuarioSAEC(rut)
            If dt.Rows.Count > 0 Then
                dt = usuarioSaec.obtenerNombreRol(rut)
                dr = dt.Rows.Item(0)
                rol = dr("nombre")
                Return rol
            Else
                Return "Usuario no encontrado"
            End If
        End If
    End Function

    Protected Sub cargarMenu()
        Dim menu As New clsMenu
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If usuario Is Nothing Then
            Dim contratista As clsContratista = Session("contratistaEntrante")
            Dim rutContratista As String = contratista.getRut()
            'Dim idCarpeta As Integer = decodificarId()
            Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutContratista)
            lblMenu.Text = stringMenu
            lblMenu.Visible = True
        Else
            Dim rutUsuario As String = usuario.getRut()
            'Dim idCarpeta As Integer = decodificarId()

            Dim stringMenu As String = menu.menuUsuarioAtiInicio(rutUsuario)
            lblMenu.Text = stringMenu
            lblMenu.Visible = True
        End If

    End Sub

    Protected Function decodificarIdDocumento() As Integer
        Dim idDocCodificado As String = Request.QueryString("i").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idDocCodificado)
        Dim idDocDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim documentoId As Integer = Convert.ToInt32(idDocDecodificado)
        Return documentoId
    End Function

    Protected Function decodificarIdArea() As Integer
        Dim idAreaCodificada As String = Request.QueryString("n").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idAreaCodificada)
        Dim idAreaDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim areaId As Integer = Convert.ToInt32(idAreaDecodificado)
        Return areaId
    End Function

    Protected Function decodificarIdCarpeta() As Integer
        Dim idCarpetaCodificada As String = Request.QueryString("o").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idCarpetaCodificada)
        Dim idCarpetaDecodificada As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim carpetaId As Integer = Convert.ToInt32(idCarpetaDecodificada)
        Return carpetaId
    End Function

    Protected Function decodificarRutAutor() As Integer
        Dim rutAutorCodificado As String = Request.QueryString("p").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(rutAutorCodificado)
        Dim rutAutorDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim rutAutor As Integer = Convert.ToInt32(rutAutorDecodificado)
        Return rutAutor
    End Function

    Protected Function decodificarRutDestinatario() As Integer
        Dim rutDestinatarioCodificado As String = Request.QueryString("y").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(rutDestinatarioCodificado)
        Dim rutDestinatarioDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim rutDestinatario As Integer = Convert.ToInt32(rutDestinatarioDecodificado)
        Return rutDestinatario
    End Function

    Protected Function decodificarIdNotificacion() As Integer
        Dim idNotificacionCodificado As String = Request.QueryString("z").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idNotificacionCodificado)
        Dim idNotificacionDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim idNotificacion As Integer = Convert.ToInt32(idNotificacionDecodificado)
        Return idNotificacion
    End Function

    Protected Function decodificarIdComentario() As Integer
        Dim idComentarioCodificado As String = Request.QueryString("q").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(idComentarioCodificado)
        Dim idComentarioDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim idComentario As Integer = Convert.ToInt32(idComentarioDecodificado)
        Return idComentario
    End Function

    Protected Function decodificarTipo() As Integer
        Dim tipoCodificado As String = Request.QueryString("x").ToString()
        Dim data() As Byte = System.Convert.FromBase64String(tipoCodificado)
        Dim tipoDecodificado As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Dim tipo As Integer = Convert.ToInt32(tipoDecodificado)
        Return tipo
    End Function

    'genera el string para la generación de cards de comentarios
    Protected Sub cargarComentarios()

        Dim areaId As String = decodificarIdArea()
        Dim documentoId As String = decodificarIdDocumento()
        Dim carpetaId As String = decodificarIdCarpeta()
        Dim rutUsuario As String = decodificarRutAutor()
        Dim tipo As String = decodificarTipo()
        'Dim rutUsuario As String = decodificarRutDestinatario()

        Dim idNotificacion As Integer = decodificarIdNotificacion()

        Dim comentario As New clsComentario
        Dim tarjeta As String = ""

        Dim listaComentarios As DataTable = comentario.obtenerComentarios(areaId, docuemntoId, carpetaId)

        'Ordenando la lista de comentarios por fecha
        Dim datav As New DataView
        datav = listaComentarios.DefaultView
        datav.Sort = "fecha"
        listaComentarios = datav.ToTable()

        'Dim Empresas As Object = crearEmpresas()

        ' Ciclo for que recorre la lista de comentarios 
        For Each fila As DataRow In listaComentarios.Rows

            If fila("rutAutor") = rutUsuario Then
                Dim nombre As String
                Dim rol As String
                Dim mensaje As String
                'Dim idComentario As Integer

                nombre = obtenerNombreAutor(fila("rutAutor"))
                rol = obtenerRolAutor(fila("rutAutor"))
                mensaje = fila("texto")
                'idComentario = fila("id")

                tarjeta = tarjeta & "  <div class=""row"">"
                tarjeta = tarjeta & "   <div class=""col-2""></div>"
                tarjeta = tarjeta & "   <div class=""col-10"">"
                tarjeta = tarjeta & "    <div Class=""card shadow mb-4""> "
                tarjeta = tarjeta & "         <div Class=""card-header"">"
                tarjeta = tarjeta & "           <div class=""row"">"
                tarjeta = tarjeta & "               <div class=""m-0 font-weight-bold text-primary col-6"" >" + nombre + "/" + rol + "</div>"
                tarjeta = tarjeta & "               <p class=""d-none d-lg-inline text-grey-600 small col-6 text-right"" >" + fila("fecha") + "</p>"
                tarjeta = tarjeta & "           </div>"
                tarjeta = tarjeta & "         </div>"
                tarjeta = tarjeta & "         <div Class=""card-body"">"
                tarjeta = tarjeta & "           <div>" + mensaje + "</div>"
                tarjeta = tarjeta & "         </div> "
                tarjeta = tarjeta & "      </div> "
                tarjeta = tarjeta & "     </div> "
                tarjeta = tarjeta & "    </div> "

                lblTarjetaComentario.Text = tarjeta

                'notificacion.actualizarEstado(fila("id"))

            Else
                tarjeta = tarjeta & "  <div class=""col-10"">"
                tarjeta = tarjeta & "   <div Class=""card shadow mb-4""> "
                Dim nombre As String
                nombre = obtenerNombreAutor(fila("rutAutor"))
                Dim rol As String
                rol = obtenerRolAutor(fila("rutAutor"))
                tarjeta = tarjeta & "         <div Class=""card-header"">"
                tarjeta = tarjeta & "           <div class=""row"">"
                tarjeta = tarjeta & "               <div class=""m-0 font-weight-bold text-primary col-6"" >" + nombre + "/" + rol + "</div>"
                tarjeta = tarjeta & "               <p class=""d-none d-lg-inline text-grey-600 small col-6 text-right"" >" + fila("fecha") + "</p>"
                tarjeta = tarjeta & "           </div>"
                tarjeta = tarjeta & "         </div>"
                tarjeta = tarjeta & "           <div Class=""card-body"">"
                Dim mensaje As String
                mensaje = fila("texto")
                tarjeta = tarjeta & "                     <div>" + mensaje + "</div>"
                tarjeta = tarjeta & "        </div> "
                tarjeta = tarjeta & "      </div> "
                tarjeta = tarjeta & "     </div> "

                lblTarjetaComentario.Text = tarjeta

                'notificacion.actualizarEstado(fila("id"))

            End If

        Next



    End Sub

    Protected Function cargarNombreDocumento(documentoId As Integer) As String
        Dim documento As New clsDocumento
        Dim nombre As String
        Dim dt As DataTable
        Dim dr As DataRow
        dt = documento.obtenerNombreDocumento(documentoId)
        If dt.Rows.Count > 0 Then
            dr = dt.Rows.Item(0)
            nombre = dr("nombre")
            Return nombre
        Else
            Return "Documento no encontrado"
        End If
    End Function

End Class