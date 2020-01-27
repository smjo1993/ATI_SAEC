Public Class Conexion
    Public Shared Function strSQLSERVER() As String
        ''String de base de datos para trabajar en el local
        Return "Server=192.168.124.9;DataBase=BD_SAEC_TEST;Password=user_saec_test;User ID=user_saec_test;"
        ''string de base de datos cuando vayas a publicar cambios a la version del servidor
        ''Return "Server=192.168.124.9;DataBase=BD_SAEC;Password=user_saec;User ID=user_saec;"
    End Function
End Class
