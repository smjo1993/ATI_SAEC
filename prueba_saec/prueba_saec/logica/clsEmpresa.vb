Imports System.Data
Imports System.Data.SqlClient
Public Class clsEmpresa
    Private razonSocial
    Private rut

    Private giro
    Private direccion
    Private ciudad
    Private personaContacto
    Private correo
    Private fono
    Private celular
    Private rutContratista

    Public Sub New()

    End Sub

    Public Sub New(razonSocial As Object, rut As Object, giro As Object, direccion As Object, ciudad As Object, personacontacto As Object, fono As Object, correo As Object, rutContratista As Object)
        Me.razonSocial = razonSocial
        Me.rut = rut
        Me.giro = giro
        Me.direccion = direccion
        Me.ciudad = ciudad
        Me.personaContacto = personacontacto
        Me.correo = correo
        Me.fono = fono
        Me.celular = celular
        Me.rutContratista = rutContratista

    End Sub
    Public Function obtenerCarpetas() As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim sql As String = "SP_SAEC_ListarCarpetasEmpresas"
            Dim ds As New DataSet()
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Return ds.Tables(0)

        Catch ex As Exception
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
    Public Function calcularPorcentaje(rutEmpresa As String) As String

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim sql As String = "SP_SAEC_CalcularProgresoCarpeta '" & rutEmpresa & "'"
            Dim ds As New DataSet()
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Dim aprobados As Integer = ds.Tables(0).Rows(0).Item(0)
            Dim resultado As String

            If aprobados = 0 Then
                Return "0"
            Else
                Dim total As Integer = ds.Tables(1).Rows(0).Item(0)
                resultado = Int((aprobados * 100) / total)
            End If

            Return resultado

        Catch ex As Exception
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function

    Public Function insertarEmpresa(nombre As String,
                                   rut As String,
                                   giro As String,
                                   direccion As String,
                                   ciudad As String,
                                   personacontacto As String,
                                   fono As String,
                                   celular As String,
                                   correo As String,
                                   rutContratista As String) As Boolean
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_InsertarEmpresa '" & nombre & "' , '" & rut & "' , '" & giro & "' , '" & direccion & "' , '" & ciudad & "' , '" & personacontacto & "' , '" & correo & "' , '" & fono & "' , '" & celular & "' , '" & rutContratista & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "InsertarEmpresa")

            Return True

        Catch ex As Exception
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function obtenerEstado(areaRevisor As Integer, rutEmpersa As String) As Boolean

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim sql As String = "SP_SAEC_ListarDocumentosParaAlertaRevisor '" & areaRevisor & "','" & rutEmpersa & " '"
            Dim ds As New DataSet()
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Dim documentosPendientesEmpresa As Integer = ds.Tables(0).Rows(0).Item(0)
            Dim documentosPendientesTrabajadores As Integer = ds.Tables(4).Rows(0).Item(0)
            Dim documentosPendientesVehiculos As Integer = ds.Tables(5).Rows(0).Item(0)
            Dim totalDocumentos As Integer = ds.Tables(2).Rows(0).Item(0)
            Dim tablaDocumentos As DataTable = ds.Tables(1)
            Dim contadorInactivos As Integer

            For Each fila As DataRow In tablaDocumentos.Rows
                'cuenta los los documentos que estan inactivos
                If fila("estado") = "inactivo" Then
                    contadorInactivos += 1
                End If
            Next

            'Si todos los documentos estan inactivos mostrar color rojo
            If totalDocumentos <> 0 And contadorInactivos = totalDocumentos Then
                Return True
            End If


            'Si el revisor tiene un documento pendiente mostrar color rojo
            If documentosPendientesEmpresa > 0 Or documentosPendientesTrabajadores > 0 Or documentosPendientesVehiculos > 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try


    End Function

    Public Function listarEmpresas() As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_Listarempresas"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ListarEmpresas")
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function obtenerEmpresa(rut As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerEmpresa '" & rut & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ObtenerEmpresa")
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
    Public Function obtenerContratistaDeEmpresa(rut As String) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerContratistaDeEmpresa '" & rut & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ObtenerContratistaDeEmpresa")
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function listarContratistasSinempresa() As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarContratistasSinEmpresa"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ListarContratistasSinEmpresa")
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function conexionDb(sql As String) As DataSet

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Dim ds As New DataSet()
        con.Open()
        Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
        dbDataAdapter.Fill(ds)
        Return ds

    End Function

    Public Function empresasSinCarpeta() As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim sql As String = "SP_SAEC_ListarEmpresasSinCarpeta "
            Dim ds As New DataSet()
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)

            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function

    Public Function actualizarEmpresa(razonSocial As String,
                                     rut As String,
                                     giro As String,
                                     direccion As String,
                                     ciudad As String,
                                     personacontacto As String,
                                     correo As String,
                                     fono As String,
                                     celular As String,
                                     contratistaRut As String) As Boolean
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ActualizarEmpresa '" & razonSocial & "' , '" & rut & "' , '" & giro & "' , '" & direccion & "' , '" & ciudad & "' , '" & personacontacto & "' , '" & correo & "' , '" & fono & "' , '" & celular & "' , '" & contratistaRut & "'"
            'Abriendo conexión
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ActualizarEmpresa")
            Return True

        Catch ex As Exception
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function


End Class
