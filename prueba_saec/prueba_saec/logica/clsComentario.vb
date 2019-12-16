Imports System.Data
Imports System.Data.SqlClient
Public Class clsComentario
    'Private id
    'Private rutAutor
    'Private fecha
    'Private texto
    'Private areaId
    'Private documentoId
    'Private carpetaArranqueId


    Public Sub New()

    End Sub

    'Public Sub New(razonSocial As Object, rut As Object, giro As Object, direccion As Object, ciudad As Object, personacontacto As Object, fono As Object, correo As Object, rutContratista As Object)
    '    Me.razonSocial = razonSocial
    '    Me.rut = rut
    '    Me.giro = giro
    '    Me.direccion = direccion
    '    Me.ciudad = ciudad
    '    Me.personaContacto = personacontacto
    '    Me.correo = correo
    '    Me.fono = fono
    '    Me.celular = celular
    '    Me.rutContratista = rutContratista

    'End Sub

    Public Function insertarComentario(rutAutor As String,
                                      texto As String,
                                      areaId As Integer,
                                      documentoId As Integer,
                                      carpetaArranqueId As Integer)

        Dim con As New SqlConnection(Conexion.strSQLSERVER)

        Console.WriteLine(con.ToString())

        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_InsertarComentario '" & rutAutor & "' , '" & texto & "' , '" & areaId & "' , '" & documentoId & "' , '" & carpetaArranqueId & "'"
            con.Open()

            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)

            dbDataAdapter.Fill(ds, "InsertarComentario")

            'Return ds.Tables(0)
            Return True

        Catch ex As Exception

            Return False

        Finally

            con.Close()
            con.Dispose()

        End Try

    End Function

    Public Function obtenerComentarios(areaId As Integer,
                                      documentoId As Integer,
                                      carpetaArranqueId As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarComentarios '" & areaId & "' , '" & documentoId & "' , '" & carpetaArranqueId & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ListarComentarios")
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function obtenerComentariosRespuesta(rutConsultante As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            'Dim sql As String = "SP_SAEC_ObtenerComentariosRespuesta '" & documentoId & "' , '" & rutConsultante & "'"
            Dim sql As String = "SP_SAEC_ObtenerComentariosRespuesta '" & rutConsultante & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ObtenerComentariosRespuesta")
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function insertarComentarioTrabajador(rutAutor As String,
                                      texto As String,
                                      areaId As Integer,
                                      documentoId As Integer,
                                      carpetaArranqueId As Integer,
                                      trabajadorId As Integer)

        Dim con As New SqlConnection(Conexion.strSQLSERVER)

        Console.WriteLine(con.ToString())

        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_InsertarComentarioTrabajador '" & rutAutor & "' , '" & texto & "' , '" & areaId & "' , '" & documentoId & "' , '" & carpetaArranqueId & "' , '" & trabajadorId & "'"
            con.Open()

            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)

            dbDataAdapter.Fill(ds, "InsertarComentarioTrabajador")

            'Return ds.Tables(0)
            Return True

        Catch ex As Exception

            Return False

        Finally

            con.Close()
            con.Dispose()

        End Try

    End Function

    Public Function obtenerComentariosTrabajador(areaId As Integer,
                                      documentoId As Integer,
                                      carpetaArranqueId As Integer,
                                      trabajadorId As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarComentariosTrabajador '" & areaId & "' , '" & documentoId & "' , '" & carpetaArranqueId & "' , '" & trabajadorId & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ListarComentariosTrabajador")
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

End Class
