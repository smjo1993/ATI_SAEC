Imports System.Data
Imports System.Configuration
Imports System.Net.Mail
Imports System.Net
Imports System.Data.SqlClient
Imports System.Windows

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMensaje.Text = ""
        'txtUsuario.Text = ""
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

        Dim alerta As New clsAlertas

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

                            Dim listaRoles As List(Of clsRol) = New List(Of clsRol) 'ACA SE GUARDAN LOS ROLES DEL USUARIO

                            listaRoles = clsUsuarioSAEC.rolesUusario(usuarioSAEC.Rows(0)("rut").ToString())



                            If (listaRoles.Count > 0) Then
                                Dim usuarioEntrante As New clsUsuarioSAEC(usuarioSAEC.Rows(0)("nombre").ToString(),
                                                                                                                      usuarioSAEC.Rows(0)("login").ToString(),
                                                                                                                      usuarioSAEC.Rows(0)("clave").ToString(),
                                                                                                                      usuarioSAEC.Rows(0)("rut").ToString(),
                                                                                                                      System.Convert.ToChar(usuarioSAEC.Rows(0)("estado").ToString()),
                                                                                                                      Convert.ToInt32(usuarioSAEC.Rows(0)("fono").ToString()),
                                                                                                                      usuarioSAEC.Rows(0)("correo").ToString(),
                                                                                                                      Convert.ToInt32(usuarioSAEC.Rows(0)("TB_SAEC_Areaid").ToString()))

                                Session("roles") = listaRoles
                                Session("usuario") = usuarioEntrante
                                Response.Redirect("Funcionarios%20ATI/verEmpresas.aspx")
                                txtUsuario.Text = ""

                            Else

                                lblMensaje.Text = alerta.alerta("ALERTA", "USUARIO SIN ROL(ES) EN EL SISTEMA")

                            End If

                        Else 'USUARIO INACTIVO EN LA PLATAFORMA SAEC
                            lblMensaje.Text = alerta.alerta("ALERTA", "USUARIO INACTIVO EN SAEC")
                        End If

                    Else 'CLAVE INCORRECTA
                        lblMensaje.Text = alerta.alerta("ALERTA", "CONTRASEÑA INCORRECTA")
                        txtUsuario.Text = ""
                    End If

                Else 'EL LOGIN DEL USUARIO NO ES DE SAEC PERO SI DE ATI
                    lblMensaje.Text = alerta.alerta("ALERTA", "ESTE USUARIO NO PARTICIPA EN SAEC")
                    txtUsuario.Text = ""
                End If

            Else 'EL USUARIO ES DE ATI PERO SU CUENTA NO ESTA ACTIVA EN LOS SISTEMA DE ATI
                lblMensaje.Text = alerta.alerta("ALERTA", "USTED NO ESTA ACTIVO EN LOS SISTEMAS DE ATI")
                txtUsuario.Text = ""
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

                        Dim contratistaEntrante As New clsContratista(contratista.Rows(0)("nombre").ToString,
                                                                      contratista.Rows(0)("login").ToString,
                                                                      contratista.Rows(0)("clave").ToString,
                                                                      contratista.Rows(0)("rut").ToString,
                                                                      contratista.Rows(0)("estado").ToString,
                                                                      Convert.ToInt32(contratista.Rows(0)("fono").ToString),
                                                                      contratista.Rows(0)("correo").ToString)

                        Session("contratistaEntrante") = contratistaEntrante
                        Response.Redirect("Contratistas/menuContratista.aspx")
                        txtUsuario.Text = ""
                    Else 'CONTRATISTA INACTIVO
                        lblMensaje.Text = alerta.alerta("ALERTA", "CONTRATISTA INACTIVO EN EL SISTEMA")
                        txtUsuario.Text = ""
                    End If

                Else 'CONTRASENIA INCORRECTA
                    lblMensaje.Text = alerta.alerta("ALERTA", "CONTRASEÑA INCORRECTA")
                    txtUsuario.Text = ""
                End If

            Else 'USUARIO INVALIDO
                lblMensaje.Text = alerta.alerta("ALERTA", "USUARIO INCORRECTO")
                txtUsuario.Text = ""
            End If
        End If
    End Sub

End Class