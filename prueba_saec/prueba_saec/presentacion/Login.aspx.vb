Imports System.Data
Imports System.Configuration
Imports System.Net.Mail
Imports System.Net
Imports System.Windows

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub lbRecuperarContrasenia_Click(sender As Object, e As EventArgs) Handles lbRecuperarContrasenia.Click
        Response.Redirect("recuperarContrasenia.aspx")
    End Sub

    Protected Sub btLogin_Click(sender As Object, e As EventArgs) Handles btLogin.Click
        Dim usuario As String = tbUsuario.Text
        Dim contrasenia As String = tbContrasenia.Text

        Dim cls As New clsUsuario

        Dim usuarioAti As Integer
        Dim contratista As Integer

        usuarioAti = cls.buscarUsuarioAti(usuario)

        'como enviar datos de una pagina a otra
        Session("usuario") = usuario
        Session("contrasenia") = contrasenia
        Response.Redirect("menu.aspx")
        'Server.Transfer("menu.aspx")

    End Sub
End Class