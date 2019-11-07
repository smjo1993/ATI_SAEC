Imports System.Data
Imports System.Data.SqlClient
Public Class clsContratista
    Private Nombre
    Private Login
    Private Clave
    Private Rut
    Private Estado
    Private Fono
    Private Correo

    Public Sub New()

    End Sub

    Public Sub New(ByVal nombre As String, ByVal login As String, ByVal clave As String, ByVal rut As String, ByVal estado As Char, ByVal fono As Integer,
                   ByVal correo As String)
        Me.Nombre = nombre
        Me.Login = login
        Me.Clave = clave
        Me.Rut = rut
        Me.Estado = estado
        Me.Fono = fono
        Me.Correo = correo
    End Sub

    Public Property nombreContratista() As String
        Get
            Return Me.Nombre
        End Get
        Set
            Me.Nombre = Value
        End Set
    End Property

    Public Property loginContratista() As String
        Get
            Return Me.Login
        End Get
        Set
            Me.Login = Value
        End Set
    End Property

    Public Property claveContratista() As String
        Get
            Return Me.Clave
        End Get
        Set
            Me.Clave = Value
        End Set
    End Property

    Public Property rutContratista() As String
        Get
            Return Me.Rut
        End Get
        Set
            Me.Rut = Value
        End Set
    End Property

    Public Property estadoContratista() As Char
        Get
            Return Me.Estado
        End Get
        Set
            Me.Estado = Value
        End Set
    End Property

    Public Property fonoContratista() As Integer
        Get
            Return Me.Fono
        End Get
        Set
            Me.Fono = Value
        End Set
    End Property

    Public Property correoContratista() As String
        Get
            Return Me.Correo
        End Get
        Set
            Me.Correo = Value
        End Set
    End Property

    Public Function listarContratistasHabilitados() As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarContratistasHabilitados"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ListarContratistasHabilitados")
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function listarContratistas() As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarContratistas"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ListarContratistas")
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function validarContratista(contratista As String) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ValidarContratista '" & contratista & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ValidarContratista")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function obtenerContratista(rut As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerContratista '" & rut & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ObtenerContratista")
            Return ds.Tables(0)

        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function insertarContratista(
                                   nombre As String,
                                   login As String,
                                   clave As String,
                                   rut As String,
                                   estado As String,
                                   fono As String,
                                   correo As String) As Boolean
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_InsertarContratista '" & nombre & "' , '" & login & "' , '" & clave & "' , '" & rut & "' , '" & estado & "' , '" & fono & "' , '" & correo & "'"
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "InsertarContratista")
            Return True
        Catch ex As Exception
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function obtenerCarpetas(rutContratista As String) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim sql As String = "SP_SAEC_ListarCarpetasContratista'" & rutContratista & "'"
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

    Public Function obtenerEstado(rutEmpersa As String) As Boolean

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim sql As String = "SP_SAEC_ListarEstadoDocumentoContratista'" & rutEmpersa & " '"
            Dim ds As New DataSet()
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Dim estadoRojo As Integer = ds.Tables(0).Rows.Count

            If estadoRojo > 0 Then
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
End Class
