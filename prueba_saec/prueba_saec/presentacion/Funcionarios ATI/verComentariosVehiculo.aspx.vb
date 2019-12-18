Imports System.Data
Imports System.Data.SqlClient
Public Class verComentariosVehiculo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
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
        accion = comentario.insertarComentarioVehiculo(Session("rutUsuario"), TxtAreaNuevoComentario.Value, Convert.ToInt32(Session("areaId")), Convert.ToInt32(Session("documentoId")), Convert.ToInt32(Session("carpetaId")), Convert.ToInt32(Session("vehiculoId")))
        If accion = True Then
            Response.Redirect("verComentariosVehiculo.aspx")
            lblPrueba.InnerText = "Inserción realizada con éxito"
        Else
            lblPrueba.InnerText = "La inserción ha fallado. Por favor inténtelo de nuevo"
        End If
    End Sub

    Public Function listarComentariosVehiculo(areaId As Integer, documentoId As Integer, carpetaArranqueId As Integer, vehiculoId As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_listarComentariosVehiculo '" & areaId & "' , '" & documentoId & "' , '" & carpetaArranqueId & "' , '" & vehiculoId & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "listarComentariosVehiculo")
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

    Protected Sub cargarComentarios()
        Dim areaId As String = Session("areaId")
        Dim documentoId As String = Session("documentoId")
        Dim carpetaId As String = Session("carpetaId")
        Dim rutUsuario As String = Session("rutUsuario")
        Dim vehiculoId As String = Session("vehiculoId")

        Dim comentario As New clsComentario
        Dim tarjeta As String = ""
        'Dim color As String
        Dim listaComentariosVehiculo As DataTable = comentario.obtenerComentariosVehiculo(Session("areaId"), Session("documentoId"), Session("carpetaId"), Session("vehiculoId"))

        'Ordenando la lista de comentarios por fecha
        Dim datav As New DataView
        datav = listaComentariosVehiculo.DefaultView
        datav.Sort = "fecha"
        listaComentariosVehiculo = datav.ToTable()


        'Dim Empresas As Object = crearEmpresas()

        ' Ciclo for que recorre la lista de comentarios 
        For Each fila As DataRow In listaComentariosVehiculo.Rows

            If fila("rutAutor") = rutUsuario Then
                tarjeta = tarjeta & "  <div class=""row"">"
                tarjeta = tarjeta & "   <div class=""col-2""></div>"
                tarjeta = tarjeta & "   <div class=""col-10"">"
                tarjeta = tarjeta & "    <div Class=""card shadow mb-4""> "
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
                tarjeta = tarjeta & "         <div Class=""card-body"">"
                Dim mensaje As String
                mensaje = fila("texto")
                tarjeta = tarjeta & "           <div>" + mensaje + "</div>"
                tarjeta = tarjeta & "         </div> "
                tarjeta = tarjeta & "      </div> "
                tarjeta = tarjeta & "     </div> "
                tarjeta = tarjeta & "    </div> "

                lblTarjetaComentario.Text = tarjeta
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