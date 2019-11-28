Imports System.Data
Imports System.Data.SqlClient
Public Class clsUsuarioSAEC

    Private nombre As String
    Private login As String
    Private clave As String
    Private rut As String
    Private estado As Char
    Private fono As Integer
    Private correo As String
    Private area As Integer

    Public Sub New()
    End Sub

    Public Sub New(ByVal nombre As String, ByVal login As String, ByVal clave As String, ByVal rut As String, ByVal estado As Char,
                   ByVal fono As Integer, ByVal correo As String, ByVal area As Integer)
        Me.nombre = nombre
        Me.login = login
        Me.clave = clave
        Me.rut = rut
        Me.estado = estado
        Me.fono = fono
        Me.correo = correo
        Me.area = area
    End Sub
    Public Property getRut() As String
        Get
            Return Me.rut
        End Get
        Set
            Me.rut = Value
        End Set
    End Property
    Public Property getNombre() As String
        Get
            Return Me.nombre
        End Get
        Set
            Me.nombre = Value
        End Set
    End Property
    Public Property getLogin() As String
        Get
            Return Me.login
        End Get
        Set
            Me.login = Value
        End Set
    End Property
    Public Property getClave() As String
        Get
            Return Me.clave
        End Get
        Set
            Me.clave = Value
        End Set
    End Property
    Public Property getFono() As Integer
        Get
            Return Me.fono
        End Get
        Set
            Me.fono = Value
        End Set
    End Property
    Public Property getEstado() As Char
        Get
            Return Me.estado
        End Get
        Set
            Me.estado = Value
        End Set
    End Property
    Public Property getCorrep() As String
        Get
            Return Me.correo
        End Get
        Set
            Me.correo = Value
        End Set
    End Property

    Public Property getArea() As Integer
        Get
            Return Me.area
        End Get
        Set
            Me.area = Value
        End Set
    End Property
    Public Function validarUsuarioSAEC(login As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ValidarUsuarioSAEC '" & login & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ValidarUsuarioSAEC")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function rolesUusario(rut As String) As List(Of clsRol)
        Dim roles As List(Of clsRol) = New List(Of clsRol)
        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerRoles '" & rut & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ValidarUsuarioSAEC")


            Dim posicion = 0
            For Each row As DataRow In ds.Tables(0).Rows
                Dim id As Integer = Convert.ToInt32(row("id").ToString())
                Dim descripcion As String = row("nombre").ToString()
                Dim rol As New clsRol(id, descripcion)
                roles.Add(rol)
                posicion += 1
            Next row

            Return roles
        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function listarUsuarios() As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarUsuarios "

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

    Public Function cambiarEstadoCuentaUsuario(rut As String, estado As Char) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_CambiarEstadoCuentaUsuario '" & rut & "','" & estado & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "CambiarEstadoCuentaUsuario")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    'Método que, según un rut, devuelve el rol que ejerce la persona con dicho rut.
    Public Function obtenerNombreRol(rut As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ObtenerNombreRol '" & rut & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ObtenerNombreRol")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function

    Public Function obtenerPermisos(rut As String) As DataTable

        Dim con As New SqlConnection(Conexion.strSQLSERVER)
        Console.WriteLine(con.ToString())
        Try

            Dim ds As New DataSet()
            Dim sql As String = "SP_SAEC_ListarPermisos '" & rut & "'"

            con.Open()
            Dim dbDataAdapter = New Data.SqlClient.SqlDataAdapter(sql, con)
            dbDataAdapter.Fill(ds, "ListarPermisos")
            Return ds.Tables(0)
        Catch ex As Exception
            Return Nothing
        Finally
            con.Close()
            con.Dispose()
        End Try
    End Function
End Class
