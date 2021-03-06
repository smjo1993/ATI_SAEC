﻿Imports System.Data
Imports System.Data.SqlClient
Public Class clsDocumento
    Public Function insertarDocumento(nombre As String,
                                      tipo As String,
                                      area As Integer)

        Dim con As New SqlConnection(Conexion.strSQLSERVER)

        Console.WriteLine(con.ToString())

        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_InsertarDocumento '" & nombre & "' , '" & tipo & "' , '" & area & "'"
            con.Open()

            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)

            dbDataAdapter.Fill(ds, "InsertarDocumento")

            'Return ds.Tables(0)
            Return True

        Catch ex As Exception

            Return False

        Finally

            con.Close()
            con.Dispose()

        End Try

    End Function

    Public Function actualizarDocumento(nombreAnterior As String,
                                        nombreNuevo As String,
                                        tipo As String,
                                        id As Integer)
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try
            Dim ds As New DataSet()

            Dim sql As String = "SP_SAEC_ActualizarDocumento '" & nombreAnterior & "' , '" & nombreNuevo & "' , '" & tipo & "', '" & id & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ActualizarDocumento")
            Return True

        Catch ex As Exception

            Return False

        Finally

            con.Close()
            con.Dispose()

        End Try

    End Function

    Public Function obtenerDocumento()

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Dim ds As New DataSet()

        Dim sql As String = "SP_SAEC_ListarDocumentos"

        con.Open()

        Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
        dbDataAdapter.Fill(ds)

        Return ds.Tables(0)

    End Function

    Public Function conexionDb(sql As String) As DataSet

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Dim ds As New DataSet()
        con.Open()
        Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
        dbDataAdapter.Fill(ds)
        Return ds

    End Function
    Public Function buscarDocumentosArea(ByVal area As Integer) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosPorArea '" & area & "'"
            con.Open()

            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)

            dbDataAdapter.Fill(ds, "ListarDocumentosArea")

            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function actualizarEstadoRequerimiento(idDocumento As Integer, estadoDocumento As String) As Boolean
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try
            Dim ds As New DataSet()

            Dim sql As String = "SP_SAEC_ActualizarEstadoRequerimiento '" & idDocumento & "' , '" & estadoDocumento & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ActualizarEstadoRequerimiento")
            Return True

        Catch ex As Exception

            Return False

        Finally

            con.Close()
            con.Dispose()

        End Try
    End Function

    Public Function buscarDocumentoPorArea(ByVal area As Integer, ByVal descripcion As String, ByVal idCarpeta As Integer) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_BuscarDocumentosArea '" & area & "' , '" & descripcion & "' , '" & idCarpeta & "'"
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

    Public Function obtenerDocumentoEstadoEsperaEmpresa(rutContratista As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentosContratista '" & rutContratista & "'"
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

    Public Function obtenerDocumentoEstadoEsperaTrabajador(rutContratista As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentosContratista '" & rutContratista & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Return ds.Tables(1)


        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function
    Public Function obtenerDocumentoEstadoEsperaVehiculo(rutContratista As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentosContratista '" & rutContratista & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Return ds.Tables(2)


        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function


    Public Function cambiarEstadoDocumento(idCarpeta As Integer, idArea As Integer,
                                           idDocumento As Integer, estado As String, ruta As String)

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_CambiarEstadoDocumento'" & idCarpeta & "','" & idArea & "','" & idDocumento & "','" & estado & "','" & ruta & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function

    Public Function cambiarEstadoDocumentoTrabajador(idCarpeta As Integer, idArea As Integer,
                                           idDocumento As Integer, idTrabajador As Integer, estado As String, ruta As String)

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_CambiarEstadoDocumentoTrabajador'" & idCarpeta & "','" & idArea & "','" & idDocumento & "','" & idTrabajador & "','" & estado & "','" & ruta & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function

    Public Function cambiarEstadoDocumentoVehiculo(idCarpeta As Integer, idArea As Integer,
                                           idDocumento As Integer, idVehiculo As Integer, estado As String, ruta As String)

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_CambiarEstadoDocumentoVehiculo'" & idCarpeta & "','" & idArea & "','" & idDocumento & "','" & idVehiculo & "','" & estado & "','" & ruta & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function
    Public Function listarRequisitosDocumentales() As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarRequisitosDocumentales"

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

    Public Function obtenerDocumentoEstadoAplicaEmpresa(idCarpeta As Integer, idArea As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentosRevisor '" & idCarpeta & "','" & idArea & "'"
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

    Public Function obtenerDocumentoEstadoAplicaTrabajador(idCarpeta As Integer, idArea As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentosRevisor '" & idCarpeta & "','" & idArea & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Return ds.Tables(1)


        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function
    Public Function obtenerDocumentoEstadoAplicaVehiculo(idCarpeta As Integer, idArea As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentosRevisor '" & idCarpeta & "','" & idArea & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Return ds.Tables(2)


        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function

    Public Function obtenerDocumentoEstadoAplicaEmpresaAdmin(idCarpeta As Integer, idArea As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentosRevisor '" & idCarpeta & "','" & idArea & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Return ds.Tables(3)


        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function
    Public Function obtenerDocumentoEstadoAplicaTrabajadorAdmin(idCarpeta As Integer, idArea As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentosRevisor '" & idCarpeta & "','" & idArea & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Return ds.Tables(4)


        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function

    Public Function obtenerDocumentoEstadoAplicaVehiculoAdmin(idCarpeta As Integer, idArea As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentosRevisor '" & idCarpeta & "','" & idArea & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Return ds.Tables(5)


        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function

    Public Function obtenerDocumentosEstadoPendienteTrabajador(rutContratista As String)
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentosContratista '" & rutContratista & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Return ds.Tables(4)


        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
    Public Function obtenerDocumentosEstadoPendienteVehiculo(rutContratista As String)

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentosContratista '" & rutContratista & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Return ds.Tables(5)


        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function
    Public Function obtenerDocumentosEstadoPendienteEmpresa(rutContratista As String)
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentosContratista '" & rutContratista & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Return ds.Tables(3)


        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function documentosEmpresaParaRevisar(idCarpeta As Integer, area As Integer)
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosEmpresaParaRevisar '" & idCarpeta & "','" & area & "'"
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

    Public Function fechaExpiracionDocumento(idCarpeta As Integer, idArea As Integer,
                                           idDocumento As Integer, fechaExpiracion As Date)
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_FechaExpiracionDocumento '" & idCarpeta & "','" & idArea & "','" & idDocumento & "','" & fechaExpiracion & "'"
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

    Public Function documentosEmpresaPendientes(idCarpeta As Integer, area As Integer)
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosEmpresaPendientes '" & idCarpeta & "','" & area & "'"
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

    Public Function documentosEmpresaHistorico(rutContratista As String, idCarpeta As Integer) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosHistoricoEmpresa '" & rutContratista & "','" & idCarpeta & "'"
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

    Public Function documentosHistoricoATI(ByVal idCarpeta As Integer) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_DocumentosHistoricoATI  '" & idCarpeta & "'"
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


    Public Function obtenerNombreDocumento(idDocumento As Integer)
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerNombreDocumento '" & idDocumento & "'"
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
    Public Function buscarDocumentoPorAreaPendiente(ByVal area As Integer, ByVal descripcion As String, ByVal idCarpeta As Integer) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_BuscarDocumentosAreaPendiente '" & area & "' , '" & descripcion & "' , '" & idCarpeta & "'"
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
    Public Function mailListaDocumento(ByVal idCarpeta As Integer) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_MailListaDocumento '" & idCarpeta & "'"
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

    Public Function buscarDocumentoPorAreaAdmin(ByVal area As Integer, ByVal descripcion As String, ByVal idCarpeta As Integer) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_BuscarDocumentosAreaAdmin '" & area & "' , '" & descripcion & "' , '" & idCarpeta & "'"
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

    Public Function buscarDocumentoPorAreaPendienteAdmin(ByVal area As Integer, ByVal descripcion As String, ByVal idCarpeta As Integer) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_BuscarDocumentosAreaPendienteAdmin '" & area & "' , '" & descripcion & "' , '" & idCarpeta & "'"
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

    Public Function documentosEmpresaParaRevisarAdmin(idCarpeta As Integer, area As Integer)
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosEmpresaParaRevisarAdmin '" & idCarpeta & "','" & area & "'"
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

    Public Function documentosEmpresaPendientesAdmin(idCarpeta As Integer, area As Integer)
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarDocumentosEmpresaPendientesAdmin '" & idCarpeta & "','" & area & "'"
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
End Class
