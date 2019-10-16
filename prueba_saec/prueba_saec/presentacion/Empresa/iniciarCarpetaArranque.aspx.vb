Imports System.Net.Mail
Imports System.Net
Public Class iniciarCarpetaArranque
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMensaje.Text = ""
        If Not Page.IsPostBack Then
            lblMensaje.Text = ""
            validarUsuario()
            cargarDatos()
        End If
    End Sub

    Protected Sub validarUsuario()
        Dim usuario As clsUsuarioSAEC = Session("usuario")
        If (usuario Is Nothing) Then
            Response.Redirect("../login.aspx")
        End If
    End Sub

    Protected Sub cargarDatos()
        Dim empresa As New clsEmpresa
        Dim listaEmpresas As DataTable = empresa.empresasSinCarpeta
        If (listaEmpresas Is Nothing) Then

        Else
            If (listaEmpresas.Rows.Count > 0) Then
                For Each celda As DataRow In listaEmpresas.Rows
                    Dim item As New ListItem()
                    item.Text = celda("razonSocial").ToString()
                    item.Value = celda("rut").ToString()
                    'item.Selected = Convert.ToBoolean(celda("IsSelected"))
                    dropEmpresas.Items.Add(item)
                Next
            End If
        End If

    End Sub

    Protected Sub notificarInicio()

        Dim clsUsuario As New clsUsuario

        Dim destinatarios As DataTable = clsUsuario.obtenerRevisores()

        If (destinatarios Is Nothing) Then

        Else
            If (destinatarios.Rows.Count > 0) Then
                For Each destinatario As DataRow In destinatarios.Rows

                    Dim mensaje As String = "La empresa" + " " + dropEmpresas.SelectedItem.Text + " " +
                        "acaba de iniciar su carpeta de arranque, por favo solicitar los documentos necesarios para su acreditacion"

                    enviarCorreo("inicio carpeta de arranque", mensaje, destinatario("correo"))

                Next
            End If
        End If



    End Sub

    Protected Sub enviarCorreo(ByVal asunto As String, ByVal mensaje As String, ByVal destinatario As String)
        Dim message As New MailMessage()
        Try
            'Email desde el que se envia el mensaje.
            Dim email As String = "ati.saec.2019@gmail.com"
            'Password del email.
            Dim password As String = "adminsaec123"
            'Instancia del protocolo smtp.
            Dim smtp = New SmtpClient()
            If True Then
                smtp.Host = "smtp.gmail.com"
                smtp.Port = 587
                smtp.EnableSsl = True
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network
                smtp.Credentials = New NetworkCredential(email, password)
                smtp.Timeout = 5000
            End If
            message.From = New MailAddress(email)
            message.To.Add(destinatario)
            message.Subject = asunto
            message.Body = mensaje

            smtp.Send(message)

            message.Dispose()

        Catch generatedExceptionName As Exception

        End Try
    End Sub

    Protected Sub btnCrearCarpeta_Click(sender As Object, e As EventArgs) Handles btnCrearCarpeta.Click
        Dim alerta As New clsAlertas

        Dim empresa As String = dropEmpresas.SelectedValue

        Dim fechaExpiracion As Date

        Dim fechaCreacion As Date

        Dim carpetaArranque As New clsCarpetaArranque

        If (txtFecha.Text = "") Then

            fechaCreacion = Today

            fechaExpiracion = DateAdd("m", 12, Today)

            Dim exito As Boolean = carpetaArranque.insertarEmpresa(fechaExpiracion, empresa, fechaCreacion)

            If (exito = True) Then
                notificarInicio()

                Response.Redirect("iniciarCarpetaArranque.aspx")
            Else
                lblMensaje.Text = alerta.alerta("ALERTA", "ERROR AL CREAR CARPETA")
            End If

        Else

            fechaCreacion = Today

            fechaExpiracion = Convert.ToDateTime(txtFecha.Text)

            If (DateTime.Compare(fechaCreacion, fechaExpiracion) = 0 Or DateTime.Compare(fechaCreacion, fechaExpiracion) > 0) Then
                lblMensaje.Text = alerta.alerta("ALERTA", "fecha erronea")
            Else

                Dim exito As Boolean = carpetaArranque.insertarEmpresa(fechaExpiracion, empresa, fechaCreacion)

                If (exito = True) Then


                    notificarInicio()

                    Response.Redirect("iniciarCarpetaArranque.aspx")

                Else
                    lblMensaje.Text = alerta.alerta("ALERTA", "ERROR AL CREAR CARPETA")
                End If
            End If

        End If


    End Sub
    Protected Sub btnCrearEmpresa_Click(sender As Object, e As EventArgs) Handles btnCrearEmpresa.Click
        Response.Redirect("crearEmpresa.aspx")
    End Sub
End Class