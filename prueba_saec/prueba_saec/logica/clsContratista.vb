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

    Public Sub New(ByVal nombre As String, ByVal login As String, ByVal clave As String, ByVal rut As String, ByVal estado As Char, ByVal fono As String,
                   ByVal correo As String)
        Me.Nombre = nombre
        Me.Login = login
        Me.Clave = clave
        Me.Rut = rut
        Me.Estado = estado
        Me.Fono = fono
        Me.Correo = correo
    End Sub

    Public Property getNombre() As String
        Get
            Return Me.Nombre
        End Get
        Set
            Me.Nombre = Value
        End Set
    End Property

    Public Property getLogin() As String
        Get
            Return Me.Login
        End Get
        Set
            Me.Login = Value
        End Set
    End Property

    Public Property getClave() As String
        Get
            Return Me.Clave
        End Get
        Set
            Me.Clave = Value
        End Set
    End Property

    Public Property getRut() As String
        Get
            Return Me.Rut
        End Get
        Set
            Me.Rut = Value
        End Set
    End Property

    Public Property getEstado() As Char
        Get
            Return Me.Estado
        End Get
        Set
            Me.Estado = Value
        End Set
    End Property

    Public Property getFono() As String
        Get
            Return Me.Fono
        End Get
        Set
            Me.Fono = Value
        End Set
    End Property

    Public Property getCorreo() As String
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

    Public Function validarContratista(login As String) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ValidarContratista '" & login & "'"

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

    Public Function activarContratista(rut As String) As Boolean

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim sql As String = "SP_SAEC_ActivarContratista'" & rut & " '"
            Dim ds As New DataSet()
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ActivarContratista")
            Return True
        Catch ex As Exception
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function desactivarContratista(rut As String) As Boolean

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim sql As String = "SP_SAEC_DesactivarContratista'" & rut & " '"
            Dim ds As New DataSet()
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "DesactivarContratista")
            Return True
        Catch ex As Exception
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function actualizarContratista(nombre As String,
                                     rut As String,
                                     fono As String,
                                     correo As String) As Boolean
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ActualizarContratista '" & nombre & "' , '" & rut & "' , '" & fono & "' , '" & correo & "'"
            'Abriendo conexión
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ActualizarContratista")
            Return True

        Catch ex As Exception
            Return False
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Function

    Public Function obtenerEstado(rutEmpresa As String) As Boolean

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim sql As String = "SP_SAEC_ListarDocumentosParaAlertaContratista '" & rutEmpresa & "'"
            Dim ds As New DataSet()
            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds)
            Dim documentosPendientes As Integer = ds.Tables(0).Rows(0).Item(0)
            Dim documentosPendientesTrabajadores As Integer = ds.Tables(1).Rows(0).Item(0)
            Dim documentosPendientesVehiculos As Integer = ds.Tables(2).Rows(0).Item(0)

            'Si el revisor tiene un documento pendiente mostrar color rojo
            If documentosPendientes > 0 Or documentosPendientesTrabajadores > 0 Or documentosPendientesVehiculos > 0 Then
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
    Public Function obtenerCarpetasHistorico(rutContratista As String) As DataTable
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Try
            Dim sql As String = "SP_SAEC_ListarHistoricoCarpetaContratista'" & rutContratista & "'"
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
End Class
