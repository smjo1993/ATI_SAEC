Imports System.Data
Imports System.Configuration
Imports System.Net.Mail
Imports System.Net
Imports System.Data.SqlClient
Imports System.Windows

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    '    Protected Sub lbRecuperarContrasenia_Click(sender As Object, e As EventArgs) Handles lbRecuperarContrasenia.Click
    '        Response.Redirect("recuperarContrasenia.aspx")
    '    End Sub

    '    Protected Sub btLogin_Click(sender As Object, e As EventArgs) Handles btLogin.Click
    '        Dim usuario As String = tbUsuario.Text
    '        Dim contrasenia As String = tbContrasenia.Text

    '        RedireccionUsuario(usuario, contrasenia)
    '    End Sub

    '    Public Sub RedireccionUsuario(usuario As String, contrasenia As String)

    '        Dim clsUsuarioATI As New clsUsuario

    '        Dim usuarioAti As DataTable = clsUsuarioATI.buscarUsuarioAti(usuario)

    '        If (usuarioAti.Rows.Count > 0) Then 'EL USUARIO ES DE ATI

    '            Dim estado As Char = System.Convert.ToChar(usuarioAti.Rows(0)("ESTADO").ToString())

    '            If (estado = "A") Then 'USUARIO ES DE ATI Y SU CUENTA ESTA ACTIVA DENTRO DE LOS SISTEMAS
    '#
    '                'Dim clsUsuarioSAEC As New clsUsuarioSAEC
    '                'Dim usuarioSAEC As DataTable = clsUsuarioSAEC.buscarUsuarioSAEC(usuario)

    '                If (usuarioSAEC.Rows.Count > 0) Then 'EL USUARIO ES DE ATI Y PARTICIPA EN SAEC

    '                    'FALTA VALIDACION DE CONTRASEÑA

    '                    estado = System.Convert.ToChar(usuarioSAEC.Rows(0)("estado").ToString())

    '                    If (estado = "A") Then 'USUARIO SAEC ESTA ACTIVO

    '                        'VER ROLES Y REDIRECCIONAR

    '                    Else 'USUARIO INACTIVO EN LA PLATAFORMA SAEC
    '                        MessageBox.Show("Usuario inactivo dentro de la plataforma SAEC")
    '                    End If

    '                Else 'EL LOGIN DEL USUARIO NO ES DE SAEC PERO SI DE ATI
    '                    MessageBox.Show("Usuario no participa en saec")
    '                End If

    '            Else 'EL USUARIO ES DE ATI PERO SU CUENTA NO ESTA ACTIVA EN LOS SISTEMA DE ATI
    '                MessageBox.Show("Usted no esta activo dentro de los sistemas de ATI")
    '            End If

    '        Else 'SI NO ES USUARIO DE ATI SE VE SI ES CONTRATISTA
    '            Dim contratista As DataTable



    '        End If

    '        'como enviar datos de una pagina a otra
    '        'Session("usuario") = usuario
    '        'Session("contrasenia") = contrasenia
    '        'Response.Redirect("menu.aspx")
    '        'Server.Transfer("menu.aspx")
    '    End Sub
End Class