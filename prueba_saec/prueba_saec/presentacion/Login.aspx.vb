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

    Protected Sub lbRecuperarContrasenia_Click(sender As Object, e As EventArgs) Handles lblRecuperarContrasenia.Click
        Response.Redirect("recuperarContrasenia.aspx")
    End Sub

    Protected Sub btLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        Dim usuario As String = txtUsuario.Text
        Dim contrasenia As String = txtContrasenia.Text

        RedireccionUsuario(usuario, contrasenia)
    End Sub

    Public Sub RedireccionUsuario(usuario As String, contrasenia As String)

        Dim clsUsuarioATI As New clsUsuario

        Dim usuarioAti As DataTable = clsUsuarioATI.buscarUsuarioAti(usuario)

        If (usuarioAti.Rows.Count > 0) Then 'EL USUARIO ES DE ATI

            Dim estado As Char = System.Convert.ToChar(usuarioAti.Rows(0)("ESTADO").ToString())

            If (estado = "A" Or estado = "a") Then 'USUARIO ES DE ATI Y SU CUENTA ESTA ACTIVA DENTRO DE LOS SISTEMAS

                Dim clsUsuarioSAEC As New clsUsuarioSAEC
                Dim usuarioSAEC As DataTable = clsUsuarioSAEC.buscarUsuarioSAEC(usuario)

                If (usuarioSAEC.Rows.Count > 0) Then 'EL USUARIO ES DE ATI Y PARTICIPA EN SAEC

                    'VALIDACION DE LA CONTRASENIA
                    Dim contraseniaBd As String = usuarioSAEC.Rows(0)("clave").ToString()

                    If (String.Compare(contraseniaBd, contrasenia) = 0) Then 'CLAVE CORRECTA

                        estado = System.Convert.ToChar(usuarioSAEC.Rows(0)("estado").ToString())

                        If (estado = "A" Or estado = "a") Then 'USUARIO SAEC ESTA ACTIVO

                            'MessageBox.Show("Usuario activo dentro de la plataforma SAEC") 'VER ROLES Y REDIRECCIONAR
                            Response.Redirect("Funcionarios%20ATI/verEmpresas.aspx")

                        Else 'USUARIO INACTIVO EN LA PLATAFORMA SAEC
                            MessageBox.Show("Usuario inactivo dentro de la plataforma SAEC")
                        End If

                    Else 'CLAVE INCORRECTA
                        MessageBox.Show("contraseÑa incorrecta")
                    End If

                Else 'EL LOGIN DEL USUARIO NO ES DE SAEC PERO SI DE ATI
                    MessageBox.Show("Usuario no participa en saec")
                End If

            Else 'EL USUARIO ES DE ATI PERO SU CUENTA NO ESTA ACTIVA EN LOS SISTEMA DE ATI
                MessageBox.Show("Usted no esta activo dentro de los sistemas de ATI")
            End If

        Else 'SI NO ES USUARIO DE ATI SE VE SI ES CONTRATISTA
            Dim contratista As DataTable

            Dim clsContratista As New clsContratista

            contratista = clsContratista.buscarContratista(usuario)

            If (contratista.Rows.Count > 0) Then ' SI EL USUARIO ES CONTRATISTA

                Dim contraseniaBd As String = contratista.Rows(0)("clave").ToString()

                If (String.Compare(contraseniaBd, contrasenia) = 0) Then 'CLAVE CORRECTA

                    Dim estado As Char = System.Convert.ToChar(contratista.Rows(0)("estado").ToString())

                    If (estado = "A" Or estado = "a") Then 'CONTRATISTA ESTA ACTIVO

                        'SE REDIRECCIONA AL MENU DE CONTRATISTAS
                        MessageBox.Show("CONTRATISTA ACTIVO EN EL SISTEMA")

                    Else 'CONTRATISTA INACTIVO
                        MessageBox.Show("CONTRATISTA INACTIVO EN EL SISTEMA")
                    End If

                Else 'CONTRASENIA INCORRECTA
                    MessageBox.Show("contraseÑa incorrecta")
                End If

            Else 'USUARIO INVALIDO
                MessageBox.Show("usuario incorrecto")
            End If
        End If

        'como enviar datos de una pagina a otra
        'Session("usuario") = usuario
        'Session("contrasenia") = contrasenia
        'Response.Redirect("menu.aspx")
        'Server.Transfer("menu.aspx")
    End Sub

End Class