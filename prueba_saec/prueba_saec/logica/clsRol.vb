
Public Class clsRol
    Private id As Integer
    Private descripcion As String
    Public Sub New()

    End Sub
    Public Sub New(ByVal id As Integer, ByVal descripcion As String)
        Me.id = id
        Me.descripcion = descripcion
    End Sub

    Public Property idRol() As Integer
        Get
            Return Me.id
        End Get
        Set
            Me.id = Value
        End Set
    End Property

    Public Property descripcionRol() As String
        Get
            Return Me.descripcion
        End Get
        Set
            Me.descripcion = Value
        End Set
    End Property
End Class
