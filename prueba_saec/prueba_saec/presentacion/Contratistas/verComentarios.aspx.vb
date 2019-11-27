﻿Imports System.Data
Imports System.Data.SqlClient
Public Class verComentarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim areaId As String = Session("areaId")
        Dim docuemntoId As String = Session("docuemntoId")
        Dim carpetaId As String = Session("carpetaId")
        Dim rutUsuario As String = Session("rutUsuario")

        'Falta sacar al contratista de Session. Por mientras trabajaremos con el usuario que es enviado por defecto

        lblPrueba.InnerText = "Area: " & areaId & ", documento: " & docuemntoId & ", carpeta: " & carpetaId & ", rutAutor: " & rutUsuario



        Dim comentario As New clsComentario
        Dim tarjeta As String = ""
        'Dim color As String
        Dim listaComentarios As DataTable = comentario.obtenerComentarios(Session("areaId"), Session("docuemntoId"), Session("carpetaId"))

        'Ordenando la lista de comentarios por fecha
        Dim datav As New DataView
        datav = listaComentarios.DefaultView
        datav.Sort = "fecha"
        listaComentarios = datav.ToTable()


        'Dim Empresas As Object = crearEmpresas()

        ' Ciclo for que recorre la lista de comentarios 
        For Each fila As DataRow In listaComentarios.Rows

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

        'End If

        'Next


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
        accion = comentario.insertarComentario(Session("rutUsuario"), TxtAreaNuevoComentario.Value, Convert.ToInt32(Session("areaId")), Convert.ToInt32(Session("docuemntoId")), Convert.ToInt32(Session("carpetaId")))
        If accion = True Then
            Response.Redirect("verComentarios.aspx")
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

    'genera el string para la generación de cards de comentarios
    Protected Sub cargarComentarios()


    End Sub

End Class