Public Class clsAlertas
    Public Shared Function alertLight(titulo As String, mensaje As String) As String
        Return "<div class=""alert alert-light alert-dismissible""><button type=""button"" class=""close"" data-dismiss=""alert"">&times;</button><strong>" & titulo & "</strong>&nbsp;" & mensaje & "</div>"
    End Function
End Class
